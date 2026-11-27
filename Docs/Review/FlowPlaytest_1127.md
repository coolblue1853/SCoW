# 흐름 실플레이 검증 2차 (11-27)

방법: 플레이 모드(Main 씬)에서 `Assets/Editor/FlowPlaytestDriver.cs`(execute_code)로 정상 캐릭터 상태를 만들고(STR60·DEX60·INT70·HP60·SAN65) 실제 경로(`PotalPoint.MovePlayer`·`SewerInvestigatePoint.Investigate`·`Rollet` 펌프·`InteractionController.*`·`StoryDatabase`)로 진행했다. 실제 키 입력·포커스는 쓰지 않았다. 세이브 슬롯(`slot_*.es3`)은 읽기만 했고 검증 전후 슬롯 파일이 없음을 확인했다(자동 저장 미발동: 하수도·가짜 장소 이동은 `MapManager` 이동 끝을 타지 않는다). 엔딩 기록은 `EndingService.FilePrefix = "flowtest_"` 로 시험 파일에 쓰고 끝나면 지웠다. `settings.es3`·`readlog.es3` 는 설정창 열기·대사 종료로 갱신됐다(슬롯 아님).

## 흐름별 결과

| # | 흐름 | 결과 | 스크린샷(`Logs/playtest/`) |
|---|---|---|---|
| 1 | 하수도 문 이동 z0 -> z1_1~3 -> 샛길(Middle) z2a_1~3 -> z3_1(Middle 도착) -> 허브 z3_2 -> 비밀기지 z4_1 -> 문양 앞 z5_1 -> 문양실 z5_2, 오른쪽 분기 z2b_1(조우 1) | 통과. 도착 문이 맞다(Middle 도착 x=-1211.1, Right 로 돌아오면 x=-1194.2). 막힌 문(허브 오른쪽, 비밀기지 방문 전)은 `IsBlocked` 로 막힘, 방문 뒤 열림 | `flow_sewer_z2a1.png`, `flow_sewer_rest.png` |
| 2 | 되돌아가기 복원 | 통과. z1_2<->z1_3, 허브<->비밀기지, z5_1<->z5_2 왕복 시 같은 외형(터널·전구)·처치한 적 표시 꺼짐 유지. `SewerFlow.RestoreCurrent`(불러오기 경로) 도 방·적 표시 복원 | - |
| 3 | 조사 포인트 3곳(Workwear 운 판정 -> 대실패 플래그 안 켜짐, Work log 대사 -> 플래그 켜짐, Signboard 지력 판정 -> 성공 플래그 켜짐) | 통과. 룰렛 -> 결과 대사 -> `looked` 기록·콜라이더 비활성. 판정 실패는 플래그 없이 한 번만 시도 | `flow_sewer_point_roll.png`, `flow_sewer_signboard_roll*.png`, `flow_sewer_log_dialog-1.png` |
| 4 | 휴식 구역 z4_1 | 통과. SAN 40 -> 41(+1d?, 처음 한 번 `restKey`=1), 전구 안 깜빡이고 시야 넓음 | `flow_sewer_rest.png` |
| 5 | 보상(S7) | 통과. 허브 처치 -> 붕대 1개(`bandages`=1, `reward.1`=1), 적 표시 꺼짐 | - |
| 6 | 미니맵(S5) | 통과(수정 1건). 방문 방 수만큼 노드(5->7->9->12) 갱신, 작게/크게 토글, 대사·판정 중 자동 숨김. 크게 보기가 왼쪽 위 목표 HUD 와 겹쳤다(BUG-1, 수정). 지도 아이템 없으면 구역 라벨 0 은 의도 | `flow_sewer_minimap_big.png`(수정 전), `flow_sewer_minimap_big_fixed-1.png`(수정 후) |
| 7 | 동료 합류(PT-3) | 통과. 알버트: time 5 에서는 합류 안 됨, time 9 에서 합류(HP75·SAN55·Ready). 메이브: time 10 에서 합류(HP55·SAN65). 파티 순서 fabian,albert,maeve. 이미 합류한 뒤 같은 키워드는 "I'm not sure about that." 폴백 | `flow_party_albert.png`, `flow_party_albert_dialog-2.png` |
| 8 | 스토리 P0 구제 3종 | 통과. 에이든 놓침: time 4·5 에서 `aidenFirstDialog` 꺼짐이면 안내 줄 추가(Fab_FirstDialog·Case1After·Case2Fail·Case2SuccAfter), 켜짐이면 원래 대사. 구제 우편: time 9 + 열쇠·하수도 손기술 없으면 Fab_MorningMail(악몽 3·4 있으면)/MorningMailB, 열쇠 있으면 우편 없음. D-day: 2일차 밤 "Tonight is July 12th ... Two more nights", 3일차 밤 "July 13th ... Just one more night", 수첩 날짜 줄 7/11·7/13·Jul 14 | - |
| 9 | 엔딩 기록(UX-12) | 통과. `InSewerDialog("Ending_BreakSymbol")` 로 문양 파괴 엔딩 장면 시작 -> `end_symbol` 처음 기록, 기록 다시 읽어도 유지, 다른 엔딩은 미기록. 설정창 엔딩 탭: A "달성", 나머지 "???" | `flow_ending_tab.png` |

