# UX-1 UI/UX·편의 기능 개선 설계

사용자 원문: "기능이랑 Ui/ux 이런것들 개선하는것도 포함해줘. 생각보다 지금 ui ux가 구려서". 작성 11-25. 코드 수정 없음.
범위: **흐름·조작·정보 구조·피드백·가독성·편의 기능**(배치·크기·애니메이션·소리 포함). 도트·프레임 그림 교체는 ART-2 담당 → 그래픽이 필요한 항목은 "임시 → ART-2 교체"로 표시.
겹침 금지: 키워드 선택 UI = `KeywordUX.md`(K1~K7), 동료 전투 UI·패드 대상 선택 = `Party.md` 7절(PT-5), 스토리 연출 = `StoryImprove.md`·`SewerImprove.md`, 규칙 = `CocDesign.md`. 이 문서는 그 밖과 **공통 UI 뼈대**를 다룬다.
근거 표기: 캡처 = `Logs/playtest/` 파일명, 코드 = 파일:줄(11-25 grep 실측). 확인 못 한 것은 "추정".

## 0. 요약: 가장 나쁜 화면 3개

| 순위 | 화면 | 이유(한 줄) |
|---|---|---|
| 1 | 설정창 | 겹친 레이아웃(TEXT 줄이 Resolution·스킵 팝업에 가려짐), 한·영 혼용, 마우스 전용 |
| 2 | 캐릭터 생성·스킬 분배 | 키보드·패드 입력 0, 값이 `—`로 빈 채 시작, 뒤로가기·설명 구조 불명 |
| 3 | 맵 이동·시간 표시 | 4일 데드라인·남은 시간·현재 목표를 어디에도 안 보여줌(전구 아이콘뿐) |

그다음: 대사창 버튼(작고 대비 낮음), 판정 룰렛 정보(영문 상태 문구·%없음), 수첩(마우스휠·패드 없음).

## 1. 현행 실측 (화면별)

공통: 입력은 `GameInput`(86곳 호출, 24파일)으로 통일됐다(INPUT-1). 그러나 **EventSystem 포커스(`Selectable`·`SetSelectedGameObject`) 를 쓰는 곳은 6파일뿐**(`UiGate`·`ManageMain`·`BacklogView`·`KeywordPicker`·`SlotMenuView`·`settingManager` 2건). 나머지 화면은 "단축키 대리 클릭" 방식이라 패드로 목록을 고르려면 W/S/A/D 매핑에 의존한다.

### 1-1. 타이틀 (`u6_insert_fix.png`, `ManageMain.cs`)
- 현행: Start / (Continue: 저장 있을 때만) / Option / Quit. W/S(패드 십자키)로 이동 + E/A 확정(`ManageMain.cs:129~157`), 마우스 호버 하이라이트. 조작 1~2단계로 양호.
- 문제: ① 메뉴 글자가 로고·제목 대비 오른쪽으로 쏠림(로고 중심 ≈1000px, 메뉴 ≈1050px) ② Continue 라벨이 영문 고정(TMP 글꼴에 한글 글리프 없음, `ManageMain.cs:104`) ③ 마지막 저장 정보(날짜·장소·시간)를 안 보여줌 → 이어하기 누르기 전엔 무엇인지 모름 ④ 키 안내 없음 ⑤ 마우스를 안 움직이면 아무 항목도 선택 표시 없음(`_navIndex=-1`).

### 1-2. 설정 (`p8_settings4.png`, `settingManager.cs`)
- 현행: BGM·SFX·TEXT 슬라이더, Full/Window 버튼, 해상도 `< 1920×1080 >` + Confirm(해상도 6종 `SettingsData`), 오른쪽 Tutorials·KeySetting·불러오기·저장, 아래 "to Main". 설정은 영구 저장(`SettingsService`).
- 문제: ① **TEXT 슬라이더가 "Resolution" 라벨과 "스킵: 읽은 것만" 토글에 겹쳐 가려짐**(캡처 600~1300px, 590~780px 구간) ② 영문 라벨(BGM·Sound·Full·Window)과 한글 라벨(불러오기·저장·스킵)이 한 창에 혼재, 불러오기는 `◀`·저장은 `▶` 장식으로 방향이 의미 없음 ③ 해상도 적용에 Confirm 별도 단계 ④ 마우스 전용: 슬라이더·토글에 패드·키보드 조작 경로 없음(`settingManager.cs` 포커스 2건뿐, 추정) ⑤ 흔들림·글자 크기·색약 항목 없음 ⑥ 텍스트 속도 슬라이더인지 "단계"인지 값 표시 없음 ⑦ KeySetting 이 보기 전용인지 리바인딩인지 불명(`ImproveAudit.md` C-4, 추정).

