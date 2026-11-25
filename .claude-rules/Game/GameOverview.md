# SCoW 게임 개요

> 엔진: Unity 6000.4.0f1 / URP 17.4(11-21 업그레이드, 태그 `pre-unity6` 이 2021.3 판). TMP 는 ugui 2.0 통합판, 셰이더는 `Assets/Font/TextMesh Pro/Shaders`.

> 2026-11-20 조사(코드·씬 grep). 확인 못 한 것은 "미확인"으로 표시. 씬 안 컴포넌트 배치는 `*.unity` 통독 금지라 일부 미확인.

## 1. 게임 한 줄
크툴루의 부름(CoC) 기반 2D 탐정 TRPG. 판정은 1d100 기능치, 정신력(SAN)·디버프(공포증 등), 전투(심해인 등), 심문·조사 대화, 엔딩 분기. 1막 1일차~2일차 낮 이벤트 + 하수도(`InSewer`). 출시 플랫폼은 미확인.

## 2. 플레이 흐름
빌드 씬 3개(`ProjectSettings/EditorBuildSettings.asset`): `Insert`, `SetCharacter`, `Main`.

| 단계 | 씬 | 전환 코드 |
|---|---|---|
| 타이틀(추정: Insert) | `Assets/Scenes/Insert.unity` | `ManageMain.FadeIn()` → DOFade 1.5초 → `GoSetCharacter()` = `LoadScene("SetCharacter")` (`Script/Main/ManageMain.cs`) |
| 캐릭터 생성(스탯→기능 배분) | `Assets/Scenes/SetCharcter/SetCharacter.unity` | `setSkill.FadeIn()` → `GoToGame()` = `LoadScene("Main")` (`Script/setStat/setSkill.cs`) |
| 본편(맵 이동·대화·조사·판정·전투·엔딩) | `Assets/Scenes/Main.unity` | 설정창 `settingManager.GoToMain()`, 엔딩 `FadingBackGround.GoToMain()` 둘 다 `DataBaseManager.instance.ResetData()` 후 `LoadScene("Insert")` |

- 씬 이름 `SetCharacter`(코드) vs 폴더 `SetCharcter`(오타). 이름 바꾸지 말 것.
- 본편 루프: `MapManager`(장소 이동, `PotalPoint`·`ChangeMapButton`) + `TimeManagere`(시간·이벤트 진행, 845줄+) → `InteractionController`(장소별 대화 분기, 3200줄+) → `DialogManager`(대사 출력, Text Animator) / `selectionUIManager`(선택지) → `Rollet`(판정 룰렛) / `BattleManager`(전투, 같은 Main 씬 안에서 UI 전환) → 엔딩(`End_Look_Judge`, `DataBaseManager.ending*` 플래그) → `FadingBackGround.DemoEndFadeIn()`(데모 엔딩 메뉴).
- 전투·대화는 씬 전환이 아니라 Main 씬 내 오브젝트 활성화로 진행.
- 진행 상태는 `DataBaseManager`의 정적 필드(`isOpenUi`, `isDirecting`, `isRollet`, `potalWait` 등)로 관리. UI 열기 전 이 플래그들을 전부 검사하는 패턴(`settingManager.OpenSetting`).

## 3. 코드 지도 (`Assets/Script/`)

