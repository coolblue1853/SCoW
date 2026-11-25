# 작업 큐 (원본)

형식: `- [ ] 내용 (담당: 이름, 파일: 건드릴 경로)`. 끝나면 `[x]` + 커밋 해시.

- [x] **SETUP-1** (11-21 Unity 6 로 연결 확인) unity-mcp 설치: `.claude-rules/Framework/EditorAutomation.md` 0절. 2021.3 호환 확인 → 컴파일·플레이 1회(`bash Tools/playtest.sh 8 first`) 확인.
- [x] **SETUP-2** (ed0797c) 코드·씬 조사로 `.claude-rules/Game/GameOverview.md` 채우기(플레이 흐름·폴더 역할·세이브 키·현지화).
- [x] **INPUT-1** (C-2 P2·P2b·P2c 104f179·ee165fb·16ca917, 실제 키·패드 동작은 사용자 플레이 확인 필요) (11-20 사용자: 옛 입력 방식 싹 다 고친다) 레거시 `Input.*` 호출 전부 New Input System 으로 교체. 순서: ① `grep -rn "Input\.Get\|Input\.mouse\|Input\.any\|Input\.touch" Assets/Script` 로 전수 목록·동작 표(`Docs/Playtest/InputMigration.md`) ② 입력 액션 에셋 설계(키보드·마우스·패드 3경로, 기존 키 배치 유지) ③ 파일 묶음별 교체·컴파일·플레이 확인 ④ Active Input Handling = Input System Package (New) 로 전환 ⑤ 서드파티(`Assets/Plugins/**`) 안의 레거시 호출은 목록만 남기고 고치지 않는다(EventSystem 은 InputSystemUIInputModule 로). SETUP-1(unity-mcp) 뒤에 한다.
- [x] **SETUP-3** (`Docs/Playtest/ImproveAudit.md`, 개선 방향은 Q1 대기) 개선 후보 점검표: 출시판 버그·불편·장르 관례 이탈(일반 게임과 달라서 유저가 힘든 부분)·코드 부채 → `Docs/Playtest/ImproveAudit.md`, 우선순위 매기기. 개선 방향은 사용자에게 받는다(OpenQuestions Q1).

## 11-21 대개선 (Project B 총괄 배분, 사용자 지시)

순서: UPG-1 → FIX-P0 → A·B 문서 + C 설계 병행 → C 구현 → B 수정 반영 → A 구현. 트랙 끝마다 state.md 3~5줄.

