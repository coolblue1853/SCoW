# P14b 전투 연출 프로필 설계 (BattleFxProfile)

대상: `Assets/Script/Battle/BattleManager.cs` 의 `RetrunRolletResult`(771~3880줄) 복제 블록.
목표: 6행동 x 5슬롯 복제를 "행동 하나 + 슬롯 프로필 데이터"로 합친다. **연출·수치는 출시판과 동일하게 유지**(동치가 목표, 개선 아님).
선행: PT-1 에서 `EnemyDefs`/`EnemyState`/`_actingEnemy` 완료. 이 문서는 남은 View 부분만 다룬다.
실측 범위: 대표 블록(evasion H1·H2·D1, counterattack H1, Deftness_attack H1, EnemyDeftness)을 읽고, 줌·회전·`OnDamageObject` 는 전 파일 grep 으로 확인. **표에서 "추출"로 적힌 값은 builder 가 F0 에서 줄 범위를 읽어 채운다**(추측 금지).

## 1. 복제 블록 목록

행동 5종은 `if (PlayerAction == "...")` 최상위, 안에서 `IsEnemyAttack(slot)`/`IsPointer(slot)` 로 5슬롯 분기, 다시 `Success` 로 성공/실패 분기. 전용 2종(EnemyDeftness·DeepOneDeftness)은 슬롯 분기 없이 단일.

| 행동(PlayerAction) | 의미 | 줄 범위 | H1 | H2 | H3 | H4 | D1 | 성공 판정 문자열 |
|---|---|---|---|---|---|---|---|---|
| evasion | 적 공격 회피 | 773~1020 | 775 | 823 | 873 | 922 | 971 | `"Success"` |
| counterattack | 회피 후 반격 | 1021~1307 | 1023 | 1079 | 1137 | 1193 | 1250 | `"Success"` |
| MA_attack_Counter | 맨손 공격(반격 단계) | 1308~1582 | 1310 | 1363 | 1418 | 1471 | 1527 | `"Success"` |
| SS_attack_Counter | 근접무기 공격(단검·배트·도끼) | 1583~2426 | 1585 | 1749 | 1916 | 2080 | 2254 | `"Success"` |
| MS_attack | 사격(권총·리볼버·소총·산탄) | 2427~3318 | 2429 | 2604 | 2779 | 2953 | 3136 | `"Result : Success"`/`"Result : Critical Success"` |
| Deftness_attack | 손재주(돌·화염병) | 3319~3795 | 3321 | 3414 | 3508 | 3601 | 3698 | 위와 같음 |
| EnemyDeftness | H3 손재주 공격 | 3796~3841 | 단일(H3 하드코딩) | | | | | 위와 같음 |
| DeepOneDeftness | D1 손재주 공격 | 3842~3880 | 단일(D1 하드코딩) | | | | | 위와 같음 |

줄 수 합계 약 3100줄. 슬롯당 블록은 evasion ~48줄, SS/MS 는 무기 분기까지 ~170줄.

주의: 판정 문자열이 행동마다 다르다(`"Success"` vs `"Result : Success"`). 통합 시 호출 쪽에서 `bool success` 로 정규화하는 헬퍼 하나만 두고, 문자열 자체는 룰렛 쪽 계약이라 바꾸지 않는다.

### 1-1. 슬롯별로 다른 값 (grep 으로 확인한 것)

