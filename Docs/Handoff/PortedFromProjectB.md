# Project B 에서 옮긴 것 (11-20)

사용자 요청: "여기서 쓰던 것처럼 대화할 수 있게 룰스·스킬 옮길 수 있는 거 다 옮겨줘". 원본은 `C:\Users\user\ProjectB`.

## 1. 저장소에 옮긴 것 (SCoW 에 맞게 고침)
| 파일 | 원본 | 바꾼 점 |
|---|---|---|
| `.claude/CLAUDE.md` | 같은 이름 | 프로젝트 소개·출시작 주의(ES3·I2 키)·스킬 사용 줄. PixelLab·GPT 위임·참조 프로젝트 줄 제거 |
| `.claude-rules/AGENTS.md` | 같은 이름 | SCoW 개요(Unity 2021.3.21f1·플러그인·씬·코드 폴더)로 새로 씀 |
| `.claude-rules/Framework/TokenEconomy.md` | 같은 이름 | 서드파티 경로를 SCoW 것으로. 사용량은 Project B 와 같은 계정 한도 공유 |
| `.claude-rules/Framework/CodeStyle.md` | 같은 이름 | CSV 규칙 → ES3·I2 키 유지 규칙 |
| `.claude-rules/Framework/ContextEconomy.md`·`Dispatch.md` | 같은 이름 | 그대로 |
| `.claude-rules/Framework/AutoRun.md` | 같은 이름 | 11-03 날짜 고유 배분표·예외 절 제거 |
| `.claude-rules/Framework/EditorAutomation.md` | 같은 이름 | unity-mcp 설치 절(0절) 추가, Project B 프로브·봇 내용 제거 |
| `.claude/rules/csharp.md` | 같은 이름 | 인코딩은 기존 파일 유지(SCoW 는 대부분 BOM 없음+CRLF), CoC 1d100 주의, **입력은 New Input System 만(11-20 사용자: 옛 입력 전부 교체)** |
| `.claude/agents/*.md` | builder·designer·reviewer-arch·reviewer-code·scout | 이름·CSV/DatabaseManager 언급을 SCoW 에 맞게 |
| `.claude/output-styles/gando.md` | 같은 이름 | 그대로(간결 모드) |
| `.claude/hooks/block-dangerous-git.sh` | 같은 이름 | 그대로(위험 git 명령 차단) |
| `.claude/settings.local.json` | 같은 이름 | 그대로(출력 스타일 간결·sonnet·medium·자동 압축 40%·훅). 커밋 안 함 |
| `.mcp.json` | `~/.claude.json` 의 Project B UnityMCP 항목 | 같은 주소 `http://127.0.0.1:8080/mcp` |
| `Tools/u.sh`·`Tools/unity_mcp_call.py`·`Tools/playtest.sh`·`Tools/probes/play_unfocused.cs` | 같은 이름 | u.sh 의 타이틀 해제·기록기 표시 제거, playtest.sh 기본 씬 = `Main.unity` |
| `.claude-rules/Game/GameOverview.md`·`Docs/Handoff/queue.md`·`state.md`·`Docs/OpenQuestions.md` | 새로 씀 | 첫 작업 큐: SETUP-1 unity-mcp 설치 → SETUP-2 개요 채우기 → INPUT-1 입력 교체 → SETUP-3 개선 점검표 |

## 2. 메모리 (`~/.claude/projects/C--Users-user-SCoW/memory/`)
작업 방식 메모리 27개를 그대로 복사했다: 한국어만·질문 말고 계속·추천대로 결정·자동 커밋 push·절약 원칙·builder 3줄 보고·spartan 스킬·포커스 뺏지 않기·에디터 재기동·heredoc 주의·질문은 OpenQuestions·장르 관례 이탈 수정·Codex 금지·예산 우선순위·낭비 작업 금지·작업 시간·웹은 Chrome·유튜브 자동 재생 금지·다운로드 허락·사용자 플레이 중 손대지 않기·최종 캡처 확인·그래픽은 사용자 검토·패드 3경로·사용량 상한·자동 재개 크론.
SCoW 전용으로 2개 추가: `ported-from-projectb`(이식 사정), `input-system-migration`(옛 입력 전부 교체).

## 3. 따로 옮길 필요 없는 것 (계정 전체에 이미 적용)
- 플러그인: caveman, spartan, spartan-mode (`~/.claude/settings.json` enabledPlugins)
- 개인 스킬: `shader-creator-personal` (`~/.claude/skills/`)

## 4. 안 옮긴 것과 이유
- Project B 게임 문서(`.claude-rules/Game/*` 70여 개): 메이플식 액션 RPG 고유 내용.
- `.claude/skills/pixel-character`: Project B 주인공 그림체(2등신 도트)·PixelLab 전용. SCoW 에 도트 캐릭터 작업이 생기면 다시 본다.
- 봇·플레이 기록기(`Assets/Script/DebugTools/Bot`, `Tools/bot_run.sh` 등): Project B 코드에 묶여 있다. SCoW 에 자동 플레이 시험이 필요하면 설계부터 새로.
- 그래픽·모션·PixelLab 관련 메모리 약 40개: Project B 그림체·모션 결정.