콘솔: 코드 error 0. 검증 틀이 `dialogManager` 를 껐다 켜서 생긴 "Coroutine couldn't be started because the the game object 'UI_Dialog' is inactive!" 와, execute_code 재진입으로 보이는 "PlayerLoop internal function has been called recursively"(5건)만 떴다. 실제 플레이 경로에서는 확인하지 못했다(미해결 의심, 아래).

## 발견한 버그

- **BUG-1 (수정함)**: 미니맵 크게 보기(M)가 화면 위쪽 가운데에 떠서 왼쪽 위 목표 HUD(Day·Deadline·목표 글)와 겹쳐 지도 왼쪽 노드가 글자에 가렸다. `SewerMinimapView` 의 크게 보기 위치를 위쪽 오른쪽으로 옮겼다(작게 보기와 같은 앵커, 위 여백만 큼). 수정 후 지도 왼쪽 끝이 HUD 오른쪽 끝보다 오른쪽이다(1920x1080 기준 스샷 확인, 다른 해상도는 미확인). UI 위치라 EditMode 회귀 테스트는 만들지 않았다.

## 관찰·큐로 넘김(고치지 않음)

- 조우 번호가 방 고정이 아니라 "마주친 순서"다(`SewerNav.Arrive`). 오른쪽 분기(z2b_1)를 건너뛰면 허브가 E1(혼혈 구성)로 열려 설계표(E2 허브 = 심해인)와 다르다. 큐 SEWER-ENC-1.
- 하수도 첫 진입 대사·전투 대사는 이번에 열지 않았다(전투는 11-27 1차 검증). 조우 방 진입 시 1초 뒤 `PrintBattleDialog` 호출은 취소하고 진행했다.
- 동료 합류에 수락/보류 UI 없음(키워드 제시 = 수락, Party.md PT-3 임시 가정). 변경 없음.
- 목표 HUD 글 "Day 1 Morning / Deadline in 15" 줄간격이 촘촘하다(2줄이 붙어 보임). 영향 작음, UX 큐에서 다룰 것.
- 키워드 획득 토스트("New keyword: ...")가 설정창 위쪽에 겹쳐 보였다(설정창이 토스트보다 아래 레이어). 영향 작음.

## 검증 틀 메모

`FlowPlaytestDriver.SewerSetup(hp, san)` -> `Door("Sewer_Right"|"Sewer_Left"|"Sewer_Middle")` -> `Investigate(i)` -> `PumpRollet()` -> `Info()`. 동일 이름 문 오브젝트가 전투용 사본에도 있어 활성 쪽을 우선 찾는다. 대사를 실제로 띄우려면 `dialogManager` 활성(`DirectingManager.dialogManager`) 필요, 대사 내용만 볼 때는 `DialogManager.ShowDialogProbe` 로 가로챈다. 미니맵은 0.2초 폴링이라 이동과 같은 호출에서 노드 수가 아직 바뀌지 않는다.