| 값 | H1 | H2 | H3 | H4 | D1 | 비고 |
|---|---|---|---|---|---|---|
| 카메라 줌 호출 | `ZoomMidle()` | `ZoomDown()` | `ZoomUp()` | `ZoomUp()` | **없음** | 확인(전 행동 공통). H4 evasion 실패 블록(948줄)은 줌이 주석 처리됨(성공은 줌) |
| 줌 호출 위치 | 행동 공통 | | | | | counter·MA·SS·MS·Deftness 는 슬롯 분기 첫 줄(성공/실패 전), **evasion 은 성공·실패 각각 안** |
| 카메라 회전 크기 | 5 | 5 | 5 | 1 | 1 | `cam.transform.DORotate(new Vector3(0,0,±n), 0.5f)` |
| 회전 부호 | 피격·회피 계열(evasion·counterattack·EnemyDeftness)은 `-`, 공격 계열(MA·SS·MS·Deftness)은 `+` | | | | | DeepOneDeftness 도 `-1` |
| 적 오브젝트 | `deepOneHybrid1Ob` | `...2Ob` | `...3Ob` | `...4Ob` | `deepOne1Ob` | 스프라이트 컨트롤러 `deepOneHybridNSC`/`deepOne1SC` |
| 피격 연출(evasion 실패, 플레이어 쪽) | 사운드 `MartialArts`, `PlayerHitted*` 없음, `resetPlayer` 없음 | 사운드 `Dagger_Attack` + `battlePlayer.PlayerHittedDagger()` + 끝에 `resetPlayer()` | H1 과 같음(F0 확인) | `PlayerHittedDagger()` + `Dagger_Attack` + `resetPlayer()`, **hitted 스프라이트 없음**(F0 확인) | 사운드 `Dagger_Attack` + `PlayerHittedDagger()` + `resetPlayer()` | 슬롯마다 피격 스타일이 다름 |
| 피격 연출(EnemyDeftness 성공) | | | `PlayerHittedRock()` + `MartialArts` + `resetPlayer()` | | | H3 전용 하드코딩 |
| evasion 성공 시 플레이어 이동 | y = `deepOneHybrid1Ob.transform.position.y`(적 y, 버그성 의존), 0.7s | y = `playerOrigin.y`, **1s** | `playerOrigin.y`, 1s(F0 확인) | `playerOrigin.y`, 1s(F0 확인) | `playerOrigin.y`, 1s | H1 만 y 기준·지속시간이 다름 |
| 마무리 `CamRotate(0)` | 있음 | 있음 | 있음 | 있음 | 있음 | **evasion·counterattack 은 5슬롯 모두 있음. MA·SS·MS·Deftness_attack 은 H1·H2·H3 에 없고(TurnEnd 의 ResetCam 에 의존) H4·D1 에만 있음**(F0 기계 확인) |
| 피격 연출 적이 맞을 때 | `HittedByPunchChange`(counter), `HittedByShotgunChange`(돌), `HittedByMolotovChange`(화염병) 등 | | | | | 무기별 적 스프라이트 상태. 슬롯 공통이면 프로필 불필요 |

### 1-2. 슬롯 공통(프로필 불필요)으로 보이는 값
- `DOMove` 접근: 적이 플레이어 앞 `(x+2.5, y-0.2, -1)` 0.5s → 밀려남 `(x+1, y)` 1.5s, 플레이어 `(x-3, y, -1)` 1s → `AppendInterval(0.5f)` → `StandChange` → `OnSpriteChangeComplete(stand)` → `TurnEnd` → 복귀 0.5s.
- `ShakeFx.Position(cam/obj, 1, 2, 90)`.
- 사운드 이름 `Evaision`(오타 그대로, 출시판 오디오 키라 유지)·`MartialArts` 등.
- 위 값이 5슬롯에서 정말 같은지는 F0 에서 기계 비교한다(아래).

## 2. 데이터 형태 제안

