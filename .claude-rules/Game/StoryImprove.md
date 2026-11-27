# 스토리 개선안 (StoryImprove)

> 2026-11-21. 근거 문서는 `Game/Story.md`(줄거리·시간·사슬·엔딩). 규칙·광기 모델은 `Game/CocDesign.md`. CSV 서식은 `Docs/Playtest/DialogAudit.md`.
> 우선순위 P0(막힘·모순·크래시급, 먼저) / P1(체감 큰 개선) / P2(다듬기). 비용 S(대사·JSON 몇 줄) / M(노드·판정·조건 추가) / L(코드·연출 신규).
> 기획 질문은 `Docs/OpenQuestions.md` Q7~Q13 에 임시 가정과 함께 적었다. 코드는 이 문서에서 고치지 않는다.
> `CSV:ID` 는 ID 열 값. 대사는 영문이라 수정 문구도 영문 예시를 함께 둔다.

## 0. 요약

| 구분 | P0 | P1 | P2 |
|---|---:|---:|---:|
| (a) 세세한 수정 | 5 | 8 | 7 |
| (b) 흐름 | 3 | 4 | 0 |
| (c) 자유도 | 0 | 4 | 3 |
| (d) SAN·광기 연출 | 0 | 6 | 5 |
| 합계 | **8** | 22 | 15 |

가장 큰 막힘·모순 3개(자세한 건 아래):
1. 엘라 집 첫 방문이 1일차(`t<6`)에 한정 → 놓치면 에이든·금고·정신분석 전부 소실, 안내 없음(B-1, C-1).
2. 하수도 진입 루트의 판정 대부분이 1회성(창문·사물함 `tryBrokenWindow`/`tryObserLokcer`) + 4일차 밤 악몽 엔딩 예고 없음(B-2, B-3).
3. 대학생 키워드 노드가 케인 대사를, 케인 점액 키워드가 존재하지 않는 줄을 가리키는 것으로 보임(A-1) + 실종자 "6명 남자만"(A-2).

## 1. (a) 세세한 수정

### 1-1. 설정 모순·데이터 오류

