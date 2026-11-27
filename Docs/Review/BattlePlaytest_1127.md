# 전투 실플레이 검증 (11-27)

방법: 플레이 모드(Main 씬)에서 `Assets/Editor/BattlePlaytestDriver.cs`(execute_code)로 정상 캐릭터 상태를 만들고(STR60·DEX60·INT70·최대 HP 60·SAN 65, 동료는 `PartyState.Join`), `DirectingManager.GotoBattleSewer()`로 하수도 전투를 연 뒤 버튼 onClick·`PartyBattleView.DebugAnswer`·룰렛 내부 호출로 진행했다. 실제 물리 키·마우스 입력은 주입하지 못해 **미검증**. HP 척도는 피해식(x5)에 맞춰 최대 HP 60(솔로 승리·도주 확인은 HP 300·적 HP 12 로 낮춘 설정). 세이브 슬롯은 쓰지 않았다(권한 거부).

## 흐름별 결과

| # | 흐름 | 결과 | 스크린샷 |
|---|---|---|---|
| 1 | 하수도 솔로 E1: 공격·회피·승리 | 통과. 무술→적 선택→룰렛→피해, 회피 선택→룰렛, 두 적 처치 후 턴 심볼 꺼짐·`startBattle=false`·`sewerBattleEndCheck=true`·`enemyDeathCount=2` | `Logs/playtest/battle_solo.png`, `battle_solo_roll.png`, `battle_solo_win.png` |
| 2 | 패배 | 솔로·파티 모두 사망 화면 도달. 솔로는 HP<=0 이후에도 전투가 계속되던 **버그 수정**(아래 BUG-1). 선택지는 저장 없음이라 2개(타이틀·종료). 3지선다(마지막 저장 재개)는 저장 슬롯을 만들 수 없어 **미검증**(`ResumeRules` EditMode 테스트가 있음) | `battle_lose.png`, `battle_party_lose.png` |
| 3 | 도주 | 솔로·파티 모두 통과(판정 성공 → `EndSewerBattle`/`Fled` 정산 → `inSewerStealthSucc` 처리) | `battle_flee.png` |
| 4 | 파티 3인(PT-5) | 통과. 행동 메뉴·대상 고르기·대열 교체(Fabian<->Albert)·사격/공격·승리 후 HP 되쓰기(`party.*.hp`) 확인 | `battle_party.png`, `battle_party_win.png` |
| 5 | 파티 E1~E3 연속 | 통과(E1 적 3·E2 적 2·E3 적 2, 동료 HP 이월). 전투 사이 `RestoreForSewer` 후 새 적 구성 | 위와 동일 |

콘솔: error 는 "Coroutine couldn't be started because the game object 'UI_Dialog' is inactive!" 1건뿐. 검증 틀이 Main 첫 대사 창(`dialogManager`)을 꺼서 생긴 것으로 판단(실제 흐름에서는 전투 전에 대사가 끝남). 그 외 전투 관련 exception 없음.

## 발견한 버그

- **BUG-1 (수정함)**: HP<=0 이 돼 사망 화면이 떠도 `BattleManager.Update` 가 새 차례를 계속 열어 사망 오버레이 밑에서 적이 행동하고 SAN 판정창까지 뜸. `BattleEngine.IsPlayerDown(hp, san)`(사망 화면과 같은 조건) 추가, `Update` 에서 쓰러졌으면 새 차례를 열지 않음. 회귀 테스트 `BattleEngineTests.IsPlayerDown_HpOrSanZeroOrLess`, 플레이로 사망 뒤 25초간 로그 변화 없음 확인.
- **큐 BATTLE-UI-1 (고치지 않음)**: 파티 전투 툴팁이 메뉴 3~5번을 가림, 전투 중 HUD(Day·Deadline) 겹침, 사망 화면 밑 옛 endGame 비침, 구 HP/SAN 글자 겹침, 옛 전투 스프라이트가 패널 뒤에 비침, 동료는 임시 파란 사각형(알려진 임시 그래픽).
- **큐 BATTLE-BAL-1 (고치지 않음)**: E1 솔로 균형. 반격 1회 최대 30·혼혈 10~15 vs 적 HP 54·104 라 솔로는 사실상 질 수밖에 없다. 의도 확인 필요.

## 검증 틀 메모

`BattlePlaytestDriver.Setup(party, hp, san)` → `Begin(mode, encounter)`(mode: attack·evade·flee) → `Tail(n)`; 로그 `Logs/playtest/driver.log`. 솔로 적 선택은 `BattleManager.onPointerEnemy` 를 먼저 맞춰야 한다(마우스 호버가 하던 일). 룰렛은 `activeInHierarchy` 로 거를 것(숨은 `Rollet` 의 `setUI.activeSelf` 가 항상 true).
