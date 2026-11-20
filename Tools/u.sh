#!/usr/bin/env bash
# unity_mcp_call.py 단축 실행: bash Tools/u.sh call <tool> '<json>' | cs <file> | list | schema <tool>
# (Project B 에서 옮김. 에디터에 MCP for Unity 패키지가 있어야 동작한다 — EditorAutomation.md 0절)
cd "$(dirname "$0")/.."
export PATH="$PATH:/c/Users/user/.local/bin"
mkdir -p Logs
uv run -q --with mcp python Tools/unity_mcp_call.py "$@" 2>/dev/null