### 결정: 순수 C# 표(Core) + Unity 참조는 View 쪽 어댑터
- 수치(줌 종류·회전·지속시간·y 오프셋·사운드 키)는 `Assets/Script/Core/Rules/EnemyFx.cs` 의 순수 C#(readonly struct + static 표). Unity 타입(Vector3·GameObject·Sprite)은 Core 에 넣지 않는다(Core asmdef 가 UnityEngine 비의존).
- Unity 참조(적 오브젝트·SC·스프라이트)는 기존 `BattleManager` 인스펙터 필드(`deepOneHybrid1Ob` 등)를 **그대로 두고** `BattleManager` 가 `_enemyViews[slot]`(작은 `EnemyView` 클래스: `GameObject Ob`, `DeepOneHybridSC SC`)로 묶는다. 인스펙터 필드 이름·직렬화는 바꾸지 않는다(씬 참조 보존, 바꿀 땐 `FormerlySerializedAs`).
- ScriptableObject 는 안 쓴다: 값이 5행 고정이고 테스트(EnemyDefDataTests)로 고정하기 쉬우며, 사용자 에셋 추가 부담이 없다.

```csharp
namespace SCoW.Core.Rules {
  public enum CamZoom { None, Up, Midle, Down }          // BattleCameraMove.ZoomUp/ZoomMidle/ZoomDown
  public enum PlayerHitStyle { None, Dagger, Rock }      // battlePlayer.PlayerHittedDagger/Rock
  public readonly struct EnemyFxProfile {
    public readonly CamZoom Zoom;
    public readonly float RotateDeg;        // 크기(양수). 부호는 행동이 결정
    public readonly PlayerHitStyle PlayerHit;   // 플레이어가 맞을 때 (F0 구현은 PlayerHitReaction 3개로 확장, §7)
    public readonly string HitSound;        // "MartialArts"/"Dagger_Attack"
    public readonly bool ResetPlayerAfterHit;   // battlePlayer.resetPlayer()
    public readonly float EvadeMoveSec;     // evasion 성공 때 플레이어 이동 시간 (H1 0.7, 나머지 1)
    public readonly bool EvadeYFromEnemy;   // H1 true: 이동 y 를 적 y 로 계산하는 옛 동작 보존
    public readonly bool ZoomInEvadeFail;   // H4 false: 옛 주석 처리 보존
    public readonly bool CamRotateResetOnDeftness; // (F0 구현명 CamRotateResetOnAttack, 값은 §7)
  }
  public static class EnemyFxProfiles { public static readonly EnemyFxProfile[] All; /* EnemyDefs.All 과 같은 인덱스 */ }
}
```

### 기존 값 그대로 옮기는 표 (확인된 것만 채움, "추출"은 F0 에서)

| 필드 | H1 | H2 | H3 | H4 | D1 |
|---|---|---|---|---|---|
| Zoom | Midle | Down | Up | Up | None |
| RotateDeg | 5 | 5 | 5 | 1 | 1 |
| PlayerHit(evasion 실패) | None | Dagger | None | Dagger | Dagger |
| HitSound | MartialArts | Dagger_Attack | MartialArts | Dagger_Attack | Dagger_Attack |
| ResetPlayerAfterHit | false | true | false | true | true |
| EvadeMoveSec | 0.7 | 1 | 1 | 1 | 1 |
| EvadeYFromEnemy | true | false | false | false | false |
| ZoomInEvadeFail | true | true | true | **false** | (Zoom None) |

테스트: `EnemyFxProfileDataTests`(EditMode)로 위 표를 고정한다(EnemyDefDataTests 와 같은 방식).

## 3. 통합 메서드와 단계

### 시그니처 (BattleManager 내부, 새 partial 또는 `BattleFxPlayer` 클래스로 분리 권장 — 파일 1000줄 이하 목표)

```csharp
// 행동 공통 판정: 현재 슬롯 -> 적 View/프로필
private int ActiveSlot(string playerAction);               // IsEnemyAttack/IsPointer 로 슬롯 하나를 돌려줌(-1 = 해당 없음)
private EnemyView ViewOf(int slot);                        // Ob, SC
private static bool IsSuccess(string s, bool longForm);    // "Success" / "Result : Success|Critical Success"

private void PlayEvade(int slot, bool success);            // evasion
private void PlayCounter(int slot, bool success);          // counterattack
private void PlayMartialAttack(int slot, bool success);    // MA_attack_Counter
private void PlayMelee(int slot, bool success);            // SS_attack_Counter (무기 분기는 데이터 표 WeaponFx)
private void PlayShoot(int slot, bool success);            // MS_attack
private void PlayDeftness(int slot, bool success);         // Deftness_attack, EnemyDeftness, DeepOneDeftness
```