- [x] **UPG-1** (c06ec75·5fec076, 씬 3개 플레이 확인) Unity 6000.4.0f1 업그레이드(사용자 11-21: 2021.3 설치 안 함). 태그 `pre-unity6` 고정 → 배치 모드 업그레이드 → 패키지 갱신·컴파일 에러·서드파티 호환 → 씬 3개 플레이·스샷 → unity-mcp 연결 → 문서 버전 줄 갱신.
- [x] **FIX-P0** (46e08c1, reviewer-code 필수 0) ImproveAudit P0 먼저: 출시판 RolletTest 치트 키, ResetData 누락 변수, BattleManager.cs:331 피해 5 고정(리팩터 전).
- [x] **A-1** (a545a8c) CoC 7판 대조 설계 `.claude-rules/Game/CocDesign.md`: 성장 체크·Push·행운·보너스/페널티 주사위·성공 단계·광기·신화 지식·단서 대체 경로·직업 선택지·대립 판정 → 효과·비용·스토리 영향 표, 우선순위, 원작 근거. 구현은 C 뒤.
- [x] **B-1** (de37813·9a6f737, lint 오류 0·경고 24 분류, 스토리 확인 14건은 Q5·DialogAudit 6절) 대사 CSV 15개·I2 전수 점검: `Tools/dialog_lint.py`(깨진 줄·누락·끊긴 분기·잘못된 다음 ID·도달 불가·막다른 길) → 결과표 → 수정 → 재검사 0건. 스토리 내용 변경은 목록으로 사용자 확인. I2 통합 방향은 설계 문서 추천안.
- [x] **C-1** (a545a8c) 아키텍처 설계 `.claude-rules/Game/Architecture.md`: 정적 플래그 → 상태 객체·저장 데이터, InteractionController·TimeManagere 분리, 데이터 주도 대화, 판정·전투 순수 C#+EditMode 테스트, 저장·백로그·스킵·자동 진행, New Input System(INPUT-1 흡수). 단계 P0~ 로 나눔.
- [x] **C-2 P4 선행(P3 리뷰 권장, 5f291ab) 완료(P4 와 함께)**: 연출 대기 플래그 `strDialogOn`·`battleSan`·`barMove`(DatabaseManager.cs:127,236,300)를 일시로 옮기거나 저장 제외. GameState Dictionary 열거 순서 비결정 확인, SchemaVersion 을 meta 와 함께 저장. Enter Play Mode Options 켤 때 `SubsystemRegistration` 에서 GameRoot·일시 정적 리셋.
- [x] **C-2 P8 후속(fde6fb5)** (11-21 완료: 버튼 클릭 Advance 억제·ForceNext·UiText. 남은 것: 실제 휠·Ctrl·패드·슬라이더·재기동 복원·버튼 클릭 실입력 사용자 확인): 대사창 log/auto(옛 skip 포함) 버튼 클릭이 좌클릭 Advance 와 겹쳐 한 줄 넘어감 → UI 위 클릭은 Advance 소비 안 하게. 선택지 `ChoiceExNextPageT` 를 `Runner.ForceNext` 로 합치기. 새 UI 문자열(log·auto·TEXT) 정리. 실제 휠·Ctrl·패드·슬라이더·재기동 복원 사용자 확인.
- [x] **C-2 P11 후속(4793559 리뷰 권장, 11-21 완료: Keyword.json·places.json 실패 LogError·폴백, ellaKey*/aidenKey* 필드는 Main 씬 직렬화 참조가 있어 유지·주석만. 키워드 UI 클릭 흐름 사용자 확인은 남음)**: StoryDatabase.cs:72 Keyword.json 로드 실패 시 Debug.LogError, DialogDatabaseManager.cs:14 Places() 실패 시 로그·폴백(전투 대사 소실 방지). InteractionController 의 ellaKey*/aidenKey* 미사용 필드 정리. 키워드 UI 클릭 흐름 사용자 확인.
- [x] **C-2 P13 설계 메모(P12 리뷰 권장, 11-21 완료: GameState.Changed·GameRoot.StateChanged 단일 통지, WorldBinder+WorldRules, 스냅샷 diff 0. Case2IntCheck CSV 는 DetectiveOffice 확인. BOM 혼재 정리는 P16 에 남음)**: GameState 에 단일 변경 통지 `Changed(key)` 를 두고 DialogDatabaseManager(PlaceChanged/ItemUiChanged)·WorldBinder 가 함께 구독. DDM check setter 비활성 시 요청 버림 주석. Case2IntCheck 의 DDM CSV 가 DetectiveOffice 인지 확인. BOM 혼재 정리(P16).
- [ ] **C-2~** 단계별 builder 구현. 단계마다 컴파일·EditMode 테스트·처음부터 엔딩까지 플레이·reviewer-code·reviewer-arch [필수] 반영 후 커밋. 마지막에 기능 묶음별(대화·판정·전투·수첩·UI·저장) 리뷰 1회 더.