### 1-3. 캐릭터 생성·스킬 (`u6_setchar.png`, `setStat/*`)
- 현행: 실루엣 초상화, "Creating stats"·"Rand stats" 두 버튼, STR·INT·DEX·HP·SAN·MP·LUK·Wealth 칸, 아래 `< — >` 페이지. 이후 스킬 점수 분배(`setSkill.cs`).
- 문제: ① `setStat`·`setSkill` 에 `GameInput` 호출 0 → **키보드·패드로 진행 불가** ② 초기값 `—` 만 있고 "무엇을 해야 하는지" 안내 없음(굴리기 버튼 이름이 모호) ③ 재굴림 횟수 제한·이전 값 비교 없음(추정) ④ 능력치가 무엇에 쓰이는지 설명이 `statExplain`·`*SkillExplain` 호버/클릭에 숨음 → 초회 플레이어가 STR/INT/DEX/SAN 의미를 모른 채 확정 ⑤ 확정 전 되돌리기·요약 화면 없음 ⑥ 스킬 분배는 `BackToStat`(`setSkill.cs:1180`) 뿐, 점수 남은 상태에서 시작 가능한지 불명(확인 필요).

### 1-4. 맵 이동·월드 (`MapManager.cs`·`PotalPoint.cs`·`Player.cs`)
- 현행: 좌우 걷기(A/D·←/→·스틱), 포탈 F(패드 십자키 위), 지도 2(패드 X), 수첩 1(패드 Y), SIS 3(패드 Select).
- 문제: ① 시간 표시는 낮/밤 전구 2개뿐(`LightBulbContorol.cs:30`). `timeCount` 는 4칸/일 감소(`MapManager.cs:456`, `% 4` 분기)인데 **남은 칸·몇 일차·데드라인 표시 UI 없음**(grep: 표시용 텍스트 없음, 추정) ② 상호작용 가능 지점에 "E"/"F" 힌트가 뜨는지 불명 → 캡처상 확인 못 함(씬 실측 필요) ③ 포탈 F 와 오브젝트 E 가 별키인데 패드는 A / 십자키 위로 갈림 — 학습 부담 ④ 지도(2)에서 목적지 선택은 마우스(`ChangeMapButton`), 키보드·패드 목록 이동 경로 불명(추정) ⑤ 현재 목표·막힘 힌트 없음(막히면 처음 장소를 다시 돌아다니는 수밖에 없음, `StoryImprove.md` 막힘 항목과 연결).

### 1-5. 대화·선택지·백로그 (`p9_office.png`, `p8_backlog.png`, `DialogManager`·`selectionUIManager`·`BacklogView`·`DialogueRunner`)
- 현행: 검은 대사창+이름 탭, 우상단 `auto / log / skip` 버튼(P8 추가). 진행 E/클릭/A. 백로그 창(휠·W/S·B/Esc 닫기), 글자 속도 5단계, 스킵 "읽은 것만".
- 잘 된 점: 이름 노란색 구분, 백로그 하단에 키 안내.
- 문제: ① 버튼 3개가 **테두리만 있는 회색 박스**(대비 낮음, 크기 ≈ 한 줄 높이), 현재 auto·skip ON/OFF 상태 표시가 캡처상 없음 ② 대사창이 크고 글자가 작아 하단 60%가 비어 있음(글자 크기 옵션 없음) ③ 백로그: 줄 간격이 촘촘하고 맨 위 줄이 잘려 보임(`p8_backlog.png` 190~230px), 영문 안내문이 "B: Close"·"Esc: Close" 중복, 키보드·패드 안내가 한 줄에 섞임 ④ 다음 진행 표시(▼ 깜박임) 없음(캡처상) ⑤ 선택지 UI는 `selectionUIManager`+`SelectionButton` — 번호키·패드 포커스 지원 불명(포커스 6파일에 없음, 추정) ⑥ 대사 중 설정 불가(`BusyRules` 8플래그 AND, `ImproveAudit.md` B-4)는 UiGate 로 일부 개선됐으나 대사 중 열림 여부 재확인 필요.

### 1-6. 조사 (Intel_ObtoUI, `Map/Intel_ObtoUI.cs`)
- 현행: 오브젝트 근처 E → 조사 UI, A/D 로 페이지(`Intel_ObtoUI.cs:24` Update 폴링). 키워드 선택은 K2 `KeywordPicker`로 교체됨(`K3_1_picker_open.png`).
- 문제: ① 조사 가능 지점 표시·"조사 완료" 표식 없음(같은 곳 반복 조사 가능한지 불명) ② 조사 결과 텍스트가 대사창과 다른 창이라 흐름이 끊김(추정) ③ KeywordPicker 하단 안내 글자가 작고 회색(`K3_1`: 2줄 범례 `* = reacts / • = asked` 가 11px급) — KeywordUX 쪽 후속이므로 여기선 "하단 안내 공통 컴포넌트"만 제안(4-2).

### 1-7. 수첩 (IntelUI)
- 현행: 키 1(패드 Y). 사건·인물·장소 3목록 + 상세. W/S 목록, A/D 상세 페이지(`PlaceButtonManage` 등).
- 문제: ① 마우스휠·화살표·패드 십자키 방향 입력 없음(`ImproveAudit.md` C-8; 패드 UpDown 은 NavUp/Down 매핑이라 가능하나 화면 안내 없음) ② 키워드 획득 시 알림은 K4 에서 해결(토스트·배지) ③ 메모·즐겨찾기·검색 없음(항목이 많아지는 후반에 불편, 추정) ④ 목록·상세 3종이 각각 다른 구현이라 정렬·스크롤 일관성 없음(코드 분리 3파일).