`RetrunRolletResult` 는 최종적으로 `switch (PlayerAction)` 로 위 메서드만 부른다(약 40줄). 무기 4종(단검·배트·도끼 / 4총 / 돌·화염병)은 슬롯과 독립인 `WeaponFx`(사운드 키·플레이어 스프라이트 필드·적 피격 스프라이트 메서드) 표로 뺀다 — 1단계 이후 2번째 패스.

### 단계 (행동 하나씩, 단계마다 동치 확인 후 커밋)

| 단계 | 내용 | 줄 감소 예상 |
|---|---|---|
| F0 **완료(11-27)** | 값 추출 도구 + 프로필 표·테스트. `Assets/Editor` 에 일회용 스크립트로 BattleManager.cs 를 정규식 파싱해 행동x슬롯별 `DOMove`/`DORotate`/사운드/콜백 문자열을 정규화해 5슬롯끼리 diff, 위 표의 "추출" 칸을 채우고 슬롯 간 불일치 목록을 `Docs/`에 남긴다. **코드 변경 없음, 프로필 데이터와 테스트만 커밋** | 0 |
| F1 **완료(11-27)** | evasion → `PlayEvade`(BattleManager). 5슬롯 x 성공/실패 10조합을 구 코드·신 코드로 120프레임 트레이스(위치·카메라 회전/줌·스프라이트·HP) 비교해 전부 동일 확인 | -170 |
| F2 | counterattack → `PlayCounter` | -280 |
| F3 | MA_attack_Counter → `PlayMartialAttack` | -270 |
| F4 | EnemyDeftness/DeepOneDeftness + Deftness_attack → `PlayDeftness` (돌·화염병 2무기) | -450 |
| F5 | SS_attack_Counter → `PlayMelee` (3무기) | -700 |
| F6 | MS_attack → `PlayShoot` (4무기, 가장 큼) | -800 |
| F7 | `TurnEnd` Hybrid1 z 리셋, `RunYes` Hybrid1 Dex (아래 §5) | 소 |
| F8 | 빈 껍데기가 된 `RetrunRolletResult` 를 `switch` 로 정리, 도구 스크립트 제거 | |

단계마다 확인(스펙: 전후 스샷 비교):
1. 변경 전 커밋에서 `BattlePlaytestDriver`(`Setup(party,hp,san)` → `Begin(mode, encounter)`)로 해당 행동·슬롯·성공/실패 조합을 **같은 시드**로 돌리고, 시퀀스 중 4시점(접근 끝·피격 순간·밀려남 끝·복귀 끝)을 스샷. `mode` 는 행동마다 필요(현재 `"attack"` 기본, 행동별 모드는 F0 에서 드라이버에 추가 — 이것만 Editor 파일 수정 허용).
2. 변경 후 같은 시드·같은 조합 스샷. 같은 파일명으로 `Docs/Verify/P14bF<n>/<행동>_<슬롯>_<success|fail>_<n>.png`.
3. 픽셀 diff(카메라 줌 값·회전 포함 화면 전체). 시퀀스는 Time 기반이므로 `DOTween.Sequence` 를 `Complete` 하는 대신 고정 `Time.timeScale` 로 재생하고, 시작 후 **고정 프레임 수**에 찍는다(실시간 지터 방지).
4. 시드 로그 동치: `OnDamageObject` 호출 인자와 `UnityRng` 소비 횟수 순서를 로그(`BattlePlaytestDriver.Tail`)로 비교(Architecture.md P14 "전투 시드 로그 동치").
5. 콘솔 error 0.
스샷은 5슬롯x성공/실패 = 10컷/행동. 슬롯 동치가 이미 F0 diff 로 입증된 값은 대표 슬롯 2개(H1, D1)만 샘플링해도 된다.

