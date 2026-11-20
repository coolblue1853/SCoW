# SCoW 코드 전면 재작성 설계 (스트랭글러 이행)

작성 2026-11-21. 코드 수정 없음, 설계만. 근거: `Game/GameOverview.md`, `Docs/Playtest/ImproveAudit.md`(이하 Audit), `Docs/Playtest/InputMigration.md`(이하 Input). 수치는 grep 실측(줄 단위 매치라 "건"은 매치된 줄 수). 확정 못 한 것은 "추정/미확인".
전제: Unity 6000.4 업그레이드 완료, ImproveAudit P0 버그(치트 `#if` 가드, `ResetData` 누락, 피해 5 고정)는 별도 큐 **FIX-P0** 에서 먼저 끝난다. 이 문서의 P0 는 그 다음이다.
원칙: 출시작이다. 새 구조는 옆에 짓고, 옛 코드는 파사드로 얇게 만들며 한 단계씩 갈아 끼운다. 단계마다 타이틀→캐릭터 생성→본편→엔딩이 이어져야 한다(스모크 정의는 5절).

## 1. 현재 구조 실측

### 1-1. 전역 정적 상태 — `old_Dialog/DatabaseManager.cs` (클래스명 `DataBaseManager`)
- `public static` 필드 **560개**: bool 419, int 127, string/float 14. 이를 참조하는 줄이 56개 파일에 **4325줄**.
- 많이 참조하는 파일(줄 수): `setSkill` 495, `InteractionController` 316, `BattleManager` 350, `KeyWordManager` 272, `DirectingManager` 268, `IntelPlaceDetail` 252, `RolletTest` 247, `DialogManager` 212, `IntelEventDetail` 198, `Intel_ObtoUI` 180, `SIS_UIManager` 166, `MapManager` 165, `IntelCharacterDetail` 163, `Rollet` 138.
- 성격이 섞여 있다(분류가 이행의 핵심, 3절 표):
  - 진행 플래그(영구): `maeveFirstDialog`, `intelQuestHouse1`, `intelFishySmell1~4`, `ending*`, `sewer*`, `isDebuff*` 등 bool 대부분.
  - 카운터·수치(영구): `timeCount`, `enemyDeathCount`, `posionDebuff`, `sewerEnemyCounter`, `battleEnemyCount`, `aidenKeywordCount`.
  - 캐릭터: `str intl dex hp mp san luk weal nowHP nowSan`, 기능 포인트 18개(`martialArtsPoint`~`disguisePoint`).
  - 인벤: `bullet shotgun rifle revolver smallPistol bat axe molotov dagger pistolAmmo… firstaidkit bandages painkillers`, `nowItem`, `battleWeapon`.
  - 위치·키워드: `nowPlace`, `potalPlace`, `keywordMain/Upper/Downer`, `condition`, `selectObject`, `lookObject`, `nowSelecter`.
  - 수첩 UI 상태: `nowPage*`·`buttonCount*` 약 60개(인물 7·사건 13·장소 14 각 2종), `gainCharacterInt/gainEventInt/gainPlaceInt`. 영구는 `gain*` 와 획득 플래그뿐, 나머지는 화면 상태.
  - 설정: `soundVolume`, `sfxVolume`, `textDelay`.
  - 일시(저장 금지) 플래그: `isOpenUi`, `isDirecting`, `isRollet`, `potalWait`, `isItemUI`, `skipActive`, `isTest`, 각종 `*SanCheck` 대기 플래그 일부.
- `ResetData()`(734줄~)가 560개를 수동 대입으로 되돌린다. 누락이 구조적으로 생긴다(Audit A-2). 상태 객체화하면 `new GameState()` 한 줄로 끝난다.
- UI 열림 검사가 8개 플래그 AND 로 복제(`settingManager.OpenSetting` 등, Audit D-3·B-4).

### 1-2. 거대 클래스
| 파일 | 줄 | 책임 덩어리(추정 포함) |
|---|---|---|
| `dialogue/InteractionController.cs` | 3228 | 1~561 인스펙터 필드(장소별 `interactionEvent` 오브젝트 참조 수백 개, `GetComponent` 519회) · 562~1892 장소별 `StartXxx(string setDialog)` 14개(`InSewerDialog` `StartSewerOffice` `Start2ndNoonEvent` `Start2ndDetectiveOffice` `StartSafeLook` `Start1stDetectiveOffice/ClientsHouse/DailyNews/University/Riverside/Hospital/Policeoffice/Slum/Bar`) · 1893~2599 `KeywordDialog`(약 700줄) · 2600~ `BattleDialog` · 2611~3198 `RetrunDialogResult`(판정 결과 분기 약 590줄) · 3199~ `SwinCheckAgain`, `Awake` |
| `Map/TimeManagere.cs` | 856+ (비 UTF-8) | 142~714 하수도 맵 생성(`MakeSewer`·`MakeSewerMapNomal/Enemy`·`SewerByRiverSide`, 약 570줄) · 63~138 `MakeCageLand/HouseLand/SighnLand` · 715~855 `Update` 가 매 프레임 `timeCount%4` 와 플래그로 오브젝트 `SetActive`·`localPosition` 갱신(월드 상태 동기화) |
| `Battle/BattleManager.cs` | 1800+ (비 UTF-8) | 턴 진행·적 AI·데미지·DOTween 연출이 한 클래스(DOTween 573줄). 적별 복제 코드(Audit D-5). `Random.Range` 주사위가 연출 안에 박혀 있음 |
| `old_Dialog/DatabaseManager.cs` | 1200+ (비 UTF-8) | 전역 상태 + `ResetData` |

