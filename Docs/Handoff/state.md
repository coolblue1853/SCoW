# 상태

- 11-20: Project B 에서 규칙·에이전트·출력 스타일·훅·에디터 자동화 도구를 옮겨 옴(`Docs/Handoff/PortedFromProjectB.md`). 다음: 큐 SETUP-1~3.
- 11-20 총괄: 이식 파일 커밋(b6ebde6). unity-mcp 를 manifest 에 등록했으나 **Unity 2021.3.21f1 미설치**(Hub 에 6000.4 만) → 컴파일·플레이 검증 불가, 설치 허락 대기(Q2).
- SETUP-2 완료: `GameOverview.md`. 코드에 ES3·PlayerPrefs 호출 0(저장 없음, Q3), I2 는 에셋만 있고 미사용(Q4).
- INPUT-1 ①② 완료: `Docs/Playtest/InputMigration.md`(20파일 69줄, 정적 래퍼 GameInput 방식). ③ 코드 교체부터는 에디터 설치 후. 비 UTF-8 .cs 다수 → 인코딩 보존 필요.
- SETUP-3 완료: `Docs/Playtest/ImproveAudit.md`. P0: 출시판에 RolletTest 치트(F1~F12·P) 노출, ResetData 누락 변수, BattleManager.cs:331 피해 고정 5, 저장·백로그·스킵 없음.
- 11-21 UPG-1 완료: Unity 6000.4.0f1 업그레이드(태그 `pre-unity6`), unity-mcp v10.3.0 이 8080 공유 서버에 `SCoW@…` 로 연결. 업그레이드 후 TMP 글자 네모 깨짐 → 옛 TMP 셰이더를 ugui 2.0 판으로 교체해 해결, ES3 Stadia 에러 우회.
- 씬 3개(Insert 타이틀·SetCharacter·Main 첫 대사) 플레이·콘솔 error 0 확인. 처음부터 엔딩까지 흐름은 미검증.
- 문서 트랙: A-1 `CocDesign.md`(규칙 버그 7건)·C-1 `Architecture.md`(P0~P16)·B-1 `dialog_lint.py`+`DialogAudit.md`(오류 6·경고 262) 완료. 다음: FIX-P0 → C-2(Architecture P0 부터) → B-1 CSV 수정.
