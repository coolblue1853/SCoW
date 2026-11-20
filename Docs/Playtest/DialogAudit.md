# 대사 데이터 점검표 (DialogAudit)

`Tools/dialog_lint.py` 가 자동 생성한 표다(`python -I Tools/dialog_lint.py --report Docs/Playtest/DialogAudit.md --dot`). CSV 수정은 아직 하지 않았다.

- 총 문제 **268건** = 오류 6 + 경고 262 (정보 351건은 건수에 넣지 않음)
- 대상: CSV 15개(`Assets/Script/Asset/Resources/`), 씬 이벤트 657개(`Main.unity`), DataBaseManager 플래그 419개, I2Languages

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
| 1st_Bar | 211 | 110 | 0 | 39 | 11 |
| 1st_Battle | 11 | 310 | 0 | 3 | 2 |
| 1st_Client'sHouse | 277 | 44 | 2 | 35 | 9 |
| 1st_DailyNews | 230 | 91 | 1 | 19 | 2 |
| 1st_DetectiveOffice | 361 | 54 | 0 | 23 | 9 |
| 1st_Hospital | 148 | 173 | 0 | 6 | 3 |
| 1st_Item | 27 | 294 | 2 | 2 | 7 |
| 1st_PoliceOffice | 232 | 89 | 0 | 12 | 4 |
| 1st_Riverside | 71 | 250 | 0 | 23 | 1 |
| 1st_Slum | 62 | 259 | 0 | 0 | 2 |
| 1st_University | 74 | 247 | 0 | 10 | 1 |
| 2nd_NoonEvent | 187 | 88 | 0 | 33 | 5 |
| InSewer | 190 | 225 | 0 | 23 | 66 |
| SCOW_SCV | 181 | 140 | 0 | 2 | 5 |
| SewerOffice | 93 | 322 | 0 | 6 | 2 |
| `Assets/Scenes/Main.unity` | - | - | 0 | 0 | 190 |
| `Assets/Script/dialogue/DialogDatabaseManager.cs` | - | - | 0 | 1 | 3 |
| `Assets/Script/dialogue/InteractionController.cs` | - | - | 1 | 0 | 7 |
| `Assets/Script/old_Dialog/DatabaseManager.cs` | - | - | 0 | 25 | 22 |

## 4. 종류별 건수

| 종류 | 오류 | 경고 | 정보 |
|---|---:|---:|---:|
| 중복 문장 | 0 | 211 | 0 |
| 플래그 설정만 | 0 | 17 | 0 |
| 도달 불가 대사 | 0 | 9 | 0 |
| 플래그 검사만 | 0 | 8 | 0 |
| 깨진 따옴표 | 0 | 6 | 0 |
| 색 표식 짝 | 0 | 5 | 0 |
| 빈 대사 참조 | 5 | 0 | 0 |
| 빈 필수 칸 | 0 | 3 | 0 |
| 괄호 짝 | 0 | 1 | 0 |
| 말투 혼용 | 0 | 1 | 0 |
| 빈 참조 필드 | 1 | 0 | 0 |
| 장소 키 | 0 | 1 | 3 |
| 개행 | 0 | 0 | 15 |
| 문장부호 | 0 | 0 | 22 |
| 공백 | 0 | 0 | 7 |
| 화자 없음 | 0 | 0 | 76 |
| 미호출 복제본 | 0 | 0 | 190 |
| 범위 공유 | 0 | 0 | 7 |
| 미참조 파일 | 0 | 0 | 1 |
| 필드 직렬화 | 0 | 0 | 1 |
| 미사용 필드 | 0 | 0 | 6 |
| 코드 미참조 | 0 | 0 | 1 |
| 플래그 미사용 | 0 | 0 | 22 |

## 5. 문제 목록: 서식·참조·오탈자 등 (스토리 내용 불변, 오류+경고 39건)