### 1-3. 하드코딩된 CSV·장소 분기
- CSV 이름 문자열 리터럴 **63줄/12개 파일**. 최다 `dialogue/DialogDatabaseManager.cs`(29줄). `Update` 가 `nowPlace` 문자열을 if-else 로 비교해 CSV 이름을 고른다(`"DetectiveOffice"→"1st_DetectiveOffice"`, `"Client'shouse"` 는 `timeCount<6` 이면 `1st_Client'sHouse` 아니면 `2nd_NoonEvent`).
- `nowPlace ==` 비교 **119줄/9개 파일**: `MapManager` 40, `Intel_ObtoUI` 24, `DialogDatabaseManager` 23, `SoundManager` 16(BGM 선택), `DirectingManager` 5, `PotalPoint` 5 등. 장소 ID 가 곳곳에 문자열로 퍼짐.
- CSV 15개(`Asset/Resources/`): 열 `ID,캐릭터이름,대사,<메모>` 뿐. 선택지·조건·효과·스프라이트는 CSV 에 없다. `interactionEvent` 가 인스펙터의 `(line.x, line.y)` 행 범위로 CSV 를 자르고(`DialogDatabaseManager.GetDialog(x,y)`), **분기 로직은 전부 `InteractionController` 의 C# if 문**이다.
- `dialogue/DialogPaser.cs`: `Split(',')`·null 무방비(Audit A-3). 새 파서가 필요.

## 2. 목표 구조