## 4. Hit1D3 vs Def.AttackDamage

질문: 반격 피해가 `BattleDamage.Hit1D3` 직접 호출이라 `Def.AttackDamage` 와 값이 다름(의도 불명). **의도 확인 결과: 의도적인 서로 다른 개념이다.**

| 위치 | 대상 | 피해 | 호출 | 출처 |
|---|---|---|---|---|
| 회피·반격 실패, EnemyDeftness 성공 등 | **플레이어가 맞음** | 슬롯 공격 피해 | `OnDamageObject("player", _enemies[slot].Def.AttackDamage.Roll(...))` | `EnemyDef.AttackDamage` |
| counterattack 성공(1042·1098·1156·1212·1268) | **적이 맞음** | 플레이어 맨손 반격 1D3 | `OnDamageObject("<적Id>", BattleDamage.Hit1D3.Roll(...))` | 플레이어 쪽 값 |
| MA_attack_Counter 성공(1323·1377·1431·1484·1539) | 적이 맞음 | 플레이어 맨손 1D3 | `BattleDamage.Hit1D3` | 플레이어 쪽 값 |
| SS/MS/Deftness 성공 | 적이 맞음 | 무기 표(Dagger·Bat·Axe·SmallPistol·...·Rock·Molotov) | `BattleDamage.<무기>` | 플레이어 쪽 값 |

값 대조 (`Core/Rules/BattleEngine.cs` 15~22줄):

| 이름 | 식 | 슬롯 |
|---|---|---|
| `Hit1D3` | 1D3 | H1·H3 의 `AttackDamage`, **그리고 플레이어 맨손 피해** |
| `Hit1D3Plus1` | 1D3+1 | H2·H4 의 `AttackDamage` |
| `Hit2D3` | 2D3 | D1 의 `AttackDamage` |
| `Martial` | = `Hit1D3` | 이미 있는 상수, 플레이어 맨손 이름 |

해석: `Hit1D3` 직접 호출은 "적 슬롯의 공격 피해"가 아니라 **플레이어 맨손(Martial) 피해**다. 적 슬롯 Def 로 바꾸면 H2·H4(1D3+1)·D1(2D3) 가 플레이어 맨손 피해로 둔갑해 **출시판 수치가 변한다**. 값이 같은 이름이 하나(`Hit1D3`)라 헷갈릴 뿐이다.

권장(출시판 수치 보존 원칙):
1. 값은 바꾸지 않는다. counter/MA 성공의 `BattleDamage.Hit1D3` → `BattleDamage.Martial`로 **이름만 바꾼다**(동일 `DiceExpr`, 난수 소비 동일). 1회 `grep` 로 `Hit1D3` 잔여가 `EnemyDefs`(H1·H3 공격 피해)뿐인지 확인.
2. `EnemyDef.AttackDamage` 주석(60줄)에 "플레이어가 맞는 값. 플레이어 쪽 피해는 BattleDamage 표"를 적는다.
3. 이 질문은 `Docs/OpenQuestions.md` 에 올리지 않는다(결론이 분명하므로 정정만 큐에 기록). 다만 "카운터가 H2·D1 전에 약해야 하나" 밸런스는 사용자 판단 몫이라 PT 단계 밸런스 목록으로 넘긴다.

## 5. 소항목 (F7)

