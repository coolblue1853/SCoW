# 작업 큐 (원본)

형식: `- [ ] 내용 (담당: 이름, 파일: 건드릴 경로)`. 끝나면 `[x]` + 커밋 해시.

- [~] **SETUP-1** (e655d13 manifest 등록, Unity 2021.3.21f1 미설치로 검증 대기 — OpenQuestions Q2) unity-mcp 설치: `.claude-rules/Framework/EditorAutomation.md` 0절. 2021.3 호환 확인 → 컴파일·플레이 1회(`bash Tools/playtest.sh 8 first`) 확인.
- [x] **SETUP-2** (ed0797c) 코드·씬 조사로 `.claude-rules/Game/GameOverview.md` 채우기(플레이 흐름·폴더 역할·세이브 키·현지화).
- [~] **INPUT-1** (①② 완료: `Docs/Playtest/InputMigration.md`, ③~ 에디터 설치 후) (11-20 사용자: 옛 입력 방식 싹 다 고친다) 레거시 `Input.*` 호출 전부 New Input System 으로 교체. 순서: ① `grep -rn "Input\.Get\|Input\.mouse\|Input\.any\|Input\.touch" Assets/Script` 로 전수 목록·동작 표(`Docs/Playtest/InputMigration.md`) ② 입력 액션 에셋 설계(키보드·마우스·패드 3경로, 기존 키 배치 유지) ③ 파일 묶음별 교체·컴파일·플레이 확인 ④ Active Input Handling = Input System Package (New) 로 전환 ⑤ 서드파티(`Assets/Plugins/**`) 안의 레거시 호출은 목록만 남기고 고치지 않는다(EventSystem 은 InputSystemUIInputModule 로). SETUP-1(unity-mcp) 뒤에 한다.
- [ ] **SETUP-3** 개선 후보 점검표: 출시판 버그·불편·장르 관례 이탈(일반 게임과 달라서 유저가 힘든 부분)·코드 부채 → `Docs/Playtest/ImproveAudit.md`, 우선순위 매기기. 개선 방향은 사용자에게 받는다(OpenQuestions Q1).
