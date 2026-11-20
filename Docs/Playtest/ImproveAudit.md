# 개선 후보 점검표 (SETUP-3)

2026-11-20 조사. 방법: `Assets/Script` grep 위주(`_/` 서드파티·씬·프리팹 통독 안 함). 코드 수정 없음. 줄 번호는 조사 시점 기준.
확정 못 한 것은 "의심/미확인"으로 표시. 입력(`Input.*` 69줄)은 `Docs/Playtest/InputMigration.md` 참조(여기 중복 기재 안 함).
표기: 크기 S(한 파일 소수 줄)/M(여러 파일 or 설계 필요)/L(구조 변경). 우선순위 P0(먼저)/P1/P2.

## 0. P0 상위 10개 요약

| # | 분류 | 항목 | 근거 | 크기 |
|---|---|---|---|---|
| 1 | ③ | 저장·불러오기 없음(종료하면 전부 소실) | A-1 / B-1 | L |
| 2 | ① | 디버그 치트(F1~F12, 숫자키, P)가 출시 빌드에도 동작 | A-1 | S |
| 3 | ① | `ResetData()` 가 일부 정적 값을 안 되돌림 → 타이틀 복귀 후 재플레이 시 엔딩·데스카운트·독 상태 오염 | A-2 | S |
| 4 | ① | CSV 파서가 `,` 로만 분리 → 대사에 쉼표 있으면 잘림, `\r` 잔존, 빈 파일/누락 시 NRE | A-3 | M |
| 5 | ② | 대사 되감기(백로그)·자동 진행·텍스트 속도 설정 없음 | B-2 | M |
| 6 | ② | 설정(볼륨·해상도·전체화면)이 저장 안 됨, 실행마다 초기화 | B-3 | S |
| 7 | ① | DOTween 573회(BattleManager) 에 `Kill`/`SetLink` 없음 → 씬 전환·중단 시 파괴된 객체 접근 위험 | A-5 | M |
| 8 | ④ | .cs 21개 비 UTF-8(CP949 추정) → 에디터/AI 도구가 수정 거부, 한글 깨짐 위험 | D-2 | M |
| 9 | ③ | 대사 넘김이 마우스 클릭/E 뿐이고 UI 위 클릭과 충돌 가능, 스킵은 있으나 중단·되감기 없음 | B-2, C-2 | M |
| 10 | ① | 전투 데미지 주사위 식 불일치(`Random.Range(1,2)*5` = 항상 5 등) | A-4 | S |

## A. ① 출시판 버그 의심