### 2-1. 어셈블리·폴더
```
Assets/Script/
  Core/            SCoW.Core.asmdef   (noEngineReferences=true, autoReferenced=true)
    State/         GameState, CharacterSheet, Inventory, SettingsData, SaveMeta, ReadLog
    Story/         StoryScript, StoryNode, Condition, Effect, ConditionParser, StoryCsvParser, DialogueRunner, Backlog
    Rules/         IRng, DiceExpr, SkillCheck, SanCheck, DamageCalc, BattleEngine, EnemyDef
    Time/          WorldClock(timeCount→일·시간대)
  App/             (Assembly-CSharp 유지) GameRoot, SceneRouter, UiStack, Services, Bootstrap
  Data/            (Assembly-CSharp) SaveService(ES3), SettingsService, StoryDatabase(Resources 로더)
  Input/           (Assembly-CSharp) GameInput (INPUT-1 그대로 + 추가 액션)
  Story/           (Assembly-CSharp) DialogueView(옛 DialogManager 후신), ChoiceView, BacklogView, WorldBinder
  Battle/ Rollet/ UI/ IntelUI/ ...  (기존 폴더, 단계적으로 View 만 남김)
Assets/Tests/EditMode/   SCoW.Tests.EditMode.asmdef (참조: SCoW.Core, UnityEngine.TestRunner, UnityEditor.TestRunner, optional nunit)
Assets/GameData/Story/   *.csv (대사 본문, 현 `Asset/Resources/*.csv` 는 이행 완료 때까지 그대로)
Assets/Resources/Story/  *.json (장면/노드 데이터, Resources 로드)
```
- **함정(필수)**: 새 폴더 `Assets/Script/Dialogue/` 는 기존 `dialogue/` 와 윈도우에서 같은 폴더다. 새 이름은 `Story/` 를 쓴다. 또 `Input/` 폴더·네임스페이스에 `Input` 단어가 들어가면 `UnityEngine.Input` 과 충돌하므로 클래스는 전역 `GameInput`, 네임스페이스 만들지 않는다.
- `SCoW.Core` 만 asmdef 로 분리한다. 기존 `Assets/Script/**` 는 asmdef 가 없어 Assembly-CSharp 이고, Assembly-CSharp 은 autoReferenced asmdef 를 자동 참조한다. 반대 방향은 불가라서 Core 는 레거시·ES3·DOTween·UnityEngine 을 못 본다. 이것이 의도다.
- ES3·DOTween·Text Animator 는 `Assets/Plugins` 의 미분리 어셈블리(추정, 에디터에서 asmdef 유무 확인)라 asmdef 코드는 못 쓴다. 그래서 Core 에 ES3 를 넣지 않고 `Data/SaveService`(Assembly-CSharp)가 어댑터가 된다. 앞으로 `SCoW.Runtime` asmdef 로 올릴 생각이면 ES3·DOTween 에 asmdef 가 있는지 먼저 확인(5절 위험).
- 네임스페이스 `SCoW.Core.*`. 전역 네임스페이스의 `Dialog`·`DialogEvent`·`DataBaseManager` 와 이름이 안 겹치게 새 타입은 `Story*`, `GameState` 등으로 짓는다.

### 2-2. 진행 상태 객체 (저장 가능한 순수 데이터)
```csharp
namespace SCoW.Core.State {
  [System.Serializable]
  public sealed class GameState {
    public int SchemaVersion = 1;                 // 저장 호환용, 마이그레이션 체인 키
    public CharacterSheet Character = new();      // str..luk, hp/mp/san, nowHP/nowSan, 기능 포인트 Dictionary<SkillId,int>
    public Inventory Inventory = new();           // Dictionary<ItemId,int>, EquippedWeapon
    public int TimeCount;                         // 기존 timeCount 의미 그대로(%4 시간대, 낮/밤)
    public string PlaceId;                        // 기존 nowPlace 문자열 값 그대로 (PlaceIds 상수)
    public HashSet<string> Flags = new();         // true 인 플래그 키만 보관. 없으면 false → ResetData 누락 불가
    public Dictionary<string,int> Counters = new();   // enemyDeathCount, posionDebuff, gainEventInt ...
    public Dictionary<string,string> Texts = new();   // keywordMain 등 (영구인 것만)
    public IntelBook Intel = new();               // 획득한 인물/사건/장소 ID 목록
    public bool GetFlag(string k); public void SetFlag(string k, bool v);
    public int GetCounter(string k); public void SetCounter(string k, int v);
  }
}
```
- 키 규약: 기존 필드명을 **그대로** 키로 쓴다(`"maeveFirstDialog"`). 근거: 파사드 자동 생성이 가능하고 대사 JSON·세이브 키가 코드명과 1:1. 오타 키(`posionDebuff`)도 유지. 상수는 `FlagKeys.cs` 에 생성 스크립트가 만든다(리터럴 중복 방지).
- 저장하지 않는 것: UI 열림·연출·대기 플래그, `nowPage*`·`buttonCount*`(화면 상태, 로드 후 재계산). 이들은 `RuntimeState`(비저장, `GameRoot` 가 수명 관리)로 이동.
- 설정은 `SettingsData`(음량·텍스트 속도·자동 진행 대기·스킵 모드·해상도·전체화면·언어) 별도 객체, 별도 파일.
- 읽은 대사 기록은 `ReadLog`(`HashSet<string>` "scriptId#nodeId") 별도 객체, 전역(슬롯 무관).

### 2-3. 저장 규약 (ES3, 키는 추가만)
| 파일 | 키 | 타입 | 설명 |
|---|---|---|---|
| `slot_auto.es3`, `slot_1.es3`~`slot_3.es3` | `meta` | `SaveMeta` | `SchemaVersion`, `SavedAtUtc`, `PlayTimeSec`, `PlaceId`, `TimeCount`, 챕터 라벨. 슬롯 목록 화면은 meta 만 읽음 |
| 같은 파일 | `state` | `GameState` | 본체 |
| `settings.es3` | `settings` | `SettingsData` | 슬롯 무관 |
| `read.es3` | `readIds` | `HashSet<string>` | 읽은 대사. 슬롯 무관 |
- 슬롯 수·자동 저장 시점은 Audit M4 가정(자동 1 + 수동 3, 장소 이동·시간대 전환 시 자동) 채택.
- `SaveService.Load` 는 `meta.SchemaVersion` 을 읽어 `ISaveMigration`(`From1To2` …)을 순서대로 적용한 뒤 역직렬화. 한 번 낸 키·필드명은 바꾸지 않는다(삭제 대신 폐기 표시). 저장 도중 크래시 방지를 위해 `ES3Settings` 의 백업 파일 옵션 사용.
- 직렬화 대상은 순수 클래스·Dictionary·HashSet 만. `UnityEngine.Object`·델리게이트·`DataBaseManager` 참조 금지. `GameState` 는 EditMode 테스트에서 `Clone()`→`Equals` 왕복 검증(ES3 호출은 어댑터 테스트 PlayMode 에서).
- 현재 출시판 저장 키 0개(GameOverview 4-1)라 기존 세이브 호환 부담은 없다. 단 출시판에 ES3 저장이 있었다면(미확인, 미결 Q1) 그 파일명·키를 먼저 확인해서 충돌을 피한다.

### 2-4. 대화 데이터 주도
데이터 모델(Core, 순수 C#):
```csharp
public sealed class StoryScript { public string Id; public List<StoryTopic> Topics; public Dictionary<string,StoryNode> Nodes; }
public sealed class StoryTopic {              // 옛 InteractionController 의 setDialog 문자열 1개 = Topic 1개
  public string Id;                           // "Maeve_FirstDialog"
  public List<StoryCase> Cases;               // 위에서부터 첫 번째 맞는 Case 실행(if/else if/else 대체)
}
public sealed class StoryCase { public Condition When; public Effect[] Pre; public string NodeId; }
public sealed class StoryNode {
  public string Id; public string Speaker; public string TextKey /* csv id */; public string Sprite;
  public Effect[] OnEnter; public StoryChoice[] Choices; public string Next; public string CheckId /* 판정 */;
  public string OnSuccess; public string OnFail; public string OnCritFail;
}
public abstract class Condition { public abstract bool Eval(IStateReader s); }   // And/Or/Not/FlagIs/CounterCmp/TimeIs/PlaceIs/SkillAtLeast
public abstract class Effect { public abstract void Apply(IStateWriter s, IStoryHost host); } // SetFlag/AddCounter/AddSan/GiveItem/Goto/StartBattle/MovePlace/PlayBgm/Unlock Intel
```
- 조건 표기(문자열 DSL, `ConditionParser` 가 파싱, 테스트 필수): `flag:intelQuestHouse1 && time>=12`, `!flag:x`, `place:Bar`, `any(flag:a,flag:b)`, `counter:enemyDeathCount>=3`. 효과 표기: `set:maeveFirstDialog`, `add:san,-1`, `goto:node_id`, `check:observation,hard,ok_node,fail_node`, `battle:sewer_deepone`.
- 데이터 파일: `Assets/Resources/Story/<scriptId>.json`(장면 구조, 조건·효과) + CSV(대사 본문, 작가가 편집). 본문은 CSV `ID` 로 참조해 번역·수정이 구조와 분리된다. 새 CSV 파서는 RFC4180(따옴표·쉼표·CRLF·빈 줄·UTF-8 BOM) 지원, 파일 없음·열 부족은 오류 목록을 반환(예외 금지, `ParseResult.Errors`).
- 옛 CSV 의 `@` 표시·`^`(쉼표 대용)·`ⓖ(..)ⓦ` 텍스트 태그는 그대로 본문에 둔다. 해석은 View(Text Animator) 몫.
- 이행 방식(스트랭글러): 장소 하나씩 `StartXxx` 의 if 트리를 JSON Topic 으로 옮기고, 옛 메서드는 `StoryDatabase.TryRun(topicId)` 에 위임한 뒤 비운다. 위임 전후 같은 상태에서 같은 `Dialog[]` 가 나오는지 **동치 테스트**(에디터 도구가 상태 프리셋 N개로 옛 경로·새 경로를 돌려 첫/끝 줄·줄 수 비교).
- `TimeManagere.Update` 의 월드 동기화는 `WorldBinder`(데이터: `{when: Condition, target: 경로/이름, action: SetActive|MoveTo, value}` 목록) + 상태 변경 이벤트 시에만 재평가(매 프레임 금지). 하수도 맵 생성(142~714)은 장면 데이터화가 어려우므로 **마지막 단계**에서 프리팹화(추정 규모 큼).
- 장소→CSV 매핑은 `PlaceDef`(데이터: `Id`, `Scripts[]{When, ScriptId}`, `Bgm`)로 `DialogDatabaseManager`·`SoundManager`·`MapManager` 의 문자열 비교를 제거. `PlaceIds` 상수 클래스(값은 기존 `nowPlace` 문자열 그대로, `"Client'shouse"` 의 소문자 h 도 유지).

### 2-5. 판정·전투 규칙 (SCoW.Core.Rules, UnityEngine 참조 없음)
```csharp
public interface IRng { int Next(int minInclusive, int maxExclusive); }   // 실게임: UnityRng, 테스트: SequenceRng/SeededRng
public readonly struct DiceExpr { public static DiceExpr Parse(string s) /*"1d6+2"*/; public int Roll(IRng r); public int Min, Max; }
public enum SuccessLevel { CriticalFail, Fail, Regular, Hard, Extreme, CriticalSuccess }
public sealed class SkillCheck {
  public static CheckResult Roll(int skill, Difficulty d, int bonusDice, int penaltyDice, IRng r); // 1d100 = Next(1,101)
}
public static class SanCheck { public static SanResult Roll(int san, DiceExpr successLoss, DiceExpr failLoss, IRng r); }
public sealed class BattleEngine {                      // 턴제 규칙. 연출 없음
  public BattleEngine(BattleSetup setup, IRng rng);
  public IReadOnlyList<BattleEvent> Step(BattleAction playerAction);   // 이벤트 목록 반환 → View 가 DOTween 으로 재생
  public bool IsOver { get; } public BattleOutcome Outcome { get; }
}
```
- 현재 `Rollet.cs`(247~268 퍼센타일, `nomal/bad/good`)의 동작을 **먼저 그대로** 포팅한다(규칙 정정은 미결 M1·M2 확정 후 `RuleSet` 매개변수로). 골든 테스트: 시드 고정 수천 회 결과 분포·경계(기능치 1·50·99, 1·100 대응)를 고정.
- 피해식은 `DiceExpr` 문자열로 `EnemyDef`(JSON/ScriptableObject)에 둔다. `Random.Range(1,4)*5` 류를 한곳(`DamageCalc`)에서 처리하고 단위 테스트로 최소·최대 단언.
- `BattleManager`(DOTween 573줄)는 `BattleView` 로 축소: `BattleEngine` 이벤트를 받아 `Sequence` 구성, 모든 트윈 `SetLink`/`Kill`. 적별 복제 코드는 `EnemyDef` 데이터 + 공통 시퀀스로.

### 2-6. 장르 기본 기능
| 기능 | 위치 | 동작·결정 |
|---|---|---|
| 저장·불러오기 슬롯 | `SaveService`, `SlotMenuView` | 자동 1+수동 3, 슬롯 카드에 `SaveMeta`(장소·시각·플레이타임). 타이틀 "이어하기", 설정창 "저장/불러오기". 대사·연출·판정 중에는 수동 저장 금지(`UiStack`/`IsBusy`), 자동 저장은 상태 안정 시점 |
| 대사 백로그 | `Core/Story/Backlog`, `BacklogView` | 링 버퍼 200줄(화자·본문·선택한 선택지). `DialogueRunner` 가 줄 표시 때 push. 호출: 휠 위·`L`/패드 LT. 읽기 전용, Cancel 로 닫기. 세이브에는 최근 50줄만 포함(선택, 미결 Q4) |
| 스킵 | `DialogueRunner.SkipMode`(None/ReadOnly/All) | ReadOnly: `ReadLog` 에 있는 줄만, 안 읽은 줄·선택지·판정 앞에서 자동 정지. All: 전부. Ctrl 누르는 동안(홀드)/패드 RT 홀드 + 토글 버튼. 옛 `Skip()` 코루틴 재귀(Audit A-7) 폐기, 단일 틱으로 처리 |
| 자동 진행 | `DialogueRunner.Auto` | 대기 = `base + chars * perChar`(설정으로 조절, 기본 0.6s + 0.05s/자), 텍스트 타이핑 완료 후 계산. 선택지·판정·연출 앞에서 정지 |
| 텍스트 속도 | `SettingsData.TextCps` | 기존 `textDelay` 와 `waitForNormalChars=0.03f`(하드코딩)를 대체. 5단계(+즉시). Text Animator `TextAnimatorPlayer` 의 속도 값에 주입 |
| 설정 영구 저장 | `SettingsService` | 음량·속도·해상도·전체화면. 해상도 12분기는 `Screen.resolutions` 목록으로 대체(Audit B-3, C-7) |
- 키: 진행 = Interact/Advance(기존 E·좌클릭·패드 A). 추가 액션은 Input 문서의 `UI` 맵에 `Backlog`(휠 위·L·LT), `SkipHold`(Ctrl·RT), `AutoToggle`(Q·R스틱 클릭), `HideUi`(H·Y 아님: 패드 X 는 지도라 충돌 → 패드 미배정, 메뉴 버튼으로) 를 추가. 키 배치는 미결 Q5.

### 2-7. 입력 — `GameInput` (INPUT-1 흡수)
- `Input` 문서 2~4절을 **그대로** 채택(정적 래퍼, 조회형 `*Down`, 소비 안 함, 에셋 `Assets/Resources/Input/SCoWInput.inputactions`). 이 재작성은 그 위에 다음만 더한다.
  - 2-6 추가 액션 4개.
  - 디버그 맵은 `#if UNITY_EDITOR || DEVELOPMENT_BUILD` 로만 활성(FIX-P0 와 정합).
  - `GameInput` 은 View 계층만 호출. Core 는 입력을 모른다(`DialogueRunner.Advance()` 같은 메서드 호출).
  - **입력 컨텍스트**: `UiStack`(App) 이 열린 패널 스택을 보유. `GameInput.CancelDown` 은 스택 맨 위 패널만 닫는다(Audit C-3). `IsBusy` = `UiStack.AnyBlocking || RuntimeState.Directing || Rollet` 한 곳으로 모아 8플래그 AND 제거.

