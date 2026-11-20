# 절약 대원칙 (상시 로드 — 2KB 이내 유지)

**모든 결정에서 토큰 절약이 먼저다.** (Project B 11-02 원칙을 옮김. 사용량은 Project B 와 같은 계정 한도를 나눠 쓴다.)

## 1. 상시 로드 최소화
- 상시 로드 = `CLAUDE.md` + `AGENTS.md`(짧은 인덱스) + 이 문서 + `CodeStyle.md` + `MEMORY.md`. 늘리지 않는다.
- 상세는 온디맨드: 작업에 필요한 문서만 Read.
- 큰 파일은 `grep -n`으로 위치 찾고 그 줄 주변만 읽는다. 읽은 파일 재독 금지. `*.unity`/`*.prefab`/로그/서드파티(`Assets/Plugins/**`, `Assets/Script/_/Stuff/CodeMonkey`) 통독 금지.
- 명령 출력은 `| head`·`| tail`로 자른다. 콘솔은 error만.

## 2. 세션 = 담당자
- 세션 하나는 **한 담당 업무 단위**로 끝낸다. 총괄(메인 세션)은 얇게 유지한다.
- 10도구 호출 이상 걸리는 일만 하위 세션/builder 로, 그보다 작으면 총괄이 직접/scout로.
- 브리프에 넣을 것: 목표, 읽을 문서 2~3개, 끝났다는 기준, "보고 5줄 이내".
- 끝나면 결과를 `Docs/Handoff/state.md`에 3~5줄로 남긴다.
- 같은 파일을 건드리는 작업은 동시에 두 개 돌리지 않는다.

## 3. 모델·에이전트
- 기본 Sonnet, 노력 medium. Opus는 큰 설계/어려운 버그만.
- 단순 조회·grep·로그 확인·문서 표·한두 줄 수정은 `scout`(haiku). 판단·설계·여러 파일 구현만 builder(sonnet). 검증 reviewer-code(haiku).

## 4. 출력
- 간결 모드(`.claude/output-styles/gando.md`). 완료 보고 1~2줄, 중간 보고 없음.
- 판단이 필요한 질문은 `Docs/OpenQuestions.md`에 쌓고 계속한다.

## 업무 생성·전달
`Framework/Dispatch.md`: 큐 원본 확인→세션 점검→전달. 두 번 일하지 않기.
