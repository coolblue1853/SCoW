# CLAUDE.md

SCoW(Shadow Cast on Water) 작업 지침. 모든 대화와 문서는 **한글**로 쓴다.
(11-20, Project B 에서 쓰던 규칙·에이전트·출력 스타일 운영 방식을 옮겨 왔다. 옮긴 목록은 `Docs/Handoff/PortedFromProjectB.md`.)

## 🔴 규칙 문서는 온디맨드 — 작업 전 반드시 Read

`.claude-rules/`의 문서는 자동 로드되지 않는다. 상시 로드되는 것은 아래 `@` 임포트뿐이다.

- 코드를 고치거나 시스템 동작을 설명하기 전에, `AGENTS.md` 인덱스에서 해당 문서를 찾아 **Read로 읽고** 작업한다.
- 인덱스 한 줄만 보고 추측하지 않는다. 여러 시스템이 얽히면 관련 문서를 전부 읽는다.

## 문서 관리

- 기획·설계 문서는 `.claude-rules/`의 `Framework/`(범용 규약) 또는 `Game/`(게임 고유)에 둔다. 새 문서를 만들면 `AGENTS.md`에 한 줄 추가한다.
- 인덱스 항목은 **백틱**으로 적는다. 마크다운 링크는 쓰지 않는다(링크는 전이 임포트되어 매 세션 로드된다).
- `.claude/rules/`에는 **`paths:` 지정한 경로 한정 규칙만** 둔다(`paths:` 없는 규칙은 상시 로드되므로 금지).
- 결정 사항은 그때그때 해당 문서에 기록한다. 대화는 압축되면 사라진다.
- 기획 질문은 바로 묻지 않고 `Docs/OpenQuestions.md`에 **임시 가정**과 함께 쌓은 뒤, 질문이 필요 없는 작업을 계속한다.

## 작업 방식

- **커밋:** 기능 단위로 커밋하고 push한다. 메시지는 `(추가)/(수정)/(삭제)/(문서) 요약` + 본문 bullet 형식으로 쓴다.
- **검증:** 버그 수정과 기능은 **Unity 에디터에서 실제로 확인**한다(unity-mcp: 콘솔 로그, 플레이 모드, 스크린샷). 확인하지 못했으면 커밋 메시지와 문서에 "미검증"으로 적는다.
- **에이전트:** 구현 builder(sonnet)·설계 designer(sonnet)·구조 리뷰 reviewer-arch(sonnet)·코드 리뷰 reviewer-code(haiku)·단순 조회 scout(haiku). 정의는 `.claude/agents/`. builder 보고는 3줄(해시·테스트·미해결), 총괄이 커밋 범위·BOM·개행을 기계 확인한다.
- **토큰 절약:** 출력은 간결 모드(`.claude/output-styles/gando.md`). 긴 문서·로그는 grep 으로 필요한 줄만.
- **Codex 금지:** Codex CLI 는 쓰지 않는다.
- **출시작이다:** 기존 세이브(Easy Save 3)·현지화(I2 Localization) 키를 깨지 않는다. 큰 변경 전에 설계 문서부터 쓴다.
- **스킬:** spartan 플러그인 스킬을 쓴다 — 커밋 전 unity-verify, 원인 모를 버그 diagnosing-bugs, 에셋·meta·개명 unity-traps, 규칙·공식 tdd, 화면 확인 unity-render.

@../.claude-rules/AGENTS.md

@../.claude-rules/Framework/TokenEconomy.md
@../.claude-rules/Framework/CodeStyle.md
