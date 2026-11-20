# 에디터 자동화 (unity-mcp)

Project B 에서 쓰던 방식을 옮겼다. **SCoW 에는 아직 MCP for Unity 패키지가 없다** → 0절부터.

## 0. 설치 (큐 SETUP-1)
- **11-20 현황:** `Packages/manifest.json` 에 Project B 와 같은 커밋(`#da7b9ae4…`, 패키지 10.3.0, `"unity": "2021.3"` 최소 지원 → 호환)으로 등록했다. **이 PC 에 Unity 2021.3.21f1 이 없다**(Hub 에 6000.4.0f1 만). 에디터 설치 전까지 컴파일·플레이 검증 불가. 6000 으로 열면 프로젝트가 업그레이드되므로 열지 않는다(출시작).
- 포트: MCP 설정(`MCPForUnity.HttpUrl`)은 EditorPrefs 라 Project B 와 공유된다. 8080 서버 하나에 두 에디터가 인스턴스로 붙는 구조이므로 포트를 바꾸지 않고, 두 에디터가 동시에 붙어 있으면 `set_active_instance`(이름 `SCoW@…`)로 고른다.
- `Packages/manifest.json` 에 `"com.coplaydev.unity-mcp": "https://github.com/CoplayDev/unity-mcp.git?path=/MCPForUnity#main"` 추가(Project B 와 같은 패키지). Unity 2021.3 지원 여부를 패키지 README 로 먼저 확인하고, 안 되면 2021 지원 태그로 고정한다.
- 에디터에서 MCP 서버를 HTTP(`http://127.0.0.1:8080/mcp`)로 켠다. 저장소 루트 `.mcp.json` 에 같은 주소가 있다(Claude Code 가 처음 열 때 승인을 묻는다).
- Project B 에디터와 동시에 켜면 포트가 겹친다. 한 번에 하나만 켜거나 포트를 바꾸고 `UNITY_MCP_URL` 환경 변수로 맞춘다.
- `Tools/u.sh` 는 `uv`(`/c/Users/user/.local/bin`)와 python `mcp` 패키지를 쓴다(Project B 와 같은 환경).

## 1. 경로
| 방식 | 사용 |
|---|---|
| MCP 도구 | `mcp__UnityMCP__*` (세션에 붙어 있을 때) |
| Bash 클라이언트 | `bash Tools/u.sh call <tool> '<json>'`, `bash Tools/u.sh cs <프로브.cs>` (세션에 MCP 가 없을 때) |

자주 쓰는 명령:
```
bash Tools/u.sh call refresh_unity '{"mode":"force","scope":"all","compile":"request","wait_for_ready":true}'
bash Tools/u.sh call read_console '{"action":"get","types":["error"],"count":"30","format":"plain"}'
bash Tools/u.sh call manage_editor '{"action":"play"}'
bash Tools/u.sh call manage_camera '{"action":"screenshot","properties":{"include_image":true}}' Logs/playtest/shot.png
```

## 2. 규칙
- 프로브(`Tools/probes/*.cs`)는 메서드 본문이다. 마지막에 `return 문자열;`.
- 플레이 중에는 컴파일을 일으키지 않는다(스크립트 수정은 플레이 종료 후).
- Unity 창을 앞으로 가져오지 않는다. `Tools/probes/play_unfocused.cs` 는 2022.2+ 기능이라 2021.3 에서는 "property not found" 가 나올 수 있다.
- 에디터가 비포커스면 Input System 이 키보드를 끌 수 있다. 입력 시험은 Input System 의 "Run In Background"·가상 장치로 한다(Project B 방식).
- 콘솔은 error 만 먼저 본다. 스크린샷은 `Logs/playtest/` 에 둔다(커밋 안 함).
- 사용자가 에디터로 플레이 중이면 에디터·코드·세이브를 건드리지 않는다. 버그는 플레이가 끝난 뒤 본다.
- 세이브를 건드리는 시험은 세이브 파일을 먼저 백업하고, 끝나면 되돌린 뒤 md5 를 확인한다.
- 에디터가 꺼져 있으면 묻지 말고 다시 켠다(사용자가 플레이 중이 아닐 때).
