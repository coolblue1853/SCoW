# 대사 데이터 점검표 (DialogAudit)

`Tools/dialog_lint.py` 가 자동 생성한 표다(`python -I Tools/dialog_lint.py --report Docs/Playtest/DialogAudit.md --dot`).

## 0. B-1 반영 결과 (작업 직전 실측 252건 -> 후 24건. 이전 기록 268건은 플래그 코드 정리 전 수치)

| 구분 | 전 | 후 | 처리 |
|---|---:|---:|---|
| 오류 | 6 | 0 | 빈 대사 참조 5건은 크래시가 아니라 빈 대사창이라 `미작성 장면 참조`(경고, 스토리 확인)로 재분류, `aidenLookJudge` 1건은 C 트랙 이월(정보) |
| 경고(서식) | 12 | 0 | 깨진 @ 5(SCV 1 포함)·날 쌍따옴표 1·색 표식 짝 4·괄호 1은 CSV 수정, `InSewer.csv:36` 은 검사기 오탐(⒳ 는 괄호 짝 없는 회색 표식)이라 규칙 수정 |
| 경고(중복 문장 211) | 211 | 0 | 같은 장면 연속 중복 0건. 떨어진 반복은 장별 재방문·분기 복제라 의도로 보고 정보(`반복 문장`)로 강등 |
| 경고(빈 칸) | 3 | 1 | `DailyNews:75`, `Item:19` 은 앞뒤가 대사인 한 박자(`연출 빈 줄`, 정보). `Client'sHouse:102~124` 는 미작성 장면으로 남김 |
| 경고(남은 것) | - | 24 | 모두 분류됨: 스토리 확인(6절) = 미작성 장면 4(참조 3: Aiden 심리치료 성공 2·Item OldMap 1, 빈 구간 1: Client'sHouse 102~124)·도달 불가 대사 9·말투 혼용 1, 코드 정리 = 플래그 7·장소 키 3 |

- 오류 6건 중 빈 대사 참조: 원문이 저장소 어디에도 없어(git 이력 2커밋 모두 빈 칸, `SCOW_SCV.csv` 는 탐정 사무소 도입부 한글본이라 해당 장면 없음) 복원 불가 -> 지어내지 않고 6절 `미작성 장면` 목록으로.
- `DailyNews.csv:185` 쉼표 잘림: 현재 검사기가 잡지 않음. 쉼표는 메모 열(4열)이고 파서는 앞 3열만 쓰므로 영향 없음(따옴표 감쌈 불필요).
- **C 트랙 이월:** `InteractionController.aidenLookJudge` 가 씬에서 비어 있음(코드가 `.GetComponent<interactionEvent>()` 호출 -> NullReference). 씬·코드 문제라 대사 CSV 범위 밖.

- 총 문제 **24건** = 오류 0 + 경고 24 (정보 544건은 건수에 넣지 않음)
- 대상: CSV 15개(`Assets/Script/Asset/Resources/`), 씬 이벤트 657개(`Main.unity`), DataBaseManager 플래그 29개, I2Languages

## 1. 대사 구조(코드에서 확인한 사실)

- CSV 열은 `ID, 캐릭터이름, 대사, 메모…`뿐이다. **다음 ID·분기·플래그 조건 열은 없다.** `DialogPaser.Prase` 는 `\n` 으로 줄을 쪼개고 `,` 로 열을 쪼개 앞 3열만 쓴다(따옴표 이스케이프 없음 -> 대사 안 쉼표는 `^`, 따옴표는 `@` 로 쓰고 `DialogManager` 가 출력 때 되돌림).
- ID 칸이 빈 줄은 앞 대사의 **이어지는 줄**이다. 게임 인덱스 = 비어 있지 않은 ID 줄의 등장 순서(1부터). ID 값은 코드가 읽지 않는다.
- 어떤 대사를 보일지는 씬 `Main.unity` 의 `interactionEvent.dialog.line`(인덱스 x~y 범위, 포함)이 정하고, `DialogDatabaseManager.GetDialog` 가 범위를 자른다. **분기·플래그 조건은 전부 `InteractionController.cs`(3200줄) 코드**에 있다. 그래서 '다음 ID 참조·막다른 길' 검사는 '이벤트 범위·미참조 대사·미호출 이벤트' 검사로 바꿔 수행했다.
- 플래그는 `DataBaseManager`(구 old_Dialog) 정적 필드이고, 대사 속 표식(`ⓡ(●1ⓟ…` 등 `DialogManager` 문자 스위치)으로도 켜진다.
- 파일 인코딩은 전부 UTF-8(BOM 없음), 개행 LF. 출시판은 영문 대사이며 한글은 메모 열과 `SCOW_SCV.csv`(한글 원본, 코드 미참조)에 있다.

## 2. 검사 규칙

| 규칙 | 내용 | 등급 |
|---|---|---|
| 인코딩/BOM/개행 | UTF-8 여부, BOM, CR 혼입 | 오류/경고/정보 |
| 헤더·열 개수 | 앞 3열이 ID,캐릭터이름,대사 인지. 열이 3 미만이면 게임이 크래시 | 오류 |
| 쉼표 포함 | 열이 파일 표준보다 많은 줄 = 대사 안 쉼표로 게임이 뒷부분을 잘라 먹음(쉼표는 ^ 로 써야 함) | 경고 |
| ID 규칙 | 중복·불연속·숫자 아님. 이어지는 줄(빈 ID)이 있으면 ID 와 게임 인덱스가 어긋나므로 정보로 표시 | 오류/경고/정보 |
| 빈 필수 칸 | 화자는 있는데 대사가 빈 줄, 이어지는 줄이 빈 줄. 패딩(ID 만 있는 빈 줄)은 건수만 집계 | 오류/경고 |
| 깨진 따옴표·괄호·색 표식 | @ 개수 홀수, 날 " , ( ) 짝, ⓖ(..)ⓦ 짝, ⓦ 가 문장 맨 앞(크래시) | 오류/경고 |
| 한글 잔류 | 영문 CSV 대사에 한글이 남음(번역 누락 후보) | 경고 |
| 오탈자 후보 | 흔한 영어/한글 오타, 단어 반복. 공백·부호 문제는 정보 | 경고/정보 |
| 중복 문장 | 같은 CSV 안에서 같은 화자의 같은 문장(길이 14자 이상) | 경고 |
| 화자 표기·말투 혼용 | 대소문자·공백 차이로 갈린 화자명, 한글 대사의 존댓말/반말 혼재 | 경고 |
| 이벤트 범위(씬) | Main.unity 의 interactionEvent 범위가 CSV 대사 수를 넘는지, 범위 안에 빈 대사가 있는지 | 오류 |
| 도달 불가 대사 | 어떤 이벤트 범위에도 안 들어가는 대사 구간(CSV 에는 다음 ID/분기가 없어서, '도달 불가' = 이벤트가 안 가리키는 대사) | 경고 |
| 미호출 이벤트·빈 참조 필드 | 이벤트 오브젝트를 아무도 참조하지 않음 / InteractionController 필드가 씬에서 비어 있음 | 오류/경고 |
| 코드↔자원 대조 | DialogDatabaseManager 가 부르는 CSV 이름, nowPlace 키 대 실제 파일·매핑 | 오류/경고/정보 |
| 플래그 | DataBaseManager 정적 bool 플래그: 검사만(항상 false) / 설정만 / 미사용 | 경고/정보 |

등급: 오류=게임 크래시·잘못된 참조, 경고=확인·수정 권장, 정보=참고(건수에 안 셈).

## 3. 파일별 건수

| 파일 | 대사 수 | 패딩(빈 줄) | 오류 | 경고 | 정보 |
|---|---:|---:|---:|---:|---:|
| 1st_Bar | 211 | 110 | 0 | 0 | 49 |
| 1st_Battle | 11 | 310 | 0 | 1 | 4 |
| 1st_Client'sHouse | 277 | 44 | 0 | 6 | 38 |
| 1st_DailyNews | 230 | 91 | 0 | 0 | 20 |
| 1st_DetectiveOffice | 361 | 54 | 0 | 1 | 31 |
| 1st_Hospital | 148 | 173 | 0 | 0 | 6 |
| 1st_Item | 27 | 294 | 0 | 2 | 8 |
| 1st_PoliceOffice | 232 | 89 | 0 | 0 | 15 |
| 1st_Riverside | 71 | 250 | 0 | 0 | 23 |
| 1st_Slum | 62 | 259 | 0 | 0 | 2 |
| 1st_University | 74 | 247 | 0 | 1 | 9 |
| 2nd_NoonEvent | 187 | 88 | 0 | 0 | 38 |
| InSewer | 190 | 225 | 0 | 1 | 87 |
| SCOW_SCV | 181 | 140 | 0 | 1 | 5 |
| SewerOffice | 93 | 322 | 0 | 1 | 7 |
| `Assets/Scenes/Main.unity` | - | - | 0 | 0 | 190 |
| `Assets/Script/dialogue/DialogDatabaseManager.cs` | - | - | 0 | 3 | 3 |
| `Assets/Script/dialogue/InteractionController.cs` | - | - | 0 | 0 | 8 |
| `Assets/Script/old_Dialog/DatabaseManager.cs` | - | - | 0 | 7 | 1 |

## 4. 종류별 건수

| 종류 | 오류 | 경고 | 정보 |
|---|---:|---:|---:|
| 도달 불가 대사 | 0 | 9 | 0 |
| 플래그 설정만 | 0 | 4 | 0 |
| 미작성 장면 참조 | 0 | 3 | 0 |
| 장소 키 | 0 | 3 | 3 |
| 플래그 검사만 | 0 | 3 | 0 |
| 미작성 장면 | 0 | 1 | 0 |
| 말투 혼용 | 0 | 1 | 0 |
| 개행 | 0 | 0 | 15 |
| 문장부호 | 0 | 0 | 22 |
| 공백 | 0 | 0 | 7 |
| 반복 문장 | 0 | 0 | 211 |
| 화자 없음 | 0 | 0 | 76 |
| 연출 빈 줄 | 0 | 0 | 2 |
| 미호출 복제본 | 0 | 0 | 190 |
| 범위 공유 | 0 | 0 | 7 |
| 미참조 파일 | 0 | 0 | 1 |
| C트랙 이월 | 0 | 0 | 1 |
| 필드 직렬화 | 0 | 0 | 1 |
| 미사용 필드 | 0 | 0 | 6 |
| 코드 미참조 | 0 | 0 | 1 |
| 플래그 미사용 | 0 | 0 | 1 |

## 5. 문제 목록: 서식·참조·오탈자 등 (스토리 내용 불변, 오류+경고 10건)

| 위치 | 등급 | 종류 | 내용 | 제안 수정 |
|---|---|---|---|---|
| Assets/Script/dialogue/DialogDatabaseManager.cs | 경고 | 장소 키 | 코드에서 nowPlace=' + DataBaseManager.nowPlace);\r\n        }\r\n        if (DataBaseManager.timeCount == 1)\r\n        {\r\n            newspaper.DOFade(1, 0.1f);\r\n        }\r\n        SaveFlow.CompleteRestore();\r\n    }\r\n    void StartGame()\r\n    {\r\n        if (DataBaseManager.isTest == false)\r\n        {\r\n            InteractionController.Instance.Start1stDetectiveOffice(' 를 쓰지만 DialogDatabaseManager.Update 가 처리하지 않음 | 매핑 추가 또는 코드 정리 |
| Assets/Script/dialogue/DialogDatabaseManager.cs | 경고 | 장소 키 | 코드에서 nowPlace='nowPlace' 를 쓰지만 DialogDatabaseManager.Update 가 처리하지 않음 | 매핑 추가 또는 코드 정리 |
| Assets/Script/dialogue/DialogDatabaseManager.cs | 경고 | 장소 키 | nowPlace="Gunshop" 비교가 코드에 있는데 대응 CSV/매핑이 없음(대사 로드 안 됨) | 미사용이면 삭제 / 쓰면 CSV 추가 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | conntectMouse : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | endDialogLine : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 검사만 | isBattleRollet : 읽는 곳 1(SaveFlow.cs) 있으나 true 로 설정하는 곳이 없음(항상 false) | 설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 검사만 | isScene : 읽는 곳 1(SaveFlow.cs) 있으나 true 로 설정하는 곳이 없음(항상 false) | 설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 검사만 | isTest : 읽는 곳 1(DirectingManager.cs) 있으나 true 로 설정하는 곳이 없음(항상 false) | 설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | setIntelCharDetail : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | skipActive : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |

## 6. 스토리 내용을 바꾸는 수정 (사용자 확인용, 14건)

아래는 고치면 **게임에서 보이는 내용·흐름이 달라지는** 항목이다(번역 보강, 대사 삭제·연결, 말투 통일, 이벤트 삭제). 사용자 확인 전에는 건드리지 않는다.

| 위치 | 등급 | 종류 | 내용 | 제안 수정 |
|---|---|---|---|---|
| 1st_Battle.csv:9 | 경고 | 도달 불가 대사 | 인덱스 8~8(1개) 를 호출되는 이벤트가 가리키지 않음. 첫 줄:  'Reduced SAN by 10' | 삭제 또는 이벤트 연결(스토리 결정 필요) |
| 1st_Client'sHouse.csv:17 | 경고 | 도달 불가 대사 | 인덱스 16~17(2개) 를 호출되는 이벤트가 가리키지 않음. 첫 줄: Fabian '(Everything looks ordinary here. Except ' | 삭제 또는 이벤트 연결(스토리 결정 필요) |
| 1st_Client'sHouse.csv:114 | 경고 | 도달 불가 대사 | 인덱스 113~114(2개) 를 호출되는 이벤트가 가리키지 않음. 첫 줄: Fabian '' | 삭제 또는 이벤트 연결(스토리 결정 필요) |
| 1st_Client'sHouse.csv:138 | 경고 | 도달 불가 대사 | 인덱스 137~137(1개) 를 호출되는 이벤트가 가리키지 않음. 첫 줄: Fabian "(It's an ordinary house that could be fo" | 삭제 또는 이벤트 연결(스토리 결정 필요) |
| 1st_DetectiveOffice.csv:363 | 경고 | 도달 불가 대사 | 인덱스 362~415(54개) 를 호출되는 이벤트가 가리키지 않음. 첫 줄: Fabian '' | 삭제 또는 이벤트 연결(스토리 결정 필요) |
| 1st_Item.csv:6 | 경고 | 도달 불가 대사 | 인덱스 5~5(1개) 를 호출되는 이벤트가 가리키지 않음. 첫 줄:  '!Not available in the current version!' | 삭제 또는 이벤트 연결(스토리 결정 필요) |
| 1st_University.csv:25 | 경고 | 도달 불가 대사 | 인덱스 24~42(19개) 를 호출되는 이벤트가 가리키지 않음. 첫 줄: Fabian '"Have you ever had a (nightmare) involvi' | 삭제 또는 이벤트 연결(스토리 결정 필요) |
| InSewer.csv:403 | 경고 | 도달 불가 대사 | 인덱스 402~415(14개) 를 호출되는 이벤트가 가리키지 않음. 첫 줄: Fabian '' | 삭제 또는 이벤트 연결(스토리 결정 필요) |
| SewerOffice.csv:403 | 경고 | 도달 불가 대사 | 인덱스 402~415(14개) 를 호출되는 이벤트가 가리키지 않음. 첫 줄: Fabian '' | 삭제 또는 이벤트 연결(스토리 결정 필요) |
| SCOW_SCV.csv:7 | 경고 | 말투 혼용 | 파비안 의 한글 대사가 존댓말 39 / 반말 19 혼재(상대에 따른 구분이면 정상) | 상대별 말투 규칙 확인 후 통일 |
| 1st_Client'sHouse.csv:102 | 경고 | 미작성 장면 | 인덱스 101~123(23개, 줄 102~124)의 화자는 있는데 대사가 비어 있음(이벤트가 참조하면 빈 대사창) | 대사를 채우거나(미작성 장면) 이벤트 범위에서 제외 |
| 1st_Client'sHouse.csv:102 | 경고 | 미작성 장면 참조 | 1st/Client'sHouse/Aiden_Judge_psychotherapy_Sucsses [101~112] : 참조 범위에 빈 대사 12개(101,102,103,104,105,106) (장: 1st/2nd/3rd) | 대사를 채우거나(미작성 장면) 범위에서 제외 |
| 1st_Client'sHouse.csv:116 | 경고 | 미작성 장면 참조 | 3rd/Client'sHouse/Aiden_Judge_psychotherapy_Sucsses_End [115~123] : 참조 범위에 빈 대사 9개(115,116,117,118,119,120) (장: 1st/2nd/3rd) | 대사를 채우거나(미작성 장면) 범위에서 제외 |
| 1st_Item.csv:29 | 경고 | 미작성 장면 참조 | 3rd/Item/OldMap_Look [28~30] : 참조 범위에 빈 대사 3개(28,29,30) (장: 3rd) | 대사를 채우거나(미작성 장면) 범위에서 제외 |

## 7. 정보(건수에 불포함, 544건)

종류별: 반복 문장 211, 미호출 복제본 190, 화자 없음 76, 문장부호 22, 개행 15, 공백 7, 범위 공유 7, 미사용 필드 6, 장소 키 3, 연출 빈 줄 2, 미참조 파일 1, C트랙 이월 1, 필드 직렬화 1, 코드 미참조 1, 플래그 미사용 1

(544건이라 종류별 대표 3건만 표시. 전체는 `python -I Tools/dialog_lint.py -v`)

| 위치 | 등급 | 종류 | 내용 | 제안 수정 |
|---|---|---|---|---|
| Assets/Script/dialogue/InteractionController.cs | 정보 | C트랙 이월 | InteractionController.aidenLookJudge 가 씬에서 비어 있음(코드가 .GetComponent<interactionEvent>() 호출 -> NullReference). C 트랙(씬·코드 수정)에서 처리 | 씬에서 연결 또는 호출 코드 제거 |
| 1st_Bar.csv:1 | 정보 | 개행 | 전체 CRLF. 코드는 \n 으로만 쪼개 마지막 열 끝에 \r 이 남지만 앞 3열엔 영향 없음 |  |
| 1st_Battle.csv:1 | 정보 | 개행 | 전체 CRLF. 코드는 \n 으로만 쪼개 마지막 열 끝에 \r 이 남지만 앞 3열엔 영향 없음 |  |
| 1st_Client'sHouse.csv:1 | 정보 | 개행 | 전체 CRLF. 코드는 \n 으로만 쪼개 마지막 열 끝에 \r 이 남지만 앞 3열엔 영향 없음 |  |
| 1st_Bar.csv:100 | 정보 | 공백 | 문장부호 앞 공백: '"I don\'t know who put it out , but I want to beat ' | 공백 제거 |
| 1st_Bar.csv:102 | 정보 | 공백 | 문장부호 앞 공백: '"Not at all, I can say this because I\'ve been usin' | 공백 제거 |
| 1st_Client'sHouse.csv:145 | 정보 | 공백 | 연속 공백: '"And the story was that the alligators would eat t' | 공백 하나로 |
| 1st_Bar.csv:15 | 정보 | 문장부호 | 마침표 2개(..): '"I don\'t know when our (Maeve) will be like that..' | ... 로 |
| 1st_Bar.csv:34 | 정보 | 문장부호 | 마침표 2개(..): '"I don\'t know when our (Maeve) will be like that..' | ... 로 |
| 1st_Bar.csv:77 | 정보 | 문장부호 | 마침표 2개(..): '"Wh.. who are you, w..why? What\'s going on..."' | ... 로 |
| Assets/Script/dialogue/InteractionController.cs | 정보 | 미사용 필드 | InteractionController.alanKeyMurderdElla 는 코드에서 한 번도 쓰이지 않음 |  |
| Assets/Script/dialogue/InteractionController.cs | 정보 | 미사용 필드 | InteractionController.alanKeyCauseOfDeath 는 코드에서 한 번도 쓰이지 않음 |  |
| Assets/Script/dialogue/InteractionController.cs | 정보 | 미사용 필드 | InteractionController.alanKeyWebbedFeet 는 코드에서 한 번도 쓰이지 않음 |  |
| SCOW_SCV.csv | 정보 | 미참조 파일 | 코드·씬이 로드하지 않는 CSV(한글 원본 보관용으로 추정) |  |
| Assets/Scenes/Main.unity | 정보 | 미호출 복제본 | 2nd/Client'sHouse/Aiden_Dialog2 [42~43] : 호출되지 않지만 같은 범위를 호출되는 이벤트가 이미 씀(장별 복제본으로 추정) |  |
| Assets/Scenes/Main.unity | 정보 | 미호출 복제본 | 3rd/NoonEvent/NoonEvent_DoorPolice_PersuasionFail [111~113] : 호출되지 않지만 같은 범위를 호출되는 이벤트가 이미 씀(장별 복제본으로 추정) |  |
| Assets/Scenes/Main.unity | 정보 | 미호출 복제본 | 3rd/Battle/Battle_SanCheck [6~7] : 호출되지 않지만 같은 범위를 호출되는 이벤트가 이미 씀(장별 복제본으로 추정) |  |
| 1st_Bar.csv:2 | 정보 | 반복 문장 | 같은 문장이 4회(2,22,143,201): '"who is this, fab. the great detective of arkham! ' |  |
| 1st_Bar.csv:3 | 정보 | 반복 문장 | 같은 문장이 3회(3,23,202): '"(maeve)... you\'re still the same as you\'ve been i' |  |
| 1st_Bar.csv:4 | 정보 | 반복 문장 | 같은 문장이 2회(4,24): '"my only strength. sit down."' |  |
| 1st_Client'sHouse.csv | 정보 | 범위 공유 | 서로 다른 이벤트 Aiden_Judge_Observation_Fail,Aiden_Judge_medicine_Fail,Aiden_Judge_psychotherapy 가 같은 범위 [100~100] 를 가리킴(재사용이면 정상) |  |
| 1st_Client'sHouse.csv | 정보 | 범위 공유 | 서로 다른 이벤트 Ella_fistDialog,NockDoor 가 같은 범위 [2~9] 를 가리킴(재사용이면 정상) |  |
| 1st_Client'sHouse.csv | 정보 | 범위 공유 | 서로 다른 이벤트 Ella_Key_FishySmell1_noGhosty1,shySmell1_noGhosty11_noGhosty1 가 같은 범위 [205~212] 를 가리킴(재사용이면 정상) |  |
| 1st_DailyNews.csv:75 | 정보 | 연출 빈 줄 | 앞뒤가 대사인 외톨이 빈 줄(말없는 한 박자로 간주, 빈 대사창이 한 번 뜸) |  |
| 1st_Item.csv:19 | 정보 | 연출 빈 줄 | 앞뒤가 대사인 외톨이 빈 줄(말없는 한 박자로 간주, 빈 대사창이 한 번 뜸) |  |
| Assets/Script/dialogue/DialogDatabaseManager.cs | 정보 | 장소 키 | 'BattleRoad' 는 Update 에서만 CSV 를 고르고 Awake 초기 로드에는 없음(씬 시작 장소가 아니면 정상) |  |
| Assets/Script/dialogue/DialogDatabaseManager.cs | 정보 | 장소 키 | 'NoonEvent' 는 Update 에서만 CSV 를 고르고 Awake 초기 로드에는 없음(씬 시작 장소가 아니면 정상) |  |
| Assets/Script/dialogue/DialogDatabaseManager.cs | 정보 | 장소 키 | 'SewerOffice' 는 Update 에서만 CSV 를 고르고 Awake 초기 로드에는 없음(씬 시작 장소가 아니면 정상) |  |
| SCOW_SCV.csv | 정보 | 코드 미참조 | DialogDatabaseManager 가 이 이름을 쓰지 않음 |  |
| Assets/Script/old_Dialog/DatabaseManager.cs | 정보 | 플래그 미사용 | isFinish : 선언만 있고 쓰이지 않음 | 삭제 후보(세이브 키면 보존) |
| Assets/Script/dialogue/InteractionController.cs | 정보 | 필드 직렬화 | 필드 Sewer_Observation_SucssesAfterSanSuc 가 씬 직렬화에 없음(씬 비교 불가) |  |
| 1st_Battle.csv:9 | 정보 | 화자 없음 | 대사는 있고 캐릭터이름이 빔(DialogManager 는 이름창 숨김 처리. 나레이션이면 정상) |  |
| 1st_DetectiveOffice.csv:347 | 정보 | 화자 없음 | 대사는 있고 캐릭터이름이 빔(DialogManager 는 이름창 숨김 처리. 나레이션이면 정상) |  |
| 1st_DetectiveOffice.csv:348 | 정보 | 화자 없음 | 대사는 있고 캐릭터이름이 빔(DialogManager 는 이름창 숨김 처리. 나레이션이면 정상) |  |

## 8. 플래그 요약

- DataBaseManager 정적 bool 플래그 29개 중 검사만 하고 true 로 설정 안 되는 것 3, 설정만 하고 읽지 않는 것 4, 선언만 있고 안 쓰이는 것 1.
- 이 분석은 정적 문자열 매칭이다. 씬의 UnityEvent·ES3 세이브 로드·리플렉션으로 켜지는 플래그는 못 본다. 목록은 5·6절의 `플래그` 항목.

## 9. I2 Localization 현황

- `Assets/Resources/I2Languages.asset` (2175바이트): 용어 **1개**, 언어 8개(en, ja, zh-CN, ru, es, nl-NL, pt-BR, ko).
- 언어별 빈 번역 수: 없음(용어 전부 채워짐)
- 코드의 `I2.Loc`/`LocalizationManager` 호출: 있음

### 통합 방향 추천안

1. **I2 에 대사를 옮기는 것은 지금 하지 않는다.** I2 에는 `Button` 용어 1개뿐이고 런타임 사용처가 없다(사실상 미사용 플러그인). 대사는 CSV 약 5천 줄이고, 분기가 코드·씬 범위(x~y)에 묶여 있어 용어 키로 바꾸면 이벤트 범위 전부를 다시 매핑해야 한다. 출시판 세이브·범위를 깨뜨릴 위험이 크다.
2. **CSV 를 대사의 원본으로 유지**하고, 먼저 5절의 서식 오류와 6절의 스토리 항목을 정리한다(쉼표 잘림·한글 잔류·미호출 이벤트).
3. 다국어가 필요해지면 **언어별 열 추가 방식**을 쓴다: `ID,캐릭터이름,대사_en,대사_ko,…` 로 열을 늘리고 `DialogPaser` 가 현재 언어 열을 읽게 한다. 인덱스/범위 구조는 그대로라 안전하다. 한글 원본 `SCOW_SCV.csv` 는 이 구조의 ko 열 후보이며, 인덱스 정렬(321줄 구조는 같음)부터 검증한다.
4. UI 문구(버튼·메뉴·설정)만 I2 로 옮긴다. 이쪽은 하드코딩 한글이 흩어져 있어 I2 의 이점이 크고 대사 범위와 무관하다. 이때 `Button` 용어와 8개 언어 구성을 재사용한다.
5. 대사 파서를 건드릴 때는 `ID≠인덱스`(이어지는 줄)·`^`/`@` 치환 규약을 CSV 검사기(이 도구)로 먼저 못 박고, 파서의 따옴표 인식 개선(진짜 CSV 파서)은 규약 이행 뒤에 한다.