| ID | 근거(파일:줄) | 내용·영향 | 크기 | 우선 |
|---|---|---|---|---|
| A-1 | `Rollet/RolletTest.cs` (키 처리 78~195줄; 컴포넌트 guid `3ac8fcc4…` 가 `Main.unity` 에 배치됨, `#if` 가드 없음) | 치트: F12(하수도 이동)·F10(엔딩 플래그)·F7/8/9·F1~F6·Alpha1~6·P. 일반 유저가 누르면 진행 꼬임·엔딩 스킵. `Awake` 에서 스탯도 굴림(21~27줄) — 동작 확인 필요. 입력 교체와 묶어 `#if UNITY_EDITOR || DEVELOPMENT_BUILD` 가드 또는 제거 | S | P0 |
| A-2 | `old_Dialog/DatabaseManager.cs` `ResetData()` 734줄~(1224줄 근처까지). `enemyDeathCount`(11), `posionDebuff`(48), `endingDoorCheck`(9), `endingLadderSanInt`(18) 는 전 코드베이스에서 `=0/false` 대입 없음 | 타이틀 복귀(설정창 `GoToMain`, 엔딩 `GoToMain`) 후 새 게임에서 이전 판 값이 남음. `ending*` 나머지 플래그도 리셋 여부 전수 확인 필요(`endingDeathCheck` 등 의심). 볼륨(227~228)은 의도적 유지로 보임 | S | P0 |
| A-3 | `dialogue/DialogPaser.cs:10~30` | `Resources.Load` 결과 null 검사 없음(CSV 이름 오타 시 NRE). `Split(',')` 라 대사에 쉼표 있으면 `row[2]` 가 잘림. 줄 끝 `\r` 미제거(CRLF CSV). 빈 줄 → `row[1]` IndexOutOfRange. `spriteList` 는 채우지 않음(미사용 확인 필요). CSV 따옴표 필드 미지원 | M | P0 |
| A-4 | `Battle/BattleManager.cs:331` `Random.Range(1,2)*5`(항상 5), `:1601`·`:1765` 주석 `1D8` vs 코드 `Range(1,7)+2`(=1d6+2), `:760~1685` 다수 `Range(1,4)*5`(=1d3, 주석 없는 곳 의도 불명) | int `Random.Range` 상한 제외 규칙 오용 의심. CoC 규칙상 1d4/1d6/1d8 이어야 하는지 기획 확인 필요(미결 M1) | S | P0 |
| A-5 | `DOTween` 사용: `BattleManager` 573줄(DO*), 프로젝트 전체 `Kill`/`SetLink` 은 `DirectingManager` 1건뿐 | 전투 중 설정→타이틀 복귀 등 씬 전환 시 트윈이 파괴된 오브젝트를 건드려 MissingReference 가능(의심). `BattleManager.cs` 는 비 UTF-8 | M | P1 |
| A-6 | `dialogue/DialogManager.cs:119~131` `Update` 가 매 프레임 `zNext1/zNext2` 갱신, `:148` `Input.GetMouseButtonDown(0) && daveCheck==false` 이지만 E 키는 `daveCheck` 무시 | 선택지 대기/연출 중 E 로 대사 진행되는 분기 불일치 의심. 입력 교체 때 동작 정의 후 정리 | S | P1 |
| A-7 | `dialogue/DialogManager.cs:214~236` `Skip()` 가 코루틴 자기 재귀(`StartCoroutine(Skip())`) + `SkipOn` 중복 호출 가드 없음 | 스킵 버튼 연타 시 코루틴 중복 → 대사 과속·건너뜀(의심). 씬 전환 중 스킵 상태 `skipActive`(`DatabaseManager.cs:344`) 는 `ResetData` 에서 해제(868줄) | S | P1 |
| A-8 | `DirectringManager/DirectingManager.cs:601,604` `Random.Range(1, enemyDeathCount*5)`, `Range(1, luk)` | `enemyDeathCount==0` 이면 `Range(1,0)` → 항상 1(의도 불명). A-2 와 연계 | S | P2 |
| A-9 | `Character/CamReMove.cs:11` `FindWithTag("MainCamera").GetComponent<CameraManager>()` null 가드 없음, `dialogue/DialogManager.cs:117` `FindObjectOfType<SpriteManager>` 후 null 가드 확인 못 함 | 씬 구성 변경 시 NRE. 현재 씬에서는 동작 추정 | S | P2 |
| A-10 | `Map/*`, `UI/*`, `BillowUI/*` 에서 `DontDestroyOnLoad` 싱글턴(`MapManager`·`DialogManager`·`InteractionController` 등) 이 `Insert`→`Main` 재진입 시 중복 파괴 로직(`Awake`)만 있고 상태 리셋은 `ResetData` 의존 | 타이틀 복귀 후 매니저 내부 비정적 상태(진행 단계 등) 가 남을 수 있음(의심). 재플레이 회귀 테스트 필요(검증 시나리오 5절) | M | P1 |
| A-11 | `Main/ManageMain.cs:29` `Application.Quit()` 만 있음, 에디터 분기 없음 | 에디터에선 종료 안 됨(출시판 영향 없음) | S | P2 |

## B. ② 불편(UX)

| ID | 근거 | 내용·영향 | 크기 | 우선 |
|---|---|---|---|---|
| B-1 | `ES3.`/`PlayerPrefs` 호출 0건(`GameOverview.md` 4-1). 상태는 `DataBaseManager` 정적 필드 561개 | 저장·이어하기 없음. 한 번에 끝까지 못 하면 처음부터. 설계 문서 필요(상태 목록 → 직렬화 계층). 미결 Q3 | L | P0 |
| B-2 | `DialogManager.cs`: `SkipOn`(181줄)·`textDelay`(109줄)·`waitForNormalChars=0.03f`(200줄) 하드코딩. 백로그·되감기·자동 진행 코드 없음(grep: `backlog/History/autoPlay` 0건) | 지나간 대사 못 다시 봄, 속도 조절은 스킵 on/off 뿐. 정보 밀도가 높은 조사/심문 게임이라 불편 큼 | M | P0 |
| B-3 | `UI/settingManager.cs:83~132` 해상도 12분기 하드코딩·`isFullScrean` 변수, `SoundManager.cs:203~314` 볼륨은 `DataBaseManager.soundVolume/sfxVolume`(정적, 227~228줄 기본 0.5) | 설정이 재실행 시 초기화(PlayerPrefs 미사용). 해상도 목록·전체화면 상태 표시 불일치 가능. 텍스트 속도·언어 설정 없음 | S | P1 |
| B-4 | `UI/settingManager.cs:48~56` `OpenSetting` 이 8개 플래그 AND(대사·연출·UI 열림 중엔 설정 못 염) | 대사 중 일시정지·옵션 변경 불가. 대사 중에도 설정(볼륨 등)을 열 수 있게 | M | P1 |
| B-5 | `Asset/Intro.cs`(스킵 처리 grep 0건), `Intro` 클래스 | 인트로 스킵 불가 의심(미확인, 확인 필요) | S | P1 |
| B-6 | `Rollet/Rollet.cs:75~102` 굴리기가 E 키/클릭, 결과 연출 길이 고정 | 판정 연출 가속/생략 없음. 전투에서 반복 시 피로(`BattleRollet`) | M | P2 |
| B-7 | 게임패드 입력 없음(`InputMigration.md` 와 별건 — 사용자 규칙 "마우스+키보드+패드 3경로") | 전 UI 패드 불가 | L | P1 |
| B-8 | 튜토리얼(`TutoralManage`, `TutorialBox`) 다시 보기 위치는 설정창 `tutorial` 오브젝트(`settingManager.cs:64`) | 존재하긴 함. 내용 점검만 필요 | S | P2 |