### 2-8. UI·로직 분리, 씬 전환·싱글턴
- 규칙: Core 와 `App` 서비스는 UI 를 모른다. View(MonoBehaviour)는 상태를 소유하지 않고 `GameRoot.State`(읽기)와 `DialogueRunner`/`BattleEngine`(명령)만 쓴다. 이벤트는 C# `event`, 구독은 `OnEnable`/`OnDisable` 쌍.
- `GameRoot`: 유일한 `DontDestroyOnLoad`. `GameState State`, `SettingsData Settings`, `ReadLog`, `DialogueRunner`, `SaveService`, `UiStack`, `RuntimeState` 를 보유하고 `GameRoot.Instance` 로 접근(이행 중에는 `DataBaseManager` 파사드가 같은 인스턴스를 가리킴). `NewGame()`, `LoadGame(slot)`, `ReturnToTitle()` 제공. `ReturnToTitle` 은 `State = new GameState()` 후 `SceneRouter.Go(SceneIds.Insert)` → 옛 `ResetData` 불필요.
- `SceneRouter.Go(string sceneId, bool fade)`: 페이드(`DOFade`, 트윈 `SetLink`)와 `LoadScene` 을 한곳에서. `SceneIds` 상수 = `"Insert" "SetCharacter" "Main"`(코드의 철자 유지, 폴더 오타 `SetCharcter` 유지). 흩어진 `LoadScene` 6곳과 `FadingBackGround` 의 중복 로직 흡수.
- 기존 DDOL 싱글턴 12개(`BillowUIManager MapManager FadingBackGround DialogManager InteractionController selectionUIManager IntelManager IntelCharacterDetail settingManager SoundManager End_Look_Judge DataBaseManager`)는 각 단계에서 (a) 상태는 `GameRoot` 로, (b) 객체는 씬 로컬 View 로 내리고 DDOL 을 제거. 씬 로컬 싱글턴은 `Awake` 에 등록, `OnDestroy` 에 해제(정적 참조 누수 방지).
- 정적 `DataBaseManager` 는 파사드(프로퍼티가 `GameRoot.State` 로 위임)로 남아 4325줄 호출을 한 번에 안 고쳐도 되게 한다. 호출처를 새 API 로 바꾸는 만큼 파사드 필드를 지우고 최종 단계에서 클래스째 삭제.

