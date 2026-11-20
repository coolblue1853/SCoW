# 입력 교체 설계 (INPUT-1 ①②)

레거시 `Input.*` 를 전부 New Input System(`com.unity.inputsystem` 1.5.0)으로 교체한다. 코드 수정 없음, 조사+설계만. 작성일 11-20.

## 1. 현황 조사

- `ProjectSettings.asset`: `activeInputHandler: 2` (Both).
- `*.inputactions` 에셋: 없음(새로 만든다).
- `Assets/Script/**` 에서 `UnityEngine.InputSystem` 사용: **0건**. 전부 레거시.
- 레거시 호출: 20파일 69줄(CodeMonkey 제외). CodeMonkey `Button_Sprite.cs`(5줄)·`UtilsClass.cs`(약 22줄)는 서드파티라 수정 대상 아님. 호출처 사용 여부 확인 필요(5절).
- 인코딩 주의: `Rollet.cs`, `BattleRollet.cs`, `EnemySelectButton.cs` 는 UTF-8 아님(레거시 인코딩). Edit/Write 거부됨. 교체 시 인코딩 보존 방식 별도 필요(예: 파이썬 바이트 치환).

### 1.1 전수 목록 (Assets/Script, CodeMonkey 제외)

| 파일:줄 | 키/버튼 | 동작(추정, 주변 코드 기준) |
|---|---|---|
| IntelUI/PlaceButtonManage.cs:15,22 | W | 위 버튼(ClickUpButton/2) 대리 클릭 |
| IntelUI/PlaceButtonManage.cs:29,36 | S | 아래 버튼(ClickDownButton/2) 대리 클릭 |
| IntelUI/EventButtonManage.cs:17,24 | W / S | 사건 목록 위/아래 |
| IntelUI/CharacterButtonManage.cs:17,24 | W / S | 인물 목록 위/아래 |
| IntelUI/IntelPlaceDetail.cs:381,388 | D / A | 장소 상세 다음/이전 |
| IntelUI/IntelEventDetail.cs:318,325 | D / A | 사건 상세 다음/이전 |
| IntelUI/IntelCharacterDetail.cs:91,99 | D / A | 인물 상세 다음/이전 |
| IntelUI/IntelManager.cs:152 | Alpha1 또는 Esc | 정보 UI 열려 있으면 닫기 |
| IntelUI/IntelManager.cs:156 | Alpha1 | 정보 UI 닫혀 있으면 열기 |
| Battle/EnemySelectButton.cs:35 | 마우스 위치 | 화면->월드 변환, Raycast 로 적 호버 판정 |
| Battle/EnemySelectButton.cs:45 | 마우스 왼쪽(Held) | 호버 적 클릭 -> ButtonClick (GetMouseButton=누르는 동안) |
| Character/Player.cs:66,70 | Horizontal 축 | 걷기 애니 move 0/1 |
| Character/Player.cs:78,85 | Horizontal 축 | 좌우 이동·스프라이트 반전·걷는 소리 |
| Rollet/Rollet.cs:53 | Esc | 주사위 UI 닫기(SAN 판정 예외 조건) |
| Rollet/Rollet.cs:75,80,97,102 | E | 굴리기 시작/종료/SAN 굴리기/SAN 종료 |
| Rollet/Rollet.cs:87 | Mouse0 또는 E | 굴리는 중 멈추기(결과 확정) |
| Rollet/BattleRollet.cs:66 | Esc | 전투 주사위 UI 닫기 |
| Rollet/BattleRollet.cs:93,98 | E | 굴리기 시작/종료 |
| Rollet/BattleRollet.cs:105 | Mouse0 또는 E | 굴리는 중 멈추기 |
| Rollet/RolletTest.cs:78,90,97,103,109,190 | F12, F10, F7, F8, F9, F7 | 디버그 치트(스탯·플래그 등) |
| Rollet/RolletTest.cs:114,129,133,139,143,147 | Alpha1~6 | 디버그 |
| Rollet/RolletTest.cs:152,157,162,167,172,186 | F1~F6 | 디버그 |
| Rollet/RolletTest.cs:195 | P | 디버그 |
| dialogue/DialogManager.cs:123 | E 또는 Mouse0(Down) | 대사 넘기기 |
| dialogue/DialogManager.cs:148 | E 또는 Mouse0(Down, daveCheck==false) | 대사 넘기기(선택 대기 분기) |
| old_Dialog/End_Look_Judge.cs:92 / 162 | D / A | 옛 엔딩 판정 다음/이전 |
| Map/Intel_ObtoUI.cs:57 | E | 오브젝트 상호작용 UI 열기(상태 플래그 가드) |
| Map/Intel_ObtoUI.cs:407 | D 또는 A | 아무 쪽이나 페이지 이동 |
| Map/Intel_ObtoUI.cs:447,544 | D | 다음 |
| Map/Intel_ObtoUI.cs:490,620 | A | 이전 |
| Map/MapManager.cs:419 | Alpha2 | 지도 열기 |
| Map/MapManager.cs:431 | Esc | 지도(mapChainingUI) 닫기 |
| Map/PotalPoint.cs:55 | F | 포탈 이동(연출·대사 중 아님) |
| UI/settingManager.cs:157,161 | Esc | 설정창 닫기/열기 |
| UI/ShopUI.cs:68 | Esc | 상점 닫기 |
| UI/SIS_UIManager.cs:233 | Alpha3 | SIS UI 열기 |
| UI/SIS_UIManager.cs:247 | Esc | SIS UI 닫기 |

