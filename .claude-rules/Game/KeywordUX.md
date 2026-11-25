# KEYWORD-1 키워드 시스템 사용성 개선 설계

> 2026-11-21 designer. 사용자 원문: "키워드시스템이 좀 쓰기 어려워보이거든 이거 개선도 부탁해". 코드는 아직 안 고쳤다. 줄 번호는 11-21 기준.
> 씬(Main.unity) 안 UI 배치는 통독 금지라 일부 "추정"으로 표시. 구현 전 unity-mcp 로 확인할 것.

## 1. 현행 실측

### 1-1. 데이터 흐름
| 단계 | 내용 | 근거 |
|---|---|---|
| 획득 | 키워드 = 수첩 정보 플래그. `intelXxxN`(예 `intelPlanetaryParade1~6`)이 true 가 되면 `KeyWordManager` 가 상위 리스트(사건/장소/인물)와 하위 리스트에 문자열을 `Add` | `KeyWordManager.cs:816`(KeywordUpdate, 상위), `:1198`(DownKeywordUpdater, 하위 ~470줄) |
| 보관 | 하위 리스트가 주제마다 하나씩, 필드 약 30개(`ellaList`, `aidenList`, `sewerList` …). 각 `xxxKeywordNum`(현재 인덱스) | `KeyWordManager.cs:775~815`, `:177~475` |
| 갱신 | `Update()` 매 프레임 `DownKeywordUpdater()`+`KeywordUpdate()` 가 전 플래그를 검사 | `KeyWordManager.cs:1672~1675` |
| 선택 상태 | 정적 문자열 3개 `keywordMain`(Event/Character/Place)·`keywordUpper`·`keywordDowner` | `DatabaseManager.cs:263~265` |
| 사용 가능 여부 | NPC 오브젝트(`Intel_ObtoUI`)의 `canKeyword`(WorldRules 가 켬) + 남은 질문 횟수 `keywordCounter` | `Intel_ObtoUI.cs:10,96~112`, `WorldRules.cs:60` |
| 응답 | `KeywordDialog(npc)` → `StoryDatabase.TryRunKeyword(npc, keyword)`: `Keyword.json` 토픽 `"NPC/키워드"`, 없으면 `"NPC/*"` 폴백 | `InteractionController.cs:103`, `StoryDatabase.cs:97` |

`Keyword.json`: 토픽 159개, NPC 12명 각자 `/*` 폴백 1개(`Ella Triss`, `Aiden Triss`, `Berkeley Swain`, `University student`, `Kane`, `Albert Bradley`, `Kate Lizzie`, `Alan Parks`, `Maeve`, `Dusty Dave`, `BlackWell`, `Symbol`).

### 1-2. 플레이어 실제 조작 순서 (NPC 에게 키워드 하나 제시)
1. NPC 근처로 가서 상호작용(`GameInput.InteractDown`) → 첫 대화(`activeDilaog`). `Intel_ObtoUI.cs:57~112`.
2. 대화가 끝나면 하단 선택 행 `End / Look / Judge / Keyword` 가 뜬다(`End_Look_Judge.cs`). 선택 상태는 각 `*Back` 이미지 알파 0/1 로 표현(`DOFade(…,0.00001f)`).
3. `PageNext/PagePrev`(`End_Look_Judge.cs:92,162`)를 눌러 탭을 순환해 Keyword 에 도달. 탭 순서가 Look→Judge→Keyword→End 이고 조건(`isJudge`/`isKeyword`)에 따라 건너뛰기 분기가 8곳 흩어져 있다.
4. Keyword 탭에 도달하면 `KeyWordManager` 의 3단 선택기가 보인다. 값을 바꾸는 메서드 `MainLeft/Right`, `UpperLeft/Right`, `DownerLeft/Right` 6개(`KeyWordManager.cs:43~477`)는 **UI 버튼 onClick 으로만 호출**된다. 코드에서 `GameInput` 호출 0건(grep). 즉 **키보드·패드로는 값을 못 바꾼다**.
5. 마우스로 Main(사건/인물/장소) → Upper(예 `PlanetaryParade`) → Downer(예 `Contents of a Newspaper`) 순서로 ◀▶ 를 눌러 원하는 항목까지 순환.
6. `Interact` 로 제시 확정. `Intel_ObtoUI.cs:119~125`: `keywordCounter -= 1` 후 `KeywordDialog`.
7. 대사 종료 → 2번으로 복귀. 다른 키워드를 또 내려면 3~6 반복.