## 3. 상태 분류표 (P3 완료 11-21, 생성 스크립트로 분류)
| 구분 | 수 | 이행 | 내용 |
|---|---|---|---|
| 영구 bool | 393 | `GameState.Flags`(true 만 보관) → `FlagKeys` | 진행 플래그 전부(`maeveFirstDialog`, `intel*`, `ending*`, `isDebuff*`, `sewer*`, 디버프). 초기값 true 는 `bgmChangeChecker` 하나 |
| 영구 int | 59 | `GameState.Counters`(0 은 미보관) → `CounterKeys` | `timeCount`, `enemyDeathCount`, `posionDebuff`, 스탯, 기능 포인트(초기값 0 아님: `martialArtsPoint` 25 등 16개), 인벤 수량, `gain*Int` |
| 영구 string | 3 | `GameState.Texts`(빈 값 미보관) → `TextKeys` | `nowPlace`(초기 `DetectiveOffice`), `potalPlace`, `battleWeapon` |
| 일시 | 26 | 정적 필드 유지(이후 `RuntimeState`) | `isOpenUi isDirecting isRollet potalWait isItemUI skipActive isTest isOpenMap secondisDirecting storyDirecting setIntelCharDetail conntectMouse workSound isBattleRollet isSelect cancelJudge isKeyword isJudge isScene isPresentation isOption endDialogLine isActiveDialog1/2 selectionOn isFinish` |
| 화면 상태 | 76 | 정적 필드 유지(이후 각 View) | `nowPage*`·`buttonCount*` 68개, `condition selectObject lookObject nowSelecter nowItem keywordMain keywordUpper keywordDowner` |
| 설정 | 3 | 정적 필드 유지(P4 `SettingsData`) | `soundVolume`(0.5) `sfxVolume`(0.5) `textDelay`(0.07) |

- 애매한 것(`*SanCheck`, `battleSan`, `barMove`, `strDialogOn`, `isBattleBeforeDialog`, `afterBattle` 등 연출 대기 플래그, `battleWeapon`)은 영구로 둠(Q3 가정, 저장은 안정 시점에서만). 사용처가 `DialogManager`→`DirectingManager` 1프레임 신호인 것들이라 P4 에서 일시로 내릴지 재검토.
- `ResetData()` = `GameRoot.Instance.NewGame()`(영구 전부 `GameState.New()`) + `ResetTransientStatics()`(옛 ResetData 가 되돌리던 일시·화면·`textDelay`). 옛 ResetData 가 빠뜨렸던 `endingLadderSan endingSwin sewerMap sewerFirstIn sewerFirstInSanCheck sewerDeepOneFirstSanCheck` 는 이제 리셋된다(의도한 수정). `isTest soundVolume sfxVolume keyword* isFinish` 는 옛 동작대로 리셋 안 함.
- 파사드: 영구 455개는 `DataBaseManager` 정적 프로퍼티(같은 이름, `S.GetFlag(FlagKeys.x)`)로 바뀌었다. 호출 4325줄 무변경·컴파일 통과(`ref/out` 없음). 읽기 비용 실측 약 20ns(3M 읽기 60ms) → 필드 백킹 불필요. `GameRoot` 는 최소판(순수 클래스, 지연 생성 `GameRoot.Instance.State`, DDOL 없음).
- 생성 스크립트는 저장소에 없다. `FlagKeys.cs` 는 자동 생성물이라 키 추가는 직접 편집(추가만).

