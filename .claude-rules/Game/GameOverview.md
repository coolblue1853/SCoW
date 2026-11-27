# SCoW 게임 개요

> 엔진: Unity 6000.4.0f1 / URP 17.4(11-21 업그레이드, 태그 `pre-unity6` 이 2021.3 판). TMP 는 ugui 2.0 통합판, 셰이더는 `Assets/Font/TextMesh Pro/Shaders`.

> 2026-11-20 조사(코드·씬 grep). 확인 못 한 것은 "미확인"으로 표시. 씬 안 컴포넌트 배치는 `*.unity` 통독 금지라 일부 미확인.

## 1. 게임 한 줄
크툴루의 부름(CoC) 기반 2D 탐정 TRPG. 판정은 1d100 기능치, 정신력(SAN)·디버프(공포증 등), 전투(심해인 등), 심문·조사 대화, 엔딩 분기. 1막 1일차~2일차 낮 이벤트 + 하수도(`InSewer`). 출시 플랫폼은 미확인.

## 2. 플레이 흐름
빌드 씬 3개(`ProjectSettings/EditorBuildSettings.asset`): `Insert`, `SetCharacter`, `Main`. 씬 전환은 `SceneRouter.Go(SceneIds.*)` 한 곳(P5).

| 단계 | 씬 | 전환 코드 |
|---|---|---|
| 타이틀 | `Assets/Scenes/Insert.unity` | `ManageMain`(새 게임·이어하기 `SlotMenuView`) → `SceneRouter.Go(SceneIds.SetCharacter)` |
| 캐릭터 생성(스탯→기능 배분) | `Assets/Scenes/SetCharcter/SetCharacter.unity` | `setSkill` → `SceneRouter.Go(SceneIds.Main)`, 입력은 `CharacterCreateController`(Core `CharacterCreation`) |
| 본편(맵 이동·대화·조사·판정·전투·엔딩) | `Assets/Scenes/Main.unity` | 설정창·엔딩이 `GameRoot.NewGame()` 후 `SceneRouter.Go(SceneIds.Insert)` |

- 씬 이름 `SetCharacter`(코드) vs 폴더 `SetCharcter`(오타). 이름 바꾸지 말 것.
- 본편 루프: `MapManager`(장소 이동, `PotalPoint`·`ChangeMapButton`) + `WorldBinder`(상태 → 씬 오브젝트 동기화) → `InteractionController`(장소 진입 → `StoryRuntime` 데이터 주도 대사) → `DialogManager`(출력) / `selectionUIManager`(선택지) → `Rollet`(판정) / `BattleManager`+`PartyBattleDirector`(전투, 같은 Main 씬 안) → 엔딩(`End_Look_Judge`).
- 전투·대화는 씬 전환이 아니라 Main 씬 내 오브젝트 활성화로 진행.
- 진행 상태 원본은 `GameRoot.State`(`GameState`, 키는 `FlagKeys`). `DataBaseManager`(`old_Dialog`)는 파사드 + 일시 정적 필드(`isDirecting` 등)만 남은 상태이고, UI 열림 검사는 `UiGate`/`UiStack` 으로 통일됐다.

## 3. 코드 지도 (`Assets/Script/`)