| ID | 우선 | 비용 | 위치 | 문제 | 수정안 |
|---|---|---|---|---|---|
| A-1 | P0 | S(검증 후) | `Keyword.json:61-67,70,227-230`, `1st_University.json` | `University student/*` 노드가 `1st_University:43-63`(= 케인 첫 만남·점액 대사)를 가리킴. 실제 학생 대사는 `24-42`이고 DialogAudit 이 "24-42 도달 불가"로 잡음. `Kane/Fishy Slime` 은 `1st_Riverside:53-63,66-74` 인데 Riverside CSV 는 71줄뿐이고 점액 대사는 `1st_University:53-74`에 있음 | 학생 노드 `studentKeyContentNightmare=24-28`, `WaterNightmare=29-31`, `Rumor=32-34`, `FishySmell=35-38`, `Details=39-42`(CSV 흐름 대로), 케인 점액 `1st_University:53-65`(눈 키워드 있음)/`66-74`(없음). **플레이 모드에서 먼저 재현**(씬 이벤트가 이미 맞게 보정했을 수 있음) |
| A-2 | P0 | S | PoliceOffice:25 vs 29, 122, 128 | "남자 4명·여자 2명"이라 해놓고 29 에서 "모두 서로 모르는 남자들" | 29 를 "모두 서로 모르는 사이였고 직업도 달랐다"로 |
| A-3 | P0 | S | NoonEvent:4, 5 | 전화 분기 1(실종 없음, 2-16): ID 4 가 알버트 입으로 파비안 서술("I could see Ella dead...")이며, 5 의 파비안 질문 "why are you asking about her?" 앞에 알버트의 "엘라 기억하나?"(20, 37)가 빠짐 | ID 4 를 `Albert,@Do you remember Ms. Ella Triss^ who came to your office yesterday in the morning?@` 로 교체 |
| A-4 | P0 | S | PoliceOffice:13-14 | 화자 뒤바뀜: 알버트가 "It's been a while, Albert", 파비안이 "Oh, Fabian." | 13 `Fabian: Hey^ Albert. It's been a while.` / 14 `Albert: Oh^ Fabian. Long time no see.` |
| A-5 | P0 | S(임시)·M(작성) | CH:101-123, Item:28-30 (`Aiden_Judge_psychotherapy_Sucsses`, `_End`, `OldMap_Look`) | 정신분석 성공 시 빈 대사창 12개, 끝 9개, 지도 3개(Q5). 첫 번째는 성공해도 이어서 `149-190`이 나오는지 미확인 | 임시: JSON 에서 `Sucsses` 노드를 `Safe`/`NoSafe`(149-190)로 직결, `OldMap_Look` 숨김. 작성: 에이든이 "금고 번호는 잊었지만 엘라 생일의 거꾸로"류 힌트를 주는 6~8줄(사용자 승인 필요, Q5) |
| A-6 | P1 ✅완료(11-27) | S | `Judge.json:664-666` | `Maeve : Persuasion@0.ok` 가 `noonEventAccessAuthorization`(경찰 설득 보상 플래그)을 세움. 복붙 오류 | `onEnter` 삭제 또는 새 플래그 `maeveDrinkOk` |
| A-7 | P1 ✅완료(11-27) | S | DailyNews:54-55, 42-68, `Keyword.json:33-34` | 알버트에게 실종 정보를 이미 들었는데도 스웨인이 처음 알려주면 파비안이 "I didn't hear anything" | `Husband's job` 첫 케이스에 `flag:intelMissingPeople2` 변형(Fabian "Albert mentioned something like that.")을 앞에 추가 |
| A-8 | P1 ✅완료(11-27, 문구 Q-P1-1) | S | DetectiveOffice:21, PoliceOffice:108, Bar:49, InSewer:146 | 행성 직렬 시작 7월 15일, 알버트 "14일 밤", 메이브 "15일 저녁", 에필로그 "약속에 늦음" | **15일 저녁**으로 통일(Q7). PoliceOffice:108, InSewer:146 수정. 에필로그는 "새벽을 지나 해가 중천… 저녁 약속에 늦지 않았다/못 갔다"로 |
| A-9 | P1 ✅완료(11-27) | S | Bar:72, PoliceOffice:106 | 에이든(남)을 "her shoulder", 메이브(여)를 "him" | "his shoulder", "how is she doing" |
| A-10 | P1 ✅완료(11-27) | S | DailyNews:101, 152 / Hospital:115 / PoliceOffice:143,152 / Riverside:46 / Item:27 / CSV 곳곳 | 같은 곳을 sewage maintenance facilities / sewerage office / sewage office / sewerge office / sewage treatment office 로 부름 | 표준 **Sewerage Maintenance Office** (Item:27 기준). 키워드 라벨(`●1ⓢ`)과 대사 동시 수정 |
| A-11 | P1 | M | Bar:81-89, SewerOffice:52-54 | 바의 해고 노동자 둘이 "비릿한 냄새의 근원"으로 묘사되나, 이들은 해고된 진짜 노동자이고 냄새 나는 건 새로 투입된 딥원. 플레이어가 "노동자=범인"으로 굳어짐 | 바 대사에 해고자 발언 추가: "The new crew doesn't talk. They smell like the bottom of the river." 근원을 "새로 온 일꾼"으로 모아 정리 단계(DetectiveOffice 270-290)에서도 "sewer workers"를 "the new crew at the sewer office"로 |
| A-12 | P1 ✅완료(11-27, 문구 Q-P1-2) | S | PoliceOffice:168,203 + NoonEvent:170-174 | 파비안이 시신 팔찌를 훔쳐도(`DeadBody : Stealing`) 알버트는 "귀금속 안 가져감" 무반응 | 새 플래그 `braceletStolen`, 알버트 3일차 대사에 `when flag:braceletStolen` 케이스(알버트가 팔찌 분실 언급 → 데이브에게 판 뒤라면 거래 후회, 에이든 에필로그에서 반환 가능은 C-6) |

### 1-2. 반복·어색한 대사·말투·오타