| 폴더 | 역할 | 핵심 클래스 |
|---|---|---|
| `Main` | 타이틀 메뉴, 캐릭터 생성 진입 | `ManageMain`, `SetCharacter_` |
| `setStat` | 캐릭터 생성 UI(스탯·기능 배분·설명 툴팁) | `setStat`, `setSkill`, `changeStatPage`, `SkillMark`, `statExplain`, `str/dex/intSkillExplain` |
| `Character` | 플레이어·카메라 | `Player`, `CameraManager`, `CamReMove` |
| `Map` | 장소 이동·시간·페이드·연출 오브젝트 | `MapManager`, `TimeManagere`, `FadingBackGround`, `PotalPoint`, `ChangeMapButton`, `Directing_Object`, `Intel_ObtoUI` |
| `dialogue` | 신 대화 시스템. CSV 파싱→대사 출력→선택지 | `DialogDatabaseManager`(장소별 CSV 선택), `DialogPaser`(`Resources.Load<TextAsset>`), `Dialog`/`DialogEvent`(데이터), `DialogManager`(출력, Text Animator), `InteractionController`, `interactionEvent`, `selectionUIManager`, `SelectionButton`, `SpriteManager` |
| `old_Dialog` | 구 대화/전역 상태. **이름은 old 지만 현역**: 전역 정적 플래그 | `DataBaseManager`(`DatabaseManager.cs`, 정적 플래그·`ResetData`), `End_Look_Judge`(엔딩 판정), `DialogAutoClose` |
| `IntelUI` | 수사 수첩(인물·장소·사건 정보) | `IntelManager`, `IntelCharacterDetail`, `IntelPlaceDetail`, `IntelEventDetail`, `*ButtonManage`, `IntelFloating`, `Test` |
| `BillowUI` | 키워드·UI 매니저 | `BillowUIManager`, `KeyWordManager`(K7(6f896de)에서 제거, 대체: KeywordBook/KeywordPicker) |
| `Rollet` | 판정 룰렛 | `Rollet`, `BattleRollet`, `RolletTest`(디버그) |
| `Battle` | 턴제 전투 | `BattleManager`(DOTween 130회), `BattlePlayer`, `EnemyClass`, `InSewerEnemy`, `BattleItemManager`, `PlayerActionButton`, `EnemySelectButton`, `Battle/Detail/*`(검술·회피 등 행동 상세), `BattleCamera/BattleCameraMove` |
| `UI` | 공용 UI: 아이템·상점·설정·사운드·튜토리얼 | `SoundManager`, `settingManager`, `SIS_UIManager`(스탯·인벤), `SIS_Debuff`, `ShopUI`, `ItemButton`, `KeyItemBox`, `UseItemBox`, `SelectedItemBox`, `KeywordButton`(K7(6f896de)에서 제거, 대체: KeywordBook/KeywordPicker), `TutorialBox`, `UpperUiChecker`, `CheckBack`, `LightBulbContorol` |
| `DirectringManager`(오타) | 스토리 연출 | `DirectingManager` |
| `Asset` | 잡다: 인트로, 튜토리얼, 자간, 안전금고 등 + **대사 CSV** | `Intro`, `TutoralManage`, `BattleSymbolManager`, `SafeChecker`, `peopleCheck`, `LetterSpacing`, `Fixed`, `EnemySpriteChanger` |
| `Asset/Resources` | 대사 CSV 15개(`1st_*.csv`, `2nd_NoonEvent`, `InSewer`, `SewerOffice`, `SCOW_SCV`) | 파일명이 `DialogDatabaseManager`에서 문자열로 하드코딩됨. 이름 변경 금지 |
| `_` | 서드파티(`_/Stuff/CodeMonkey`)·`_HealthSystem`. 읽지 않음 | - |

## 4. 세이브·현지화

### 4-1. Easy Save 3 — 키 0개 (중요)
- `Assets/Script/**`·`Assets/` 전체(Plugins 제외) 에서 `ES3.`·`PlayerPrefs` 호출 **없음**. `ES3AutoSaveMgr` 도 씬에 없음(grep). 현재 코드에는 저장하는 세이브 키가 **없다**. 진행 상태는 `DataBaseManager` 정적 필드뿐이라 앱 종료 시 사라지고, 타이틀 복귀 시 `ResetData()`.
- 프로젝트에 ES3 플러그인(`Assets/Plugins/Easy Save 3`)만 있음. 출시판에 저장 기능이 있었는지는 미확인(사용자 확인 필요). 세이브 기능을 새로 넣으면 키는 추가만 하고 이름은 고정.
- 세이브에 필요한 상태 후보: `old_Dialog/DatabaseManager.cs` 의 정적 필드 전부(엔딩·획득품·디버프 플래그, `enemyDeathCount`, `posionDebuff` 등) + `setStat`·`SIS_UIManager` 의 스탯·인벤. 구현은 설계 문서부터.