의존 방향: `Core`(순수 C#, `SCoW.Core.asmdef`, 엔진 비의존, EditMode 테스트 대상) 를 `App`·`Data`·씬 MonoBehaviour 가 쓴다. 규칙·공식·상태 기계는 Core 에 두고, 씬 연결만 바깥에 둔다.

### 3-1. Core (순수 C#)
| 폴더 | 역할 | 핵심 |
|---|---|---|
| `Core/State` | 진행 상태·동료 상태·읽은 기록 | `GameState`, `FlagKeys`, `PartyState`, `PartyAssist`, `PartyBattleBridge`, `ReadLog` |
| `Core/Rules` | CoC 판정·SAN·생성·전투 엔진 | `IRng`, `DiceExpr`, `SkillCheck`, `SanCheck`, `CharacterCreation`, `BattleEngine`·`BattleField*`·`BattleTimeline`, `PartyBattleRules`, `CompanionDef`, `EnemyDef` |
| `Core/Story` | 데이터 주도 대화 | `StoryCsvParser`, `StoryScript`, `DialogueRunner`, `StoryRuntime`, `StoryResolver`, `Condition`/`Effect`, `Backlog`, `TextSpeed`, `ObjectiveCatalog`, `PlaceDef` |
| `Core/Keyword` | 키워드 수첩 | `KeywordCatalog`, `KeywordBook`, `KeywordAsk`, `KeywordBudget`, `KeywordNotice`, `IntelUpperMap` |
| `Core/Sewer` | 하수도 그래프·조사·조우·조명 | `SewerGraph`, `SewerNav`, `SewerState`, `SewerEncounters`, `SewerRewards`, `SewerSight`, `SewerRest`, `SewerPartyRules`, `SewerLegacy` |
| `Core/World` | 시간·월드 규칙 | `WorldClock`, `WorldRules`, `IWorldScene` |
| `Core/Save` | 세이브 형식·마이그레이션 | `SaveCodec`, `SaveMigrator`, `SaveTypes`, `SettingsData`, `SlotSummary`, `ResumeRules`, `EndingRecord` |
| `Core/Ui` | UI 규칙(스택·막힘·포커스·툴팁) | `UiStack`, `BusyRules`, `DirectionalFocus`, `HintStack`, `BindingRules`, `SettingsChoices`, `VitalsTracker`, `BattleCue`, `BattleSelectNav`, `BattleTooltipText`, `ScrollMapping` |

### 3-2. App · Data · Input (엔진 연결층)
| 폴더 | 역할 | 핵심 |
|---|---|---|
| `App` | 앱 뼈대 | `GameRoot`(상태 보유·`NewGame`·저장 주입), `Persist`(DontDestroyOnLoad 단일 지점), `SceneRouter`, `UiGate`, `WorldBinder` |
| `Data` | ES3 저장·설정·엔딩·읽은 기록 | `SaveService`, `SaveFlow`, `SettingsService`, `EndingService`, `ReadLogService`, `SaveText` |
| `Input` | New Input System 래퍼 | `GameInput`(+`.Rebind`), `Store/BindingStore`(`SCoW.InputStore.asmdef`) |
| `Story` | 스토리 데이터 로드 | `StoryDatabase` |

### 3-3. 씬 MonoBehaviour
| 폴더 | 역할 | 핵심 클래스 |
|---|---|---|
| `Main` | 타이틀 메뉴 | `ManageMain`, `SetCharacter_` |
| `setStat` | 캐릭터 생성 UI | `setStat`, `setSkill`, `CharacterCreateController`, `changeStatPage`, `SkillMark`, `statExplain` |
| `Character` | 플레이어·카메라 | `Player`, `CameraManager`, `CamReMove` |
| `Map` | 장소 이동·페이드·연출·하수도 | `MapManager`, `TimeManagere`(`WorldBinder` 를 붙이고 인스펙터 참조를 보유), `FadingBackGround`, `PotalPoint`, `ChangeMapButton`, `MapFocusNav`, `Directing_Object`, `Intel_ObtoUI`, `SewerFlow`, `SewerRoomApplier`, `SewerInvestigatePoint` |
| `dialogue` | 대사 출력·선택지·장소 진입 | `InteractionController`(+`.SceneRefs`), `DialogManager`, `DialogDatabaseManager`, `DialogPaser`, `selectionUIManager`, `SelectionButton`, `SpriteManager`, `interactionEvent`, `Dialog`, `DialogHudStyle`, `DialogNextMarker` |
| `old_Dialog` | **이름은 old, 현역**: 일시 정적 필드·파사드·엔딩 판정 | `DataBaseManager`(`DatabaseManager.cs`), `End_Look_Judge` |
| `IntelUI` | 수사 수첩(인물·장소·사건) | `IntelManager`, `IntelCharacterDetail`, `IntelPlaceDetail`, `IntelEventDetail`, `*ButtonManage`, `IntelFloating`, `IntelTodoTab`, `NotebookScroll` |
| `BillowUI` | UI 매니저 | `BillowUIManager` |
| `Rollet` | 판정 룰렛 | `Rollet`, `BattleRollet`, `UnityRng`(Core `IRng` 구현), `RolletTest`(디버그 치트, Main 씬 부착 — 삭제는 씬 편집 필요) |
| `Battle` | 전투 | `BattleManager`(4000줄, 분리 후보), `BattlePlayer`, `InSewerEnemy`, `BattleItemManager`, `PlayerActionButton`, `EnemySelectButton`, `BattleResetUI`, `Detail/*`(검술·사격·회피·재장전 상세), `BattleCamera/*` |
| `Battle/Party` | 동료 3칸 대열 전투 | `PartyBattleDirector`, `PartyBattleView`(+`.Input`), `PartyBattlePanel`, `BattleEventPlayer` |
| `UI` | 공용 UI(HUD·설정·슬롯·툴팁·키워드·사운드) | `UxOverlay`, `SettingsView`(+`.Keys`), `settingManager`, `SlotMenuView`, `SoundManager`, `SIS_UIManager`, `ShopUI`, `KeywordPicker`, `ObjectiveBoard`, `TimeHudView`, `SewerSightView`, `TooltipView`, `HintBar`, `BacklogView`, `SaveToast`, `DeathMenuView`, `UiText` |
| `DirectringManager`(오타) | 스토리 연출 | `DirectingManager` |
| `Asset` | 잡다: 인트로·튜토리얼·적 스프라이트·안전금고 + **대사 CSV** | `Intro`, `TutoralManage`, `BattleSymbolManager`, `SafeChecker`, `peopleCheck`, `LetterSpacing`, `EnemySpriteChanger` |
| `Asset/Resources` | 대사 CSV(`1st_*.csv`, `2nd_NoonEvent`, `InSewer`, `SewerOffice`, `SCOW_SCV`) | 파일명이 로더에서 문자열 참조됨. 이름 변경 금지 |
| `_` | 서드파티(`_/Stuff/CodeMonkey`)·`_HealthSystem`. 읽지 않음 | - |

기타: `Assets/Tests/EditMode`(`SCoW.Tests.EditMode`, 489개), `Assets/Editor`(대사·월드 동치 골든 도구).

## 4. 세이브·현지화

### 4-1. Easy Save 3
- ES3 호출은 `Data/*`(`SaveService`·`SettingsService`·`EndingService`·`ReadLogService`·`SaveText`)에만 있다. 키는 **추가만, 이름 변경 금지**(출시판 호환). 형식은 `Core/Save/SaveCodec`·`SaveMigrator` 가 버전 관리.
- 슬롯 저장·불러오기·자동 저장·설정 저장·엔딩 기록·읽은 기록(P4 이후). 저장 허용 지점은 장소 이동 직후의 안정 상태.

### 4-2. I2 Localization
- 코드는 아직 `LocalizationManager` 를 쓰지 않는다. 문구는 한글 하드코딩 + CSV, 영문 고정 표기에는 `// TODO(I2)` 표시. 에셋 `Assets/Resources/I2Languages.asset` 에 언어 4개(English, Japanese, Chinese (Simplified), Korean).
- 새 문구는 새 키 추가만, 기존 키 변경 금지.

## 5. 싱글턴·매니저, 트윈·텍스트 연출
- 패턴: `public static X Instance` + `Awake` 에서 중복이면 `Destroy`, 아니면 `Persist.Keep(gameObject)`. DontDestroyOnLoad 호출은 `App/Persist.cs` 한 곳. 지연 생성 아님, 씬 배치 전제.
- 상주(Persist) 오브젝트: `BillowUIManager`, `MapManager`, `FadingBackGround`, `DialogManager`, `InteractionController`, `selectionUIManager`, `IntelManager`, `IntelCharacterDetail`, `settingManager`, `SoundManager`, `End_Look_Judge`, `UxOverlay` 등. 상태 객체 `GameRoot` 는 씬 오브젝트가 아니라 정적 인스턴스.
- 씬 로컬 싱글턴: `BattleManager`, `BattleItemManager`, `BattleCameraMove`, `CameraManager`, `DirectingManager`, `Rollet`, `BattleRollet`, `TimeManagere`, `ShopUI`, `SIS_UIManager` 등.
- DOTween: `BattleManager`(압도적), `FadingBackGround`, `SetCharacter_`, `setSkill`, `ManageMain`, `DialogManager`, `PotalPoint`, `MapManager`, `DirectingManager`, `SoundManager`, `Intro`, `CameraManager`, 신규 View 들. Text Animator 는 `DialogManager` 만.

## 6. 코드 부채
- 거대 클래스: `BattleManager` 4000줄, `DataBaseManager` 860줄(정적 필드), `InteractionController.SceneRefs` 560줄. 500줄 기준 초과.
- 오타·소문자 클래스명은 씬 직렬화 위험 때문에 개명하지 않았다. 후보 목록은 `Architecture.md` P16 행 아래.
- 레거시 `Input.Get*` 는 0(전부 New Input System). 일부 .cs 는 UTF-8 이 아니거나 BOM 이 섞여 있다 — 수정 시 기존 인코딩·개행 유지.
- 디버그 잔재: `RolletTest`(Main 씬 부착, 출시판 노출 전 제거 필요).
- P16 에서 호출·참조 0 인 메서드·필드·주석 처리 코드를 삭제했다(`*IntelInt` 정적 필드 34개 등). 씬 직렬화가 걸린 public 필드는 남겼다.