| ID | 우선 | 비용 | 위치 | 문제 → 수정 |
|---|---|---|---|---|
| A-13 | P2 | M | Bar:2-20, 22-39, 142-158, 200-211 | 같은 인사말 4세트 복제 → 첫 방문 1세트 + 재방문은 `ReadLog`(`Core/State/ReadLog.cs`)로 스킵, 변형은 차이 줄만 |
| A-14 | P2 | M | Riverside:29-48 vs 49-69, University 중복 | 비릿함 유/무 두 버전이 거의 같음 → 공통부 1벌, 차이 3줄만 변형 |
| A-15 | P2 | M | CH:149-169 vs 170-190 | 금고 유/무 변형이 20줄 복제 → 앞 6줄 공통, 마지막 5줄 변형 |
| A-16 | P1 | M | Keyword 근원 3종("Source speculation / Sewer & FishySmell / Cause of Fishy smell") | 같은 답 3번 반복 → 1회 답 + 후속 2개는 "더 구체적" 추가 정보(첫 번째는 하수도, 두 번째는 노동자, 세 번째는 정비소) 순차 공개 |
| A-17 | P2 | S | Bar:3, 11 / NoonEvent:11 / CH:125,169 / SewerOffice:66,71 / DetectiveOffice:3 | 직역투: "My only strength."→"That's my one good quality.", "You're a bitch."→"You're a piece of work.", "I hate don't feel refreshed"→"I hate leaving things unsettled.", "harvestless/there's a harvest"→"not a total loss / at least that got me something", "map of the priests"→"map of the maintenance tunnels" |
| A-18 | P2 | S | 오타 | CH:46 horrable→horrible, DetectiveOffice:63 everytime→every time, DailyNews:32 Trice→Triss, PoliceOffice:59 gottencalls, :109 Miscatonic, Hospital:103 heared, 113 docter, Bar:119 romur, :151 root→route, 화자 끝 공백(`Fabian ,`·`Albert ,` 다수), 대문자 `Ella triss` |
| A-19 | P2 | S | CH:2, NoonEvent:96, Riverside "except at night" | 낮·저녁인데 "Good morning" → 시간대 맞춤 문구(Good day) |
| A-20 | P2 | S | SewerOffice:88, Bar:119 | 셀 메모가 대사에 섞인 흔적(`rce speculation Source of the smell?...`) 정리 |

## 2. (b) 흐름: 끊기지 않게

### 2-1. 막힘 방지

| ID | 우선 | 비용 | 현재 | 제안 |
|---|---|---|---|---|
| B-1 | P0 | S | 엘라 집 첫 방문 `t<6`(`places.json:5`). 놓치면 에이든 영영 못 만남. 1일차 이동은 2번뿐(`MapManager:704~790`) | 1일차 밤 정리(Fab_EndDay 182-207) 앞에 `when: !flag:aidenFirstDialog` 변형 노드 추가: `"I haven't seen Aiden himself yet. Tomorrow might be too late... something feels off."` 2일차 아침 첫 대사에도 동일. 선택지는 막지 않음 |
| B-2 | P0 | M | 하수도 진입: 소장 설득/절도 → 창문(힘)·사물함(관찰력) 1회성 → 강변 자물쇠(손재주) → 맨홀(자료조사 성공 필요). 모두 실패 가능, 일부 재시도 불가 | (1) **핵심 단서(진입)는 판정 무관 경로 1개 보장**: 3일차 아침 알버트(생존 시) 또는 병원 케이트의 사촌 단서로 "하수도 정비소 설계도"를 우편·전언으로 지급하는 구제 노드(`counter:timeCount>=9 && !flag:sewerageKey` 일 때 `Fab_MorningMail`). (2) 창문·사물함 실패 재시도: 하루에 1번 → 다음 시간대에 다시(`tryBrokenWindow` 를 phase 별로 리셋) 또는 행운 소모 |
| B-3 | P0 | S | 4일차 밤 악몽 엔딩 예고 없음(`DetectiveOffice 343-361`). 시간 경과 안내가 대사 속에 없음 | 밤 정리(2일차 254-290, 3일차 신규)에 "별이 모두 늘어서는 건 15일 밤. 오늘이 몇 일이지?" 형태의 D-day 대사, 남은 밤 수 표시. 수첩에 `날짜: 7/11~7/14` 한 줄 |
| B-4 | P1 ⏸보류(Q-P1-3) | S | 새벽 시간대 NPC 부재(`Kate ◎⑦`, 학생 저녁 부재, 하수도 소장 저녁 부재)의 이유가 대사에 없음 | 도착 시 한 줄: "No one's here at this hour." 가 아니라 이유 한 줄("The ward is closed to visitors after noon.") — Topic `when time%4==…` 에 새 노드 |