대안 경로: 수첩(IntelUI)에 키워드 이름(`Upper_Detail`) 단추가 있고 `KeywordButton.SeparateString()`(`KeywordButton.cs:11`)이 선택 3값을 직접 채운다(프리팹 연결은 추정, 미확인). 이 경로도 마우스 전용이고 대화 중 수첩을 열 수 있는지 불명.

조작 수 추정: 인물 키워드 1개 제시 = 상호작용 1 + 탭 이동 1~3 + ◀▶ 클릭 3~12 + 확정 1 = **6~17회**, 그중 4~12회가 마우스 전용.

### 1-3. 어려운 점 (근거)
| # | 문제 | 근거 | 영향 |
|---|---|---|---|
| P1 | **패드·키보드 불가**: 6개 선택 메서드가 마우스 버튼 전용. 탭 이동만 `PageNext` 로 되어 키보드로 키워드 탭까지 가고서 멈춤 | `KeyWordManager.cs` 전체 GameInput 0건, `End_Look_Judge.cs:92` | 필수 위반(3경로 원칙, 큐 INPUT-1 취지) |
| P2 | **3단 순환 선택**: 목록을 한눈에 못 보고 한 번에 하나만 보임. 항목 N개면 평균 N/2 클릭. 주제(Upper)·세부(Downer) 이중 구조라 "무슨 키워드가 있나" 파악 불가 | `KeyWordManager.cs:102~477` | 발견성·단계 수 |
| P3 | **횟수 제한이 숨겨짐**: NPC 별 `keywordCounter`(3~5, 시간 5 시점에 초기화)가 줄지만 표시 코드 없음(`Intel_ObtoUI` 외 참조 없음). 질문이 갑자기 안 열림 | `Intel_ObtoUI.cs:104,122,738~787` | 피드백 없음 |
| P4 | **틀린 제시도 횟수를 깎음**: `keywordCounter -= 1` 이 응답 판별 전. 폴백 `/*` 대사가 나와도 소모. 힌트 없이 찍기 → 제한 소진 | `Intel_ObtoUI.cs:122` | 틀렸을 때 패널티가 크고 이유가 안 보임 |
| P5 | **틀렸을 때 반응이 빈약**: NPC 당 폴백 1개(`ellaKeyDisable` 등). Aiden 만 1회 차이(`aidenKeyFail1/2`). "이 사람에겐 의미 없음"과 "지금은 말 못 함"이 구분 안 됨 | `Keyword.json:14,26,58` | 반응 다양성 |
| P6 | **새 키워드 알림 없음**: 수첩 플래그가 켜지면 리스트에 조용히 Add. 획득 토스트·수첩 배지 코드 없음 | `KeyWordManager.cs:828~` | 발견성 |
| P7 | **내부 식별자가 그대로 표시**: `mainSetting/upperSetting/downerSetting.text` 에 `PlanetaryParade`, `Contents of a Newspaper` 같은 영문 키가 노출(현지화 키 없음) | `KeyWordManager.cs:818~820` | 가독성 |
| P8 | **빈 목록 예외 위험**: 사건 리스트가 비어 있을 때만 건너뛰는 가드가 있고, 장소·인물 빈 상태에서 `upperPlaceKeywordList[nowPlace…]` 인덱스 접근 가드 없음 | `KeyWordManager.cs:55,80,98` | 초반 진행 중 예외 가능(미재현) |
| P9 | 프레임당 약 100개 `GetFlag` 검사·30개 리스트 Count 재계산 | `KeyWordManager.cs:1672` | 성능(작지만 낭비) |
| P10 | 1700줄 단일 클래스, 비슷한 if 블록 복붙(`KeywordButton.cs` 250줄도 같은 패턴). 키워드 추가 = 코드 수정 | 두 파일 | 확장성 |