### 1-8. 판정 룰렛 (`Rollet.cs`)
- 현행: E 로 굴림 시작, E/클릭으로 정지, 종료도 E. 화면에 `Condition : Nomal/Good/Best/Bad/Worst`·`Probability : Very Hard~Very Easy`·`Dice : n`·`Result : …`(`Rollet.cs:143~267`).
- 문제: ① 이미 확률을 5단계 **문구**로 보여주나 실제 %와 목표 숫자는 없음(예: 기능치 60 → "Nomal") ② "Nomal" 오타·영문 고정 ③ 결과가 `resultEnd.text == "Result : Success"` 문자열 비교로 분기(`Rollet.cs:300`) — 표시를 바꾸면 로직이 깨짐(UX 수정 시 함정, 4-6) ④ 굴리기 가속·생략 없음(`ImproveAudit.md` B-6) ⑤ 성공·실패 소리/색 피드백 구성 확인 못 함 ⑥ Esc 로 닫기 조건이 SAN 판정에서 예외(입력 문서).

### 1-9. SAN·HP 표시 (`BillowUIManager.cs:75~79`)
- 현행: `HP : n` / `SAN : n` 텍스트+체력바. 매 프레임 문자열 연결(`Update`→`HpIntMagaer`, 성능·GC).
- 문제: ① 현재값만 있고 최댓값·광기 임계(SAN 5의 배수 손실 등)·직전 변화량 표시 없음 ② SAN 이 깎일 때 화면 연출 없음(피격은 `ShakeCam` `:84` 있음, 끄는 옵션 없음) ③ 광기 단계(`StoryImprove.md` M1~M3)가 들어오면 표시할 곳이 필요 ④ 색약: 체력/SAN 구분이 색(빨강 계열) 의존인지 확인 필요.

### 1-10. 전투 (`Battle/*`, 동료 도입 전 현행)
- 현행: 마우스 호버+클릭으로 적 선택(`EnemySelectButton.cs:41,51`, `Mouse.current` 없으면 생략). 패드 불가(알려진 문제, `Party.md` 7절에서 PT-5 로 해소 예정).
- 문제: ① 행동 버튼(`PlayerActionButton`)의 키보드·패드 포커스 없음(추정) ② 판정 전 성공률·예상 피해 표시 없음 ③ 턴 순서·적 의도 표시 없음(다키스트 던전·XCOM류 관례와 다름) ④ 룰렛(`BattleRollet`)이 매 행동 반복되어 피로(B-6) ⑤ 사망 화면 "You Died!"+Go to Main 만(`screenshot-20261121-172857.png`) — **마지막 저장 불러오기·재도전 선택 없음**, 원인(어떤 적·어떤 판정) 안내 없음 ⑥ 전투 UI 다인원 개편은 Party.md PT-5 에서 하므로 여기선 공통 규약만(4절).

### 1-11. 저장·불러오기 (`SlotMenuView.cs`, `SaveFlow.cs`)
- 현행: 슬롯 UI(저장/불러오기 모드, 포커스 지원 15건), 자동 저장 `AutoSaveWhenStable`(`SaveFlow.cs:102`, 안정 상태에서 `AutoSlot` 저장), 설정창 안 "저장/불러오기" 버튼.
- 문제: ① 자동 저장 시 **표시 없음**(KeywordToast 같은 알림 없음, grep: `SaveFlow` 에 토스트 호출 0) ② 빠른 저장·불러오기 키 없음 ③ 슬롯에 표시되는 정보(장소·일차·플레이타임·스크린샷) 확인 필요 ④ 덮어쓰기 확인·삭제 확인 UI 확인 필요 ⑤ 사망 시 자동 불러오기 연결 없음.

### 1-12. 엔딩·사망
- 현행: 엔딩 판정은 `End_Look_Judge`(A/D 페이지) 및 연출. 사망은 "You Died!" 후 Go to Main.
- 문제: 엔딩 도달 후 "다른 엔딩 진행도·재시작 가이드" 없음, 엔딩 목록·회상 없음(추정). 엔딩 분기 자체는 StoryImprove 담당.

## 2. 장르 관례 대비 (메모리 규칙: 관례와 달라 힘든 건 수정, 고유 설계는 예외)

| 항목 | 관례(탐정 ADV·TRPG·다키스트 던전류) | 현행 | 판정 |
|---|---|---|---|
| 남은 시간·목표 표시 | 항상 보이는 일자·시간대, 퀘스트 로그 | 전구만 | 수정(P0) |
| 설정 영구·접근성 | 글자 크기·흔들림·색약·키 안내 | 일부(속도·볼륨·해상도만) | 수정(P1) |
| 패드/키보드 완결 | 전 화면 포커스 이동 | 일부 화면만 | 수정(P0~P1) |
| 자동 저장 알림·빠른 저장 | 저장 아이콘, F5/F9 | 없음 | 수정(P0) |
| 판정 정보 | 성공률 % 사전 표시 | 5단계 문구 | 수정(P1). 룰렛 연출 자체는 고유 설계(유지) |
| 사망 후 흐름 | 마지막 저장 불러오기 | 메인으로만 | 수정(P0) |
| 대사 하단 버튼/진행 표시 | 명확한 상태·▼ | 약함 | 수정(P1) |
| 4일 데드라인 | 명시적 안내 | 안내 없음 | 고유 설계(압박)지만 **안내는 필요**(P0) |
| 도트 폰트·저해상도 UI | — | 고유 스타일 | 유지(ART-2) |

