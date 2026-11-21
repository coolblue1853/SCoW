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
- [ ] **C-2 P13 설계 메모(P12 리뷰 권장)**: GameState 에 단일 변경 통지 `Changed(key)` 를 두고 DialogDatabaseManager(PlaceChanged/ItemUiChanged)·WorldBinder 가 함께 구독. DDM check setter 비활성 시 요청 버림 주석. Case2IntCheck 의 DDM CSV 가 DetectiveOffice 인지 확인. BOM 혼재 정리(P16).
- [ ] **C-2~** 단계별 builder 구현. 단계마다 컴파일·EditMode 테스트·처음부터 엔딩까지 플레이·reviewer-code·reviewer-arch [필수] 반영 후 커밋. 마지막에 기능 묶음별(대화·판정·전투·수첩·UI·저장) 리뷰 1회 더.
