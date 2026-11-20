# 코드 스타일 · 커밋 규칙

## 1. C#

`.claude/rules/csharp.md`(경로 한정, .cs 작업 때만 로드). C# 수정 전 로드되지 않았으면 직접 Read.

## 2. Git

- 기능 단위로 커밋해 push한다.
- 메시지 형식: `(추가|수정|삭제|문서|리팩터) 한 줄 요약` + 빈 줄 + 본문 bullet.
  - 본문에 **검증 방법**을 적는다. 확인하지 못했으면 "미검증"이라고 적는다.
  - 마지막 줄은 `Co-Authored-By: Claude ...`이다.
- `git add -A` 금지. 내가 바꾼 파일만 add 한다. `sed -i` 금지(개행이 바뀐다).
- 커밋하지 않는 것: `Library/`, `Logs/`, `Temp/`, `.claude/settings*.json`.

## 3. 데이터·세이브

- Easy Save 3 세이브 키는 이름을 바꾸지 않는다. 추가만 한다(출시판 세이브 호환).
- I2 Localization 용어 키는 바꾸지 않는다. 새 문구는 새 키로 추가한다.