## 3. 개선안 표

우선순위 P0(플레이 막힘·없으면 출시 품질 문제) / P1 / P2. 비용 S(파일 1~2)·M(여러 파일)·L(구조). **3경로 열**은 필수 항목.

| ID | 항목 | 화면 | 우선 | 비용 | 마우스 | 키보드 | 패드 |
|---|---|---|---|---|---|---|---|
| UX-A1 | 시간·일차·데드라인 HUD (아래 4-1) | 월드 | P0 | M | 항상 표시, 호버 시 설명 | `Tab`(또는 `H`)로 상세 | 십자키 아래 길게 |
| UX-A2 | 현재 목표 한 줄(수첩 "할 일" 탭 포함) | 월드·수첩 | P0 | M | 수첩 탭 클릭 | 수첩 안 `Q`/`E` 탭 | LB/RB |
| UX-A3 | 설정창 레이아웃 재구성(겹침 제거, 한 언어, 탭 3: 소리·화면·게임) | 설정 | P0 | M | 클릭·드래그 | W/S 항목, A/D 값 | 십자키·스틱, A/B |
| UX-A4 | 캐릭터 생성·스킬 분배 키보드·패드 | 생성 | P0 | M | 기존 | W/S·A/D·E | 십자키·A/B |
| UX-A5 | 자동 저장 알림(토스트)·빠른 저장/불러오기 | 전체 | P0 | S | 설정창 버튼 | `F5` 저장 `F9` 불러오기 | Start 메뉴 안 |
| UX-A6 | 사망 화면: 마지막 저장/재도전/메인 3지선다 + 원인 한 줄 | 전투 | P0 | S | 클릭 | W/S+E | 십자키+A |
| UX-A7 | 공통 `HintBar`(하단 키 안내, 입력 장치별 글리프 자동) | 전체 | P0 | M | 숨김 가능 | 표시 | 패드 아이콘 |
| UX-B1 | 대사창 버튼 재디자인(크기·ON 상태·툴팁)·▼ 진행 표시 | 대사 | P1 | S | 클릭 | `A`/`L`/`Tab` 토글 | LB/RB·Y |
| UX-B2 | 글자 크기 3단계(소·중·대), 흔들림 끄기, 색 구분 보조(아이콘·패턴), 대사창 불투명도 | 설정 | P1 | M | 설정 | 설정 | 설정 |
| UX-B3 | 판정 룰렛 숫자 공개: 목표 `n%`(보통/어려움/극단 임계 포함)·보정 내역 한 줄 | 룰렛 | P1 | M | 표시만 | 표시만 | 표시만 |
| UX-B4 | 판정 연출 가속(스킵 키 누르면 즉시 확정, 설정에 "빠른 판정") | 룰렛·전투 | P1 | M | 우클릭 | `Shift` | RT |
| UX-B5 | SAN/HP 패널: 현재/최대·직전 변화량·SAN 위험 단계·피격/SAN 연출(색 플래시) | HUD | P1 | M | 호버로 수치 | `Tab` 상세 | 십자키 아래 |
| UX-B6 | 수첩 일관성: 휠·화살표·십자키 이동 통일, 3목록 공통 스크롤 컴포넌트, 새 항목 NEW(K4 연동) | 수첩 | P1 | M | 휠 | W/S/방향키, A/D | 십자키·LB/RB |
| UX-B7 | 상호작용 힌트: 가까이 가면 머리 위 "E 조사 / F 이동" 말풍선(장치별 글리프) | 월드 | P1 | S | 호버 | 표시 | 표시 |
| UX-B8 | 지도 UI 키보드·패드 이동(목적지 목록 포커스), 현재 위치·이동 불가 사유 표시 | 지도 | P1 | M | 클릭 | 방향키+E | 십자키+A |
| UX-B9 | 타이틀 개선: 이어하기 슬롯 요약(장소·일차·시각), 선택 표시 기본값, 한글 라벨 | 타이틀 | P1 | S | 호버 | W/S+E | 십자키+A |
| UX-B10 | 전투 정보: 예상 성공률·피해 범위·적 의도·턴 순서(Party PT-5 와 공유 컴포넌트) | 전투 | P1 | L | 호버 | 포커스 | 포커스 |
| UX-C1 | 글꼴: 한글 포함 도트 글꼴 SDF(동적/Fallback) 도입(`ManageMain.cs:104` 한글 불가 해소) | 전체 | P1 | M | — | — | — |
| UX-C2 | 툴팁 시스템(호버/포커스 시 용어·아이템·상태 설명) | 전체 | P2 | M | 호버 | 포커스 후 `Tab` | 포커스 후 Y |
| UX-C3 | 대사 버튼 ON 상태 저장·"마지막 선택 위치"(auto·skip) | 대사 | P2 | S | — | — | — |
| UX-C4 | 키 리바인딩 UI(`KeySetting`) 완성 + 패드 배치 안내 | 설정 | P2 | L | 클릭 | 선택 후 키 입력 | 선택 후 버튼 |
| UX-C5 | 엔딩 목록·회상, 도전 과제 연동 | 타이틀 | P2 | L | 목록 | 목록 | 목록 |
| UX-C6 | 이중 UI 동시 입력 방지: `UiStack` 위계 정리(Esc/B 가 최상단만 닫음) | 전체 | P1 | M | — | — | — |