### 2-2. 핵심 단서 대체 경로(실패해도 이야기가 이어진다)

| 핵심 단서 | 현재 출처 | 판정 실패 시 | 제안(추가) |
|---|---|---|---|
| 하수도 정비소 위치 ●1ⓢ | Swain 101·152·186, Albert 143·152, Kate 115, Dave 37, Maeve 175, Item 27, NoonEvent 146 | 출처 10곳이라 안전 | 변경 없음(모범) |
| 열쇠(강변 하수도) | 소장 설득, 소장 절도, 사물함(관찰력), 강변 자물쇠 손재주 | 4개 중 3개 판정 | + 메이브(옛 밀수로, Bar 101-102)·알버트(경찰 반년 전 하수도 굴착 PoliceOffice 62)가 **비판정 경로**로 열쇠·도면 제공 (C-2) |
| 작업복(변장) | 사물함, 소장 | 실패 시 은밀 판정으로 대체 가능(InSewer 1-13) | 유지 |
| 에이든 금고 | 힘/언변/정신분석/방치 | 방치 시 서류 없음 | 서류 내용(Item 15-26)은 알버트·스웨인·데이브 키워드로 모두 얻으므로 금고는 부가. 금고 실패 시 파비안 독백: "I'll hear it from somebody else." |
| 사건 단서(사인·물갈퀴·맨홀) | 시신 의학·관찰력·자료조사 | 현장 재방문으로 자료조사 재시도 가능(NoonEvent 180-187). 시신은 `t>=7`에 사라짐 | 사라지기 전 재시도 안내: 병원 3일차 부검(Hospital 131-148)이 사인·악력을 판정 없이 제공(이미 있음) → 물갈퀴는 병원에서도 "손자국" 한 줄 추가 |

### 2-3. 이야기 연결·후일담

| ID | 우선 | 비용 | 문제 | 제안 |
|---|---|---|---|---|
| B-5 | P1 | M | 소장(블랙웰) 가족은 이미 희생됨(InSewer 29)인데 소장은 희망을 가짐(SewerOffice 52-56), 에필로그에 소장 없음 | 에필로그 변주 `Ending_Warden`: 소장은 열쇠를 건넨 걸 자백하고 연행(또는 강에 투신). 일지를 읽은 플레이어만 이해하는 한 줄 |
| B-6 | P1 | M | 데이브 증발(Slum 61-62)에 설명·여운 없음, "반으로 나누기"(Slum 42) 불이행 | 에필로그 신문 한 줄: "Dusty Dave found in the river." 또는 데이브 유품에 쪽지. 금고 지급 후 사망까지 걸린 시간(밤)을 대사로 암시 |
| B-7 | P1 | M | 에이든이 하수도에서 안 나오고 에필로그 한 줄로 생존 | 하수도 중반 선택 장면: 묶인 에이든 발견(문양 파괴 전/후), 짧은 대면·키워드 회수. 못 만나면 현재 에필로그 유지 |

## 3. (c) 자유도