| 위치 | 현상 | 처리 |
|---|---|---|
| `TurnEnd()` 3960~3961 | player z=0, **Hybrid1 만** z=0 으로 되돌림. 연출이 z=-1 로 옮기는 건 슬롯 공통이라 다른 적은 리셋 없음(출시판 동작: 이후 연출 시작 시 `DOMove` 가 z 를 덮어씀) | 활성 적 전원(`_enemies` 순회, `ObOf(slot)`)의 z 를 0 으로. 다른 슬롯이 -1 로 남는지 F0 diff 로 확인 후 적용. 시각 차이가 보이면 H1 만 유지 |
| `RunYes()` 660 | `BattleRollet.SetBattleRollet(..., "DeepOneHybrid", ..., _enemies[SlotHybrid1].Dex)` — 도망 판정 상대 Dex 가 항상 H1 | 상대 Dex = 살아 있는 적 중 최대 Dex 로? **출시판 보존이면 H1 고정 유지**. 도망 판정 규칙은 CocDesign 영역이라 `OpenQuestions.md` Q 추가(임시 가정: H1 고정 유지, H1 이 죽었으면 첫 생존 적 Dex) |

## 6. 위험·되돌리기

| 위험 | 대응 |
|---|---|
| 슬롯 간 미세한 불일치(H1 y 기준, H4 줌 주석, DeepOne1 줌 없음)를 통합 중 무심코 평준화 | F0 diff 목록을 프로필 필드로 모두 명시. 불일치를 "버그 수정"하지 않는다(별도 큐). 모르는 차이가 나오면 필드 추가 |
| DOTween 시퀀스는 콜백·Join 순서에 민감 — 합칠 때 `Join` 이 붙는 대상이 바뀜 | 시퀀스 빌더는 기존 체인 문자 그대로 템플릿화(슬롯만 파라미터). 체인 순서 재배열 금지 |
| `Success` 문자열이 행동마다 다름 | `IsSuccess(s, longForm)` 로만 정규화, 룰렛 계약 불변 |
| 줌 호출 위치(evasion 은 분기 내부, 나머지는 분기 앞) | 순서 차이가 눈에 안 보여도 evasion 은 기존 위치 유지(`ZoomInEvadeFail` 필드) |
| 사운드 키 오타(`Evaision`) | 유지(리소스 키) |
| 파티 전투(`PartyBattleView`)와 겹침 | 파티 모드는 별도 뷰. 이 작업은 솔로 `BattleManager` 만. 같은 파일 동시 수정 금지 |
| 사용자 플레이 중 에디터 손대지 않기 | 플레이테스트는 사용자가 안 할 때만 |

되돌리기: 단계당 1커밋, 커밋 직전 `git tag p14b-f<n>-pre`. 스샷 diff 가 틀어지면 해당 커밋만 `git revert`. F0 데이터·테스트는 F1 이전에 독립 커밋이라 남겨도 무해. 전체 롤백 기준점: F0 커밋.

미검증: 이 문서는 대표 블록과 grep 으로만 작성. 5슬롯 전부의 값 비교는 F0 에서 기계로 확인한다.

## 7. F0 결과 (11-27, 완료)

방법: 일회용 정규식 스크립트(저장소 밖)로 행동 6종 x 슬롯 5개 블록을 잘라 슬롯 이름(`deepOneHybridN`/`deepOne1`/`Slot*`)을 정규화한 뒤 H1 과 줄 단위 diff, 줌·회전·사운드·`PlayerHitted*`·`resetPlayer`·hitted 스프라이트·`CamRotate(0)` 개수를 슬롯별로 비교. 코드 변경 없음. 구현: `Assets/Script/Core/Rules/EnemyFx.cs`(`EnemyFxProfiles.All`, `EnemyDefs.All` 과 같은 인덱스), 고정 테스트 `Assets/Tests/EditMode/EnemyFxProfileDataTests.cs`.

### 7-1. 표 확장 (원본 확인으로 추가된 필드)
- 피격 반응이 행동별로 다르다 → `PlayerHitReaction{PlayerHit, HitSound, HittedSprite, ResetPlayer}` 를 `EvadeFailHit`(evasion·counterattack 공통) / `MartialFailHit`(MA) / `MeleeFailHit`(SS, 무기 3종 공통)로 3개 둔다.
- `CamRotateResetOnDeftness` → `CamRotateResetOnAttack`(MA·SS·MS·Deftness 전체에 해당).