## 2. 장르 관례 정리 (참고, 코드 아님)
- **역전재판 증거 제시**: 증언 중 언제든 "제시"(전용 버튼), 증거 목록은 한 줄 가로 아이콘 스크롤, 오답은 감점 + 즉시 반응. 정답 힌트는 "무엇을 제시할지"가 아니라 "증언의 모순"으로 준다.
- **대화형 추리(예: 키워드 질문형 어드벤처)**: NPC 별 질문 목록을 한 화면 리스트로 보여주고, 이미 물은 항목은 색을 바꾼다. 새 항목에 `NEW` 표시.
- **수첩 연동**: 수첩 항목에서 "이 단서로 묻기" 가 가능하고, 대화 중 수첩을 열어도 대화가 끊기지 않는다.
- 공통: 오답이 무조건 벌이 아니라 "정보 한 조각"이 된다.

## 3. 개선 설계

### 3-1. 목표 요약
| 항목 | 현재 | 목표 |
|---|---|---|
| 조작 횟수(키워드 1개) | 6~17 | 3~5 (단축키 사용 시 2~3) |
| 입력 경로 | 마우스(+탭만 키/패드) | 마우스·키보드·패드 3경로 모두 완결 |
| 목록 | 3단 순환, 1개씩 | 한 화면 목록(카테고리 탭 3개 + 스크롤 목록) |
| 횟수 제한 | 숨김, 오답도 소모 | 남은 질문 N 표시, 오답(폴백 응답)은 환불 |
| 새 키워드 | 무음 | 획득 토스트 + 수첩 배지 + 목록 `NEW` |
| 오답 반응 | NPC 당 1개 | 3유형 반응 + 이미 물은 항목 표시 |
| 수첩 | 별개 | 수첩 항목에서 "질문 키워드로 선택" |

### 3-2. 데이터: KeywordCatalog (K1)
`Assets/Resources/Story/KeywordCatalog.json`(새 파일, 기존 `Keyword.json` 대사 데이터는 그대로).
| 필드 | 타입 | 설명 |
|---|---|---|
| `id` | string | 기존 하위 키워드 문자열 그대로(예 `Contents of a Newspaper`). 토픽 `"NPC/id"` 와 일치해야 하므로 **절대 변경 금지** |
| `category` | enum | `Event` / `Character` / `Place` (기존 `keywordMain` 값 그대로) |
| `upper` | string | 기존 Upper 키(`PlanetaryParade`, `Ella Triss` …) |
| `unlockFlag` | string | 켜지면 키워드가 생기는 `FlagKeys` 이름(예 `intelPlanetaryParade1`). 기존 `DownKeywordUpdater` 의 조건을 그대로 이관 |
| `displayKey` | string | 새 I2 용어 키(`Keyword/<id 슬러그>`). 없으면 `id` 를 그대로 표시. 기존 I2 키는 건드리지 않고 **추가만**(I2 는 코드 미사용 상태이므로 우선 미사용·폴백 `id` 허용) |
| `order` | int | 목록 표시 순서(기존 Add 순서 보존) |

생성: `DownKeywordUpdater`(`KeyWordManager.cs:1198~1670`)를 읽는 에디터 도구로 초안을 뽑는다(수작업 복사 금지, 오타 위험). 동치 시험: 플래그 프리셋 N개에서 "옛 리스트 == 새 카탈로그 필터 결과"를 EditMode 테스트로 고정.

`SCoW.Core` 에 순수 C#: `KeywordCatalog.Entries`, `KeywordBook.GetUnlocked(GameState)`(→ `unlockFlag` 평가), `KeywordBook.Reaction(string npc, string id)`(→ `Specific`/`Fallback`, `StoryScript.HasTopic` 이용).

