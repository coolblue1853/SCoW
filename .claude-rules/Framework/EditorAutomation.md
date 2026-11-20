# 에디터 자동화 (unity-mcp)

Project B 에서 쓰던 방식을 옮겼다. **SCoW 에는 아직 MCP for Unity 패키지가 없다** → 0절부터.

## 0. 설치·연결 (11-21 완료)
- 에디터: Unity 6000.4.0f1(Project B 와 같은 버전). 실행: `"/c/Program Files/Unity/Hub/Editor/6000.4.0f1/Editor/Unity.exe" -projectPath C:/Users/user/SCoW`.
- 패키지: `com.coplaydev.unity-mcp` 를 `#v10.3.0` 태그로 고정(커밋 해시가 아닌 PackageCache 지문으로 고정하면 해석 실패).
- 서버: Project B 가 띄운 8080 HTTP 서버를 같이 쓴다(EditorPrefs 공유라 포트 분리 불가). 두 에디터가 붙어 있으면 먼저 `set_active_instance` 로 `SCoW@…` 를 고른다(`mcpforunity://instances` 로 이름 확인).
- **"ping not answered" 가 반복되면** 에디터가 모달 창에 막힌 것이다(11-21: "URP Material upgrade" 확인창). Win32 로 창 목록·버튼을 찾아 `BM_CLICK` 을 보내 닫는다(포커스 안 뺏음). 창 목록 스크립트는 프로세스 ID 로 `EnumWindows`.
- 플레이 진입 전 Game 뷰를 PlayUnfocused 로(`Tools/probes/play_unfocused.cs`, Unity 6 에서 동작).

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
- 🔴 **(11-21 사용자) SCoW 에디터 창을 절대 앞으로 가져오지 않는다.** 사용자가 게임 중일 수 있다.
  - 업그레이드·컴파일 확인은 가능하면 배치 모드(`Unity.exe -batchmode -nographics -quit -projectPath ... -logFile ...`, 에디터가 꺼져 있을 때만).
  - 에디터를 띄울 때는 최소화로: `powershell -NoProfile -Command "Start-Process -WindowStyle Minimized -FilePath 'C:\Program Files\Unity\Hub\Editor\6000.4.0f1\Editor\Unity.exe' -ArgumentList '-projectPath','C:/Users/user/SCoW'"`. Hub 로 열지 않는다. 꺼진 에디터를 다시 켤 때도 같다.
  - 이미 떠 있는 창은 Win32 `ShowWindowAsync(hwnd, 7)`(SW_SHOWMINNOACTIVE, 활성화 없이 최소화)로 내린다.
  - 플레이 진입은 Game 뷰 PlayUnfocused 설정(`Tools/probes/play_unfocused.cs`) 뒤에만. 창을 활성화·포커스하는 메뉴·API(`EditorWindow.Focus`, `FocusWindowIfItsOpen` 등)는 쓰지 않는다. 스크린샷은 `manage_camera screenshot`(게임 뷰 렌더)만.
- 프로브(`Tools/probes/*.cs`)는 메서드 본문이다. 마지막에 `return 문자열;`.
- 플레이 중에는 컴파일을 일으키지 않는다(스크립트 수정은 플레이 종료 후).
- Unity 창을 앞으로 가져오지 않는다. `Tools/probes/play_unfocused.cs` 로 플레이 시 포커스를 뺏지 않게 한다.
- 에디터가 비포커스면 Input System 이 키보드를 끌 수 있다. 입력 시험은 Input System 의 "Run In Background"·가상 장치로 한다(Project B 방식).
- 콘솔은 error 만 먼저 본다. 스크린샷은 `Logs/playtest/` 에 둔다(커밋 안 함).
- 사용자가 에디터로 플레이 중이면 에디터·코드·세이브를 건드리지 않는다. 버그는 플레이가 끝난 뒤 본다.
- 세이브를 건드리는 시험은 세이브 파일을 먼저 백업하고, 끝나면 되돌린 뒤 md5 를 확인한다.
- 에디터가 꺼져 있으면 묻지 말고 다시 켠다(사용자가 플레이 중이 아닐 때).