## 4. 비 UTF-8 .cs 방침
- 목록: Audit D-2 의 21개(`DatabaseManager`, `TimeManagere`, `BattleManager`, `Rollet`, `BattleRollet`, `setSkill`, `MapManager`, `FadingBackGround`, `DirectingManager`, `End_Look_Judge` 등). 추정이므로 변환 도구가 엄격 디코드로 재확정한다.
- **추천: 일괄 UTF-8 변환 단계(P1)를 재작성 맨 앞에 둔다.** 이유: (1) Edit/Write 가 거부해서 이행 단계가 이 파일들을 못 건드림(파사드화 대상이 바로 `DatabaseManager`·`TimeManagere`·`BattleManager`·`Rollet`). (2) 한글 문자열 리터럴이 개발 PC 코드페이지에 의존 → 다른 로캘 빌드 머신에서 깨질 위험(의심, 확인 필요). (3) 이후 단계의 diff·리뷰가 가능해진다.
- 절차(builder, 파이썬 스크립트는 `Docs/Tools/` 에 두지 않고 스크래치 사용): ① 모든 `.cs` 를 UTF-8 strict 디코드 시도, 실패한 것만 목록화 ② CP949(cp949 → 실패 시 euc-kr)로 디코드, 대체 문자(U+FFFD) 0 확인 ③ **개행(CRLF/LF)·BOM 유지**, BOM 없이 UTF-8 로 저장(파일이 BOM 이었다면 BOM 유지) ④ 변환 전후 문자열 리터럴 추출 비교(디코드 결과가 일치) ⑤ 파일 수에 관계없이 **변환 전용 커밋 1개**(기능 변경 섞지 않음, 해시를 `state.md` 에 기록) ⑥ Unity 컴파일·스모크·한글 UI 스크린샷 1장 ⑦ `.meta` 변경 없음 확인(`git status` 에 .meta 가 나오면 이상).
- 위험: git diff 가 파일 전체로 보여 blame 이 끊긴다(허용). 변환 실패 파일은 건드리지 말고 목록 보고. `.gitattributes` 에 `*.cs text eol=crlf` 가 없다면(미확인) 개행 정규화가 추가로 발생하지 않게 확인.
- 이후 신규·수정 파일은 UTF-8(+기존 BOM 정책 유지). `.claude/rules/csharp.md` 서식 규칙 준수.

## 5. 단계 계획

스모크 S: 타이틀(`Insert`) → 새 게임 → 캐릭터 생성(스탯·기능 배분) → `Main` 1일차 탐정사무소 대사 → 장소 이동 1회 → 판정 룰렛 1회 → 전투 1회 → 설정창 → 타이틀 복귀 → 새 게임 재진입. 엔딩 확인은 치트 대신 FIX-P0 이후의 에디터 전용 점프(`#if UNITY_EDITOR`)로 `End_Look_Judge`→`DemoEndFadeIn` 메뉴까지. 각 단계 공통 끝 기준: 컴파일 에러 0, EditMode 테스트 전부 통과, 스모크 S 통과(unity-mcp 플레이 모드·콘솔 error 0·스크린샷), 커밋 전 `unity-verify`.
공통 규칙: 한 단계 = builder 한 명 = 커밋 1~3개. 같은 파일을 두 builder 가 동시에 건드리지 않는다. 옛 동작을 바꾸는 수정은 이 재작성에서 하지 않는다(버그 수정은 FIX 큐).