새 상태(세이브 키는 **추가만**): `GameState` 에 `SeenKeywords`(HashSet<string>, ES3 키 `keywordSeen`)·`AskedKeywords`(`npc|id` 집합, ES3 키 `keywordAsked`). 현재 코드에 ES3 호출은 없으나(GameOverview 4-1) 추후 SaveService 도입 시 이 이름으로 고정.

### 3-3. 선택 UI: KeywordPicker (K2)
`Assets/Script/UI/KeywordPicker.cs`(View, 상태 비소유). `UiStack` 패널로 등록(`Core/Ui/UiStack.cs`, 닫기는 `CancelDown` 맨 위 패널만).
- 레이아웃: 좌측 탭 3개(사건·인물·장소) + 우측 세로 스크롤 목록. 항목 한 줄 = `[NEW] 이름` + 오른쪽 마커.
- 마커: `●` 이 NPC 가 이미 물어봄(회색), `◆` 이 NPC 에게 고유 토픽 있음 & 아직 안 물음(힌트, 설정으로 끄기 가능), 없음 = 일반.
- 상단: "남은 질문 N" (`keywordCounter`), NPC 이름.
- 하단 안내: 현재 입력 장치에 맞는 키 아이콘/글자(`GameInput` 쪽에서 장치 감지 필요, 최소 글자 안내 "↑↓ 선택 / ←→ 탭 / Interact 제시 / Cancel 닫기").
- 입력 3경로(전부 기존 `GameInput` 액션 재사용, 새 액션 불필요):
  | 동작 | 마우스 | 키보드 | 패드 |
  |---|---|---|---|
  | 항목 이동 | 호버 | `NavUp/NavDown` | 십자·왼스틱(`NavUp/NavDown` 에 이미 바인딩 추정, 확인) |
  | 탭 전환 | 탭 클릭 | `PageNext/PagePrev` | LB/RB(추정, 확인) |
  | 제시 | 항목 클릭 | `Interact` | A(확인) |
  | 닫기 | 바깥/닫기 버튼 | `Cancel` | B |
  | 수첩 열기 | 수첩 버튼 | `OpenIntel` | (기존 바인딩) |
- 탭 순환 규칙: 비어 있는 탭은 건너뜀(옛 `upperEventKeywordList.Count != 0` 분기 일반화). 빈 상태면 "아직 단서가 없다" 문구를 보이고 예외 없음(P8 해소).
- 호출 경로: 대화 선택 행의 Keyword 탭을 확정하면 Picker 가 열림(기존 선택 행은 유지, 안의 3단 선택기만 제거). 닫으면 선택 행으로 복귀.
- 기존 `KeywordButton`/`KeyWordManager` 6개 메서드는 **K7 에서 제거할 때까지 비활성**(씬 직렬화 호환용으로 껍데기만 남기고 호출을 끊는다).

### 3-4. 횟수·오답·반응 (K3)
- 소모 규칙: 고유 토픽(`Specific`) 응답일 때만 `keywordCounter` 감소. 폴백(`Fallback`)은 환불(감소하지 않음)하고 "아무 반응이 없다" 연출. 단, 이미 물은 `AskedKeywords` 를 다시 내도 감소 안 함.
- 새 순서: `Intel_ObtoUI.cs:122` 의 선차감을 제거하고 `KeywordBook.Reaction` 결과로 차감 결정. `KeywordDialog` 의 `StoryDatabase.TryRunKeyword` 가 폴백을 탔는지 알려주는 `bool` 반환 추가(시그니처 변경은 `out bool usedFallback`).
- 반응 유형 3종(폴백 대사를 NPC 마다 1→3개로 확장, `Keyword.json` 의 `NPC/*` 를 `"cases"` 조건으로 분기: 같은 NPC 의 연속 오답 횟수 카운터 `kwWrongStreak:<npc>`):
  1. 모름: "그건 잘 모르겠네요." 계열
  2. 말하기 곤란: 관련은 있으나 아직 못 말함(호감·시간 조건 미충족) → 힌트 대사
  3. 짜증: 연속 3회 오답이면 대화 종료 유도(질문 횟수는 환불 유지, 대신 한 번만 경고)