| ID | 우선 | 비용 | 현재 제약 | 제안 |
|---|---|---|---|---|
| C-1 | P1 ✅완료(11-27, 해석 Q-P1-5) | M(코드) | 일차당 방문 2곳(낮·저녁), 엘라 집 데드라인, 2일차 낮 강제 이벤트 | 강제 이벤트 발동을 `time>=6 && (flag:aidenFirstDialog \|\| time>=7)` 로 완화(코드: `PotalPoint.cs:61`, `MapManager.cs:498`). 에이든을 놓친 채 2일차 낮이면 이벤트 인트로에 "He is still in the bedroom" 변형 없이 현행 유지. 대안: 아침(t=5)에 이동하면 방문 순서 선택 가능하도록 1일 4칸으로 확장(L) |
| C-2 | P1 | M | 하수도 진입이 소장·침입 판정 위주 | **사회 경로 2개 신설**: ① 메이브(Bar 101-102 "내가 하수도 밀수로를 썼다") 키워드 `Manhole`·`Way to Escape` → 옛 밀수로 열쇠·약도(언변·호감 판정 없이, 대가는 "인스머스 술 한 모금" SAN 또는 알버트 숨기기). ② 알버트(PoliceOffice 62) 키워드 `Manhole` → 경찰 도면(생존·`!albertDeathKeyword` 일 때만). 새 Topic: `Maeve/Old route`, `Albert/Blueprint`, 새 키워드 이름 `Old route`·`Blueprint`는 새 키 |
| C-3 | P1 | M | 금고: 힘/언변/정신분석 | 4번째: **엘라를 올려 보내 설득**(비판정, 시간 대가: 엘라 귀가 대사에서 "내가 올라가 볼게요" 선택지 추가, 키워드 `Ella Triss/Aiden's Safe` 사용 후). 5번째: 행운 소모(CocDesign #4)로 실패한 힘·언변 판정을 성공 전환 |
| C-4 | P1 ✅완료(11-27, 문구 Q-P1-5) | M(코드) | 시신 입장 설득 실패면 엿듣기만 | 비판정: 알버트 통화 후 `Albert's Information` 키워드가 있으면 문경찰 "알버트가 보냈다" 선택지(NoonEvent 96-113 `when flag:intelMissingPeople2`) |
| C-5 | P2 | M | 의뢰 수락/거절 차이가 에필로그 한 줄 | 거절 시 2일차 밤 정리(254-290)에 `!ifGotRequset` 변형: "She came to me and I sent her away." 죄책감 대사. 수락 시 엘라 피살 직전 장면에서 "I'll check on her tomorrow" 회상. 한 줄씩이라도 반응 |
| C-6 | P2 | S | 팔찌 매각($10) 선택에 결과 없음 | 팔찌 매각 시 에필로그에서 에이든이 "Ella's bracelet... did you see it?" 한 줄(`flag:braceletSold`). 안 팔고 가지고 있다 돌려주면 감사·SAN 소량 회복 |
| C-7 | P2 | M | 인스머스·딥원이 일지 읽기 전엔 연결 불가 | 신규 키워드 `Innsmouth liquor`(Bar 105-108, 127-134): 메이브 → 케인(샘플 분석: 비늘 조각) → 앨런. 일지 없이도 "인스머스 = 딥원" 추리 가능(`Keyword.json` 신규 토픽, CSV 신규 `1st_Innsmouth.csv` 또는 기존 파일 끝 ID 추가) |

선택이 남게 하는 요약: 의뢰 수락, 팔찌, 약속(알버트 생존), 키워드 누설(스웨인·데이브·알버트), 문양 선택, 살상(체포). 신규 에필로그 변주는 `InSewer.json` `Ending_*` 케이스를 앞에 추가하는 방식(기존 노드 보존).

### 3-1. 키워드 누설 사망의 예고(P1 M, B 계열이지만 자유도 핵심)

현재: 특정 키워드(스웨인 `Manhole`·`Aiden's warning`·`Witness`·`Culprit`, 알버트 `Root of problem` 등, 데이브 금고 전달)가 밤에 NPC 를 소멸시키나 사전 암시가 약하다(스웨인만 DailyNews:153 "If I do disappear..."). 제안:
1. 해당 키워드 답변 끝에 파비안 독백(불길함 1줄): "Something about the way he looked at the door... I shouldn't have said that."
2. 수첩 키워드에 위험 표식(⚠) — 새 UI 불필요, 키워드 이름 앞 문자만. 표식은 한 번 사망을 겪은 뒤 해금(재시도 방지가 아니라 학습용).
3. 사망 시점 아침 전화·신문 한 줄("Missing: Berkeley Swain")로 인지시킴.
4. 결과는 엔딩 변주에 이미 반영됨(알버트 148-153, 스웨인 162-163). 데이브는 B-6.
의도(비밀 누설 → 보복)를 약화시키지 않는 선에서 사전 경고만 더한다.

## 4. (d) SAN·광기 연출 — 장면 단위

### 4-1. 구조(CocDesign §4·Q-C1·Q-C6 와 정합)

| 상태 | 진입 | 해소 | 대사·연출 변형 |
|---|---|---|---|
| M0 안정 | 기본 | - | 현행 |
| M1 불안(경미) | 오늘 SAN 손실 ≥ 1 | 다음 시간대 경과 | 환청 1줄, 수첩 정상 |
| M2 일시 광기 | 한 번에 5 이상 손실 + INT 판정 실패 | 하루 단위(`timeCount` 4칸 후). 병원 진정제·메이브 술로 앞당김 | 환청·거짓 선택지·시각 왜곡 |
| M3 무기한 광기 | 하루 누적 손실 ≥ 현재 SAN 의 1/5 | 앨런 치료 판정·엘라 유품·악몽이 끝날 때까지(엔딩) | 수첩 왜곡, 낮잠 강제(1시간대 소모), 환각 NPC |

- 새 카운터(추가만): `sanToday`(오늘 SAN 손실 합, 밤→아침에 0), `madStage`(0~3), `madUntil`(`timeCount` 목표 시점). 새 플래그: `madTemp`, `madIndef`. 기존 ES3 키는 건드리지 않고 `FlagKeys.cs`/`CounterKeys` 에 추가만(`Core/State`).
- 판정 hook: `SanCheck` 결과에서 손실량을 `sanToday` 에 가산(`add:sanToday,N` Effect) 후 `madStage` 계산을 순수 C# `MadnessRules`(신규, `Core/Rules`)에 둔다. 기존 영구 디버프 `debuff` 룰렛은 M3 효과로 대체/연결(Q-C1).
- 대사 변형은 **Topic 의 `cases` 앞에 `when: counter:madStage>=N` 케이스를 추가**해 기존 노드를 건드리지 않는다(`StoryResolver` 가 위에서 아래로 첫 일치 선택, `ConditionParser` 가 `counter:x>=n` 지원). 거짓 선택지는 `StoryNode.Choices[].When` 으로, 환청 삽입은 `OnEnter`+`Next` 체인으로.
- 새 대사는 **새 CSV 파일**(예 `Madness.csv`, ID 1~)을 만들고 `TextKey = "Madness:1-4"` 로 참조한다(기존 CSV·ID 불변). 파일 로드는 `StoryScriptLoader` 가 TextKey 의 CSV 이름으로 `Resources.Load` 하는지 확인 후(미확인) 이름을 `DialogDatabaseManager` 하드코딩에 추가해야 하는지 결정.

### 4-2. 연출 수단(UI·소리 위주, 그래픽은 사용자 검토 후 적용)

| 수단 | 구현 단서 | 단계 |
|---|---|---|
| 글자 떨림·지그재그 | Text Animator 태그(`<shake>`,`<wiggle>`)를 새 CSV 에 직접 | M1~M3 |
| 화면 색조·비네팅 | 기존 `LightColorController.time`(밤 0.9) 활용 + 청록 오버레이 CanvasGroup, 투명도 0.05~0.3 | M2~M3 |
| 소리 | `SoundManager` 에 물소리 루프·심장박동, 저역 필터는 믹서 그룹 | M1~ |
| 수첩 왜곡 | `KeyWordManager`(K7 에서 제거) 표시 이름만 치환("Aiden's warning"→"Welcome home" 1개), 클릭 시 `Symbol/*` 처럼 무반응 | M3 |
| 거짓 선택지 | 대화 선택지 중 첫 줄에 번지는 색 글씨 1개, 선택 시 "손이 움직이지 않는다" 1줄 후 원래 선택지로 | M2~ |
| 환각 NPC | 엘라가 사무소·집에 서 있음(Sprite 변경), 말하면 "Run away." 한 줄 후 사라짐(밤마다 1회) | M3 |
| 낮잠 | 장소 도착 후 `timeCount` +1 강제(시간 대가), 악몽 짧은 장면 | M3 |

### 4-3. 장면별 배치

| ID | 우선 | 비용 | 장면(근거) | 단계 | 연출 내용 |
|---|---|---|---|---|---|
| D-1 | P1 | M | 1일차 밤 악몽 후 사무소(DetectiveOffice 223-253) | M0→M1 | 물컵에 물결, 소금 맛 1줄. SAN 판정 성공·실패 모두 `sanToday` 기록 |
| D-2 | P1 | M | 2일차 낮 엘라 시신(NoonEvent 114-121) | M1 이상 | 시신 얼굴이 에이든 얼굴로 겹쳐 보임(OnEnter 변형 1줄). 이후 경찰 대사 "You okay^ Fabian? You went pale." |
| D-3 | P1 | M | 2일차 밤 정리(DetectiveOffice 254-290) | M2 | 수첩 키워드에 가짜 항목 "Welcome home" 추가(클릭 무반응), 정리 대사 중 한 줄이 파비안 자신의 이름으로 바뀜: `Missing Fabian... who?` |
| D-4 | P2 | M | 병원(Hospital 15-20, 46-85) | M1~M3 | 앨런이 환자 목록에 파비안 이름을 봄 / 진정제 처방(해소: M2→M1, 대가 `timeCount`+1 또는 "drowsy" 컨디션 하락) |
| D-5 | P2 | S | 바(Bar 183-199 술 판정, 210 "alcohol is best") | M2 | 메이브 술로 일시 광기 해소(단 무기한은 해소 안 됨). 인스머스 해초주 선택 시 SAN 판정·악몽 플래시백(이미 192) |
| D-6 | P2 | M | 강변 하수도 관찰(Riverside 15-28) | M2 | 관찰 실패 시에도 눈이 보이는 환각(실제가 아니라 시간 소모), 25 "when I woke up nothing" 변형: "There were more of them." |
| D-7 | P2 | S | 스웨인·알버트 대화 중 | M2 | 해당 NPC 대사 도중 `<shake>` 한 줄: "...Welcome home." 이후 NPC "What did you say?" |
| D-8 | P1 | M | 하수도 일지·마도서(InSewer 19-53) | M1~M3 | 일지 34 "(Aiden triss)" 자리에 한 박자 "(Fabian Tyler)" 로 흔들린 뒤 원문으로. 마도서 판정 실패 시 "I read the wrong thing"(51) 대신 글자가 수첩 필체로 보임 |
| D-9 | P1 | M | 문양 앞(InSewer 54-77) | M2~M3 | 거짓 선택지 "Cut your hand" 가 첫 항목(선택하면 `bloodEnding` 이 아니라 SAN 재판정). 현재 피의 엔딩 진입(SAN 실패)과 같은 의미를 선택지로 가시화 |
| D-10 | P1 | L | 사다리 엔딩 SAN 9연타(InSewer 112-125, `Judge.json:391-428`) | M3 | 단계마다 화면 색조 진해짐, 118-125 대괄호 대사 중 한 줄이 파비안의 필체("MY HOME") |
| D-11 | P2 | S | 4일차 밤 악몽(DetectiveOffice 343-361) | M3 | 엔딩 359 `[Welcome home.]` 직전, 낮에 쓰던 대사들이 거꾸로 들림(과거 대사 3개 인용) |

### 4-4. 환청 대사 예시(새 CSV `Madness.csv` 초안, 영문 + 번역)

| ID(안) | 단계 | 대사 | 의미 |
|---|---|---|---|
| 1 | M1 | `(A drip... somewhere in the walls. I haven't left a tap on.)` | 물소리 |
| 2 | M1 | `(For a moment the coffee tasted like brine.)` | 입안의 소금 |
| 3 | M2 | `@...Fabian...@` (이름 호명, 화자 없음) | 환청 |
| 4 | M2 | `(Albert's eyes are very large. He blinks. They are normal again.)` | 환시 |
| 5 | M2 | `'Run away.' — That was Ella's voice. Ella is dead.` | 환청 |
| 6 | M3 | `[Welcome home.]` | 악몽 인용 |
| 7 | M3 | `(The notebook has a word I don't remember writing.)` | 수첩 왜곡 |
| 8 | M3 | `(I closed my eyes for a second. It was already evening.)` | 낮잠 |
| 9 | 해소 | `(The water in the glass is only water.)` | 일시 광기 종료 |

### 4-5. 규칙과 연결·함정

- 광기 상태는 진행을 막지 않는다. CocDesign §5 가 지적한 Lv3(난독=수첩 불가, 인지장애=이동 무작위, 밤공포)는 **막힘이 되므로 M3 에서 쓰지 않고** 위 표의 "연출 위주"로 바꾼다(Q-C1 에 따라 디버프와 중복되면 한쪽을 선택, Q13 참고).
- 거짓 선택지·환각은 **진행을 바꾸지 않는다**(Q12 가정). 환각에서 얻은 키워드가 수첩에 남으면 이후 NPC 반응이 어긋나므로 환각은 키워드를 주지 않는다.
- 세이브: 새 카운터·플래그는 `SaveCodec`·`SaveMigrator` 에 추가만 하고 구세이브에서는 0/false 로 읽는다(`Core/Save`). 구세이브 키 이름 변경 금지.

## 5. 데이터 주도 구조에 얹는 방법

1. **텍스트**: 새 줄은 새 CSV(또는 기존 CSV 끝 ID 추가)에. 기존 ID·행 순서 변경 금지(JSON `TextKey` 범위가 ID 에 묶임). ID≠인덱스 규약(이어지는 줄)과 `^`(쉼표)·`@`(따옴표) 치환을 지킨다.
2. **분기**: `1st_*.json` 의 해당 `topics[].cases[]` **맨 앞**에 새 `when` 케이스를 추가(기존 케이스는 뒤에 그대로 남는다). 새 노드는 `nodes` 에 추가. `pre` 효과로 `set:`/`clear:`/`add:`/`setc:`.
3. **키워드 반응**: `Keyword.json` 에 `{ "id": "인물/키워드", ... }` 토픽 추가. 같은 키워드가 NPC 마다 다른 답을 주도록 새 노드를 분리(A-16).
4. **현지화(I2)**: 코드가 I2 를 쓰지 않으므로(DialogAudit §9) 영향 없음. 장차 CSV 언어 열 추가 방식(`대사_ko`)이라면 새 CSV 는 `대사_en`,`대사_ko` 열을 함께 둔다.
5. **순수 C#**: `MadnessRules`(SAN 누적→상태), `StoryResolver` 조건 확장은 `Core/Rules`·`Core/Story` 에 두고 EditMode 테스트로 분포·경계 검증.

## 6. 검증 시나리오(프로브)

| # | 무엇을 | 기대 |
|---|---|---|
| V-1 | A-1: 1일차 대학교 학생에게 `Content of a Nightmare` | 학생이 악몽 이야기를 한다(케인 첫 인사 아님). 점액 키워드: 케인이 점액 설명 |
| V-2 | A-3: 의뢰 수락 + 실종 키워드 없이 2일차 낮 전화 | 알버트가 엘라를 물은 다음 파비안이 "why ask?" |
| V-3 | B-1: 1일차에 엘라 집을 안 가고 사무소 복귀 | 밤 정리에 에이든 미방문 힌트. 2일차 아침 힌트 재표시 |
| V-4 | B-2: 창문·사물함·소장 전부 실패 → 3일차 아침 | 구제 노드로 도면 또는 열쇠 확보, 하수도 진입 가능 |
| V-5 | B-3: 2일차·3일차 밤 | 남은 밤 수·15일 안내 대사 |
| V-6 | C-2: 메이브에 `Manhole`/`Way to Escape` | 비판정으로 옛 밀수로 열쇠(스웨인 생존 등 조건 불문) |
| V-7 | 3-1: 스웨인에게 `The Culprit` | 독백 불길함, 다음 아침 신문 한 줄, 에필로그 162-163 유지 |
| V-8 | D: SAN 판정 연속 실패 → 하루 손실 ≥ 현재 SAN/5 | `madStage=3`, 수첩 가짜 키워드, 다음 장면 환각 노드 |
| V-9 | D: 구세이브 로드 | 새 카운터 0, 크래시 없음, 기존 키 보존 |
| V-10 | 기존 엔딩 A·B·C·D 경로 | 모든 엔딩 변주 대사가 기존과 같음(회귀) |

## 7. 구현 순서 제안

1. **P0 묶음(대사·JSON 수정 위주)**: A-1~A-5, B-1, B-3(대사), B-2 의 구제 노드. 코드 변경 최소. 프로브 V-1~V-5.
2. P1 설정·표기 통일: A-6~A-12, A-16, B-4~B-7.
3. 자유도 C-1~C-4(코드·씬 이벤트 1~2곳), C-2 는 새 키워드·토픽.
4. 광기(D): CocDesign #7 설계 완료 후 `MadnessRules`·새 CSV. D-1·D-3·D-9 부터(스토리 임팩트 큼), 나머지는 점진.
5. P2 문장 다듬기는 한꺼번에(스크립트화: `Tools/dialog_lint.py` 확장).