| 단계 | 내용 | 건드리는 파일 | 끝 기준(공통 외) | 위험 |
|---|---|---|---|---|
| P0 ✅ deda9eb | 어셈블리·테스트 골격. `SCoW.Core`(빈 `Marker`), `SCoW.Tests.EditMode`, 스모크 테스트 1개. 폴더 생성(`Story/` 이름 주의) | `Assets/Script/Core/*.asmdef`, `Assets/Tests/EditMode/*` (신규만) | Test Runner(EditMode) 가 에디터·MCP 에서 실행됨, Core 의 `UnityEngine` 참조 시도가 컴파일 에러로 막힘 확인 | asmdef 추가로 Assembly-CSharp 참조가 깨지는 경우(autoReferenced 확인). 도메인 리로드 시간 |
| P1 ✅ d89dda4 | 비 UTF-8 일괄 UTF-8 변환(4절) | .cs 21개 내외(내용 불변) | 변환 전용 커밋, 문자열 리터럴 동치 검증, 한글 UI 스크린샷 | 잘못된 코드페이지 추정 → 문자 깨짐. 반드시 U+FFFD 0 검증 |
| P2 ✅ | `GameInput` 도입 + 레거시 `Input.*` 교체(= INPUT-1 단계 0~6, `Input` 문서 4절 순서). EventSystem·Active Input Handling 전환(7·8단계)은 P2b 로 분리 | `Input/GameInput.cs`, `Assets/Resources/Input/*.inputactions`, 20개 파일 | 에디터에서 KB·마우스·가상 패드로 각 UI 동작, `Input.*` grep 0(서드파티 제외), 스모크 S | 한 키 이중 소비 유지(소비 금지), 전투 적 선택 패드 불가(Q·A-2) |
| P2b ✅ | 3개 씬 `StandaloneInputModule`→`InputSystemUIInputModule`, DNP·I2·Text Animator 서드파티 대응, Active Input Handling New | 씬 3개(에디터 MCP 로만), 서드파티 최소 패치 | Console 에 `InvalidOperationException ... Input class` 0, UI 클릭 전부 동작 | 단독 커밋(되돌릴 수 있게). 씬 직렬화 |
| P3 ✅ | `GameState` 도입 + `DataBaseManager` 파사드. 3절 분류표 작성, 생성 스크립트(스크래치)로 `FlagKeys.cs`·프로퍼티 파사드 생성, `ResetData()` 를 `GameRoot.NewGame()` 위임으로 교체. `GameRoot` 최소판(상태만) | `Core/State/*`, `App/GameRoot.cs`, `old_Dialog/DatabaseManager.cs`(P1 후) | 테스트: 모든 영구 키 `New()` 시 기본값, `Clone` 왕복. 스모크 S 에서 두 번째 새 게임의 `enemyDeathCount`·`posionDebuff`·`ending*` 가 0/false | 560개 중 분류 오류(영구/일시). 파사드 프로퍼티는 읽기 비용이 `HashSet` 조회로 늘어남(매 프레임 폴링 호출처 `TimeManagere.Update` 주의, 확인 필요). 초기값이 0 아닌 필드(`soundVolume` 0.5 등) 보존 |
| P4 | 저장·불러오기·설정 저장: `SaveService`/`SettingsService`(ES3), 슬롯 UI(타이틀 "이어하기", 설정창), 자동 저장, 마이그레이션 훅, `ReadLog` 기록 시작 | `Data/*`, `UI/settingManager.cs`, `Main/ManageMain.cs`, 신규 View | 저장→앱 재기동→불러오기에서 장소·시간·스탯·인벤·플래그 동일(수동 비교 로그), 손상 파일 로드 시 오류 UI(크래시 아님), 설정이 재실행 후 유지 | 씬 오브젝트 상태(`SetActive`) 복원은 `TimeManagere.Update` 가 플래그로 재현하는 부분만 가능 → 저장 허용 지점을 "장소 이동 직후 안정 상태"로 제한. 저장 중 DOTween 진행. ES3 비 UTF-8 경로(윈도우 한글 사용자명) 확인 |
| P5 | `UiStack`·`IsBusy`·`SceneRouter`. 8플래그 AND 복제 제거, Esc 우선순위 통일, `LoadScene` 6곳 통합(`SceneIds`) | `App/*`, `settingManager`, `MapManager`, `ShopUI`, `SIS_UIManager`, `IntelManager`, `Rollet`, `BattleRollet`, `ManageMain`, `setSkill`, `FadingBackGround` | 다중 패널 상태에서 Esc 가 맨 위 하나만 닫음, 대사 중 설정창 열림(Audit B-4) | UI 열림 규칙 변경이 흐름을 바꿀 수 있음 → 기존 막히던 상황 목록을 테스트로 고정 |
| P6 | 순수 규칙 추출: `IRng`·`DiceExpr`·`SkillCheck`·`SanCheck`. `Rollet`·`BattleRollet` 이 `SkillCheck` 를 호출하도록(동작 동일). 골든 테스트 | `Core/Rules/*`, `Tests/*`, `Rollet/Rollet.cs`, `BattleRollet.cs` | 시드 고정 분포 테스트·경계값 테스트 통과, 룰렛 UI 판정 결과가 이전과 같은 시드에서 동일 | 현 규칙의 의도 불명(M1·M2). **포팅만, 정정 금지**. 연출(룰렛 회전)과 결과 결정 순서가 코드에 섞임 |
| P7 | 스토리 데이터 모델·파서(Core). `StoryCsvParser`(RFC4180), `ConditionParser`, `Condition/Effect` 평가기, `StoryScript` JSON 로더. 아직 게임에 연결 안 함 | `Core/Story/*`, `Tests/*` | 쉼표·따옴표·CRLF·빈 줄·BOM CSV 케이스 테스트, 조건 DSL 문법·평가 테스트, 옛 CSV 15개를 모두 파싱해 오류 0 | 옛 CSV 의 `@`·`^` 표기 보존. 열 구성(메모 열) 차이. 문법을 작게 유지(스코프 과욕 금지) |
| P8 | `DialogueRunner`(Core) + `DialogManager` 가 Runner 를 구동하는 View 로 전환, `DialogPaser` 를 새 파서로 교체. 백로그·스킵(읽은 것만/전부)·자동 진행·텍스트 속도 UI. `Advance` 단일 경로화(Audit A-6·A-7) | `Core/Story/DialogueRunner,Backlog`, `Story/DialogueView`, `dialogue/DialogManager.cs`, `DialogPaser.cs`, `UI/settingManager` | 단위 테스트: 스킵 정지 조건·자동 대기 시간·백로그 링버퍼. 플레이: 휠 백로그, Ctrl 스킵이 안 읽은 줄에서 멈춤, 속도 5단계 | `DialogManager`(Text Animator 타이핑 완료 이벤트)와 Runner 상태 동기. 선택지·판정 앞 정지 누락. 비 UTF-8 변환(P1) 선행 필수 |
| P9 | 파일럿 장소 이행: `1st_Bar`+`1st_DetectiveOffice`(1일차 시작, 분기 적당). Topic JSON, `StoryDatabase`, 동치 테스트 도구, `PlaceDef` | `Resources/Story/*.json`, `Story/StoryDatabase`, `InteractionController.cs`(해당 두 메서드만 위임·본문 삭제), `DialogDatabaseManager.cs`(해당 분기) | 동치 도구 상태 프리셋 전부 통과(줄 수·첫/끝 줄), 스모크 S, `interactionEvent` 인스펙터 참조 안 깨짐 | 인스펙터 `interactionEvent` 라인 범위(x,y)를 CSV ID 로 옮길 때 off-by-one. 프리셋이 분기를 다 덮는지. 씬 오브젝트 삭제 금지(파사드만) |
| P10 | 장소 이행 2차: `ClientsHouse` `DailyNews` `University` `Riverside` `Hospital` `Policeoffice` `Slum` `2nd_NoonEvent` `2ndDetectiveOffice` `SafeLook` | 위와 동일 패턴 | P9 와 동일 | 분량 큼 → builder 둘로 쪼개되 `InteractionController` 같은 파일 동시 수정 금지(순차) |
| P11 | `KeywordDialog`(약 700줄)·`BattleDialog` 이행(키워드 UI `KeyWordManager` 연동, 272줄 참조) | `InteractionController`, `BillowUI/KeyWordManager.cs` | 동치 테스트(키워드×플래그 조합 샘플), 스모크 | 키워드 문자열이 `DataBaseManager.keyword*` 와 UI 양쪽에 걸림 |
| P12 | `RetrunDialogResult`(판정 결과 약 590줄) + `InSewerDialog`·`StartSewerOffice` → 노드 `OnSuccess/OnFail` 데이터화. `InteractionController` 를 얇은 어댑터(<300줄)로 축소, `DialogDatabaseManager` 폴링 `Update` 제거 | `InteractionController`, `DialogDatabaseManager`, `Rollet` 호출부 | 판정 성공·실패·대성공/대실패 분기 샘플 동치, SAN 체크 포함 | `SanCheck` 결과가 `san` 변경·디버프 부여·플래그와 얽힘. 연출 `isDirecting` 타이밍 |
| P13 | 월드 동기화: `TimeManagere.Update`·`BulbCheck` → `WorldBinder` + `WorldClock`. 하수도 생성(142~714)은 이 단계에서 프리팹 분해 또는 유지(미결 Q6) | `Map/TimeManagere.cs`, `Core/Time/*` | 시간대(`timeCount%4`)·플래그별 오브젝트 상태가 이전과 동일(에디터 스냅샷 비교: 활성 상태·위치 덤프 diff) | `localPosition` 좌표가 코드에 박힘(-65.10986 등) → 값 그대로 데이터화. 연산자 우선순위 `&&`/`||` 가 섞인 기존 식(735줄 부근)은 **의미 보존**(버그면 FIX 큐로) |
| P14 | 전투: `BattleEngine`(Core) + `EnemyDef` 데이터 + `BattleView`(DOTween `SetLink`). 2분할: 14a 엔진·피해식 테스트, 14b View 전환(적 종류별 복제 제거) | `Core/Rules/BattleEngine`, `Battle/*`(비 UTF-8 변환 후), `InSewerEnemy`, `EnemyClass` | 시드 고정 전투 로그 동치, 일반 적·하수도 적 각 1회 전투 승리·패배 | 가장 큰 덩어리·연출 결합도 최고. 14a/14b 사이에 옛 BattleManager 가 엔진을 호출하는 중간 상태를 반드시 거침 |
| P15 | 싱글턴·DDOL 정리, 파사드 제거: DDOL 12개를 `GameRoot` 로 일원화, `DataBaseManager` 호출처 치환 후 삭제, 디버그 잔재(`isTest`, `IntelUI/Test`) 정리 | 전반(작은 파일 다수) | `DataBaseManager` grep 0, DDOL 은 `GameRoot` 1개, 스모크 S + 저장/불러오기 왕복 + 타이틀 복귀 3회 반복 | 호출처 4325줄(파일별로 커밋 분할). IntelUI(`nowPage*` 등 화면 상태) 이전 시 View 분리 필요 |
| P16 | 오타·소문자 클래스명 정리(`FormerlySerializedAs`), 데드 코드 삭제, 문서 갱신(`GameOverview.md`) | 이름 오타 파일들 | 씬·프리팹 참조 유지(Console 에 Missing script 0), 스모크 S | 씬 직렬화 깨짐 → 클래스 이름 변경은 `.meta` GUID 유지 개명, 마지막에 |