- **대사 문구는 사용자/스토리 담당 작성 필요**(지어내지 않음, 임시 대사는 `TODO` 표기). NPC 12명 × 신규 대사 ≤ 2개 = 최대 24줄.

### 3-5. 새 키워드 알림 (K4)
- `KeywordBook` 의 해금 집합이 늘면(플래그 변화 시점, 폴링 금지: `GameRoot.NotifyTransient`/상태 변경 이벤트 구독) `KeywordToast`: 우상단에 "새 키워드: {이름}" 1.5초(대사·판정 중이면 큐에 쌓았다 `IsBusy` 해제 시 표시).
- 수첩 버튼에 점 배지, 수첩 항목·Picker 목록에 `NEW`, 열면 `SeenKeywords` 에 추가.
- 효과음: 기존 `SoundManager` 클립 중 시스템음 재사용(새 사운드 에셋은 사용자 검토 필요).

### 3-6. 대화 중 바로 제시 (K5)
- 새 `GameInput` 액션 `Present`(키보드 `F`, 패드 Y 가정; `Assets/Resources/Input/*.inputactions` 에 추가). 대화 선택 행이 떠 있을 때(`checker.activeSelf && !isSelect`) 누르면 탭 순환 없이 Picker 직접 열림 → 3~5회 → 2~3회.
- NPC 대화 도중(`DialogManager` 가 줄을 내보내는 중)에는 열지 않음(선택 행 구간만).

### 3-7. 수첩 연동 (K6)
- 수첩 항목(`IntelCharacterDetail`/`IntelEventDetail`/`IntelPlaceDetail`)에 "질문에 사용" 단추(`KeywordCatalog` 에 대응 항목이 있을 때만). Picker 가 열려 있으면 해당 항목을 선택 상태로 반환하고 수첩 닫기, 아니면 "대화 중에만 사용 가능" 안내.
- 반대로 Picker 에서 항목에 `OpenIntel` 키: 해당 수첩 페이지로 점프(수첩 열고 `Cancel` 로 복귀, `UiStack` 순서 유지).
- 수첩 항목 본문에는 손대지 않는다(스토리 담당).

### 3-8. 정리 (K7)
`KeyWordManager`(1700줄)·`KeywordButton`(250줄)을 카탈로그 기반 `KeywordBook`+`KeywordPicker` 로 대체 후 삭제. 씬 오브젝트 참조가 끊기므로 씬 직렬화 확인(unity-traps), `DataBaseManager.keywordMain/Upper/Downer` 정적 3개는 호환 파사드로 남기고 사용처 0이 되면 제거. `Intel_ObtoUI` 의 `keywordCounter` 초기화(738~787 하드코딩 11분기)는 카탈로그와 함께 `NpcKeywordBudget`(NPC → 횟수) 표로 이관(JSON `Resources/Story/KeywordBudget.json`).

## 4. 구현 단계 (builder 단위, 순서대로)

| 단계 | 내용 | 파일 | 비용 | 우선 |
|---|---|---|---|---|
| K1 | 카탈로그 JSON 생성 도구 + `KeywordBook` + 동치 EditMode 테스트 | `Core/Keyword/*`, `Editor/KeywordCatalogExporter.cs`, `Resources/Story/KeywordCatalog.json` | M | P0 |
| K2 | `KeywordPicker`(View) + 3경로 입력 + `UiStack` 등록, 선택 행 Keyword 확정 시 호출, 구 3단 선택기 비활성 | `UI/KeywordPicker.cs`, `End_Look_Judge.cs`, Main.unity UI | L | P0 |
| K3 | 횟수 표시·오답 환불·`usedFallback`·반응 3종(대사는 TODO) | `Intel_ObtoUI.cs`, `InteractionController.cs`, `StoryDatabase.cs`, `Keyword.json` | M | P0 |
| K4 | 새 키워드 토스트·수첩 배지·`NEW` 표시 | `UI/KeywordToast.cs`, `IntelManager` | S | P0 |
| K5 | `Present` 액션 + 선택 행에서 즉시 열기 | `GameInput.cs`, `.inputactions`, `End_Look_Judge.cs` | S | P1 |
| K6 | 수첩 ↔ Picker 양방향 | `IntelUI/*Detail.cs` | M | P1 |
| K7 | `KeyWordManager`·`KeywordButton` 제거, 예산 표 이관 | 위 | M | P1 |