## C. ③ 장르 관례 이탈 (고유 설계가 아니면 수정)

| ID | 근거 | 일반 관례 vs 현재 | 크기 | 우선 |
|---|---|---|---|---|
| C-1 | 저장 없음(B-1), 설정 비저장(B-3) | 비주얼노벨·TRPG 는 수동/자동 저장·이어하기·설정 영구 저장이 기본 | L | P0 |
| C-2 | `DialogManager.cs:123,148` 대사 진행이 E 또는 좌클릭만. 되감기·로그 없음(B-2) | VN 관례: 휠 위/아래 로그, Space/Enter 진행, 자동/스킵, Ctrl 스킵 | M | P0 |
| C-3 | `Rollet.cs:53` 등 Esc 가 닫기에 쓰이나 `settingManager.cs:157,161` 에서 설정창 토글도 담당 | Esc 우선순위(가장 위 UI 먼저 닫기)가 코드마다 따로. 겹치면 둘 다 반응 가능. `UpperUiChecker` 존재(역할 미확인) | M | P1 |
| C-4 | 키 배치: 대사 E, 포탈 F, 수첩 Alpha1, 지도 Alpha2, SIS Alpha3, 이동 Horizontal(상하 없음) — 키 변경 불가(`keySetting` 오브젝트는 있음, 리바인드 코드 미확인) | 관례: 리바인딩·화면 하단 키 안내 | M | P2 |
| C-5 | CoC 판정: `Rollet.cs:247~268` 10의 자리·1의 자리 따로 굴리는 퍼센타일 구현. 보너스/페널티 주사위 개념으로 구현된 듯(`nomal/bad/good`). 공식 룰의 어려움 단계(보통/어려움/극단) 구현 여부 미확인 | 룰 일치 확인 필요(미결 M2) | M | P2 |
| C-6 | 전투 데미지 단위 `*5`(A-4) 스케일이 룰북(HP 약 10~15) 과 다름. `str*5` 스탯 방식은 7판 룰과 일치 | 의도 확인 필요(미결 M1) | S | P2 |
| C-7 | 해상도 640x360 등 저해상도 12분기. 창모드/전체화면이 따로 변수 | 관례: 모니터 해상도 자동, 전체화면 토글 1개 | S | P2 |
| C-8 | 키워드/인물 UI 이동이 W/S·A/D(IntelUI) 로만 | 마우스 휠·화살표·패드 방향키 | S | P1 |

## D. ④ 코드 부채