### 4-2. I2 Localization — 코드 미사용
- 코드(`Assets/Script`)에 `LocalizationManager`·`I2.Loc` 호출 **없음**, 씬·프리팹에 `Localize` 컴포넌트도 없음(grep). 대사·UI 문구는 한글 하드코딩 + CSV(`Asset/Resources/*.csv`).
- 에셋 `Assets/Resources/I2Languages.asset` 에 언어 4개: English, Japanese, Chinese (Simplified), Korean. 용어 키 목록은 통독하지 않았음(미확인, 용어 수는 asset 에서 확인 가능).
- 새 문구는 새 키 추가만, 기존 키 변경 금지(출시판 호환 규칙).

## 5. 싱글턴·매니저, 트윈·텍스트 연출
- 패턴: `public static X Instance`(또는 소문자 `instance`) + `Awake`에서 중복이면 `Destroy`, 아니면 `DontDestroyOnLoad`. 지연 생성 아님, 씬 배치 전제.
- `DontDestroyOnLoad` 유지: `BillowUIManager`, `MapManager`, `FadingBackGround`, `DialogManager`, `InteractionController`, `selectionUIManager`, `IntelManager`, `IntelCharacterDetail`, `settingManager`, `SoundManager`, `End_Look_Judge`, `DataBaseManager`.
- 씬 로컬 싱글턴: `KeyWordManager`(K7(6f896de)에서 제거, 대체: KeywordBook/KeywordPicker), `BattleManager`, `BattleItemManager`, `BattleCameraMove`, `CameraManager`, `DirectingManager`, `Rollet`, `BattleRollet`, `TimeManagere`, `ShopUI`, `SIS_UIManager`, `DialogDatabaseManager`.
- DOTween(`using DG.Tweening`) 16개 파일: `BattleManager`(압도적), `FadingBackGround`, `SetCharacter_`, `setSkill`, `ManageMain`, `DialogManager`, `PotalPoint`, `MapManager`, `TimeManagere`, `DirectingManager`, `SoundManager`, `Intro`, `CameraManager`, `BattleCameraMove`, `EnemySelectButton`, `End_Look_Judge`. 대부분 페이드(`DOFade`)+`OnComplete`로 씬 전환/단계 진행.
- Text Animator(`Febucci.UI`): `DialogManager`(`TextAnimatorPlayer textAnimatorPlayer`)에서만 사용. DamageNumbersPro 코드 사용은 `Assets/Script`에서 미발견.

## 6. 코드 부채
- 거대 클래스: `InteractionController` 3200줄+, `TimeManagere` 845줄+, `BattleManager`. 규칙(500줄) 크게 초과.
- 전역 정적 bool 플래그 100여 개(`DataBaseManager`)에 상태 전부 의존. 세이브·리셋·리팩터 걸림돌. UI 열림 검사가 8개 플래그 AND 로 복제되어 있음.
- 문자열 하드코딩: 씬 이름, CSV 이름(`"1st_DetectiveOffice"` 등), 장소 분기.
- 레거시 `Input.Get*`: 20개 파일 68건(`RolletTest` 19건 등). 큐 INPUT-1 대상.
- 오타·혼재 이름: `TimeManagere`, `DialogPaser`, `DirectringManager`, `SetCharcter`, `old_Dialog`(현역), 소문자 클래스(`setStat`, `interactionEvent`). 이름 바꾸면 씬 직렬화 깨짐 주의(`FormerlySerializedAs`).
- 일부 .cs(`FadingBackGround`, `setSkill`, `settingManager` 등)가 UTF-8 아님(레거시 인코딩) — Edit/Write 거부됨, 수정 시 인코딩 유지 필요.
- 디버그 잔재: `RolletTest`, `IntelUI/Test.cs`, `DataBaseManager.isTest`.
- 현지화·세이브 미사용 상태에서 플러그인만 포함(빌드 크기).