전제: Architecture P-단계 중 P5(UiStack)·P2(GameInput) 완료(둘 다 ✅). 씬 수정(Picker 프리팹 배치)은 K2 에서 한 번에, 같은 Main.unity 를 건드리는 다른 작업(SEWER-1 의 S1~S3)과 동시에 돌리지 않는다.

### 함정
- 토픽 키는 `"NPC/id"` 문자열 일치다. `id` 를 정리·번역하면 159개 토픽이 폴백으로 빠진다. 표시 이름은 `displayKey` 로 분리.
- `Intel_ObtoUI.name`(NPC 식별)이 `Keyword.json` NPC 접두와 같은 문자열이다. 이름 변경 금지.
- `Dusty Dave` 키워드는 대사 전에 `selectionUIManager.EndDialog()` 부수효과가 있다(`InteractionController.cs:105`). 제시 경로를 바꿔도 유지.
- 선택 행은 알파 0.00001 트윈으로 상태를 표현한다. 포커스 상태를 알파 읽기로 판단하는 코드를 새 코드에서 복제하지 말 것(상태는 `nowSelecter` 하나만 읽기).
- `ResetKeyCount`(시간 5 에 한 번)로만 횟수가 채워진다. 시간 5 이전 NPC 의 `keywordCounter` 는 인스펙터 값이라 표시 UI 가 0 으로 보일 수 있음 → K3 에서 초기값 확인.
- 레거시 인코딩 파일 수정 시 인코딩 유지(GameOverview 6절).

## 5. 검증 기준 (프로브)
1. EditMode: 카탈로그 동치 — 플래그 프리셋 12개(획득 0개, 전부, 각 NPC 직전 상태)에서 옛 리스트 == 새 결과.
2. 플레이 모드, 키보드만: NPC 대화 → 선택 행 → Keyword → `NavDown` 으로 항목 → `Interact` 제시 → 해당 NPC 토픽 대사 출력. 마우스 사용 0.
3. 같은 시나리오를 마우스만, 패드(가상 패드)만.
4. 오답 제시: 폴백 대사 + 남은 질문 수 불변. 정답 제시: 수가 1 감소. 0 이 되면 Picker 의 제시 비활성 + 문구.
5. 새 키워드: `intelSewerGhostStory1` 을 켜는 사건 직후 토스트 1회, 수첩 배지, Picker `NEW`. Picker 를 열면 `NEW` 소거(재실행 시 동일).
6. 빈 상태(키워드 0개) Picker 열기 → 예외 없음, 안내 문구.
7. `Cancel` 이 Picker → 선택 행 순서로 한 겹씩만 닫는지(UiStack), 수첩에서 돌아와도 선택 유지.
8. 콘솔 error 0, `Keyword.json` 159 토픽 중 무작위 20개를 제시해 폴백으로 새지 않는지.
9. 조작 횟수 측정: 인물 키워드 1개 제시를 입력 로그로 세어 목표(3~5) 이내.