### 3-1. 판단 메모
- 확률 미리 보기: 이미 "Very Hard~Very Easy" 문구가 있어 UI 뼈대는 있다. **%를 바로 보여줘도 게임 긴장감을 해치지 않는지**는 Q-UX2. 가정: `n%` 는 룰렛이 열릴 때 보이고, 보통/어려움/극단 임계(`CocDesign.md`)는 작게.
- 텍스트 크기: 도트 폰트는 정수 배율이 깨지지 않게 **3단계 고정 배율**(1.0·1.25·1.5 가 아니라 폰트 포인트 ×1/×1.5/×2 처럼 도트 격자 6배 단위 유지, `ArtStyle.md`). 임의 스케일 금지.
- 접근성: 색약은 "색만으로 구분하지 않기"(아이콘·텍스트 병기)로 처리. 별도 색약 팔레트는 P2 보류. 흔들림 끄기는 `CameraManager.ShakeCam`·DOTween 화면 흔들림·대사창 떨림을 한 플래그로 묶는다.
- 툴팁(UX-C2)은 마우스 전용이 되기 쉬워 **포커스에도 뜨게** 해야 3경로가 성립한다.

## 4. 구현 설계

### 4-1. 시간·목표 HUD (UX-A1·A2)
- 신규 `Assets/Script/UI/TimeHudView.cs`(MonoBehaviour, 월드 씬 캔버스 상단 좌측). 표시: `N일차 · 낮|저녁|밤|새벽`(`timeCount`→일·시간대는 `Core/World/WorldClock.cs` 의 읽기 전용 값 사용, 직접 `% 4` 계산 금지) + 남은 칸 점 아이콘 N개 + "마감까지 M칸".
- 갱신: `WorldBinder` 의 상태 변경 단일 통지(P13) 구독(Update 폴링 금지). 값이 줄어들 때 DOTween `DOPunchScale`(0.2s)+종소리(`SoundManager`).
- 상세 패널(`Tab`/십자키 아래 길게): 4일 일정 한 줄씩 + 오늘 방문 가능 장소 수(남은 방문 칸).
- 목표: 신규 `Core/Story/ObjectiveCatalog.cs`(순수 C#, 조건 `ConditionParser` 재사용)에서 "현재 가장 우선 미획득 단서에 대응하는 한 줄 힌트" 선택. 데이터는 `Resources/Story/objectives.csv`(id, cond, ko, en). 스포일러 방지: 힌트는 "무엇을 해야 하나"가 아니라 "어디를 아직 안 갔나" 수준(Q-UX3).
- 수첩에 "할 일" 탭 추가(IntelUI 3목록과 별도 컴포넌트, 새 키).

### 4-2. 공통 컴포넌트 (UX-A7·B6·C2·C6)
- `HintBar`(UI 하단 한 줄): `HintBar.Show(new[]{ new Hint(Action.Confirm,"선택"), ... })`, `HintBar.Hide()`. 현재 입력 장치(`GameInput` 신규 프로퍼티 `LastDevice` Keyboard/Mouse/Gamepad)에 따라 글리프 변경. 각 패널은 열릴 때 push, 닫힐 때 pop(스택). BacklogView·KeywordPicker 의 개별 안내문을 이것으로 교체.
- 입력 장치 추적: `GameInput` 에 마지막으로 눌린 장치 기록(InputSystem `InputSystem.onActionChange` 또는 `onAnyButtonPress`). 새 코드 위치 `Assets/Script/Input/GameInput.cs` 에 추가.
- `UiStack`(`UiGate`)에 설정·수첩·지도·상점·룰렛·피커 전부 등록, Esc/B(`CancelDown`)는 최상단만 처리. 현재 개별 패널이 각자 `CancelDown` 을 읽는 곳(`Rollet.cs:53`, `MapManager.cs:431`, `ShopUI.cs:68`, `SIS_UIManager.cs:247`, `IntelManager.cs:152`)을 `UiStack.TryCloseTop()` 호출로 이관.
- 포커스 헬퍼 `FocusScope`: 패널 열릴 때 첫 항목 `SetSelectedGameObject`, 닫힐 때 이전 선택 복원. 선택 하이라이트는 공통 스프라이트 하나(임시 → ART-2).
- 툴팁(UX-C2): `TooltipTarget`(호버·포커스 이벤트) + `TooltipView`(단일 인스턴스). 내용은 I2 새 키.

### 4-3. 설정창 (UX-A3·B2)
- 탭 3: 소리(BGM·SFX·텍스트 사운드?) / 화면(전체화면 토글 1개, 해상도 드롭다운식 `<>` 선택, 적용 즉시 + 15초 되돌리기 확인, 흔들림, 글자 크기) / 게임(텍스트 속도 5단계, 스킵 읽은 것만, auto 대기 시간, 키 안내).
- `settingManager` 는 현재 약 400줄+(`OpenSlotMenu` 231~, `ChangeResolution` 268~, 해상도 12분기 흔적): 탭별 `SettingsTabView` 3개로 분리하고 `SettingsService`(`Data/SettingsService.cs`)가 값 소유. `SettingsData` 에 **필드 추가만**: `ShakeEnabled=true`, `TextScaleLevel=1`, `AutoWaitSec=1.5f`, `ShowHints=true`, `FastRoll=false`(옛 파일은 기본값, `Clamp` 갱신 `SettingsData.cs:23`~).
- 슬라이더·토글: Selectable 포커스 + A/D 가 값 증감(`GameInput.PageNext/Prev` 재사용, 슬라이더는 길게 누르면 연속).
- 해상도 목록은 `SettingsService.ResolutionWidth/Height` 유지, 한글 표기 라벨은 새 I2 키.

### 4-4. 캐릭터 생성 (UX-A4)
- `setStat`·`setSkill`: 화면에 포커스 체인(굴리기 → 재굴림 → 다음 → 스킬 칸들 → 확정). `NavUp/Down` 이동, `Interact` 실행, `Cancel` 뒤로. 스킬 점수 `PageNext/Prev` 로 ±1.
- 초기 안내 문구 "주사위를 굴려 능력치를 정하세요" 1줄 + 각 능력치에 호버·포커스 설명(기존 `statExplain` 데이터 재사용 → 툴팁 컴포넌트).
- 재굴림: 횟수·이전 값 비교(두 번째 값과 나란히) 여부는 Q-UX4. 가정: 재굴림 무제한, 이전 값 1개 보존, "이전 값으로 돌리기" 버튼.
- 확정 전 요약 패널(능력치 합·잔여 스킬 점수) 후 "시작" — 점수가 남으면 경고 후 허용.

### 4-5. 저장·사망 (UX-A5·A6)
- `SaveFlow.AutoSaveWhenStable` 성공 시 `SaveToast.Show("자동 저장됨")` (신규, 우상단 저장 아이콘+글, 1.2s, `KeywordToast` 와 같은 큐 사용 → 겹침 방지). 실패는 빨간 문구+지속 표시.
- 빠른 저장·불러오기: 새 액션 `QuickSave`(F5)·`QuickLoad`(F9)를 `SCoWInput.inputactions` `UI` 맵에 **추가**(기존 이름 변경 금지), 패드는 Start 메뉴 안 버튼으로만(우발 입력 방지). `QuickLoad` 는 확인 팝업.
- 사망 화면: "You Died!" 아래 `마지막 저장에서 재개 / 자동 저장에서 재개 / 메인`. 슬롯 정보는 `SaveService` 메타 사용. 원인 문구: 마지막 판정 결과 로그(`DialogueRunner` 백로그 최근 판정)에서 `BattleEngine` 이 넘기는 "적 이름·마지막 피해" 한 줄(데이터가 없으면 생략).

### 4-6. 판정 정보·연출 (UX-B3·B4)
- 룰렛 상단에 `기능 n% / 어려움 n/2 / 극단 n/5`(룰은 `Core/Rules/SkillCheck`) + 상태(보너스·페널티 주사위 내역) 표시. 문구 "Nomal"→ 새 I2 키로 "보통" 계열 신설(옛 키 변경 금지).
- **함정**: `Rollet.cs:300~316` 이 `resultEnd.text == "Result : Success"` 로 분기 → 표시 문구를 바꾸기 전에 `SkillCheck.Evaluate` 결과 enum 을 직접 쓰도록 먼저 고친다(UX-B3 의 선행 작업). `Rollet.cs` 는 비 UTF-8(`ImproveAudit.md` D-2) → 변환 선행 또는 파이썬 바이트 치환.
- 빠른 판정: 굴리기 시작 후 Advance 입력 → 즉시 정지(현행). 추가로 "연출 생략" 설정 시 굴림 0.3s·결과 즉시.

### 4-7. HUD SAN·HP (UX-B5)
- `BillowUIManager.HpIntMagaer` 문자열 연결을 값 변경 시에만 갱신(이벤트). 표시 `SAN 42/60 (-3)` + 단계 아이콘(안정/불안/위험). 변화량은 1.5초 후 사라짐. 피격 시 붉은 비네트 0.15s, SAN 감소 시 청록 비네트+소리(흔들림 끔이면 비네트만).
- 광기 단계(M1~M3)는 StoryImprove 구현 때 같은 패널에 아이콘 슬롯만 예약(`MadnessIcon` 비활성).

### 4-8. 대사창 (UX-B1)
- `auto/log/skip` 3개를 아이콘+글자(▶▶, ≡, ⏭) 버튼으로, ON 상태 배경 색·밑줄. 호버·포커스 툴팁에 단축키(키보드 `A`·`L`·`Tab`? 기존 `DialogAutoToggleDown` 등 `GameInput` 액션이 있으므로 **키 재할당 금지**, 바인딩 확인 후 표기만).
- 다음 줄 표시 ▼(DOTween Y ±2px, 0.5s, `SetLink`) — 텍스트 출력 완료·입력 대기 중에만.
- 글자 크기: `TextScaleLevel` 에 따라 대사창 폰트 사이즈 3단계. 줄 간격 +15%. 임시 → ART-2 가 창 프레임 교체.

### 4-9. 타이틀·지도 (UX-B8·B9)
- 타이틀: 이어하기 항목 하단에 `3일차 저녁 · 경찰서 · 02:14:33` 작은 글(저장 메타). 시작 시 첫 항목 자동 선택(`_navIndex=0`), 마우스가 움직이면 해제. 메뉴 중앙 정렬(로고와 같은 앵커). 한글 라벨은 UX-C1 글꼴 선행(그 전엔 영문 유지).
- 지도: `ChangeMapButton` 에 포커스 체인, 선택 시 "이동 시 N칸 소비" 미리보기, 불가 시 사유(밤·전투 중·하수도 잠김 등). 현재 위치 깜박임.

## 5. 기능 개선 (UI 밖, 겹침 정리)

| ID | 내용 | 겹치는 문서 | 처리 |
|---|---|---|---|
| F-1 | 대사 중에도 설정·저장·백로그 열기(대사 중 입력 일시정지 규칙 `BusyRules`) | `ImproveAudit.md` B-4 | 이 문서 UX-C6 에서 UiStack 규칙으로 해결 |
| F-2 | 같은 조사 지점 재조사 시 "이미 조사함" 요약 대사 한 줄로 단축 | `StoryImprove.md` 막힘 | 표식·요약만 여기, 새 대사 내용은 스토리 문서 |
| F-3 | 시간 소비 경고: 이동·조사로 칸이 줄기 전 "N칸 소비" 확인(옵션) | `StoryImprove.md` 데드라인 | 규칙 변경 없이 표시만 |
| F-4 | 되돌릴 수 없는 선택(키워드 누설 등) 직전 경고 확인 | Q8 | 표시 방식만(`StoryImprove.md` 3-1 사전 경고와 같은 위치) |
| F-5 | 전투 자동 진행·빠른 전투(반복 판정 가속) | `Party.md` | UX-B4 설정 `FastRoll` |
| F-6 | 인트로·컷신 스킵(첫 시청 후) | `ImproveAudit.md` B-5 | `ReadLog` 에 인트로 기록, 두 번째부터 길게 눌러 스킵 |
| F-7 | 튜토리얼을 상황별 1회 팝업(룰렛 처음 열 때 등)으로 분산, 설정의 Tutorials 는 다시 보기 | B-8 | `TutoralManage` 재사용 |

## 6. 구현 단계 (builder 단위)

순서는 의존성 순. 각 단계는 한 커밋. 코드 변경 전 해당 파일 인코딩 확인(`csharp.md`), 새 문구는 I2 새 키만, `SettingsData`·세이브는 필드 추가만.

| 단계 | 내용 | 선행 | 검증(프로브) | 그래픽 |
|---|---|---|---|---|
| UX-1 | 공통 뼈대: `GameInput.LastDevice`, `HintBar`, `FocusScope`, `UiStack` 최상단 닫기 이관(Esc/B) | — | EditMode: 스택 push/pop 순서·`TryCloseTop`; 플레이: 룰렛 위 설정 열기 후 Esc 한 번에 한 겹만 닫힘, 패드 B 동일 | 임시 포커스 링 → ART-2 |
| UX-2 | 설정창 재구성(탭 3, 겹침 제거, 포커스 조작) + `SettingsData` 필드 추가(흔들림·글자 크기 등, 적용은 UX-7) | UX-1 | 설정창 스크린샷에서 겹침 0, 키보드·패드로 모든 항목 변경, 옛 설정 파일 로드 시 기본값 | 임시 → ART-2 |
| UX-3 | 시간 HUD+목표+수첩 "할 일" 탭 | UX-1 | EditMode: 목표 선택 규칙(조건 샘플 5개), 플레이: 1일차 낮→밤 전환 시 HUD 값·펀치, 저장 후 불러오기 재현 | 임시 아이콘 |
| UX-4 | 캐릭터 생성·스킬 입력·안내·요약 패널 | UX-1 | SetCharacter 씬을 키보드만/패드 가상 입력으로 완료, 잔여 점수 경고 | 임시 |
| UX-5 | 저장 토스트·F5/F9·사망 화면 3지선다 | UX-1 | 자동 저장 시 토스트 1회, 사망→마지막 저장 재개 후 상태 동치(골든), 실패 경로 토스트 | 임시 아이콘 |
| UX-6 | 판정 UX: Result enum 선행 수정 → 확률 숫자 표시·빠른 판정 | UX-1 | EditMode: 임계값 표시 = `SkillCheck` 값, 플레이: 룰렛에서 `n%` 표시·FastRoll ON 시 0.3s | 없음 |
| UX-7 | HUD SAN·HP 이벤트 갱신·변화량·비네트·흔들림 끄기 적용 | UX-2 | HP/SAN 변동 시 문자열 갱신 횟수(프로파일 카운터), 흔들림 OFF 에서 `ShakeCam` 무시 | 임시 비네트 |
| UX-8 | 대사창 버튼·▼·글자 크기 3단계 | UX-2 | 3단계 각 줄바꿈 정상, auto/skip ON 표시, 속도 5단계 영향 없음 | 임시 → ART-2 |
| UX-9 | 수첩 일관성(휠·방향키·공통 스크롤)·상호작용 힌트 말풍선·지도 포커스 | UX-1 | 수첩 3목록 휠·W/S·십자키 동일 동작, 힌트가 장치별 글리프 | 임시 말풍선 |
| UX-10 | 타이틀 개선·한글 글꼴(`ManageMain.cs:104` 해소) | UX-1 | 이어하기에 한글 라벨 정상(글리프 존재), 슬롯 요약 표시 | 글꼴은 사용자 검토 |
| UX-11 | 툴팁 시스템·전투 정보 표시(PT-5 와 컴포넌트 공유) | UX-1, PT-5 | 호버·포커스 모두 툴팁 표시, 전투 대상 포커스 시 예상 성공률 | 임시 |
| UX-12 | P2 묶음: 키 리바인딩·엔딩 목록 | UX-2 | 리바인딩 후 세이브(설정) 유지, `GameInput` 충돌 검출 | ART-2 |

우선순위 요약: **P0 = UX-A1~A7(7개)** → 단계 UX-1·2·3·4·5. P1 → UX-6~11. P2 → UX-12.

### UX-6 구현 기록 (11-25)
- `Rollet.cs` 는 P1(d89dda4)에서 이미 UTF-8 로 변환돼 있어 변환 커밋은 필요 없었다(strict 디코드 통과·U+FFFD 0).
- 결과 분기는 `SuccessLevel resultLevel` 로 한다(`resultEnd.text` 는 표시와 옛 콜백 전달용으로만 남김). 판정 규칙·난수 소비 불변.
- 성공 확률 `n%`: Core `SkillCheck.SuccessChance/SuccessPercent`(Evaluate 를 눈 10^4 가지에 돌려 센 값). 룰렛 `Probability : <구간> (n%)` 에 붙이고 구간 문구도 같은 값에서 나온다(옛 손식 `succesProbability` 대체). Q-UX2 가정대로 `ShowHints` OFF·`??? : Deftness` 는 숨김. 문구는 TODO I2.
- 빠른 판정: `SettingsData.FastRoll` ON 이면 굴림 시작 즉시 눈 갱신, 0.3초 뒤 자동 정지, 결과 지연 0. 손으로 정지해도 지연 0.
- 함정: 기능치가 10의 배수(예 50)면 `00+0=100` 규칙 때문에 Good 이 Nomal 과 같은 확률이 된다(이득·손실 상쇄, 규칙 그대로).
- `BattleRollet.cs` 는 이번 범위 밖(옛 손식·문자열 비교 유지).

## 7. 공통 검증 기준

- 새 UI는 마우스·키보드·패드 세 경로 각각 한 번씩 완결 확인(에디터 비포커스 한계로 자동 주입 불가한 것은 "미검증"으로 기록, 사용자 확인 필요 목록에 추가: `Docs/Handoff/state.md`).
- 스크린샷: `unity-render`로 1920×1080 과 1280×720 두 해상도에서 겹침·잘림 확인(`p8_settings4.png` 같은 겹침 회귀 방지).
- 콘솔 error 0, EditMode 전체 통과, `SCoWInput.inputactions` 에 액션 **추가만**(삭제·이름 변경 금지).
- ES3 세이브 키·I2 키 변경 금지(새 키 추가만). 설정 파일 옛 버전 로드 → 새 필드 기본값 확인.

## 8. 함정

- `Rollet.cs`·`BattleRollet.cs`·`BattleManager.cs` 등 비 UTF-8: Edit/Write 거부(`ImproveAudit.md` D-2). UX-6 은 인코딩 변환 커밋을 먼저 분리.
- 씬 직접 수정(Main.unity 대용량): 가능하면 런타임에 코드로 UI 생성(`ManageMain` 의 Continue 복제 방식처럼)하고, 필요한 만큼만 씬 변경. Unity 6 재직렬화 diff 주의.
- TMP 정적 글꼴에 한글 없음: 새 한글 문구는 UX-10 글꼴 도입 전엔 영문/아이콘으로 두고 I2 키만 만든다.
- DOTween: 새 애니는 `SetLink` 필수, 재호출 전 `Kill`(`csharp.md`). 시간 정지 중 UI 애니는 `SetUpdate(true)`.
- `E` 키 공용: 상호작용 힌트(UX-B7)와 대사 진행이 같은 키 → 힌트는 `UiGate.AnyOpen`·대사 중에는 숨김.
- 패드 A 겹침(Interact·Advance): 새 화면은 반드시 `UiStack` 최상단일 때만 입력 처리.

## 9. 미결 (임시 가정, `Docs/OpenQuestions.md` Q-UX1~5 에도 있음)

- Q-UX1 시간·일차 HUD 를 항상 보이게 할지(몰입 vs 정보) — 가정: 항상 표시, 설정에서 숨김 가능.
- Q-UX2 판정 성공률을 숫자(%)로 공개할지 — 가정: 공개(룰렛 열린 후), 설정 `ShowHints` 로 숨김 가능.
- Q-UX3 현재 목표 힌트의 수준(스포일러) — 가정: "어디를 아직 안 갔나"만, 정답 단서는 말하지 않음.
- Q-UX4 캐릭터 생성 재굴림 제한 — 가정: 무제한+이전 값 1개 보존.
- Q-UX5 한글 글꼴 도입(UX-C1)과 아트 폰트 방향(도트 영문 폰트 유지 vs 한글 도트 폰트) — 가정: 영문은 기존 도트 폰트, 한글은 Fallback 별도 한글 도트 폰트, 사용자 검토 후 적용.