| 위치 | 등급 | 종류 | 내용 | 제안 수정 |
|---|---|---|---|---|
| Assets/Script/dialogue/InteractionController.cs | 오류 | 빈 참조 필드 | InteractionController.aidenLookJudge 가 씬에서 비어 있음(코드가 .GetComponent<interactionEvent>() 호출 -> NullReference) | 씬에서 연결 또는 필드 삭제 |
| 1st_Bar.csv:70 | 경고 | 깨진 따옴표 | @(따옴표) 개수가 홀수(3) | 여닫는 @ 짝 맞추기 |
| 1st_Client'sHouse.csv:228 | 경고 | 색 표식 짝 | 색 표식 열림 0 / 닫힘 )ⓦ 1 | ⓖ(..)ⓦ 짝 맞추기 |
| 1st_Client'sHouse.csv:239 | 경고 | 깨진 따옴표 | @(따옴표) 개수가 홀수(1) | 여닫는 @ 짝 맞추기 |
| 1st_DailyNews.csv:122 | 경고 | 깨진 따옴표 | @(따옴표) 개수가 홀수(1) | 여닫는 @ 짝 맞추기 |
| 1st_Hospital.csv:24 | 경고 | 괄호 짝 | ( ) 개수 불일치 2/1 | 괄호 짝 맞추기 |
| 1st_Hospital.csv:43 | 경고 | 깨진 따옴표 | 날 쌍따옴표(") 있음. 규약은 @ 사용(CSV 따옴표 이스케이프와 충돌) | @ 로 교체 |
| 1st_Hospital.csv:114 | 경고 | 색 표식 짝 | 색 표식 열림 1 / 닫힘 )ⓦ 2 | ⓖ(..)ⓦ 짝 맞추기 |
| 1st_PoliceOffice.csv:95 | 경고 | 색 표식 짝 | 색 표식 열림 2 / 닫힘 )ⓦ 1 | ⓖ(..)ⓦ 짝 맞추기 |
| 1st_Riverside.csv:3 | 경고 | 깨진 따옴표 | @(따옴표) 개수가 홀수(1) | 여닫는 @ 짝 맞추기 |
| 1st_University.csv:36 | 경고 | 색 표식 짝 | 색 표식 열림 2 / 닫힘 )ⓦ 1 | ⓖ(..)ⓦ 짝 맞추기 |
| InSewer.csv:36 | 경고 | 색 표식 짝 | 색 표식 열림 1 / 닫힘 )ⓦ 0 | ⓖ(..)ⓦ 짝 맞추기 |
| SCOW_SCV.csv:163 | 경고 | 깨진 따옴표 | @(따옴표) 개수가 홀수(1) | 여닫는 @ 짝 맞추기 |
| Assets/Script/dialogue/DialogDatabaseManager.cs | 경고 | 장소 키 | nowPlace="Gunshop" 비교가 코드에 있는데 대응 CSV/매핑이 없음(대사 로드 안 됨) | 미사용이면 삭제 / 쓰면 CSV 추가 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | bgmChangeChecker : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | careless : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | carelessness : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | conntectMouse : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | debilitation : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | dizziness : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | endDialogLine : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | eyeDisease : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 검사만 | intelDave2 : 읽는 곳 3(CharacterButtonManage.cs,IntelCharacterDetail.cs,K) 있으나 true 로 설정하는 곳이 없음(항상 false) | 설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 검사만 | intelDave3 : 읽는 곳 3(CharacterButtonManage.cs,IntelCharacterDetail.cs,K) 있으나 true 로 설정하는 곳이 없음(항상 false) | 설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 검사만 | intelDave4 : 읽는 곳 3(CharacterButtonManage.cs,IntelCharacterDetail.cs,K) 있으나 true 로 설정하는 곳이 없음(항상 false) | 설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 검사만 | intelDave5 : 읽는 곳 3(CharacterButtonManage.cs,IntelCharacterDetail.cs,K) 있으나 true 로 설정하는 곳이 없음(항상 false) | 설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 검사만 | intelDave6 : 읽는 곳 3(CharacterButtonManage.cs,IntelCharacterDetail.cs,K) 있으나 true 로 설정하는 곳이 없음(항상 false) | 설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | isSewerOfficeBrokenWindow : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 검사만 | isTest : 읽는 곳 1(DirectingManager.cs) 있으나 true 로 설정하는 곳이 없음(항상 false) | 설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 검사만 | judgeSafeTryOpen : 읽는 곳 1(KeyItemBox.cs) 있으나 true 로 설정하는 곳이 없음(항상 false) | 설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | migraines : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | musclePain : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | setIntelCharDetail : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 검사만 | sewerHouseIn : 읽는 곳 2(TimeManagere.cs) 있으나 true 로 설정하는 곳이 없음(항상 false) | 설정 코드 누락인지 확인. 대사 표식(ⓟ 등)으로 켜지는 경우 포함해 점검 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | skipActive : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | sprains : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | swainDie : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | thinWallets : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |
| Assets/Script/old_Dialog/DatabaseManager.cs | 경고 | 플래그 설정만 | unlucky : 1곳에서 설정하나 읽는 곳이 없음 | 불필요 플래그면 삭제, 엔딩/세이브용이면 문서화 |