| 반응(실패 시) | H1 | H2 | H3 | H4 | D1 |
|---|---|---|---|---|---|
| EvadeFail(evasion·counter): Hit/Sound/hitted/reset | None/MartialArts/o/x | Dagger/Dagger_Attack/o/o | = H1 | Dagger/Dagger_Attack/**x**/o | Dagger/Dagger_Attack/o/o |
| MartialFail(MA): 〃 | None/MartialArts/o/x | **None**/Dagger_Attack/o/o | = H1 | Dagger/Dagger_Attack/x/o | **None**/Dagger_Attack/o/o |
| MeleeFail(SS): 〃 | None/MartialArts/o/x | None/**MartialArts**/o/o | = H1 | Dagger/Dagger_Attack/x/o | None/Dagger_Attack/o/o |
| CamRotateResetOnAttack | x | x | x | o | o |

Deftness 단일 블록: EnemyDeftness(H3) 성공=`PlayerHittedRock`+`MartialArts`+`resetPlayer`, 실패=`Evaision` 이동만. DeepOneDeftness(D1) 성공=`posionDebuff=3`+`PlayerHittedPosion`+`Posion_Attack`+`resetPlayer`, 실패=`Evaision` 이동만. 둘 다 표에 안 넣음(F4 에서 단일 처리).

### 7-2. 슬롯 간 불일치 목록 (통합 때 평준화 금지)
1. evasion 성공 이동: H1 만 y=적 오브젝트 y, 0.7s. 나머지 `playerOrigin.y`, 1s.
2. evasion 실패 줌: H4 만 줌 주석 처리(성공은 줌). D1 은 줌 자체 없음.
3. evasion·counter 에서 H4 만 hitted 스프라이트 없이 `PlayerHittedDagger`. MA·SS 도 H4 만 `PlayerHittedDagger` 호출(H2·D1 은 hitted+`resetPlayer` 만, PlayerHittedDagger 없음).
4. SS 실패 사운드: H2 는 `resetPlayer` 가 있지만 사운드는 `MartialArts`(MA 의 H2 는 `Dagger_Attack`).
5. 호출 순서가 슬롯마다 다르다: `PlayerHittedDagger` 가 `OnDamageObject` 앞(H4) 또는 뒤(H2·D1), 체인 순서 재배열 금지(§6).
6. counterattack(`CamRotate(+n)` 1회)·MA(`CamRotate(-n)` 1회)·SS(`CamRotate(-n)` 3회)는 `DORotate` 외에 콜백 회전을 더 부른다. 크기 n 은 슬롯별 RotateDeg(5/5/5/1/1)와 같고 부호·위치는 F2·F3 에서 원본 줄 그대로 옮긴다.
7. MS_attack 의 H3·H4 에는 `SmallPisol_Hit` 사운드 호출이 없다(H1·H2·D1 은 있음, 사운드 키 `SmallPisol_*` 오타 유지).
8. 줌 호출 위치: evasion 만 성공·실패 각각 안, 나머지는 슬롯 분기 첫 줄(문서 §1-1 확인).
9. `CamRotate(0)`: 위 표 마지막 줄. 문서 초안의 "Deftness_attack H1 만 없음"은 틀렸고 H1~H3 가 모두 없다.
10. (F1 에서 발견) evasion 성공 이동에서 H1 은 `new Vector3(x-4, 적y)` 라 **z=0**, 나머지는 z=-1. 프로필 `EvadeYFromEnemy` 가 y 와 z 를 함께 재현한다.

미확인: DOMove 접근·밀려남 좌표가 5슬롯 같다는 것은 evasion·counter 는 diff 로 확인(차이는 위 목록뿐), MA·SS·MS·Deftness 는 줌·회전·사운드·피격 호출 개수 비교까지만 확인(DOMove 수치 diff 는 F3 이후 해당 단계에서 전체 diff 로 재확인).
