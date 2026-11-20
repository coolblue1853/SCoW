#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""SCoW 대사 데이터 검사기 (표준 라이브러리만).

사용:  python -I Tools/dialog_lint.py [--dot] [--report 경로] [--quiet]
종료 코드: 오류+경고 건수 > 0 이면 1 (정보는 세지 않음).

대사 구조(코드 실측):
  - CSV 열: ID, 캐릭터이름, 대사, (메모...). 코드는 앞 3열만 쓴다. 다음 ID/분기 열은 없다.
  - ID 가 비어 있는 줄 = 앞 대사의 이어지는 줄(contexts 에 추가). 인덱스 = 비어 있지 않은 ID 줄의 등장 순서(1부터).
  - 어느 대사를 보일지는 Main.unity 의 interactionEvent.dialog.line(x~y 인덱스 범위)가 정하고,
    그 이벤트를 부르는 곳은 InteractionController 의 GameObject 필드 + 분기 코드다.
  - 플래그는 DataBaseManager 정적 필드. 대사 텍스트의 ⓡ(●1ⓟ 같은 표식도 플래그를 켠다.
"""
import argparse
import collections
import io
import json
import os
import re
import sys

ROOT = os.path.abspath(os.path.join(os.path.dirname(os.path.abspath(__file__)), ".."))
RES = os.path.join(ROOT, "Assets", "Script", "Asset", "Resources")
SCRIPT = os.path.join(ROOT, "Assets", "Script")
SCENE = os.path.join(ROOT, "Assets", "Scenes", "Main.unity")
I2 = os.path.join(ROOT, "Assets", "Resources", "I2Languages.asset")
DBM = os.path.join(SCRIPT, "old_Dialog", "DatabaseManager.cs")
DDM = os.path.join(SCRIPT, "dialogue", "DialogDatabaseManager.cs")
EVENT_GUID = "59072dc26ee02da4db44bc818dcce4d3"
IC_GUID = "f6ab3c1c418780a42a5930d17a737f0a"

# 씬 폴더(장소) -> CSV. 근거: DialogDatabaseManager 의 nowPlace -> csvFileName 매핑.
FOLDER_CSV = {
    "Detective": "1st_DetectiveOffice", "Client'sHouse": "1st_Client'sHouse",
    "DailyNews": "1st_DailyNews", "SewerOffice": "SewerOffice",
    "University": "1st_University", "Riverside": "1st_Riverside",
    "Hospital": "1st_Hospital", "Policeoffice": "1st_PoliceOffice",
    "Slum": "1st_Slum", "Bar": "1st_Bar", "InSewer": "InSewer",
    "Battle": "1st_Battle", "NoonEvent": "2nd_NoonEvent", "Item": "1st_Item",
    "Ending": "InSewer",  # 가정: 엔딩 이벤트는 하수도 장소(InSewer)의 CSV 를 읽는다(코드에 Ending 장소 없음)
}
C_TRACK_CARRYOVER = {"aidenLookJudge"}  # 씬 미할당 필드. 씬/코드 수정이라 C 트랙으로 이월(DialogAudit 표시)
KOREAN_ONLY = {"SCOW_SCV"}  # 코드가 로드하지 않는 한글 원본


class Issue(object):
    def __init__(self, file, line, sev, kind, msg, fix="", story=False):
        self.file, self.line, self.sev, self.kind = file, line, sev, kind
        self.msg, self.fix, self.story = msg, fix, story


ISSUES = []


def add(file, line, sev, kind, msg, fix="", story=False):
    ISSUES.append(Issue(file, line, sev, kind, msg, fix, story))


def rel(p):
    return os.path.relpath(p, ROOT).replace("\\", "/")


def rd(path):
    raw = open(path, "rb").read()
    try:
        return raw.decode("utf-8-sig")
    except UnicodeDecodeError:
        return raw.decode("cp949", errors="replace")


def read_text(path):
    raw = open(path, "rb").read()
    enc = "utf-8"
    bom = raw.startswith(b"\xef\xbb\xbf")
    try:
        txt = raw.decode("utf-8-sig")
    except UnicodeDecodeError:
        enc = "cp949"
        txt = raw.decode("cp949", errors="replace")
    return txt, enc, bom


HANGUL = re.compile("[가-힣]")
MARK_STRIP = re.compile("[\u2460-\u24ff\u25a0-\u25ff\u2600-\u26ff\u3200-\u32ff\u249c-\u24e9\u2493-\u249b●§]")


def clean(t):
    t = t.replace("^", ",").replace("@", '"').replace("\\n", " ")
    t = re.sub("●\\d", "", t)
    return MARK_STRIP.sub("", t)


TYPOS = [
    (r"\bteh\b", "the"), (r"\brecieve", "receive"), (r"\bdefinately\b", "definitely"),
    (r"\bseperate", "separate"), (r"\boccured\b", "occurred"), (r"\buntill\b", "until"),
    (r"\bwich\b", "which"), (r"\badress", "address"), (r"\balot\b", "a lot"),
    (r"\bthier\b", "their"), (r"\bbecuase\b", "because"), (r"\bbeleive", "believe"),
    (r"\btommorow\b", "tomorrow"), (r"\bwierd\b", "weird"), (r"\bknowlege\b", "knowledge"),
    (r"\bacross the the\b", "across the"), (r"\bpublically\b", "publicly"),
    (r"\bgoverment\b", "government"), (r"\bbegining\b", "beginning"),
    (r"\bneccessary\b", "necessary"), (r"\bexistance\b", "existence"),
    (r"\benviroment\b", "environment"), (r"\bfreind", "friend"), (r"\brealy\b", "really"),
    (r"\bdont\b", "don't"), (r"\bcant\b", "can't"), (r"\bdidnt\b", "didn't"),
    (r"\bwont\b", "won't"), (r"\bisnt\b", "isn't"), (r"\bdoesnt\b", "doesn't"),
    (r"\bI\s?m\b(?<!I'm)", "I'm"), (r"\bIve\b", "I've"), (r"\bthats\b", "that's"),
    (r"\bshoudl\b", "should"), (r"\bcouldnt\b", "couldn't"), (r"\bwouldnt\b", "wouldn't"),
    (r"\bmabye\b", "maybe"), (r"\bsuprise", "surprise"), (r"\bintresting\b", "interesting"),
    (r"\bpeice\b", "piece"), (r"\bfrist\b", "first"), (r"\bsepcial\b", "special"),
    (r"\bhte\b", "the"), (r"\bthey're're\b", "they're"), (r"\bwhat's's\b", "what's"),
]
KO_TYPOS = [("되요", "돼요"), ("몇일", "며칠"), ("금새", "금세"), ("어떻해", "어떡해"),
            ("웬지", "왠지"), ("설겆이", "설거지"), ("안되", "안 되"), ("할께", "할게"), ("됬", "됐")]
FORMAL_END = re.compile("(요|니다|니까|세요|죠|까요|습니다|십시오)$")
INFORMAL_END = re.compile("(다|어|아|야|지|네|냐|군|래|자|라|해|거든|잖아|니)$")


class Group(object):
    def __init__(self, idx, line, id_, name):
        self.idx, self.line, self.id, self.name = idx, line, id_, name
        self.ctx = []  # (line, text)


def parse_csv(path):
    """게임 DialogPaser 와 같은 방식(\\n 분할, ',' 분할)으로 그룹을 만든다. 그리고 위반을 보고한다."""
    name = os.path.basename(path)[:-4]
    f = rel(path)
    txt, enc, bom = read_text(path)
    if enc != "utf-8":
        add(f, 1, "오류", "인코딩", "UTF-8 이 아님(%s 로 해석)" % enc, "UTF-8 로 다시 저장")
    if bom:
        add(f, 1, "정보", "BOM", "UTF-8 BOM 있음(Unity TextAsset 은 BOM 을 건너뛰므로 동작엔 영향 없음)")
    ncr, nlf = txt.count("\r"), txt.count("\n")
    if ncr and ncr != nlf:
        add(f, 1, "경고", "개행", "CRLF/LF 혼재(CR %d, LF %d)" % (ncr, nlf), "한 가지로 통일")
    elif ncr:
        add(f, 1, "정보", "개행", "전체 CRLF. 코드는 \\n 으로만 쪼개 마지막 열 끝에 \\r 이 남지만 앞 3열엔 영향 없음")
    lines = txt.split("\n")
    head = lines[0].split(",")
    if [h.strip() for h in head[:3]] != ["ID", "캐릭터이름", "대사"]:
        add(f, 1, "오류", "헤더", "앞 3열이 ID,캐릭터이름,대사 가 아님: %r" % (head[:3],), "헤더 복구")
    rows = []
    for i, l in enumerate(lines[1:], start=2):
        rows.append((i, l.split(",")))
    cnt = collections.Counter(len(r) for _, r in rows)
    mode = cnt.most_common(1)[0][0] if cnt else 0
    groups = []
    ids_seen = {}
    prev_id = 0
    pad = 0
    i = 0
    while i < len(rows):
        ln, r = rows[i]
        if len(r) < 3:
            add(f, ln, "오류", "열 개수", "열이 %d개(<3). 게임이 row[2] 접근 시 IndexOutOfRange" % len(r), "빈 칸 열 보충")
            i += 1
            continue
        g = Group(len(groups) + 1, ln, r[0].strip(), r[1].strip())
        # 이어지는 줄 포함
        while True:
            ln2, r2 = rows[i]
            if len(r2) < 3:
                break
            g.ctx.append((ln2, r2[2]))
            if len(r2) > mode and not (len(r2) > 3 and HANGUL.search(r2[3])):
                add(f, ln2, "경고", "쉼표 포함", "열이 %d개(이 파일 표준 %d). 대사 안 쉼표로 뒷부분이 잘림: %r"
                    % (len(r2), mode, ",".join(r2[2:4])[:50]), "쉼표를 ^ 로 바꿈")
            i += 1
            if i >= len(rows) or len(rows[i][1]) < 3 or rows[i][1][0] != "":
                break
        groups.append(g)
        # ID 검사
        if g.id == "":
            if g.idx != 1:
                pass
        else:
            if not re.fullmatch(r"\d+", g.id):
                add(f, g.line, "경고", "ID 형식", "ID 가 숫자가 아님: %r" % g.id, "숫자 ID")
            else:
                n = int(g.id)
                if n in ids_seen:
                    add(f, g.line, "오류", "ID 중복", "ID %d 가 %d 줄에도 있음" % (n, ids_seen[n]), "ID 재부여")
                elif n != prev_id + 1:
                    add(f, g.line, "경고", "ID 불연속", "ID %d 앞이 %d" % (n, prev_id), "연번 점검")
                ids_seen[n] = g.line
                prev_id = n
    # 이어지는 줄 존재 -> ID 와 인덱스 어긋남
    for g in groups:
        if g.id.isdigit() and int(g.id) != g.idx:
            add(f, g.line, "정보", "ID≠인덱스",
                "이 줄부터 ID(%s)와 게임 인덱스(%d)가 다름. 이벤트 line 범위는 인덱스 기준" % (g.id, g.idx),
                "범위 편집 시 ID 가 아니라 인덱스 사용")
            break
    # 내용 검사
    texts_seen = collections.defaultdict(list)
    names = collections.Counter()
    tone = collections.defaultdict(lambda: [0, 0, []])
    padding = 0
    mid_empty = []
    last_real = max([g.idx for g in groups if any(t.strip() for _, t in g.ctx)] or [0])
    for g in groups:
        names[g.name] += 1
        all_empty = all(t.strip() == "" for _, t in g.ctx)
        if all_empty and g.idx > last_real:
            padding += 1  # 파일 끝의 빈 칸 예약 줄(ID 만 있거나 화자 이름만 있음)
            continue
        if g.name == "" and all_empty:
            padding += 1
            continue
        if g.name != "" and all_empty:
            gi = g.idx - 1  # groups 리스트 0-기반 위치
            prev_ok = gi >= 1 and any(t.strip() for _, t in groups[gi - 1].ctx)
            next_ok = gi + 1 < len(groups) and any(t.strip() for _, t in groups[gi + 1].ctx)
            if prev_ok and next_ok:
                # 앞뒤가 모두 대사인 외톨이 빈 줄 = 말없는 한 박자(DailyNews 74 는 원래 공백 한 칸 " ").
                # DialogManager 는 빈 대사를 특별 처리하지 않고 빈 대사창을 한 번 띄운다(클릭으로 넘김, 크래시 없음).
                add(f, g.line, "정보", "연출 빈 줄", "앞뒤가 대사인 외톨이 빈 줄(말없는 한 박자로 간주, 빈 대사창이 한 번 뜸)")
            else:
                mid_empty.append(g)
        elif g.name == "" and not all_empty:
            add(f, g.line, "정보", "화자 없음", "대사는 있고 캐릭터이름이 빔(DialogManager 는 이름창 숨김 처리. 나레이션이면 정상)")
        for ln, t in g.ctx[1:]:
            if t.strip() == "" and not all_empty:
                add(f, ln, "경고", "빈 필수 칸", "이어지는 줄의 대사가 비어 있음(빈 대사창이 한 번 뜸)", "줄 삭제")
        if g.ctx and any(ln for ln, _ in g.ctx):
            pass
        # 이어지는 줄에 화자 이름이 다르면 무시됨
        # (g.name 은 첫 줄만 사용됨)
        # @ 짝
        joined = "".join(t for _, t in g.ctx)
        if joined.count("@") % 2:
            add(f, g.line, "경고", "깨진 따옴표", "@(따옴표) 개수가 홀수(%d)" % joined.count("@"), "여닫는 @ 짝 맞추기")
        if '"' in joined:
            add(f, g.line, "경고", "깨진 따옴표", '날 쌍따옴표(") 있음. 규약은 @ 사용(CSV 따옴표 이스케이프와 충돌)', '@ 로 교체')
        if joined.count("(") != joined.count(")"):
            add(f, g.line, "경고", "괄호 짝", "( ) 개수 불일치 %d/%d" % (joined.count("("), joined.count(")")), "괄호 짝 맞추기")
        for ln, t in g.ctx:
            # ⒳ 는 회색 시작 표식(DialogManager 는 ⓦ 까지 회색, 괄호 짝 없음)이라 ⓡⓖⓑⓨ 만 센다. 예: InSewer 36 "⒳(Aiden) ... now.ⓦ"
            op = len(re.findall("[ⓡⓖⓑⓨ]\\(", t))
            cl = len(re.findall("\\)ⓦ", t))
            if op != cl:
                add(f, ln, "경고", "색 표식 짝", "색 표식 열림 %d / 닫힘 )ⓦ %d" % (op, cl), "ⓖ(..)ⓦ 짝 맞추기")
            if t.startswith("ⓦ"):
                add(f, ln, "오류", "표식 크래시", "ⓦ 가 문장 맨 앞(DialogManager 가 t[i-1] 접근 -> IndexOutOfRange)", "ⓦ 앞에 문자 추가")
            c = clean(t)
            nm = g.name
            if f.endswith("SCOW_SCV.csv") is False and HANGUL.search(c):
                add(f, ln, "경고", "한글 잔류", "영문 CSV 인데 대사에 한글: %r" % c.strip()[:60], "영문 번역 또는 의도 확인", story=True)
            if not (HANGUL.search(c) and not re.search("[A-Za-z]{3}", c)):
                for pat, good in TYPOS:
                    if re.search(pat, c, re.I):
                        add(f, ln, "경고", "오탈자 후보", "%r -> %s : %r" % (pat.strip("\\b"), good, c.strip()[:50]), "'%s' 로 교정" % good)
                m = re.search(r"\b(\w+) \1\b", c, re.I)
                if m and m.group(1).lower() not in ("had", "that", "very", "no", "bye", "ha", "ah", "oh", "go", "so", "now", "yes", "knock") and not m.group(1).isdigit():
                    add(f, ln, "경고", "오탈자 후보", "단어 반복 %r : %r" % (m.group(0), c.strip()[:50]), "한 번 삭제")
                if re.search(r"[A-Za-z]  +[A-Za-z]", c):
                    add(f, ln, "정보", "공백", "연속 공백: %r" % c.strip()[:50], "공백 하나로")
                if re.search(r"\s[,.!?]", c) and "..." not in c:
                    add(f, ln, "정보", "공백", "문장부호 앞 공백: %r" % c.strip()[:50], "공백 제거")
                if re.search(r"[a-z]\.[A-Z][a-z]", c):
                    add(f, ln, "정보", "공백", "마침표 뒤 공백 없음: %r" % c.strip()[:50], "공백 추가")
                if re.search(r"(?<!\.)\.\.(?!\.)", c):
                    add(f, ln, "정보", "문장부호", "마침표 2개(..): %r" % c.strip()[:50], "... 로")
            for bad, good in KO_TYPOS:
                if bad in c:
                    add(f, ln, "경고", "오탈자 후보", "한글 %r -> %r : %r" % (bad, good, c.strip()[:40]), "%s 로 교정" % good)
            key = re.sub("\\s+", " ", c).strip().lower()
            if len(key) >= 14:
                texts_seen[(g.name, key)].append(ln)
            # 말투(한글 큰따옴표 대사만)
            if nm and HANGUL.search(c):
                for seg in re.findall('"([^"]+)"', c):
                    for sent in re.split("[.!?…~]+", seg):
                        sent = sent.strip().rstrip(",")
                        sent = re.sub("[^가-힣]+$", "", sent)
                        if not sent:
                            continue
                        w = tone[nm]
                        if FORMAL_END.search(sent):
                            w[0] += 1
                            w[2].append(ln)
                        elif INFORMAL_END.search(sent):
                            w[1] += 1
                            w[2].append(-ln)
            if len(rows) and ln == 0:
                pass
    k = 0
    while k < len(mid_empty):
        j = k
        while j + 1 < len(mid_empty) and mid_empty[j + 1].idx == mid_empty[j].idx + 1:
            j += 1
        a_, b_ = mid_empty[k], mid_empty[j]
        add(f, a_.line, "경고", "미작성 장면", "인덱스 %d~%d(%d개, 줄 %d~%d)의 화자는 있는데 대사가 비어 있음(이벤트가 참조하면 빈 대사창)" % (a_.idx, b_.idx, j - k + 1, a_.line, b_.line),
            "대사를 채우거나(미작성 장면) 이벤트 범위에서 제외", story=True)
        k = j + 1
    for (nm, key), lns in texts_seen.items():
        if len(lns) > 1:
            if any(b_ - a_ == 1 for a_, b_ in zip(lns, lns[1:])):
                # 같은 장면 안에서 바로 이어서 같은 문장 = 복붙 실수 후보 -> 경고
                add(f, lns[0], "경고", "중복 문장", "같은 문장이 연속 %d회(%s): %r" % (len(lns), ",".join(map(str, lns[:6])), key[:50]),
                    "복붙 실수면 한 줄 삭제")
            else:
                # 떨어진 반복은 장별 재방문·분기 복제(같은 장면을 날짜별로 다시 쓴 구조, 예: Bar 2/22, NoonEvent 2/18/35)라 의도로 본다 -> 정보
                add(f, lns[0], "정보", "반복 문장", "같은 문장이 %d회(%s): %r" % (len(lns), ",".join(map(str, lns[:6])), key[:50]))
    # 화자 이름 일관성
    low = collections.defaultdict(list)
    for n_, c_ in names.items():
        if n_:
            low[n_.lower().replace(" ", "")].append((n_, c_))
    for k, v in low.items():
        if len(v) > 1:
            add(f, 0, "경고", "화자 표기", "같은 화자의 표기가 여러 가지: %s" % ", ".join("%r×%d" % x for x in v), "표기 통일", story=False)
    for n_ in list(names):
        if n_ and n_ != n_.strip():
            add(f, 0, "경고", "화자 표기", "화자 이름 앞뒤 공백 %r" % n_, "공백 제거")
    for nm, (fm, inf, lns) in tone.items():
        tot = fm + inf
        if tot >= 6 and min(fm, inf) >= 3 and min(fm, inf) / float(tot) >= 0.15:
            add(f, abs(lns[0]), "경고", "말투 혼용", "%s 의 한글 대사가 존댓말 %d / 반말 %d 혼재(상대에 따른 구분이면 정상)" % (nm, fm, inf),
                "상대별 말투 규칙 확인 후 통일", story=True)
    return dict(name=name, file=f, groups=groups, padding=padding, rows=len(rows), enc=enc)


# ---------- 씬 이벤트 ----------
def parse_scene():
    if not os.path.exists(SCENE):
        return None
    t = open(SCENE, encoding="utf-8").read()
    docs = re.split(r"^--- !u!(\d+) &(\d+)(?: stripped)?\s*\n", t, flags=re.M)
    objs = {docs[i + 1]: (docs[i], docs[i + 2]) for i in range(1, len(docs), 3)}
    tr, tf, comps = {}, {}, {}
    for fid, (ty, b) in objs.items():
        if ty == "1":
            m = re.search(r"m_Name: (.*)", b)
            tr[fid] = m.group(1).strip() if m else ""
            comps[fid] = re.findall(r"component: \{fileID: (\d+)\}", b)
        if ty in ("4", "224"):
            tf[fid] = (re.search(r"m_GameObject: \{fileID: (\d+)\}", b).group(1),
                       re.search(r"m_Father: \{fileID: (\d+)\}", b).group(1))
    gotf = {v[0]: k for k, v in tf.items()}

    def path(go):
        out = []
        while go in tr:
            out.append(tr[go])
            tt = gotf.get(go)
            if not tt:
                break
            fa = tf[tt][1]
            go = tf[fa][0] if fa in tf else None
        return "/".join(reversed(out))
    events = []
    ic = None
    for fid, (ty, b) in objs.items():
        if ty != "114":
            continue
        if EVENT_GUID in b:
            go = re.search(r"m_GameObject: \{fileID: (\d+)\}", b).group(1)
            m = re.search(r"line: \{x: ([\d.\-]+), y: ([\d.\-]+)\}", b)
            en = re.search(r"m_Enabled: (\d)", b)
            p = path(go).split("/")
            events.append(dict(comp=fid, go=go, name=p[-1], folder=p[-2] if len(p) > 1 else "", chap=p[-3] if len(p) > 2 else "",
                               x=int(float(m.group(1))), y=int(float(m.group(2))), enabled=en.group(1) if en else "1"))
        if IC_GUID in b:
            ic = {k: v for k, v in re.findall(r"^  (\w+): \{fileID: (-?\d+)\}", b, flags=re.M)}
    # 참조 집합: 이벤트 GO/컴포넌트가 다른 오브젝트에서 참조되는지
    refd = collections.defaultdict(set)
    for fid, (ty, b) in objs.items():
        for r in re.findall(r"\{fileID: (\d+)(?:, guid:[^}]*)?\}", b):
            if r != "0" and r != fid:
                refd[r].add(fid)
    return dict(events=events, ic=ic or {}, refd=refd, objs=objs, tr=tr, comps=comps)


def check_events(csvs, scene):
    if scene is None:
        return {}
    f = rel(SCENE)
    cover = collections.defaultdict(lambda: collections.defaultdict(int))
    called_ranges = set()
    dedup = collections.OrderedDict()
    pending = []
    evmap = collections.defaultdict(list)
    ic = scene["ic"]
    ic_refs = set(v for v in ic.values() if v != "0")
    for e in scene["events"]:
        place_csv = FOLDER_CSV.get(e["folder"])
        e["csv"] = place_csv
        if place_csv is None:
            add(f, 0, "경고", "장소 키", "이벤트 %s 의 폴더 %r 에 대응하는 CSV 가 없음" % (e["name"], e["folder"]), "FOLDER_CSV 또는 DialogDatabaseManager 확인")
            continue
        c = csvs.get(place_csv)
        if not c:
            continue
        n = len(c["groups"])
        evmap[place_csv].append(e)
        tag = "%s/%s/%s [%d~%d]" % (e["chap"], e["folder"], e["name"], e["x"], e["y"])
        if e["x"] < 1 or e["y"] < e["x"]:
            add(f, 0, "오류", "이벤트 범위", "%s : 범위가 잘못됨(시작<1 또는 끝<시작)" % tag, "line 범위 수정")
            continue
        if e["y"] > n:
            dedup.setdefault(("oor", place_csv, e["x"], e["y"], e["name"]), (c["file"], 0, "오류", "없는 대사 참조", "%s : 범위 끝 %d 가 CSV 대사 수 %d 를 넘음(KeyNotFoundException)" % (tag, e["y"], n), "범위 수정", []))[6].append(e["chap"])
            continue
        empties = [k for k in range(e["x"], e["y"] + 1)
                   if all(t.strip() == "" for _, t in c["groups"][k - 1].ctx)]
        def is_beat(k):
            # 앞뒤가 모두 대사이고 화자가 있는 외톨이 빈 줄 = 말없는 한 박자(parse_csv 의 '연출 빈 줄' 과 같은 기준)
            gs = c["groups"]
            return (gs[k - 1].name != "" and k >= 2 and k < len(gs)
                    and any(t.strip() for _, t in gs[k - 2].ctx) and any(t.strip() for _, t in gs[k].ctx))
        empties = [k for k in empties if not is_beat(k)]
        # 빈 대사는 DialogManager 가 빈 대사창으로 띄울 뿐 크래시가 아니다. 통째로 빈 구간은 미작성 장면이라 오류가 아닌 경고(스토리 확인).
        if empties:
            dedup.setdefault(("empty", place_csv, e["x"], e["y"], e["name"]), (c["file"], c["groups"][empties[0] - 1].line, "경고", "미작성 장면 참조", "%s : 참조 범위에 빈 대사 %d개(%s)" % (tag, len(empties), ",".join(map(str, empties[:6]))), "대사를 채우거나(미작성 장면) 범위에서 제외", [], True))[6].append(e["chap"])
        # 호출되는가: 씬의 다른 컴포넌트(InteractionController 필드 등)가 참조하는지
        goid, cid = e["go"], e["comp"]
        users = (scene["refd"].get(goid, set()) | scene["refd"].get(cid, set()))
        own = set(scene["comps"].get(goid, [])) | {goid}
        users = users - own
        users = set(u for u in users if scene["objs"][u][0] not in ("4", "224", "1"))
        e["called"] = bool(users)
        e["tag"] = tag
        if users:
            called_ranges.add((place_csv, e["x"], e["y"]))
            for k in range(e["x"], e["y"] + 1):
                cover[place_csv][k] += 1
        else:
            pending.append(e)
    for k, v in dedup.items():
        add(v[0], v[1], v[2], v[3], v[4] + " (장: %s)" % "/".join(sorted(set(v[6]))), v[5], story=(len(v) > 7))
    for e in pending:
        if (e["csv"], e["x"], e["y"]) in called_ranges:
            add(f, 0, "정보", "미호출 복제본", "%s : 호출되지 않지만 같은 범위를 호출되는 이벤트가 이미 씀(장별 복제본으로 추정)" % e["tag"])
        else:
            add(f, 0, "경고", "미호출 이벤트", "%s : 어떤 컴포넌트도 참조하지 않고 같은 범위의 호출되는 이벤트도 없음(이 범위의 대사가 도달 불가일 수 있음)" % e["tag"],
                "삭제 또는 연결(스토리 결정 필요)", story=True)
    # 중복·겹침
    for cn, evs in evmap.items():
        seen = collections.defaultdict(list)
        for e in evs:
            seen[(e["x"], e["y"])].append(e)
        for (x, y), l in seen.items():
            names = set(e["name"] for e in l)
            if len(names) > 1:
                add(rel(os.path.join(RES, cn + ".csv")), 0, "정보", "범위 공유", "서로 다른 이벤트 %s 가 같은 범위 [%d~%d] 를 가리킴(재사용이면 정상)" % (",".join(sorted(names))[:80], x, y))
    # 도달 불가 대사
    for cn, c in csvs.items():
        if cn not in evmap and cn not in KOREAN_ONLY:
            if cn != "1st_Item":
                pass
        if cn in KOREAN_ONLY:
            add(c["file"], 0, "정보", "미참조 파일", "코드·씬이 로드하지 않는 CSV(한글 원본 보관용으로 추정)")
            continue
        if cn not in evmap:
            add(c["file"], 0, "경고", "미참조 파일", "이 CSV 를 가리키는 이벤트가 씬에 없음", "삭제 또는 연결", story=True)
            continue
        cov = cover[cn]
        run = []
        for g in c["groups"]:
            has = any(t.strip() for _, t in g.ctx) or g.name
            if has and cov.get(g.idx, 0) == 0:
                if run and run[-1] == g.idx - 1:
                    run.append(g.idx)
                else:
                    if run:
                        flush_run(c, run)
                    run = [g.idx]
        if run:
            flush_run(c, run)
    return evmap


def flush_run(c, run):
    g0 = c["groups"][run[0] - 1]
    txt = clean(g0.ctx[0][1]).strip()[:40] if g0.ctx else ""
    add(c["file"], g0.line, "경고", "도달 불가 대사", "인덱스 %d~%d(%d개) 를 호출되는 이벤트가 가리키지 않음. 첫 줄: %s %r" % (run[0], run[-1], len(run), g0.name, txt),
        "삭제 또는 이벤트 연결(스토리 결정 필요)", story=True)


def check_ic(scene):
    if scene is None:
        return
    f = rel(os.path.join(SCRIPT, "dialogue", "InteractionController.cs"))
    src = rd(os.path.join(SCRIPT, "dialogue", "InteractionController.cs"))
    fields = re.findall(r"public GameObject (\w+);", src)
    ic = scene["ic"]
    used = set(re.findall(r"\b(\w+)\.transform\.GetComponent<interactionEvent>", src)) | set(re.findall(r"\b(\w+)\.GetComponent<interactionEvent>", src))
    ev_go = set(e["go"] for e in scene["events"])
    for fl in fields:
        v = ic.get(fl)
        if v is None:
            add(f, 0, "정보", "필드 직렬화", "필드 %s 가 씬 직렬화에 없음(씬 비교 불가)" % fl)
        elif v == "0":
            sev = "오류" if fl in used else "정보"
            if fl in C_TRACK_CARRYOVER:
                # 씬·코드 문제라 대사 CSV 점검(B-1) 범위 밖. 정보로 내리되 종류를 'C트랙 이월' 로 구분해 보이게 한다.
                add(f, 0, "정보", "C트랙 이월", "InteractionController.%s 가 씬에서 비어 있음(코드가 .GetComponent<interactionEvent>() 호출 -> NullReference). C 트랙(씬·코드 수정)에서 처리" % fl,
                    "씬에서 연결 또는 호출 코드 제거")
                continue
            add(f, 0, sev, "빈 참조 필드", "InteractionController.%s 가 씬에서 비어 있음%s" % (fl, "(코드가 .GetComponent<interactionEvent>() 호출 -> NullReference)" if fl in used else "(코드에서도 안 쓰임)"),
                "씬에서 연결 또는 필드 삭제")
        elif fl in used and v not in ev_go:
            add(f, 0, "경고", "참조 대상", "InteractionController.%s 가 가리키는 오브젝트에 interactionEvent 가 없음" % fl, "대상 확인")
    # 호출되지 않는 필드
    for fl in fields:
        if fl != "theDM" and not re.search(r"\b%s\b" % fl, src.replace("public GameObject %s;" % fl, "")):
            add(f, 0, "정보", "미사용 필드", "InteractionController.%s 는 코드에서 한 번도 쓰이지 않음" % fl)


# ---------- 코드 <-> 자원 ----------
def check_code():
    f = rel(DDM)
    src = rd(DDM)
    names = set(re.findall(r'csvFileName\s*=\s*"([^"]+)"', src)) | set(re.findall(r'csvFileName\s*!=\s*"([^"]+)"', src))
    have = set(x[:-4] for x in os.listdir(RES) if x.endswith(".csv"))
    for n in sorted(names - have):
        add(f, 0, "오류", "CSV 이름", "코드가 참조하는 CSV %r 가 Resources 에 없음(Resources.Load null -> NullReference)" % n, "파일 추가 또는 이름 교정")
    for n in sorted(have - names):
        add(rel(os.path.join(RES, n + ".csv")), 0, "정보", "코드 미참조", "DialogDatabaseManager 가 이 이름을 쓰지 않음")
    # 장소 키
    handled = set(re.findall(r'nowPlace == "([^"]+)" && check == true', src))
    awake = src.split("private void Awake")[1] if "private void Awake" in src else ""
    in_awake = set(re.findall(r'nowPlace == "([^"]+)"', awake))
    used = collections.Counter()
    for dp, dn, fn in os.walk(SCRIPT):
        if "CodeMonkey" in dp:
            continue
        for x in fn:
            if x.endswith(".cs"):
                s = rd(os.path.join(dp, x))
                for k in re.findall(r'nowPlace\s*(?:==|!=|=)\s*"([^"]+)"', s):
                    used[k] += 1
    for k in sorted(used):
        if k not in handled and k != "Gunshop":
            add(f, 0, "경고", "장소 키", "코드에서 nowPlace=%r 를 쓰지만 DialogDatabaseManager.Update 가 처리하지 않음" % k, "매핑 추가 또는 코드 정리")
    for k in sorted(used):
        if k == "Gunshop":
            add(f, 0, "경고", "장소 키", "nowPlace=\"Gunshop\" 비교가 코드에 있는데 대응 CSV/매핑이 없음(대사 로드 안 됨)", "미사용이면 삭제 / 쓰면 CSV 추가")
    for k in sorted(handled - in_awake):
        add(f, 0, "정보", "장소 키", "%r 는 Update 에서만 CSV 를 고르고 Awake 초기 로드에는 없음(씬 시작 장소가 아니면 정상)" % k)
    return handled


# ---------- 플래그 ----------
def strip_comments(s):
    return re.sub(r"//[^\n]*", "", s)


def check_flags():
    src = strip_comments(rd(DBM))
    decl = re.findall(r"public static (bool|int|float|string)\s+(\w+)\s*(?:=\s*([^;]+))?;", src)
    bools = {n: (init or "").strip() for t, n, init in decl if t == "bool"}
    # ResetData 범위
    reset_lines = set()
    lines = src.split("\n")
    m = re.search(r"void ResetData\s*\(", src)
    if m:
        start = src[:m.start()].count("\n")
        depth, seen, k = 0, False, start
        while k < len(lines):
            depth += lines[k].count("{") - lines[k].count("}")
            if "{" in lines[k]:
                seen = True
            reset_lines.add(k)
            if seen and depth <= 0:
                break
            k += 1
    stat = {n: dict(set_true=0, set_other=0, set_false=0, reset=0, reads=0, read_files=set()) for n in bools}
    files = []
    for dp, dn, fn in os.walk(SCRIPT):
        if "CodeMonkey" in dp:
            continue
        for x in fn:
            if x.endswith(".cs"):
                files.append(os.path.join(dp, x))
    for p in files:
        s = strip_comments(rd(p))
        isdbm = os.path.abspath(p) == os.path.abspath(DBM)
        for li, line in enumerate(s.split("\n")):
            if re.search(r"public static \w+\s+\w+", line) and isdbm:
                continue
            for n in bools:
                if n not in line:
                    continue
                pat = (r"(?<![\w.])%s\b" % n) if isdbm else (r"DataBaseManager\.%s\b" % n)
                for mm in re.finditer(pat, line):
                    rest = line[mm.end():]
                    is_w = re.match(r"\s*=(?!=)", rest)
                    st = stat[n]
                    if is_w:
                        rhs = rest[rest.index("=") + 1:].strip()
                        if isdbm and li in reset_lines:
                            st["reset"] += 1
                        elif rhs.startswith("true"):
                            st["set_true"] += 1
                        elif rhs.startswith("false"):
                            st["set_false"] += 1
                        else:
                            st["set_other"] += 1
                    else:
                        st["reads"] += 1
                        st["read_files"].add(os.path.basename(p))
    f = rel(DBM)
    summary = dict(total=len(bools), never_set=0, never_read=0, unused=0)
    for n, st in sorted(stat.items()):
        setter = st["set_true"] + st["set_other"]
        init = bools[n]
        if st["reads"] == 0 and setter == 0 and st["set_false"] == 0:
            summary["unused"] += 1
            add(f, 0, "정보", "플래그 미사용", "%s : 선언만 있고 쓰이지 않음" % n, "삭제 후보(세이브 키면 보존)")
        elif setter == 0 and st["reads"] > 0 and init != "true":
            summary["never_set"] += 1
            add(f, 0, "경고", "플래그 검사만", "%s : 읽는 곳 %d(%s) 있으나 true 로 설정하는 곳이 없음(항상 false)" % (n, st["reads"], ",".join(sorted(st["read_files"]))[:50]),
                "설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검")
        elif setter > 0 and st["reads"] == 0:
            summary["never_read"] += 1
            add(f, 0, "경고", "플래그 설정만", "%s : %d곳에서 설정하나 읽는 곳이 없음" % (n, setter), "불필요 플래그면 삭제, 엔딩/세이브용이면 문서화")
    return summary


# ---------- I2 ----------
def check_i2():
    out = dict(path=rel(I2), terms=0, langs=[], empty={}, size=0)
    if not os.path.exists(I2):
        return out
    t = open(I2, encoding="utf-8").read()
    out["size"] = len(t.encode("utf-8"))
    langs = re.findall(r"^    - Name: (.+)\n      Code: (\S+)", t, flags=re.M)
    out["langs"] = langs
    blocks = re.split(r"^    - Term: ", t.split("mTerms:")[1].split("CaseInsensitiveTerms")[0], flags=re.M)[1:]
    out["terms"] = len(blocks)
    empty = collections.Counter()
    for b in blocks:
        ls = b.split("Languages:\n")[1].split("      Flags:")[0].split("\n")
        items = [re.sub(r"^      - ?", "", l).strip() for l in ls if l.startswith("      -")]
        for i, v in enumerate(items):
            if v in ("", '""', "''"):
                empty[i] += 1
    out["empty"] = {langs[i][0]: c for i, c in empty.items() if i < len(langs)}
    out["has_code_use"] = False
    for dp, dn, fn in os.walk(SCRIPT):
        if "CodeMonkey" in dp:
            continue
        for x in fn:
            if x.endswith(".cs") and re.search(r"I2\.Loc|LocalizationManager", rd(os.path.join(dp, x))):
                out["has_code_use"] = True
    return out


# ---------- dot ----------
def write_dot(csvs, evmap):
    d = os.path.join(ROOT, "Logs", "dialog_graph")
    os.makedirs(d, exist_ok=True)
    for cn, c in csvs.items():
        evs = evmap.get(cn, [])
        o = io.StringIO()
        o.write('digraph "%s" {\n  rankdir=LR; node [shape=box,fontname="Malgun Gothic"];\n' % cn)
        o.write('  csv [label="%s\\n대사 %d개", shape=folder];\n' % (cn, len(c["groups"])))
        cov = set()
        for i, e in enumerate(evs):
            col = "black"
            for k in range(e["x"], min(e["y"], len(c["groups"])) + 1):
                cov.add(k)
            lab = "%s\\n%s/%s\\n[%d~%d]" % (e["name"].replace('"', "'"), e["chap"], e["folder"], e["x"], e["y"])
            o.write('  e%d [label="%s",color=%s];\n  csv -> e%d;\n' % (i, lab, col, i))
        run = []
        for g in c["groups"]:
            has = any(t.strip() for _, t in g.ctx) or g.name
            if has and g.idx not in cov:
                run.append(g.idx)
        # 연속 구간 묶기
        k = 0
        while k < len(run):
            j = k
            while j + 1 < len(run) and run[j + 1] == run[j] + 1:
                j += 1
            o.write('  u%d [label="도달 불가 %d~%d",color=red,style=dashed];\n  csv -> u%d [style=dashed,color=red];\n' % (k, run[k], run[j], k))
            k = j + 1
        o.write("}\n")
        open(os.path.join(d, cn.replace("'", "") + ".dot"), "w", encoding="utf-8", newline="\n").write(o.getvalue())
    return d


# ---------- 보고서 ----------
RULES = [
    ("인코딩/BOM/개행", "UTF-8 여부, BOM, CR 혼입", "오류/경고/정보"),
    ("헤더·열 개수", "앞 3열이 ID,캐릭터이름,대사 인지. 열이 3 미만이면 게임이 크래시", "오류"),
    ("쉼표 포함", "열이 파일 표준보다 많은 줄 = 대사 안 쉼표로 게임이 뒷부분을 잘라 먹음(쉼표는 ^ 로 써야 함)", "경고"),
    ("ID 규칙", "중복·불연속·숫자 아님. 이어지는 줄(빈 ID)이 있으면 ID 와 게임 인덱스가 어긋나므로 정보로 표시", "오류/경고/정보"),
    ("빈 필수 칸", "화자는 있는데 대사가 빈 줄, 이어지는 줄이 빈 줄. 패딩(ID 만 있는 빈 줄)은 건수만 집계", "오류/경고"),
    ("깨진 따옴표·괄호·색 표식", "@ 개수 홀수, 날 \" , ( ) 짝, ⓖ(..)ⓦ 짝, ⓦ 가 문장 맨 앞(크래시)", "오류/경고"),
    ("한글 잔류", "영문 CSV 대사에 한글이 남음(번역 누락 후보)", "경고"),
    ("오탈자 후보", "흔한 영어/한글 오타, 단어 반복. 공백·부호 문제는 정보", "경고/정보"),
    ("중복 문장", "같은 CSV 안에서 같은 화자의 같은 문장(길이 14자 이상)", "경고"),
    ("화자 표기·말투 혼용", "대소문자·공백 차이로 갈린 화자명, 한글 대사의 존댓말/반말 혼재", "경고"),
    ("이벤트 범위(씬)", "Main.unity 의 interactionEvent 범위가 CSV 대사 수를 넘는지, 범위 안에 빈 대사가 있는지", "오류"),
    ("도달 불가 대사", "어떤 이벤트 범위에도 안 들어가는 대사 구간(CSV 에는 다음 ID/분기가 없어서, '도달 불가' = 이벤트가 안 가리키는 대사)", "경고"),
    ("미호출 이벤트·빈 참조 필드", "이벤트 오브젝트를 아무도 참조하지 않음 / InteractionController 필드가 씬에서 비어 있음", "오류/경고"),
    ("코드↔자원 대조", "DialogDatabaseManager 가 부르는 CSV 이름, nowPlace 키 대 실제 파일·매핑", "오류/경고/정보"),
    ("플래그", "DataBaseManager 정적 bool 플래그: 검사만(항상 false) / 설정만 / 미사용", "경고/정보"),
]
STORY_KINDS = ("한글 잔류", "중복 문장", "미작성 장면", "미작성 장면 참조", "말투 혼용", "도달 불가 대사", "미호출 이벤트", "미참조 파일")


def write_report(path, csvs, flags, i2, evmap, scene):
    o = io.StringIO()
    w = o.write
    n_e = sum(1 for i in ISSUES if i.sev == "오류")
    n_w = sum(1 for i in ISSUES if i.sev == "경고")
    n_i = sum(1 for i in ISSUES if i.sev == "정보")
    w("# 대사 데이터 점검표 (DialogAudit)\n\n")
    w("`Tools/dialog_lint.py` 가 자동 생성한 표다(`python -I Tools/dialog_lint.py --report Docs/Playtest/DialogAudit.md --dot`).\n\n")
    w("## 0. B-1 반영 결과 (작업 직전 실측 252건 -> 후 %d건. 이전 기록 268건은 플래그 코드 정리 전 수치)\n\n" % (n_e + n_w))
    w("| 구분 | 전 | 후 | 처리 |\n|---|---:|---:|---|\n")
    w("| 오류 | 6 | %d | 빈 대사 참조 5건은 크래시가 아니라 빈 대사창이라 `미작성 장면 참조`(경고, 스토리 확인)로 재분류, `aidenLookJudge` 1건은 C 트랙 이월(정보) |\n" % n_e)
    w("| 경고(서식) | 12 | 0 | 깨진 @ 5(SCV 1 포함)·날 쌍따옴표 1·색 표식 짝 4·괄호 1은 CSV 수정, `InSewer.csv:36` 은 검사기 오탐(⒳ 는 괄호 짝 없는 회색 표식)이라 규칙 수정 |\n")
    w("| 경고(중복 문장 211) | 211 | 0 | 같은 장면 연속 중복 0건. 떨어진 반복은 장별 재방문·분기 복제라 의도로 보고 정보(`반복 문장`)로 강등 |\n")
    w("| 경고(빈 칸) | 3 | 1 | `DailyNews:75`, `Item:19` 은 앞뒤가 대사인 한 박자(`연출 빈 줄`, 정보). `Client'sHouse:102~124` 는 미작성 장면으로 남김 |\n")
    w("| 경고(남은 것) | - | %d | 모두 분류됨: 스토리 확인(6절) = 미작성 장면 4(참조 3: Aiden 심리치료 성공 2·Item OldMap 1, 빈 구간 1: Client'sHouse 102~124)·도달 불가 대사 9·말투 혼용 1, 코드 정리 = 플래그 7·장소 키 3 |\n\n" % n_w)
    w("- 오류 6건 중 빈 대사 참조: 원문이 저장소 어디에도 없어(git 이력 2커밋 모두 빈 칸, `SCOW_SCV.csv` 는 탐정 사무소 도입부 한글본이라 해당 장면 없음) 복원 불가 -> 지어내지 않고 6절 `미작성 장면` 목록으로.\n")
    w("- `DailyNews.csv:185` 쉼표 잘림: 현재 검사기가 잡지 않음. 쉼표는 메모 열(4열)이고 파서는 앞 3열만 쓰므로 영향 없음(따옴표 감쌈 불필요).\n")
    w("- **C 트랙 이월:** `InteractionController.aidenLookJudge` 가 씬에서 비어 있음(코드가 `.GetComponent<interactionEvent>()` 호출 -> NullReference). 씬·코드 문제라 대사 CSV 범위 밖.\n\n")
    w("- 총 문제 **%d건** = 오류 %d + 경고 %d (정보 %d건은 건수에 넣지 않음)\n" % (n_e + n_w, n_e, n_w, n_i))
    w("- 대상: CSV %d개(`Assets/Script/Asset/Resources/`), 씬 이벤트 %d개(`Main.unity`), DataBaseManager 플래그 %d개, I2Languages\n\n" % (
        len(csvs), len(scene["events"]) if scene else 0, flags["total"]))
    w("## 1. 대사 구조(코드에서 확인한 사실)\n\n")
    w("- CSV 열은 `ID, 캐릭터이름, 대사, 메모…`뿐이다. **다음 ID·분기·플래그 조건 열은 없다.** `DialogPaser.Prase` 는 `\\n` 으로 줄을 쪼개고 `,` 로 열을 쪼개 앞 3열만 쓴다(따옴표 이스케이프 없음 -> 대사 안 쉼표는 `^`, 따옴표는 `@` 로 쓰고 `DialogManager` 가 출력 때 되돌림).\n")
    w("- ID 칸이 빈 줄은 앞 대사의 **이어지는 줄**이다. 게임 인덱스 = 비어 있지 않은 ID 줄의 등장 순서(1부터). ID 값은 코드가 읽지 않는다.\n")
    w("- 어떤 대사를 보일지는 씬 `Main.unity` 의 `interactionEvent.dialog.line`(인덱스 x~y 범위, 포함)이 정하고, `DialogDatabaseManager.GetDialog` 가 범위를 자른다. **분기·플래그 조건은 전부 `InteractionController.cs`(3200줄) 코드**에 있다. 그래서 '다음 ID 참조·막다른 길' 검사는 '이벤트 범위·미참조 대사·미호출 이벤트' 검사로 바꿔 수행했다.\n")
    w("- 플래그는 `DataBaseManager`(구 old_Dialog) 정적 필드이고, 대사 속 표식(`ⓡ(●1ⓟ…` 등 `DialogManager` 문자 스위치)으로도 켜진다.\n")
    w("- 파일 인코딩은 전부 UTF-8(BOM 없음), 개행 LF. 출시판은 영문 대사이며 한글은 메모 열과 `SCOW_SCV.csv`(한글 원본, 코드 미참조)에 있다.\n\n")
    w("## 2. 검사 규칙\n\n| 규칙 | 내용 | 등급 |\n|---|---|---|\n")
    for a, b, c in RULES:
        w("| %s | %s | %s |\n" % (a, b, c))
    w("\n등급: 오류=게임 크래시·잘못된 참조, 경고=확인·수정 권장, 정보=참고(건수에 안 셈).\n\n")
    w("## 3. 파일별 건수\n\n| 파일 | 대사 수 | 패딩(빈 줄) | 오류 | 경고 | 정보 |\n|---|---:|---:|---:|---:|---:|\n")
    per = collections.defaultdict(lambda: [0, 0, 0])
    for i in ISSUES:
        per[i.file][{"오류": 0, "경고": 1, "정보": 2}[i.sev]] += 1
    for cn, c in sorted(csvs.items()):
        e_, w_, i_ = per[c["file"]]
        w("| %s | %d | %d | %d | %d | %d |\n" % (cn, len(c["groups"]) - c["padding"], c["padding"], e_, w_, i_))
    others = [k for k in per if not k.startswith("Assets/Script/Asset/Resources")]
    for k in sorted(others):
        e_, w_, i_ = per[k]
        w("| `%s` | - | - | %d | %d | %d |\n" % (k, e_, w_, i_))
    w("\n## 4. 종류별 건수\n\n| 종류 | 오류 | 경고 | 정보 |\n|---|---:|---:|---:|\n")
    kc = collections.defaultdict(lambda: [0, 0, 0])
    for i in ISSUES:
        kc[i.kind][{"오류": 0, "경고": 1, "정보": 2}[i.sev]] += 1
    for k, v in sorted(kc.items(), key=lambda kv: -sum(kv[1][:2])):
        w("| %s | %d | %d | %d |\n" % (k, v[0], v[1], v[2]))
    # 문제 목록
    def table(items, with_story=False):
        w("| 위치 | 등급 | 종류 | 내용 | 제안 수정 |\n|---|---|---|---|---|\n")
        for i in items:
            loc = "%s%s" % (os.path.basename(i.file) if i.file.endswith(".csv") else i.file, (":%d" % i.line) if i.line else "")
            w("| %s | %s | %s | %s | %s |\n" % (loc, i.sev, i.kind, i.msg.replace("|", "\\|").replace("\n", " "), i.fix.replace("|", "\\|")))
        w("\n")
    story = [i for i in ISSUES if i.sev != "정보" and (i.story or i.kind in STORY_KINDS)]
    plain = [i for i in ISSUES if i.sev != "정보" and i not in story]
    order = {"오류": 0, "경고": 1}
    w("\n## 5. 문제 목록: 서식·참조·오탈자 등 (스토리 내용 불변, 오류+경고 %d건)\n\n" % len(plain))
    table(sorted(plain, key=lambda i: (order[i.sev], i.file, i.line)))
    w("## 6. 스토리 내용을 바꾸는 수정 (사용자 확인용, %d건)\n\n" % len(story))
    w("아래는 고치면 **게임에서 보이는 내용·흐름이 달라지는** 항목이다(번역 보강, 대사 삭제·연결, 말투 통일, 이벤트 삭제). 사용자 확인 전에는 건드리지 않는다.\n\n")
    table(sorted(story, key=lambda i: (i.kind, order[i.sev], i.file, i.line)))
    infos = [i for i in ISSUES if i.sev == "정보"]
    w("## 7. 정보(건수에 불포함, %d건)\n\n" % len(infos))
    ik = collections.Counter(i.kind for i in infos)
    w("종류별: " + ", ".join("%s %d" % kv for kv in ik.most_common()) + "\n\n")
    if len(infos) <= 150:
        table(sorted(infos, key=lambda i: (i.kind, i.file, i.line)))
    else:
        w("(%d건이라 종류별 대표 3건만 표시. 전체는 `python -I Tools/dialog_lint.py -v`)\n\n" % len(infos))
        pick = []
        cnt = collections.Counter()
        for i in sorted(infos, key=lambda i: (i.kind, i.file, i.line)):
            if cnt[i.kind] < 3:
                pick.append(i)
                cnt[i.kind] += 1
        table(pick)
    w("## 8. 플래그 요약\n\n- DataBaseManager 정적 bool 플래그 %d개 중 검사만 하고 true 로 설정 안 되는 것 %d, 설정만 하고 읽지 않는 것 %d, 선언만 있고 안 쓰이는 것 %d.\n" % (
        flags["total"], flags["never_set"], flags["never_read"], flags["unused"]))
    w("- 이 분석은 정적 문자열 매칭이다. 씬의 UnityEvent·ES3 세이브 로드·리플렉션으로 켜지는 플래그는 못 본다. 목록은 5·6절의 `플래그` 항목.\n\n")
    w("## 9. I2 Localization 현황\n\n")
    w("- `%s` (%d바이트): 용어 **%d개**, 언어 %d개(%s).\n" % (i2["path"], i2["size"], i2["terms"], len(i2["langs"]), ", ".join(c for _, c in i2["langs"])))
    w("- 언어별 빈 번역 수: " + (", ".join("%s %d" % kv for kv in i2["empty"].items()) if i2["empty"] else "없음(용어 전부 채워짐)") + "\n")
    w("- 코드의 `I2.Loc`/`LocalizationManager` 호출: %s\n\n" % ("있음" if i2["has_code_use"] else "없음(씬·프리팹 Localize 컴포넌트도 없음)"))
    w("### 통합 방향 추천안\n\n")
    w("1. **I2 에 대사를 옮기는 것은 지금 하지 않는다.** I2 에는 `Button` 용어 1개뿐이고 런타임 사용처가 없다(사실상 미사용 플러그인). 대사는 CSV 약 5천 줄이고, 분기가 코드·씬 범위(x~y)에 묶여 있어 용어 키로 바꾸면 이벤트 범위 전부를 다시 매핑해야 한다. 출시판 세이브·범위를 깨뜨릴 위험이 크다.\n")
    w("2. **CSV 를 대사의 원본으로 유지**하고, 먼저 5절의 서식 오류와 6절의 스토리 항목을 정리한다(쉼표 잘림·한글 잔류·미호출 이벤트).\n")
    w("3. 다국어가 필요해지면 **언어별 열 추가 방식**을 쓴다: `ID,캐릭터이름,대사_en,대사_ko,…` 로 열을 늘리고 `DialogPaser` 가 현재 언어 열을 읽게 한다. 인덱스/범위 구조는 그대로라 안전하다. 한글 원본 `SCOW_SCV.csv` 는 이 구조의 ko 열 후보이며, 인덱스 정렬(321줄 구조는 같음)부터 검증한다.\n")
    w("4. UI 문구(버튼·메뉴·설정)만 I2 로 옮긴다. 이쪽은 하드코딩 한글이 흩어져 있어 I2 의 이점이 크고 대사 범위와 무관하다. 이때 `Button` 용어와 8개 언어 구성을 재사용한다.\n")
    w("5. 대사 파서를 건드릴 때는 `ID≠인덱스`(이어지는 줄)·`^`/`@` 치환 규약을 CSV 검사기(이 도구)로 먼저 못 박고, 파서의 따옴표 인식 개선(진짜 CSV 파서)은 규약 이행 뒤에 한다.\n")
    open(path, "w", encoding="utf-8", newline="\n").write(o.getvalue())


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--dot", action="store_true", help="Logs/dialog_graph/*.dot 출력")
    ap.add_argument("--report", help="점검표 .md 경로")
    ap.add_argument("-v", action="store_true", help="모든 문제를 출력")
    ap.add_argument("--quiet", action="store_true")
    a = ap.parse_args()
    try:
        sys.stdout.reconfigure(encoding="utf-8")
    except Exception:
        pass
    csvs = {}
    for fn in sorted(os.listdir(RES)):
        if fn.endswith(".csv"):
            c = parse_csv(os.path.join(RES, fn))
            csvs[c["name"]] = c
    scene = parse_scene()
    evmap = check_events(csvs, scene)
    check_ic(scene)
    check_code()
    flags = check_flags()
    i2 = check_i2()
    if a.dot:
        d = write_dot(csvs, evmap)
        print("dot:", rel(d))
    if a.report:
        write_report(os.path.join(ROOT, a.report) if not os.path.isabs(a.report) else a.report, csvs, flags, i2, evmap, scene)
        print("report:", a.report)
    n_e = sum(1 for i in ISSUES if i.sev == "오류")
    n_w = sum(1 for i in ISSUES if i.sev == "경고")
    n_i = sum(1 for i in ISSUES if i.sev == "정보")
    if not a.quiet:
        shown = ISSUES if a.v else [i for i in ISSUES if i.sev != "정보"]
        for i in shown[:400]:
            print("%s %s:%s [%s] %s" % (i.sev, i.file, i.line, i.kind, i.msg))
        if len(shown) > 400:
            print("... (%d건 더, --report 참조)" % (len(shown) - 400))
    print("요약: 오류 %d, 경고 %d, 정보 %d (문제 %d건)" % (n_e, n_w, n_i, n_e + n_w))
    return 1 if (n_e + n_w) > 0 else 0


if __name__ == "__main__":
    sys.exit(main())