| ID | 근거 | 내용 | 크기 | 우선 |
|---|---|---|---|---|
| D-1 | `dialogue/InteractionController.cs` 3200줄+(`GetComponent` 519회), `Map/TimeManagere.cs` 845줄+(`Debug.Log` 20), `Battle/BattleManager.cs` 1800줄+ | 500줄 규칙 초과. 장소별 분기 분리(데이터 주도) 필요. 단독 수정 위험 큼 → 설계 문서부터 | L | P1 |
| D-2 | **비 UTF-8 추정 .cs 21개**(non-ASCII 바이트는 있으나 UTF-8 한글 매치 없음): `setStat/setSkill`, `old_Dialog/End_Look_Judge`, `old_Dialog/DatabaseManager`, `UI/TutorialBox`, `UI/ShopUI`, `UI/KeywordButton`, `UI/KeyItemBox`, `Rollet/Rollet`, `Rollet/BattleRollet`, `Map/TimeManagere`, `Map/PotalPoint`, `Map/MapManager`, `Map/Intel_ObtoUI`, `Map/FadingBackGround`, `Map/ChangeMapButton`, `DirectringManager/DirectingManager`, `BillowUI/BillowUIManager`, `Battle/InSewerEnemy`, `Battle/BattleManager`, `Asset/SafeChecker`, `Asset/Fixed` | 도구가 Edit/Write 거부(실제 확인: `DatabaseManager.cs`·`RolletTest.cs`·`BattleManager.cs`). 나머지는 추정(파일 단위 확인 필요). UTF-8 일괄 변환(바이트 검증 후 BOM 없음 + CRLF 유지) 별도 작업 권장. `settingManager.cs` 는 ASCII 만이라 해당 없음(`GameOverview.md` 6절 서술과 다름, 재확인) | M | P0 |
| D-3 | `DatabaseManager.cs` 정적 필드 561개, 8플래그 AND 가 `settingManager.cs:50` 등에 복제 | `IsBusy()` 한 곳으로 모으기(S), 장기적으로 상태 객체화(L, 저장과 함께) | M | P1 |
| D-4 | 씬/CSV 이름 문자열 하드코딩: `LoadScene` 6곳(`ManageMain:33`, `FadingBackGround:18`, `setSkill:71`, `settingManager:42`), `SoundManager.cs:353~364` 이름 비교 | 상수화(S). 이름 변경 금지 | S | P2 |
| D-5 | `DOTween` 만 573회 직접 호출한 `BattleManager`(행동 시퀀스 복붙: 1565~1830줄 구간 적별 동일 구조) | 적 종류별 복제 코드. 데이터화 | L | P2 |
| D-6 | 디버그 잔재: `Debug.Log` 43건/14개 파일(`TimeManagere` 20, `RolletTest` 7), `IntelUI/Test.cs`, `DataBaseManager.isTest`(86줄, `DirectingManager.cs:98` 에서 사용) | 출시 빌드 콘솔 로그·성능. 제거/조건부 | S | P1 |
| D-7 | 이름 오타: `TimeManagere`, `DialogPaser`/`Prase`, `DirectringManager`, `SetCharcter`, `Dialog.name`, `daveCheck` 등 | 직렬화 영향 → `FormerlySerializedAs` 필요. 우선순위 낮음 | M | P2 |
| D-8 | `Update` 가 58개 파일, 이 중 상당수가 매 프레임 입력만 폴링 | 입력 교체 때 이벤트 기반으로 같이 정리 | M | P2 |
| D-9 | I2 Localization 에셋(4개 언어)만 있고 코드 미사용, ES3 플러그인만 포함 | 빌드 크기·유지 비용. 미결 Q4 | S | P2 |
| D-10 | `Player.cs` 에 `GetComponent` 3회(Update 안 여부 확인 필요), `IntelManager` 8회, `DialogManager` 4회 | 캐시 확인 필요 | S | P2 |

## 5. 검증 시나리오(수정 때 프로브)

- 재플레이 회귀: 새 게임 → 전투에서 적 처치·독 부여 → 설정 → 타이틀 → 새 게임. `enemyDeathCount`·`posionDebuff`·`ending*` 를 로그로 출력해 0/false 확인(A-2, A-10).
- 빌드/에디터에서 F1~F12·Alpha·P 입력 시 치트 동작 여부(A-1).
- CSV 파서: 쉼표 포함 대사 한 줄·CRLF·빈 줄을 넣은 테스트 CSV로 `DialogPaser.Prase` EditMode 테스트(A-3).
- 전투 데미지 분포: `OnDamageObject` 호출 인자를 수천 회 샘플링해 최소·최대 확인(A-4).
- 스킵 버튼 연타 후 `skipActive`·코루틴 수 확인(A-7).
- 인코딩: 각 .cs 를 `iconv`/파이썬으로 UTF-8 strict 디코드 시도해 D-2 목록 확정.

## 미결 (임시 가정)

- M1 전투 데미지 주사위 의도(예: `Range(1,4)*5` 는 1d3×5 인지 1d4 인지, `*5` 스케일 의도) — 가정: 코드 주석 표기(1D4/1D8) 가 의도, 불일치만 수정. 기획 확인 필요.
- M2 CoC 판정 규칙(보통/어려움/극단 성공 단계·보너스 페널티 주사위) 일치 정도 — 가정: 현재 구현 유지, 룰 정확성 개선은 후순위.
- M3 출시 플랫폼(PC만? 패드/모바일?) — 가정: PC(Steam 추정), 패드는 사용자 규칙대로 추가.
- M4 저장 방식(슬롯 수·자동 저장 시점) — 가정: 자동 저장 1슬롯 + 수동 3슬롯, 장소 이동·낮/밤 전환 시 자동 저장. ES3 사용, 키 추가만. Q3 와 연동.
- M5 D-2 인코딩 일괄 UTF-8 변환 허용 여부 — 가정: 변환 전 바이트 검증하고 허용(커밋 분리).
- M6 치트 키(A-1)를 완전 삭제할지 에디터/개발 빌드 한정으로 남길지 — 가정: 에디터/개발 빌드 한정.