## 6. 스토리 내용을 바꾸는 수정 (사용자 확인용, 229건)

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
| SCOW_SCV.csv:7 | 경고 | 말투 혼용 | 파비안 의 한글 대사가 존댓말 38 / 반말 19 혼재(상대에 따른 구분이면 정상) | 상대별 말투 규칙 확인 후 통일 |
| 1st_Client'sHouse.csv:102 | 오류 | 빈 대사 참조 | 1st/Client'sHouse/Aiden_Judge_psychotherapy_Sucsses [101~112] : 참조 범위에 빈 대사 12개(101,102,103,104,105,106) (장: 1st/2nd/3rd) | 대사를 채우거나(미작성 장면) 범위에서 제외 |
| 1st_Client'sHouse.csv:116 | 오류 | 빈 대사 참조 | 3rd/Client'sHouse/Aiden_Judge_psychotherapy_Sucsses_End [115~123] : 참조 범위에 빈 대사 9개(115,116,117,118,119,120) (장: 1st/2nd/3rd) | 대사를 채우거나(미작성 장면) 범위에서 제외 |
| 1st_DailyNews.csv:75 | 오류 | 빈 대사 참조 | 1st/DailyNews/Swain_Key_MissingPeople1 [70~76] : 참조 범위에 빈 대사 1개(74) (장: 1st) | 대사를 채우거나(미작성 장면) 범위에서 제외 |
| 1st_Item.csv:19 | 오류 | 빈 대사 참조 | 1st/Item/Document_Look [16~27] : 참조 범위에 빈 대사 1개(18) (장: 1st) | 대사를 채우거나(미작성 장면) 범위에서 제외 |
| 1st_Item.csv:29 | 오류 | 빈 대사 참조 | 3rd/Item/OldMap_Look [28~30] : 참조 범위에 빈 대사 3개(28,29,30) (장: 3rd) | 대사를 채우거나(미작성 장면) 범위에서 제외 |
| 1st_Client'sHouse.csv:102 | 경고 | 빈 필수 칸 | 인덱스 101~123(23개, 줄 102~124)의 화자는 있는데 대사가 비어 있음(이벤트가 참조하면 빈 대사창) | 대사를 채우거나(미작성 장면) 이벤트 범위에서 제외 |
| 1st_DailyNews.csv:75 | 경고 | 빈 필수 칸 | 인덱스 74~74(1개, 줄 75~75)의 화자는 있는데 대사가 비어 있음(이벤트가 참조하면 빈 대사창) | 대사를 채우거나(미작성 장면) 이벤트 범위에서 제외 |
| 1st_Item.csv:19 | 경고 | 빈 필수 칸 | 인덱스 18~18(1개, 줄 19~19)의 화자는 있는데 대사가 비어 있음(이벤트가 참조하면 빈 대사창) | 대사를 채우거나(미작성 장면) 이벤트 범위에서 제외 |
| 1st_Bar.csv:2 | 경고 | 중복 문장 | 같은 문장이 4회(2,22,143,201): '"who is this, fab. the great detective of arkham! ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:3 | 경고 | 중복 문장 | 같은 문장이 3회(3,23,202): '"(maeve)... you\'re still the same as you\'ve been i' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:4 | 경고 | 중복 문장 | 같은 문장이 2회(4,24): '"my only strength. sit down."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:5 | 경고 | 중복 문장 | 같은 문장이 2회(5,25): '"you do realize it\'s crackdown soon, right?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:6 | 경고 | 중복 문장 | 같은 문장이 2회(6,26): '"(albert) will handle it. besides, even if they ge' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:7 | 경고 | 중복 문장 | 같은 문장이 2회(7,27): '((maeve) smirked and brought me a drink.)' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:8 | 경고 | 중복 문장 | 같은 문장이 2회(8,28): '"i envy you for that, don\'t you?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:10 | 경고 | 중복 문장 | 같은 문장이 2회(10,30): '"so what are you doing here? business as usual"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:14 | 경고 | 중복 문장 | 같은 문장이 2회(14,33): '"it\'s business as usual for us, all the bigwigs ar' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:15 | 경고 | 중복 문장 | 같은 문장이 2회(15,34): '"i don\'t know when our (maeve) will be like that..' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:17 | 경고 | 중복 문장 | 같은 문장이 2회(17,36): '"haha... by the way, it\'s quiet here today."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:18 | 경고 | 중복 문장 | 같은 문장이 2회(18,37): '"because of crack down. today, it\'s just those two' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:19 | 경고 | 중복 문장 | 같은 문장이 2회(19,38): '"i heard that they were a (sewer worker) and got u' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:20 | 경고 | 중복 문장 | 같은 문장이 2회(20,39): '"they must have needed a drink."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:21 | 경고 | 중복 문장 | 같은 문장이 4회(21,40,207,212): '((maeve) and i joked about it for a while.)' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:82 | 경고 | 중복 문장 | 같은 문장이 2회(82,86): '(two men who appear to be (sewer workers) are sitt' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:83 | 경고 | 중복 문장 | 같은 문장이 2회(83,87): "(they're already blushing, as if they are had quit" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:84 | 경고 | 중복 문장 | 같은 문장이 2회(84,88): "'what a smell..!'" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:85 | 경고 | 중복 문장 | 같은 문장이 2회(85,89): '(upon examination, their bodies emit a (fishy smel' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:116 | 경고 | 중복 문장 | 같은 문장이 2회(116,121): '(i walked into the (bar) and a few men were handin' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:117 | 경고 | 중복 문장 | 같은 문장이 2회(117,122): "'is that a...?'" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:118 | 경고 | 중복 문장 | 같은 문장이 2회(118,123): '(while i was staring at them, i smelled a (fishy s' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:119 | 경고 | 중복 문장 | 같은 문장이 2회(119,124): "'this smells like...'" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:128 | 경고 | 중복 문장 | 같은 문장이 2회(128,145): '"these are the people we\'ve been helped lately, ne' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:129 | 경고 | 중복 문장 | 같은 문장이 2회(129,146): '"supplier and buyer? what does that mean?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:130 | 경고 | 중복 문장 | 같은 문장이 2회(130,147): '"recently, the smuggling route was blocked."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:131 | 경고 | 중복 문장 | 같은 문장이 2회(131,148): '"so we\'ve been getting our booze from the innsmout' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:132 | 경고 | 중복 문장 | 같은 문장이 2회(132,149): '"they\'re making algae-based liquor. it\'s like a sp' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:133 | 경고 | 중복 문장 | 같은 문장이 2회(133,150): '"so i\'m bringing in that alcohol as well."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:134 | 경고 | 중복 문장 | 같은 문장이 2회(134,151): '"lately, we\'ve had a few people from innsmouth com' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:135 | 경고 | 중복 문장 | 같은 문장이 2회(135,152): '"we\'re happy to have a new root, they\'re happy to ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:136 | 경고 | 중복 문장 | 같은 문장이 2회(136,153): '"heh... that\'s great, yeah. why don\'t you give me ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:137 | 경고 | 중복 문장 | 같은 문장이 2회(137,154): '"hmm... unfortunately, that\'s going to be tough, f' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:138 | 경고 | 중복 문장 | 같은 문장이 2회(138,155): '"they made me promise, don\'t sell this to anyone e' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:139 | 경고 | 중복 문장 | 같은 문장이 2회(139,156): '"if we\'re going to work on this, shouldn\'t we at l' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:140 | 경고 | 중복 문장 | 같은 문장이 2회(140,157): '"well, if that\'s the case, so be it..."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:141 | 경고 | 중복 문장 | 같은 문장이 2회(141,158): '"other than that, let them order anything."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Bar.csv:142 | 경고 | 중복 문장 | 같은 문장이 2회(142,159): '(with that, (maeve) lifted her glass and began to ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Battle.csv:7 | 경고 | 중복 문장 | 같은 문장이 2회(7,10): '(while i was mentally gagging, they were getting c' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Battle.csv:8 | 경고 | 중복 문장 | 같은 문장이 2회(8,11): '(with a clear purpose)' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:75 | 경고 | 중복 문장 | 같은 문장이 2회(75,85): "'i can try to unlock it myself, or... take it to (" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:125 | 경고 | 중복 문장 | 같은 문장이 2회(125,133): '"what do you think...?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:132 | 경고 | 중복 문장 | 같은 문장이 2회(132,137): '"yes, thanks..."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:143 | 경고 | 중복 문장 | 같은 문장이 2회(143,209): '"(sewer ghost story)? what was that about?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:144 | 경고 | 중복 문장 | 같은 문장이 2회(144,210): '"i don\'t know because i\'ve only heard it through t' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:145 | 경고 | 중복 문장 | 같은 문장이 2회(145,211): '"and the story was that the alligators would eat t' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:146 | 경고 | 중복 문장 | 같은 문장이 2회(146,212): '"that\'s an interesting story, thank you."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:147 | 경고 | 중복 문장 | 같은 문장이 2회(147,213): "'alligators in the sewers... ask (albert), he migh" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:150 | 경고 | 중복 문장 | 같은 문장이 2회(150,171): '(his eyes came alive, albeit slightly. his fear se' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:151 | 경고 | 중복 문장 | 같은 문장이 2회(151,172): '"well... who are you... why are you in my house...' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:152 | 경고 | 중복 문장 | 같은 문장이 2회(152,173): '"now that you\'ve calmed down, i\'m a private invest' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:153 | 경고 | 중복 문장 | 같은 문장이 2회(153,174): '"(ella)... oh, but it\'s no use, you won\'t understa' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:154 | 경고 | 중복 문장 | 같은 문장이 2회(154,175): '"what do you mean it\'s over? tell me anything."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:155 | 경고 | 중복 문장 | 같은 문장이 2회(155,176): '"so if i told you the world was about to end, you\'' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:156 | 경고 | 중복 문장 | 같은 문장이 2회(156,177): "'what? the end of the world? why is he writing a n" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:157 | 경고 | 중복 문장 | 같은 문장이 2회(157,178): '" end of the world, you say..? people these days m' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:158 | 경고 | 중복 문장 | 같은 문장이 2회(158,179): '"haha, look at that, wouldn\'t believe you if i tol' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:162 | 경고 | 중복 문장 | 같은 문장이 2회(162,183): '"okay, that\'s great. so... what\'s the password?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:163 | 경고 | 중복 문장 | 같은 문장이 2회(163,184): '"uh..... forgot."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:164 | 경고 | 중복 문장 | 같은 문장이 2회(164,185): "'is this guy trying to play a joke on me?'" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:165 | 경고 | 중복 문장 | 같은 문장이 2회(165,186): "'there are people who, just because i mention bein" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:166 | 경고 | 중복 문장 | 같은 문장이 2회(166,187): '"you forgot? but isn\'t it your (safe)?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:167 | 경고 | 중복 문장 | 같은 문장이 2회(167,188): '"yes, that\'s right, but i don\'t remember..."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:168 | 경고 | 중복 문장 | 같은 문장이 2회(168,189): '"...okay, well, i\'ll try to fix that on my end."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:169 | 경고 | 중복 문장 | 같은 문장이 2회(169,190): '(at that moment, his pupils dilated as if he had s' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:170 | 경고 | 중복 문장 | 같은 문장이 2회(170,191): "'at least there's a harvest. and i'm sure i'll get" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:203 | 경고 | 중복 문장 | 같은 문장이 2회(203,206): '"have you noticed a (fishy smell) on the street or' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:204 | 경고 | 중복 문장 | 같은 문장이 2회(204,207): '"hmm... i don\'t think i\'ve ever done that, but ah!' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Client'sHouse.csv:205 | 경고 | 중복 문장 | 같은 문장이 2회(205,208): '"i think my husband\'s clothes (smelled) like that ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:2 | 경고 | 중복 문장 | 같은 문장이 2회(2,125): '"good day, sir. i\'m here to ask you a few question' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:3 | 경고 | 중복 문장 | 같은 문장이 2회(3,126): '"we\'re a newspaper, not a city hall information ce' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:4 | 경고 | 중복 문장 | 같은 문장이 2회(4,127): '"do you know mr. (aiden triss)? i\'m here about him' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:90 | 경고 | 중복 문장 | 같은 문장이 2회(90,117): '"that\'s weird, yeah."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:93 | 경고 | 중복 문장 | 같은 문장이 2회(93,105): '"during our investigation, i learned that the rece' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:94 | 경고 | 중복 문장 | 같은 문장이 2회(94,106): '"hmm? well, that could be a possibility. "' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:95 | 경고 | 중복 문장 | 같은 문장이 2회(95,107): '"but don\'t you think it\'s odd that the smell has b' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:96 | 경고 | 중복 문장 | 같은 문장이 2회(96,108): '"that\'s kind of weird, too."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:98 | 경고 | 중복 문장 | 같은 문장이 2회(98,110): '"what\'s wrong with you, you look so serious."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:158 | 경고 | 중복 문장 | 같은 문장이 2회(158,172): '"so where did mr.(aiden) end up?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:159 | 경고 | 중복 문장 | 같은 문장이 2회(159,173): '"well, that\'s a lot of facts to consider."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:165 | 경고 | 중복 문장 | 같은 문장이 2회(165,175): '"but at the same time, (aiden) has had enemies, so' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:167 | 경고 | 중복 문장 | 같은 문장이 2회(167,176): '"in fact, unlike before, the culprit actually comm' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:168 | 경고 | 중복 문장 | 같은 문장이 2회(168,177): '"anyway, this is something i\'ll have to investigat' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:169 | 경고 | 중복 문장 | 같은 문장이 2회(169,178): '"let me know if you learn anything too."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:170 | 경고 | 중복 문장 | 같은 문장이 2회(170,179): '"i need more information to resolve this issue."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DailyNews.csv:171 | 경고 | 중복 문장 | 같은 문장이 2회(171,180): '"yes, i\'ll do that"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:183 | 경고 | 중복 문장 | 같은 문장이 2회(183,255): '"let\'s recap what happened today."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:225 | 경고 | 중복 문장 | 같은 문장이 2회(225,237): "'wait, i dreamed i was drowning...? this is'" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:226 | 경고 | 중복 문장 | 같은 문장이 2회(226,238): '(i can hear the words of aiden and the doctor at t' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:227 | 경고 | 중복 문장 | 같은 문장이 2회(227,239): '(no way... no way...)' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:229 | 경고 | 중복 문장 | 같은 문장이 2회(229,241): '(a wave of nausea washed over me).' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:230 | 경고 | 중복 문장 | 같은 문장이 2회(230,242): "(and that face of aiden's that comes to mind.)" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:231 | 경고 | 중복 문장 | 같은 문장이 2회(231,243): "(i'm trying to deny it, but my thoughts are going " | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:232 | 경고 | 중복 문장 | 같은 문장이 2회(232,244): "(i don't know if i have the same problem as aiden." | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:234 | 경고 | 중복 문장 | 같은 문장이 2회(234,246): '"whoa... let\'s calm down, i\'m sure i\'ve heard so m' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:235 | 경고 | 중복 문장 | 같은 문장이 2회(235,247): '"more than that, i need to move quickly to solve t' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:236 | 경고 | 중복 문장 | 같은 문장이 2회(236,248): '(i rushed to get ready to head out the door.)※' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:249 | 경고 | 중복 문장 | 같은 문장이 2회(249,252): "(i was trying to think of something, but couldn't " | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:250 | 경고 | 중복 문장 | 같은 문장이 2회(250,253): "(what is this deja vu...? never mind, that's not t" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:251 | 경고 | 중복 문장 | 같은 문장이 2회(251,254): "(i'll have to move fast to raise the money.)※" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:271 | 경고 | 중복 문장 | 같은 문장이 3회(271,274,280): '"there\'s one big takeaway from today\'s research...' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:272 | 경고 | 중복 문장 | 같은 문장이 2회(272,281): '"the cause of (disappearances) and (fishy smell) i' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:273 | 경고 | 중복 문장 | 같은 문장이 2회(273,282): '"the person with the (fishy smell) is responsible ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:276 | 경고 | 중복 문장 | 같은 문장이 2회(276,283): '"and the owner of that (fishy smell) was a (sewer ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:277 | 경고 | 중복 문장 | 같은 문장이 2회(277,284): '"maybe the cops will find out soon enough."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:278 | 경고 | 중복 문장 | 같은 문장이 2회(278,285): '"then the case will be solved without my involveme' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:279 | 경고 | 중복 문장 | 같은 문장이 2회(279,286): '"um... i don\'t know what to do."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_DetectiveOffice.csv:339 | 경고 | 중복 문장 | 같은 문장이 2회(339,343): '"i think i\'ll... take a little more rest today."※' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Hospital.csv:5 | 경고 | 중복 문장 | 같은 문장이 2회(5,22): '"hi, i know you\'re busy, but can i ask you a quest' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Hospital.csv:7 | 경고 | 중복 문장 | 같은 문장이 2회(7,25): '"maybe there\'s a lot of people around these days?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Hospital.csv:39 | 경고 | 중복 문장 | 같은 문장이 2회(39,78): '"is there anything special about (paranoia)?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_PoliceOffice.csv:87 | 경고 | 중복 문장 | 같은 문장이 2회(87,97): '"have you heard that (river water quality) has bee' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_PoliceOffice.csv:88 | 경고 | 중복 문장 | 같은 문장이 2회(88,98): '"what, like someone\'s been sneaking factory wastew' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_PoliceOffice.csv:89 | 경고 | 중복 문장 | 같은 문장이 2회(89,99): '"no, it\'s actually cleaner, except for the fishy s' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_PoliceOffice.csv:119 | 경고 | 중복 문장 | 같은 문장이 2회(119,136): '"i hear you\'ve been out in the site... no problem,' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_PoliceOffice.csv:120 | 경고 | 중복 문장 | 같은 문장이 2회(120,137): '"of course. phew, (murder) in broad daylight in a ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_PoliceOffice.csv:144 | 경고 | 중복 문장 | 같은 문장이 2회(144,153): '"(sewer)... maybe i should pay a visit to the (sew' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_PoliceOffice.csv:145 | 경고 | 중복 문장 | 같은 문장이 2회(145,154): '"well, at least it\'s safer than walking right into' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_PoliceOffice.csv:146 | 경고 | 중복 문장 | 같은 문장이 2회(146,155): '"i\'m thinking the same thing, i\'ll have to take a ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_PoliceOffice.csv:182 | 경고 | 중복 문장 | 같은 문장이 2회(182,187): '"we\'ve sent it to the hospital for an autopsy, so ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_PoliceOffice.csv:228 | 경고 | 중복 문장 | 같은 문장이 2회(228,232): '"albert did that?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_PoliceOffice.csv:229 | 경고 | 중복 문장 | 같은 문장이 2회(229,233): "'that's weird.'" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:25 | 경고 | 중복 문장 | 같은 문장이 2회(25,28): '"what the hell is that?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:26 | 경고 | 중복 문장 | 같은 문장이 2회(26,29): '(when i woke up and checked again, there was nothi' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:30 | 경고 | 중복 문장 | 같은 문장이 2회(30,50): '"hello, professor kane, i heard there\'s been a pro' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:31 | 경고 | 중복 문장 | 같은 문장이 2회(31,51): '(only then did the old man look at me and open his' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:32 | 경고 | 중복 문장 | 같은 문장이 2회(32,52): '"hmm~? that\'s right. look at this."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:33 | 경고 | 중복 문장 | 같은 문장이 2회(33,53): '(he handed me a small vial of reagent.)' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:34 | 경고 | 중복 문장 | 같은 문장이 2회(34,54): '"what is this...?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:35 | 경고 | 중복 문장 | 같은 문장이 2회(35,55): '"you\'ll know it when you smell it."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:36 | 경고 | 중복 문장 | 같은 문장이 2회(36,56): '(when i smell the bottle, i smell a light fishy sc' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:37 | 경고 | 중복 문장 | 같은 문장이 2회(37,57): "(it doesn't smell like a typical river.)" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:38 | 경고 | 중복 문장 | 같은 문장이 2회(38,58): '"that\'s a very strange smell, is the water contami' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:39 | 경고 | 중복 문장 | 같은 문장이 2회(39,59): '"no, surprisingly, the opposite is true. the water' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:40 | 경고 | 중복 문장 | 같은 문장이 2회(40,60): "'that doesn't smell like clean water at all.'" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:41 | 경고 | 중복 문장 | 같은 문장이 2회(41,61): '"the quality of the water in the miskatonic river ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:42 | 경고 | 중복 문장 | 같은 문장이 2회(42,62): '(with that, he took a sip of that water from the r' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:43 | 경고 | 중복 문장 | 같은 문장이 2회(43,63): '"but now i\'ve gotten to the point where i don\'t mi' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:44 | 경고 | 중복 문장 | 같은 문장이 2회(44,64): '"yes, i see..."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:45 | 경고 | 중복 문장 | 같은 문장이 2회(45,65): "'i don't think i could ever do that. college profe" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:46 | 경고 | 중복 문장 | 같은 문장이 2회(46,66): '"but where is that sewage coming from?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:47 | 경고 | 중복 문장 | 같은 문장이 2회(47,67): '"you ask the obvious, but it\'s the (sewage treatme' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:48 | 경고 | 중복 문장 | 같은 문장이 2회(48,68): '"anyway, so i went to the lab and looked into it..' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_Riverside.csv:49 | 경고 | 중복 문장 | 같은 문장이 2회(49,69): '(since then, most of what the old professor says i' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_University.csv:54 | 경고 | 중복 문장 | 같은 문장이 2회(54,67): '"i heard that (strange slime) was recently found i' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_University.csv:55 | 경고 | 중복 문장 | 같은 문장이 2회(55,68): '"yes. some sort of clear, gel-like substance has b' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_University.csv:56 | 경고 | 중복 문장 | 같은 문장이 2회(56,69): '"when i did some research in the lab, i found some' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_University.csv:57 | 경고 | 중복 문장 | 같은 문장이 2회(57,70): '"within the mucus were fish scales."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_University.csv:58 | 경고 | 중복 문장 | 같은 문장이 2회(58,71): '"at the same time, there were pieces of what appea' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_University.csv:59 | 경고 | 중복 문장 | 같은 문장이 2회(59,72): '"and some of the scales were connected by tissues ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_University.csv:60 | 경고 | 중복 문장 | 같은 문장이 2회(60,73): '"it\'s like a mermaid, which is pretty amazing, rig' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 1st_University.csv:61 | 경고 | 중복 문장 | 같은 문장이 2회(61,74): '"huh... mermaid."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:2 | 경고 | 중복 문장 | 같은 문장이 3회(2,18,35): '(things really took a turn for the worse when i st' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:3 | 경고 | 중복 문장 | 같은 문장이 3회(3,19,36): '"what\'s going on, (albert)?"▒' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:4 | 경고 | 중복 문장 | 같은 문장이 3회(4,20,37): '((albert) called me out of the blue and started wh' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:6 | 경고 | 중복 문장 | 같은 문장이 3회(6,22,39): '"her husband was suffering from (nightmares). but ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:7 | 경고 | 중복 문장 | 같은 문장이 3회(7,23,40): '"she was just murdered, about ten minutes ago."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:9 | 경고 | 중복 문장 | 같은 문장이 3회(9,25,42): '"it looks like the (wife) was murdered and the (hu' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:10 | 경고 | 중복 문장 | 같은 문장이 3회(10,26,43): '"we\'re still investigating, but there\'s a lot that' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:11 | 경고 | 중복 문장 | 같은 문장이 2회(11,27): '"you should probably stop getting involved in that' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:12 | 경고 | 중복 문장 | 같은 문장이 2회(12,28): '"you know my personality. i hate don\'t feel refres' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:13 | 경고 | 중복 문장 | 같은 문장이 2회(13,29): '"ugh... you do realize i can\'t help you in the fie' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:14 | 경고 | 중복 문장 | 같은 문장이 2회(14,30): '"i suppose so. thanks anyway."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:15 | 경고 | 중복 문장 | 같은 문장이 2회(15,31): '"don\'t bother hands-on guys, but let me know if yo' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:17 | 경고 | 중복 문장 | 같은 문장이 3회(17,34,54): '(i hung up the phone with that and ran out the doo' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:21 | 경고 | 중복 문장 | 같은 문장이 2회(21,38): '"do you remember (ms. ella triss), who came to you' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:64 | 경고 | 중복 문장 | 같은 문장이 3회(64,73,85): '(i began to listen in surreptitiously, slightly ou' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:65 | 경고 | 중복 문장 | 같은 문장이 3회(65,74,86): '"so... can you continue the story?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:66 | 경고 | 중복 문장 | 같은 문장이 3회(66,75,87): '"yeah... so about 10 minutes ago, i was walking do' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:67 | 경고 | 중복 문장 | 같은 문장이 3회(67,76,88): '"so i was looking to see what was going on, and i ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:68 | 경고 | 중복 문장 | 같은 문장이 3회(68,77,89): '"and a man in a raggedy (workman\'s outfit) with a ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:69 | 경고 | 중복 문장 | 같은 문장이 3회(69,78,90): '"he disappeared down the alley with a big sack of ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:70 | 경고 | 중복 문장 | 같은 문장이 3회(70,79,91): '"didn\'t you see anything else?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:71 | 경고 | 중복 문장 | 같은 문장이 3회(71,80,92): '"it happened so fast..."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:72 | 경고 | 중복 문장 | 같은 문장이 3회(72,81,93): '(this was followed by a story that sounded like a ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:130 | 경고 | 중복 문장 | 같은 문장이 2회(130,137): '(the neck is crooked at a weird angle that must be' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:131 | 경고 | 중복 문장 | 같은 문장이 2회(131,138): '(i can tell by the look on her face that she was i' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:132 | 경고 | 중복 문장 | 같은 문장이 2회(132,139): '(as i was doing that, i noticed something a (fishy' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:136 | 경고 | 중복 문장 | 같은 문장이 2회(136,141): "'is that means that the source of (fishy smell) mi" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:164 | 경고 | 중복 문장 | 같은 문장이 2회(164,182): '"oh... a little bloodstained..."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:166 | 경고 | 중복 문장 | 같은 문장이 2회(166,184): "'where does it lead to...?'" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:167 | 경고 | 중복 문장 | 같은 문장이 2회(167,185): "'the blood trail seems to break here...'" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:168 | 경고 | 중복 문장 | 같은 문장이 2회(168,186): "'did the culprit crawl into a (manhole)?'" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:169 | 경고 | 중복 문장 | 같은 문장이 2회(169,187): "'i could go in there myself, but it might be dange" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| 2nd_NoonEvent.csv:170 | 경고 | 중복 문장 | 같은 문장이 2회(170,188): '(nothing special was found.)' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:3 | 경고 | 중복 문장 | 같은 문장이 2회(3,91): "'how do i....'⊙" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:52 | 경고 | 중복 문장 | 같은 문장이 2회(52,54): '"damn, i read the wrong thing."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:64 | 경고 | 중복 문장 | 같은 문장이 2회(64,68): '(i picked up a rock lying around and slammed it do' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:67 | 경고 | 중복 문장 | 같은 문장이 2회(67,72): '"this... this is... what the hell..."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:97 | 경고 | 중복 문장 | 같은 문장이 2회(97,134): '(with that thought in mind, i pulled myself out of' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:101 | 경고 | 중복 문장 | 같은 문장이 2회(101,135): '"wait… i think this is going to overflow...?"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:103 | 경고 | 중복 문장 | 같은 문장이 2회(103,136): '(while i was thinking about it, the water started ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:104 | 경고 | 중복 문장 | 같은 문장이 2회(104,137): '"this is going to flood the whole city..."' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:105 | 경고 | 중복 문장 | 같은 문장이 2회(105,138): '(the turbid water that submerges the city rises to' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:106 | 경고 | 중복 문장 | 같은 문장이 2회(106,139): "'get on the roof or i'll be swept away...!'" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:150 | 경고 | 중복 문장 | 같은 문장이 2회(150,158): 'albert is dead.' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:151 | 경고 | 중복 문장 | 같은 문장이 2회(151,159): 'instead of being saddened, maeve was furious and v' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:152 | 경고 | 중복 문장 | 같은 문장이 2회(152,160): "it's probably because of me that albert is the way" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:153 | 경고 | 중복 문장 | 같은 문장이 2회(153,161): 'albert must have known too much.' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:154 | 경고 | 중복 문장 | 같은 문장이 2회(154,162): "i'll have to keep this a secret from maeve for the" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:165 | 경고 | 중복 문장 | 같은 문장이 2회(165,174): 'the news that surprised me the most was that aiden' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:166 | 경고 | 중복 문장 | 같은 문장이 2회(166,175): 'he was found inside a sewer with his limbs tied to' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:167 | 경고 | 중복 문장 | 같은 문장이 2회(167,176): 'by the time the police found him, he was already a' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:168 | 경고 | 중복 문장 | 같은 문장이 2회(168,177): 'he was very saddened by the death of his wife, but' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:171 | 경고 | 중복 문장 | 같은 문장이 2회(171,181): 'still, i feel relieved to have survived such a ter' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| InSewer.csv:172 | 경고 | 중복 문장 | 같은 문장이 2회(172,182): "after all, it's better to drink this crappy coffee" | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| SewerOffice.csv:10 | 경고 | 중복 문장 | 같은 문장이 2회(10,13): "'it's not the time to argue about this and that..." | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| SewerOffice.csv:17 | 경고 | 중복 문장 | 같은 문장이 2회(17,19): '(the locker contains dirty worker outfit.)' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| SewerOffice.csv:26 | 경고 | 중복 문장 | 같은 문장이 2회(26,28): '(it appears to be an internal map of the (sewers).' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| SewerOffice.csv:27 | 경고 | 중복 문장 | 같은 문장이 2회(27,29): '(a map of the inside of a sewer that looks pretty ' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |
| SewerOffice.csv:86 | 경고 | 중복 문장 | 같은 문장이 2회(86,88): '"i\'m sure they did this….!"' | 의도적 반복인지 확인(분기 재사용이면 이벤트 범위 공유로 대체) |

## 7. 정보(건수에 불포함, 351건)

종류별: 미호출 복제본 190, 화자 없음 76, 문장부호 22, 플래그 미사용 22, 개행 15, 공백 7, 범위 공유 7, 미사용 필드 6, 장소 키 3, 미참조 파일 1, 필드 직렬화 1, 코드 미참조 1

(351건이라 종류별 대표 3건만 표시. 전체는 `python -I Tools/dialog_lint.py -v`)

| 위치 | 등급 | 종류 | 내용 | 제안 수정 |
|---|---|---|---|---|
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
| 1st_Client'sHouse.csv | 정보 | 범위 공유 | 서로 다른 이벤트 Aiden_Judge_Observation_Fail,Aiden_Judge_medicine_Fail,Aiden_Judge_psychotherapy 가 같은 범위 [100~100] 를 가리킴(재사용이면 정상) |  |
| 1st_Client'sHouse.csv | 정보 | 범위 공유 | 서로 다른 이벤트 Ella_fistDialog,NockDoor 가 같은 범위 [2~9] 를 가리킴(재사용이면 정상) |  |
| 1st_Client'sHouse.csv | 정보 | 범위 공유 | 서로 다른 이벤트 Ella_Key_FishySmell1_noGhosty1,shySmell1_noGhosty11_noGhosty1 가 같은 범위 [205~212] 를 가리킴(재사용이면 정상) |  |
| Assets/Script/dialogue/DialogDatabaseManager.cs | 정보 | 장소 키 | 'BattleRoad' 는 Update 에서만 CSV 를 고르고 Awake 초기 로드에는 없음(씬 시작 장소가 아니면 정상) |  |
| Assets/Script/dialogue/DialogDatabaseManager.cs | 정보 | 장소 키 | 'NoonEvent' 는 Update 에서만 CSV 를 고르고 Awake 초기 로드에는 없음(씬 시작 장소가 아니면 정상) |  |
| Assets/Script/dialogue/DialogDatabaseManager.cs | 정보 | 장소 키 | 'SewerOffice' 는 Update 에서만 CSV 를 고르고 Awake 초기 로드에는 없음(씬 시작 장소가 아니면 정상) |  |
| SCOW_SCV.csv | 정보 | 코드 미참조 | DialogDatabaseManager 가 이 이름을 쓰지 않음 |  |
| Assets/Script/old_Dialog/DatabaseManager.cs | 정보 | 플래그 미사용 | case2SanCheck : 선언만 있고 쓰이지 않음 | 삭제 후보(세이브 키면 보존) |
| Assets/Script/old_Dialog/DatabaseManager.cs | 정보 | 플래그 미사용 | cognitiveBreakdown : 선언만 있고 쓰이지 않음 | 삭제 후보(세이브 키면 보존) |
| Assets/Script/old_Dialog/DatabaseManager.cs | 정보 | 플래그 미사용 | cognitiveDisorder : 선언만 있고 쓰이지 않음 | 삭제 후보(세이브 키면 보존) |
| Assets/Script/dialogue/InteractionController.cs | 정보 | 필드 직렬화 | 필드 Sewer_Observation_SucssesAfterSanSuc 가 씬 직렬화에 없음(씬 비교 불가) |  |
| 1st_Battle.csv:9 | 정보 | 화자 없음 | 대사는 있고 캐릭터이름이 빔(DialogManager 는 이름창 숨김 처리. 나레이션이면 정상) |  |
| 1st_DetectiveOffice.csv:347 | 정보 | 화자 없음 | 대사는 있고 캐릭터이름이 빔(DialogManager 는 이름창 숨김 처리. 나레이션이면 정상) |  |
| 1st_DetectiveOffice.csv:348 | 정보 | 화자 없음 | 대사는 있고 캐릭터이름이 빔(DialogManager 는 이름창 숨김 처리. 나레이션이면 정상) |  |

## 8. 플래그 요약

- DataBaseManager 정적 bool 플래그 419개 중 검사만 하고 true 로 설정 안 되는 것 8, 설정만 하고 읽지 않는 것 17, 선언만 있고 안 쓰이는 것 22.
- 이 분석은 정적 문자열 매칭이다. 씬의 UnityEvent·ES3 세이브 로드·리플렉션으로 켜지는 플래그는 못 본다. 목록은 5·6절의 `플래그` 항목.

## 9. I2 Localization 현황

- `Assets/Resources/I2Languages.asset` (2175바이트): 용어 **1개**, 언어 8개(en, ja, zh-CN, ru, es, nl-NL, pt-BR, ko).
- 언어별 빈 번역 수: 없음(용어 전부 채워짐)
- 코드의 `I2.Loc`/`LocalizationManager` 호출: 없음(씬·프리팹 Localize 컴포넌트도 없음)

### 통합 방향 추천안

1. **I2 에 대사를 옮기는 것은 지금 하지 않는다.** I2 에는 `Button` 용어 1개뿐이고 런타임 사용처가 없다(사실상 미사용 플러그인). 대사는 CSV 약 5천 줄이고, 분기가 코드·씬 범위(x~y)에 묶여 있어 용어 키로 바꾸면 이벤트 범위 전부를 다시 매핑해야 한다. 출시판 세이브·범위를 깨뜨릴 위험이 크다.
2. **CSV 를 대사의 원본으로 유지**하고, 먼저 5절의 서식 오류와 6절의 스토리 항목을 정리한다(쉼표 잘림·한글 잔류·미호출 이벤트).
3. 다국어가 필요해지면 **언어별 열 추가 방식**을 쓴다: `ID,캐릭터이름,대사_en,대사_ko,…` 로 열을 늘리고 `DialogPaser` 가 현재 언어 열을 읽게 한다. 인덱스/범위 구조는 그대로라 안전하다. 한글 원본 `SCOW_SCV.csv` 는 이 구조의 ko 열 후보이며, 인덱스 정렬(321줄 구조는 같음)부터 검증한다.
4. UI 문구(버튼·메뉴·설정)만 I2 로 옮긴다. 이쪽은 하드코딩 한글이 흩어져 있어 I2 의 이점이 크고 대사 범위와 무관하다. 이때 `Button` 용어와 8개 언어 구성을 재사용한다.
5. 대사 파서를 건드릴 때는 `ID≠인덱스`(이어지는 줄)·`^`/`@` 치환 규약을 CSV 검사기(이 도구)로 먼저 못 박고, 파서의 따옴표 인식 개선(진짜 CSV 파서)은 규약 이행 뒤에 한다.