## 6. K1 결과 (11-21)
- 완료: `Editor/KeywordCatalogExporter.cs`(메뉴 `SCoW/Keyword/Export Catalog`, 옛 `KeyWordManager` 소스를 정규식으로 읽음) → `Resources/Story/KeywordCatalog.json`(상위 27·하위 57·의심 토픽 9). `Core/Keyword/KeywordCatalog.cs`·`KeywordBook.cs`(획득·분류·상위/하위 목록·NPC별 제시 가능·`Reaction`). 골든 `Golden/KeywordBook.golden.txt`(352 프리셋, 도구 `SCoW/Keyword/Dump Golden (Play Mode)`, 옛 매니저를 임시 생성해 덤프) 대조 `Tests/EditMode/KeywordBookTests.cs`. UI·씬 무변경.
- 카탈로그 `suspect`(기록만, 고치지 않음): ① 케인(`Osborn Louis Kane`/`노교수`)·강가(`Riverside`/`미스캐토닉 강`)는 옛 3단이 하위를 다른 이름으로 비교해 옛 UI 로 하위 선택 불가(K2 에서 자연 해소). ② `Keyword.json` 어느 NPC 에도 전용 토픽이 없는 키워드. ③ `suspectTopics`: 대학생·케인 노드 범위 오류(StoryImprove A-1, 스토리 P0 몫).
- 순서: 목록 순서는 옛 코드 검사 순서. 옛 실제 동작은 획득 순서였으므로 K4 의 본 키워드 집합으로 획득 순서를 얹을 것. `displayKey` 는 예약값(I2 미등록, 표시는 `id` 폴백).
- `unlockFlag` 로 `KeywordBook` 은 `GameState` 를 직접 받는다(`IStateReader` 어댑터 불필요).