- 순서 근거: P0~P2b 는 기반(도구·인코딩·입력), P3~P5 는 상태·저장·UI 뼈대, P6~P8 은 규칙·대화 엔진, P9~P13 은 데이터 이행(분량 큼, 장소별 반복), P14 전투, P15~P16 정리. P6·P7 은 P3 이후 서로 독립이라 병렬 가능(서로 다른 파일). P9 부터는 `InteractionController` 단일 파일을 건드리므로 직렬.
- 단계마다 옛 경로를 지우지 않고 위임 후 비운다. 되돌리려면 해당 단계 커밋 revert 로 끝나야 한다(단계=독립 커밋 묶음).
- 에디터 도구(동치 테스트·스냅샷 덤프)는 `Assets/Editor/` 에 두고 EditMode 에서 실행. Core 테스트는 에디터 없이 `dotnet test` 로도 돌릴 수 있게 Core 에 UnityEngine 참조를 두지 않는다.

## 6. 검증 프로브(공통)
- 상태 리셋: 새 게임→적 처치·독 부여·엔딩 플래그 세팅→설정→타이틀→새 게임 → `GameState` 전체가 `new GameState()` 와 `Equals`(P3 이후 자동 테스트).
- 저장 왕복: 임의 진행 상태 → 저장 → `GameState` 덤프 → 재기동·불러오기 → 덤프 동일. 손상·버전 불일치·키 누락 파일 로드 테스트(P4).
- 판정 분포·경계(P6), 전투 시드 로그 동치(P14), 대화 동치 도구(P9~P12), 월드 스냅샷 diff(P13).
- 입력: KB·마우스·가상 패드 각각으로 같은 동작(P2). Esc 다중 패널(P5).
- 스킵: 연타·홀드 후 코루틴 중복 없음, 안 읽은 줄 정지(P8).
- 콘솔 error·Missing reference 0(트윈 `SetLink`), 에디터 플레이 스크린샷 연속 캡처(타이틀→생성→본편).

## 미결 (임시 가정)
- Q1 출시판에 ES3 저장 파일이 있었는가? — 가정: 없음(코드 0건). 있다면 P4 전에 파일명·키 목록을 받아 호환 마이그레이션 `From0To1` 추가.
- Q2 `GameState` 키를 기존 필드명 그대로 쓸지 — 가정: 그대로(오타 포함). 근거: 자동 생성·대사 데이터 일치. 세이브 호환은 이후 "추가만".
- Q3 `*SanCheck` 등 대기 플래그·연출 중 상태를 저장에 포함할지 — 가정: 영구 취급, 저장은 안정 시점(장소 이동 직후·시간대 전환)에서만 허용.
- Q4 백로그를 세이브에 넣을지 — 가정: 최근 50줄만 포함(없으면 로드 직후 백로그 비어도 허용).
- Q5 추가 키 배치(백로그 휠 위/L/LT, 스킵 Ctrl/RT 홀드, 자동 Q/R스틱) — 가정: 위와 같음. 사용자 확인 시 `GameInput` 한 곳만 바꾼다.
- Q6 하수도 맵 생성 코드(`TimeManagere` 142~714)를 프리팹 데이터화할지 유지할지 — 가정: P13 에서 코드 그대로 `SewerMapBuilder` 로 이동(분리만), 데이터화는 하지 않음.
- Q7 룰 정정(M1·M2: 주사위식·어려움 단계) 반영 시점 — 가정: 재작성 중에는 동작 보존, 정정은 P6 이후 `RuleSet` 옵션으로 별도 큐.
- Q8 대사 데이터 포맷(JSON+CSV 분리 vs 단일 CSV) — 가정: 구조 JSON + 본문 CSV. 작가가 구조까지 편집한다면 단일 CSV 확장으로 바꾸는 편이 나을 수 있음(P7 시작 전 확인).
- Q9 새 폴더 이름 `Story/`·`App/`·`Data/` — 가정: 위와 같음. 한 번 정하면 `.meta` 때문에 바꾸기 번거로움.
- Q10 ES3·DOTween·Text Animator 의 asmdef 유무 — 미확인. 없으면 `SCoW.Runtime` asmdef 는 만들지 않음(현 설계는 이를 전제로 Core 만 분리).