## 11-21 사용자 추가 요청(기획 → 설계 문서 먼저)
- [ ] **STORY-1** 대사 CSV 전수 읽고 `Game/Story.md`(줄거리·인물·단서·엔딩) + `Game/StoryImprove.md`(세부 수정·흐름·자유도·광기 연출). designer 진행 중.
- [ ] **SEWER-1** 하수도 내부 개선(사용자: "되게 별로"). 구조·탐색·연출·전투 배치 문제 실측 → 개선 설계 → 구현.
- [ ] **PARTY-1** 동료 동행: 스토리의 친구 2명을 최대 3인 파티로 탐색·전투에 데리고 다님. 동료 능력치로 판정 보조(도움 판정·대신 판정 등). STORY-1 결과로 인물 확정 후 설계. **P14b(전투 View)는 파티 설계 확정 뒤 진행**(전투 엔진이 다인원 전제를 가져야 함). 사용자(11-21): 전투는 다키스트 던전과 약간 비슷한 결(횡스크롤 대열·턴 순서·스트레스(SAN) 연동) → 파티 설계에 대열 위치·위치별 행동·SAN 피해를 함께 검토. 장르 관례만 참고하고 고유 표현은 따라 하지 않음.
- [ ] **KEYWORD-1** 키워드 시스템 사용성 개선(사용자: "쓰기 어려워 보임"). 현행 흐름 실측(KeyWordManager·Intel_ObtoUI·수첩) → 마우스·키보드·패드 3경로 개선 설계 → 구현. 기존 세이브·I2 키 유지.
- [ ] **P14b 메모(P14a 리뷰 권장, 4bbac2e)**: 실전투 플레이 확인(MCP 서버 다운으로 미검증). EnemyState 5필드·이름 박힌 슬롯 분기(46곳+)를 EnemyState[]+인덱스 루프로, BattleManager.cs:595 `EnemyDefs.DeepOne1` 하드코딩 → 슬롯 state.Def, EnemyDefs.Get(id)/All. ChooseIntent 종류 분기 → EnemyDef 가중치 데이터. 피해식은 Def.AttackDamage 로 통일(현재 BattleDamage 직접 호출), PoisonTickDamage·StepGauge out 미사용 정리, battleState 문자열 → enum. SymbolUp 매 프레임 문자열 비교 → bool. EnemyClass 빈 껍데기 삭제(P15). 파티(PARTY-1) 다인원 전제로 설계.
- [ ] **ASSET-1** 보유 에셋 86종 SCoW 기준 재분류 `Docs/AssetPlan.md`(적용/후보/안 맞음, 트랙 C 도구 판단). 적용분은 필요한 파일만 임포트(MCP 복구 후), 그래픽은 사용자 검토 후. 문서 builder 진행 중.
- [ ] **ART-1** 그래픽 현황 `Docs/ArtInventory.md` + `Game/ArtStyle.md` 초안(림버스 연출+도트 탐정·호러). PixelLab 생성은 11-25 이후, 생성 전 상세 설정 사용자 확인. 문서 builder 진행 중.
- [ ] **ART-2** UI 창·그래픽 전면 개선(사용자 11-21: "나중에 ui창이라던지 그래픽개선 다 해야해"). 범위: 대화상자·HUD·수첩·키워드 선택·룰렛·전투 UI 프레임, 배경·캐릭터·전투 일러스트 도트 정리, 임포트 설정 통일. 근거 `Docs/ArtInventory.md` 우선순위·`Game/ArtStyle.md`. 11-25 이후 PixelLab, 생성 전 상세 설정 사용자 확인, 적용은 사용자 검토 후. 새 UI(KeywordPicker·파티 전투 화면)는 기능 구현 때 임시 그래픽으로 두고 ART-2 에서 교체.
- [ ] **UX-1** 기능·UI/UX 전면 개선(사용자 11-25: "기능이랑 Ui/ux 이런것들 개선하는것도 포함해줘. 생각보다 지금 ui ux가 구려서"). 화면별(타이틀·생성·이동·대화·조사·수첩·룰렛·SAN·전투·저장·엔딩) 흐름·조작·피드백·편의 기능 감사 → `Game/UxImprove.md` → builder 단계 구현. 그림 교체는 ART-2. designer 감사 진행 중.