합계: 20파일 69줄(위 표에서 줄 묶음 포함). 패턴은 전부 GetKeyDown(1회 입력) 위주. 예외: EnemySelectButton:45(GetMouseButton, Held), Player(축), mousePosition.

### 1.2 서드파티 레거시 호출 (수정 안 함, 목록만)

| 위치 | 호출 | 비고 |
|---|---|---|
| Text Animator/TextAnimatorPlayer.cs:90 | `Input.anyKeyDown` | WaitInput 코루틴. 실제 사용 여부는 5절 확인 |
| I2/Localization/.../SpecializationManager.cs:39 | `Input.touchSupported` | 플랫폼 특화 문자열("Touch"/"PC"). 로컬라이즈 시 호출될 수 있어 위험도 높음 |
| DamageNumbersPro/Scripts/Internal/DamageNumber.cs:1737 | `Input.mousePosition` | `#if ENABLE_INPUT_SYSTEM && DNP_NewInputSystem` 의 else 쪽. 심볼 `DNP_NewInputSystem` 정의하면 해결 |
| DamageNumbersPro/Demo/** (DNP_InputHandler.cs 외, DNP_2DDemo.cs) | GetKey·GetMouse·GetAxisRaw 다수 | 데모. 같은 심볼 분기. 게임 로직 아님 |
| External_rostCrypt/** | (이미 `using UnityEngine.InputSystem`) | 이미 New 사용 |

### 1.3 EventSystem 입력 모듈

`StandaloneInputModule` 스크립트 GUID `4f231c4fb786f3946a6b90b886c48677` grep 결과(게임 씬):

- `Assets/Scenes/Main.unity` 1
- `Assets/Scenes/SetCharcter/SetCharacter.unity` 1
- `Assets/Scenes/Insert.unity` 1
- 프리팹: 0건
- (플러그인 예제 씬 6개에도 있음: DOTween 3, DNP 데모 2, I2 예제 1 — 무시)

## 2. 입력 액션 에셋 설계

경로: `Assets/Settings/Input/SCoWInput.inputactions` (JSON, Generate C# Class 체크 안 함 -> 5.1 참조).

원칙: 기존 키 유지. 키보드(KB)·마우스·게임패드(GP) 3경로. 모든 액션은 Button(Value 아님), 예외 Move/Point.

### 2.1 Map `Gameplay` (월드/전투/대화)

| 액션 | 타입 | 키보드·마우스 | 게임패드 | 치환 대상 |
|---|---|---|---|---|
| Move | Value Vector2(axis X만 사용) | A/D 가 아님! 레거시 Horizontal = 방향키 ←→ + A/D 둘 다 -> 둘 다 바인딩(1D Axis: A/D, ←/→) | 왼스틱 X, 십자키 ←→ | Player.cs 66~85 |
| Interact | Button | E | A(buttonSouth) | E 전부(Rollet·Dialog·Intel_ObtoUI) |
| Advance | Button | Mouse 왼쪽 | (Interact 와 동일 A 바인딩 추가) | Mouse0 Down(Dialog·Rollet) |
| Portal | Button | F | A(buttonSouth) | PotalPoint:55 (조건이 Interact 와 겹치지 않음, 가정 A-1) |
| PointerPrimary | Button | Mouse 왼쪽 (Held 판정 `IsPressed`) | A | EnemySelectButton:45 |
| Point | Value Vector2 | Mouse position | 오른스틱 가상 커서 대신 아래 가정 A-2 | EnemySelectButton:35 |

### 2.2 Map `UI` (패널·메뉴)

| 액션 | 타입 | 키보드·마우스 | 게임패드 | 치환 대상 |
|---|---|---|---|---|
| Cancel | Button | Esc | B(buttonEast) | 모든 Esc 닫기 |
| Menu | Button | Esc (설정창 토글) | Start | settingManager:157,161 |
| OpenIntel | Button | 1 | Y(buttonNorth) | IntelManager:152,156 |
| OpenMap | Button | 2 | X(buttonWest) | MapManager:419 |
| OpenSIS | Button | 3 | Select/Back | SIS_UIManager:233 |
| NavUp | Button | W | 십자키 위 | W (Place/Event/CharacterButtonManage) |
| NavDown | Button | S | 십자키 아래 | S |
| PageNext | Button | D | 십자키 오른쪽, RB | D 전부(Detail 3, Intel_ObtoUI, End_Look_Judge) |
| PagePrev | Button | A | 십자키 왼쪽, LB | A 전부 |

Esc 한 키에 Cancel 과 Menu 가 동시에 걸린다. 지금도 각 UI 가 `activeSelf` 로 가드해 동시 반응을 피함. 같은 구조 유지(액션 2개를 같은 키에 바인딩해도 각각 이벤트 발생).

### 2.3 Map `Debug` (KB 전용, RolletTest)

F1~F12, Alpha1~6, P 를 각 액션으로. `GameInput.Debug.*`. 패드 바인딩 없음(디버그 전용, 에디터·Development 빌드만 활성: `#if UNITY_EDITOR || DEVELOPMENT_BUILD` 로 맵 enable).
주의: Debug 의 Alpha1/2 와 UI 의 OpenIntel/OpenMap 이 같은 키(1,2). 레거시도 동일 동작이므로 유지(RolletTest 가 씬에 있을 때만).

### 2.4 Control Scheme

`Keyboard&Mouse`, `Gamepad` 2개. UI 맵은 InputSystemUIInputModule 가 기본 `DefaultInputActions` 를 쓰므로 모듈에는 별도 에셋(`Assets/Settings/Input/SCoWUI.inputactions`, 기본 UI 맵 복제) 지정하거나, 기본 에셋 그대로 사용. 가정 A-3.

## 3. 코드 접근 방식 (추천 1개)

**정적 래퍼 `GameInput`**(`Assets/Script/Input/GameInput.cs`) 추천. 근거: 호출처는 69줄 모두 "이번 프레임 눌렸나" 형태라 `Input.GetKeyDown(KeyCode.E)` -> `GameInput.InteractDown` 한 줄 치환이면 끝난다. 생성 클래스는 호출처마다 `new SCoWInput()` 수명 관리·Enable/Disable 구독이 필요해 교체량이 크고 리뷰가 어렵다.

시그니처:

```csharp
public static class GameInput
{
    // 첫 접근 시 에셋 로드(Resources) + 모든 맵 Enable. 도메인 리로드 꺼진 에디터 대비 RuntimeInitializeOnLoadMethod(SubsystemRegistration)로 리셋.
    public static bool InteractDown { get; }   // WasPressedThisFrame
    public static bool AdvanceDown { get; }
    public static bool CancelDown { get; }
    public static bool MenuDown { get; }
    public static bool OpenIntelDown, OpenMapDown, OpenSISDown;
    public static bool NavUpDown, NavDownDown, PageNextDown, PagePrevDown;
    public static bool PortalDown;
    public static bool PointerHeld { get; }    // IsPressed
    public static float MoveX { get; }         // -1..1, GetAxisRaw 대응: 데드존 후 Mathf.Sign 로 -1/0/1 로 스냅
    public static Vector2 PointerPosition { get; } // Mouse.current null 이면 마지막 값/zero
    public static class Debug { public static bool F1Down ... }
}
```

함정:
- 에셋은 `Resources`(또는 `[SerializeField]` 대신 정적 로드)로 가져와야 정적 클래스에서 접근 가능 -> `Assets/Resources/SCoWInput.inputactions`로 두거나 코드에서 `InputActionAsset.FromJson` 하지 않는다. 가정 A-4: 에셋을 `Assets/Resources/Input/` 에 둔다.
- 레거시 `GetKeyDown` 은 Update 시점 프레임 기준, `WasPressedThisFrame` 도 같은 의미. 다만 Update 를 건너뛰는 FixedUpdate 에서 부르면 어긋남(해당 호출처 없음).
- 같은 프레임 중복 소비: E 하나가 Rollet 시작과 Dialog 넘기기 모두 트리거되는 기존 현상을 그대로 유지해야 하므로, `WasPressedThisFrame` 는 소비하지 않는 순수 조회로 한다(이벤트 콜백 방식 금지).
- `Mouse.current` / `Keyboard.current` null 체크는 래퍼 안에 일원화.
- 마우스 UI 위 클릭 구분은 현재 코드가 안 함. 변경하지 않는다.

## 4. 교체 순서 (파일 묶음, 한 묶음 = 한 커밋, 매번 플레이 확인)

0. 준비: 에셋 생성, `GameInput.cs`, 단위 확인용 간단 테스트(에디터에서 Interact 읽힘). Active Input Handling 은 Both 유지.
1. UI 패널: settingManager, ShopUI, SIS_UIManager, MapManager, IntelManager (Esc/1/2/3).
2. Intel 목록·상세: PlaceButtonManage, EventButtonManage, CharacterButtonManage, IntelPlaceDetail, IntelEventDetail, IntelCharacterDetail, End_Look_Judge (W/S/A/D).
3. Map/월드: Intel_ObtoUI, PotalPoint, Player(축).
4. 대화·주사위: DialogManager, Rollet, BattleRollet (E/Mouse0/Esc). 비 UTF-8 파일 주의.
5. 전투: EnemySelectButton (mousePosition·Held). 비 UTF-8 주의.
6. 디버그: RolletTest.
7. EventSystem: 3개 씬 `StandaloneInputModule` -> `InputSystemUIInputModule`(씬당 Unity 에디터에서 컴포넌트 교체. 에디터 MCP 로). Main/SetCharacter/Insert 각각 확인.
8. 서드파티 대응(6절) 후 Active Input Handling -> `Input System Package (New)`(에디터 재시작 필요).

게임패드 포커스: UI 가 마우스 클릭 전용 버튼이라 패드로 선택 이동이 없다. NavUp/Down/PageNext 등은 기존 단축키 방식이므로 패드도 같은 단축키 방식으로 동작하며 EventSystem 기본 선택 이동은 건드리지 않는다(가정 A-5).

## 5. 확인할 것 / 위험

- CodeMonkey `UtilsClass`·`Button_Sprite` 중 레거시를 쓰는 메서드 호출 여부: `Grep` 으로 `UtilsClass.GetMouseWorldPosition|CreateWorldTextPopup|Button_Sprite` 호출처를 교체 시 확인. 호출되면 Input System 에서 던짐. 호출처 없으면 방치(컴파일만 되면 됨, 런타임 미호출).
- TextAnimatorPlayer.WaitInput: Text Animator 의 `waitForInput` 계열 옵션을 쓰는 곳이 있으면 터짐. 쓰지 않으면 무해.
- I2 `SpecializationManager.touchSupported`: 로컬라이즈 시 `Specialization` 조회에서 호출 가능. 확인 필수.

## 6. Active Input Handling -> New 전환 시 서드파티 위험·대응

New 전환 시 `UnityEngine.Input` 의 모든 호출은 **런타임에 `InvalidOperationException`** (컴파일은 통과). 영향:

| 대상 | 위험 | 대응 |
|---|---|---|
| DamageNumbersPro | 높음(실제 게임 사용) | Scripting Define Symbols 에 `DNP_NewInputSystem` 추가 -> `DamageNumber.cs:1732` 분기가 New 쪽으로. 데모 폴더는 런타임에서 안 쓰므로 무시(같은 심볼로 통과) |
| Text Animator `Input.anyKeyDown` | 중간 | 사용처 없으면 무시. 있으면 해당 한 줄을 `#if ENABLE_INPUT_SYSTEM ... Keyboard.current.anyKey.wasPressedThisFrame #else ... #endif` 로 최소 패치하고 이 문서에 기록(규칙: 서드파티 수정은 꼭 필요할 때만) |
| I2 `Input.touchSupported` | 높음 가능성 | `#if ENABLE_LEGACY_INPUT_MANAGER` 가드 또는 `Touchscreen.current != null` 로 최소 패치. 또는 I2 의 Specialization 이 PC 로 고정되는 값으로 대체. 확인 후 기록 |
| CodeMonkey | 호출되면 높음 | 5절 확인. 호출되면 해당 유틸 메서드만 `GameInput.PointerPosition` 로 교체 |
| External_rostCrypt | 낮음 | 이미 New 사용 |
| 씬의 StandaloneInputModule | 필수 | 7단계에서 모두 교체, 안 하면 UI 클릭 전부 불가 |

검증: 전환 직후 에디터 Play, Console 에 `InvalidOperationException ... You are trying to read Input using the UnityEngine.Input class` 가 0인지 확인. 흐름 전체(타이틀 -> 캐릭터 생성 -> 맵 이동 -> 대화 -> 주사위 -> 전투 -> 설정창) 한 바퀴. 안전장치: 문제가 남으면 Both 로 되돌릴 수 있게 전환 커밋을 단독으로 분리.

## 7. 검증 시나리오(프로브)

- 각 묶음 교체 후: 해당 UI 의 키(KB), 마우스, 패드(Gamepad 시뮬: 에디터 Input Debugger 로 가상 패드) 로 동일 동작 확인.
- Player 이동: A/D, ←/→, 왼스틱, 십자키 모두 좌우 이동·걷기 애니·걷는 소리.
- 대화: E / 클릭 / 패드 A 로 넘기기, 한 번 입력에 한 칸만 진행.
- 주사위: E / 클릭 / A 로 굴리기·멈추기·종료, Esc/B 닫기.
- 전투 적 선택: 마우스 호버+클릭. 패드는 가정 A-2 에 따름.
- Esc 와 설정창 토글이 동시 닫기·열기 되지 않는지(기존과 동일).

## 미결 (임시 가정)

- A-1 Portal(F) 의 패드 키: A(buttonSouth). 상호작용(E)과 같은 버튼. 두 동작의 발생 조건이 겹치지 않는다고 가정(PotalPoint 는 포탈 근처 + 연출·대화 아님).
- A-2 전투 적 선택은 마우스 전제(월드 Raycast). 패드에서는 좌우 십자키로 적 순회 + A 확인 UX 가 필요하나 이번 교체 범위에서 제외, 별도 큐로. 임시: 패드 사용자는 전투 적 선택이 불가할 수 있음(위험, 별도 확인 필요).
- A-3 UI 모듈의 액션 에셋은 `DefaultInputActions` 그대로 사용(Submit=A/Enter, Cancel=B/Esc, Navigate=십자키·스틱, Click=마우스).
- A-4 입력 액션 에셋 위치는 `Assets/Resources/Input/SCoWInput.inputactions` (정적 래퍼 로드용). `Assets/Settings/Input/` 가 아님.
- A-5 패드 UI 는 기존 단축키 방식 유지(포커스 이동 UI 신설 안 함).
- A-6 `Esc` 로 Cancel/Menu 이중 바인딩 구조 유지.
- A-7 패드 기본 배치: A=확인/상호작용, B=취소, Start=설정, Y=정보, X=지도, Select=SIS, LB/RB=이전/다음 페이지.