## 7. K2 결과 (11-21)
- 완료: `UI/KeywordPicker.cs`(코드 생성 오버레이, 씬 무수정, `PanelKind.Keyword`). 선택 행에서 Keyword 를 확정하면 `Intel_ObtoUI` 가 Picker 를 열고, 확정 콜백 `OnKeywordPicked` 가 옛 흐름(차감·`keywordDowner` 설정·`KeywordDialog`)을 그대로 탄다. 입력: 이동 NavUp/Down·호버, 탭 PagePrev/Next·클릭(빈 탭 건너뜀), 제시 Interact·클릭, 닫기 Cancel·바깥 클릭. `*` = 이 NPC 전용 토픽 있음. 빈 상태 문구 "No clues yet.".
- 검증: EditMode 270 통과. 플레이: 키보드(E 열기·S 이동)·가상 패드(A 열기, 십자, RB 탭, B 닫기, A 제시)·마우스(호버·클릭 이벤트)로 Ella 에게 `Contents of a Newspaper` 제시 → 옛 방식과 같은 노드(1st_Client'sHouse:213) 출력, 콘솔 error 0. 캡처 `Logs/playtest/KeywordPicker_*.png`.
- 임시·미해결: 문구·분류 이름은 영문 하드코딩(TODO I2), 그래픽 임시(ART-2). 횟수 0 일 때 제시는 무반응(K3 에서 안내). `●`(이미 물음)은 AskedKeywords(K3) 후. 구 3단 선택기(`BillowUI/GameObject/KeyWord`)는 씬에 그대로 남고 이 경로가 값을 읽지 않는다(씬 직렬화 보호, K7 에서 제거). 가상 키보드는 에디터 비포커스 상태에서 한 번 끊겼다(패드는 정상, 같은 `GameInput` 코드 경로).
- K1 리뷰 권장 반영: 주석 클래스명, 의심 표시 항목 단위(`EntryToList`), 상위 unlockFlags 빈 값 검사, 골든 352 정확 일치, `KeywordBook` null 가드.
cat: /tmp/k3add.txt: No such file or directory

## 8. K3 결과 (11-21)
- 완료: `Core/Keyword/KeywordAsk.cs`(순수: `Decide` 결과 4종 Correct/Unrelated/AlreadyAsked/NoBudget, `Settle` 정산, 물음 기록 = GameState 플래그 `kwAsked:NPC/키워드`, 새 키만 추가). `StoryDatabase.TryRunKeyword(..., out usedFallback)`(대사 없음·맞는 Case 없음도 폴백 취급). `InteractionController.KeywordDialog` 가 usedFallback 을 반환하고, `Intel_ObtoUI.OnKeywordPicked` 는 선차감을 없애고 대사 뒤에 정산한다(Q-K1: 폴백 환불, 전용 응답만 -1). 선택 행 키워드 칸에 `keyword (N)`, Picker 에 `Questions left`·`●`(이미 물음)·횟수 0 / 이미 물음 안내 문구(대사 없이 창 유지). 횟수 0 이어도 키워드 칸이 열려 안내가 나온다(옛: 칸이 숨음).
- 반응 3종 중 대사: 모름 = 기존 `NPC/*` 폴백 그대로, 이미 물음·횟수 0 = Picker 안내 문구(TODO I2 `kw.already_asked`/`kw.no_budget`). 말하기 곤란·짜증(연속 오답)은 Q-K4 대사가 올 때까지 보류.
- 검증: EditMode 278(신규 8) 통과. 플레이(가상 패드): 정답 2→1·● 기록, 오답(Meetings) 2 유지, 이미 물음 안내, 횟수 0 안내(캡처 `Logs/playtest/K3_*.png`). 씬 직렬화 무변경.

## 9. K4 결과 (11-21)
- 완료: `Core/Keyword/KeywordNotice.cs`(순수) + `UI/KeywordToast.cs`(DontDestroyOnLoad 오버레이, 씬 무수정). 새 GameState 키만 추가: 카운터 `kwSeq`·`kwBadge`·`kwOrder:id`(획득 순번), 플래그 `kwSeen:id`·`kwNoticeBase`(기준선). 첫 동기화는 기준선(이미 풀린 키워드는 알림 없이 확인 처리, 구세이브 폭주 방지). `GameRoot.StateChanged` 구독(폴링 없음), 연출 중(`UiGate.IsBusy`)에 얻으면 큐에 쌓았다 끝나면 1.5초 토스트(한 번에 여럿이면 "+N"). 수첩 배지 = `NoteButton`(IntelManager.OpenSetting 를 부르는 버튼)에 코드로 붙인 빨간 원(개수). 수첩을 열면 배지만 끄고(`IntelManager.OpenSetting`), Picker 를 열면 `NEW` 를 한 번 보여 준 뒤 전부 확인 처리. Picker 목록은 획득 순서(K1 메모 처리).
- 검증: EditMode 285(신규 7) 통과. 플레이(Main, 대사 중): 플래그 3개 → 큐 적재, 대사 끝나자 토스트 표시, 배지 3, Picker 에 `NEW` 3개·획득 순서, 다시 열면 `NEW` 소거, 콘솔 error 0(캡처 `Logs/playtest/screenshot-20261121-19113*.png`, `-191139`, `-191146`). 배지 자체는 인트로 검은 화면이라 육안 캡처 못 함(오브젝트 활성·텍스트 3 확인).
- 임시: 문구 영문(TODO I2), 그래픽 임시(ART-2), 효과음 없음(사용자 검토 필요).

## 9. K5 결과 (11-25)
- 완료: `GameInput.PresentDown`(Gameplay 맵 `Present`, 키보드 F·패드 Y — Q-K3 임시 가정). `Intel_ObtoUI.PresentDirect` 가 선택 행(`End_Look_Judge.CanPresentKeyword`)에서 탭 순환 없이 Picker 를 연다. 겹침 처리: F=Portal 은 `PotalPoint` 에 `!KeywordPicker.IsOpen`, Y=OpenIntel 은 `IntelManager` 가 제시 가능한 선택 행에서 Present 우선, Interact 경로에도 `!IsOpen`.
- 검증: EditMode 285, 플레이(가상 패드 Y)로 Picker 직행→A 제시→Ella 뉴스 장면 출력, 횟수 3→2. 캡처 `Logs/playtest/K5_present_*.png`. 강제 상태(인트로 대사 중)라 `DialogManager.cs:462` IndexOutOfRange 1건은 시험 설정 탓으로 추정(미확인).
