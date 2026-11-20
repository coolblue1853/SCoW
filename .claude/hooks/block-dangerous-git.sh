#!/usr/bin/env bash
# Spartan git-guardrails — Claude Code PreToolUse 훅 (Bash · PowerShell 도구)
# 막으면 종료 코드 2 + 표준 오류에 이유를 쓴다 → Claude 가 그 글을 읽고 다른 길로 간다.
# jq 없이 돈다(Git Bash · macOS · Linux).

# 1 = Unity 저장소 규칙도 켠다(셸로 에셋 이동 · 삭제 · sed -i). 0 = git 규칙만.
UNITY_RULES=1

INPUT=$(cat)
# tool_input.command 를 꺼낸다(JSON 문자열 이스케이프째 — 패턴 대조엔 충분하다).
# 앞에 표식 '@' 를 붙여 「빈 명령」 과 「못 꺼냄」 을 가른다 — 못 꺼냈을 때만 입력 전체로 대조한다.
RAW=$(printf '%s' "$INPUT" | tr '\n' ' ' \
  | sed -nE 's/.*"command"[[:space:]]*:[[:space:]]*"(([^"\\]|\\.)*)".*/@\1/p' | head -n 1)
if [ -n "$RAW" ]; then CMD="${RAW#@}"; else CMD="$INPUT"; fi
[ -z "$CMD" ] && exit 0
# 여러 줄 명령 — JSON 의 \n · \r · \t 이스케이프를 명령 구분자로 본다.
CMD=$(printf '%s' "$CMD" | sed -E 's/\\[nrt]/ ; /g')

block() {
  printf '⛔ Spartan git-guardrails 가 막음 — %s\n   명령: %s\n   사용자에게 맡기거나 안전한 길로 다시 한다.\n' "$1" "$CMD" >&2
  exit 2
}
has() { printf '%s' "$CMD" | grep -qE -- "$1"; }

# git 과 하위 명령 사이의 -C <경로> · -c <설정> 을 건너뛴다.
G='(^|[;&|({[:space:]])git[[:space:]]+((-C|-c)[[:space:]]+[^[:space:]]+[[:space:]]+)*'

# ── git ──────────────────────────────────────────────────────────────
has "${G}push[^;&|]*(--force|--force-with-lease|--delete|[[:space:]]-f([[:space:]]|\$)|[[:space:]]\+[^[:space:]]|[[:space:]]:[^[:space:]])" \
  && block "강제 · 삭제 푸시는 금지 — fast-forward 만, 체크아웃 없이 \`git push origin <b>:<b>\`"

has "${G}reset[^;&|]*--hard" \
  && block "reset --hard 는 남의 미커밋 변경까지 지운다 — 넣은 줄을 Edit 로 도로 뺀다"

has "${G}clean[^;&|]*[[:space:]]-[a-zA-Z]*f" \
  && block "git clean -f 는 미추적 파일(남의 새 파일 · 에셋)을 지운다"

has "${G}branch[^;&|]*[[:space:]](-D|--delete[[:space:]]+--force|--force[[:space:]]+--delete)([[:space:]]|\$)" \
  && block "브랜치 강제 삭제는 사용자 몫"

has "${G}(checkout|switch)([[:space:]]|\$)" \
  && block "체크아웃 금지(브랜치 · 파일) — 켜진 Unity 에 대량 리임포트 · 남의 미커밋 변경이 사라진다. 다른 브랜치는 \`git push origin <b>:<b>\` · 파일 되돌리기는 Edit"

if has "${G}restore([[:space:]]|\$)"; then
  if ! has "--staged" || has "(--worktree|[[:space:]]-W)([[:space:]]|\$)"; then
    block "git restore <파일> 은 그 파일의 미커밋 변경 전부를 지운다(복구본 없음) — 넣은 줄만 Edit 로 뺀다"
  fi
fi

if has "${G}stash([[:space:]]|\$)" && ! has "stash[[:space:]]+(list|show)"; then
  block "stash 는 같은 트리의 다른 세션 변경까지 쓸어 간다"
fi

has "${G}add[^;&|]*[[:space:]](-N|--intent-to-add)([[:space:]]|\$)" \
  && block "git add -N(폴더)은 그 아래 남의 삭제까지 스테이징한다 — 비교는 \`git show HEAD:<경로>\` 와 diff"

has "${G}add[^;&|]*[[:space:]](-A|--all|-u|--update|\.|\*)([[:space:]]|\$)" \
  && block "통째 git add 금지 — 내 파일만 이름으로 \`git add <파일>\`"

has "${G}commit[^;&|]*([[:space:]]-[b-zA-Z]*a[a-zA-Z]*([[:space:]]|\$)|[[:space:]]--all([[:space:]]|\$))" \
  && block "commit -a 는 남의 수정까지 싼다 — 내 파일만 add 하고 \`git diff --cached --stat\` 을 본 뒤 커밋"

# ── Unity 저장소 ─────────────────────────────────────────────────────
if [ "$UNITY_RULES" = "1" ]; then
  has "(^|[;&|({[:space:]])(mv|rm|rmdir|git[[:space:]]+mv|git[[:space:]]+rm|Move-Item|Remove-Item|Rename-Item|ren|del)[[:space:]][^;&|]*(Assets|Packages|ProjectSettings|Library)[/\\\\]" \
    && block "셸로 에셋을 옮기거나 지우면 켜진 에디터가 옛 경로를 되살려 같은 GUID 가 둘이 된다 — AssetDatabase.MoveAsset · DeleteAsset(에디터 안 · CLI eval) 또는 사용자에게"

  has "(^|[;&|({[:space:]])sed[[:space:]]+([^;&|]*[[:space:]])?(-[a-zA-Z]*i|--in-place)" \
    && block "Git Bash 의 sed -i 는 CRLF 를 LF 로 바꿔 파일 전체 diff 를 만든다 — Edit 도구 또는 바이트 보존 스크립트"
fi

exit 0
