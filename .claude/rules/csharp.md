---
paths:
  - "**/*.cs"
---

# C# 코드 규칙 (.cs 파일을 다룰 때만 로드)

- **인코딩·서식:** 기존 파일의 인코딩·개행을 그대로 유지한다(대부분 UTF-8 BOM 없음 + CRLF, 일부 BOM 있음). 새 파일은 UTF-8 + CRLF, 들여쓰기 4칸. `sed -i` 로 고치지 않는다.
- **필드:** 새 코드는 `[SerializeField] private T _camelCase;`. 기존 public 필드는 씬·프리팹 직렬화가 걸려 있으므로 이름을 바꿀 때 반드시 `[FormerlySerializedAs("old")]`를 붙인다.
- **스타일:** if/for/while은 항상 중괄호를 쓴다. 접근 제한자를 명시한다.
- **크기:** 클래스 500줄, 메서드 40줄 안팎을 목표로 한다. 기존 큰 클래스는 기능을 고칠 때 조금씩 분리한다.
- **판정·확률:** CoC 판정(1d100, 기능치 %)에서 정수 나눗셈을 조심한다(`/ 100f` 또는 정수 비교). `Random.Range(int,int)` 상한은 제외값이다(1d100 = `Random.Range(1, 101)`).
- **입력(11-20 사용자: 옛 입력 방식 전부 교체):** New Input System 만 쓴다. 새 코드에 레거시 `Input.GetKey*`·`Input.GetMouse*`·`Input.GetAxis` 금지(디버그 키 포함). 기존 레거시 호출(약 21개 파일)은 큐 INPUT-1 로 전부 바꾼다. `Keyboard.current`·`Mouse.current` 는 null 체크한다. 다 바꾼 뒤 Player Settings Active Input Handling 을 Input System 전용으로 돌린다.
- **Update:** 매 프레임 `GetComponent` / `Find*` / 문자열 연결 / `Debug.Log`를 하지 않는다. 캐시한다.
- **DOTween:** 트윈을 다시 걸기 전에 기존 트윈을 `Kill()`한다. `SetLink(gameObject)` 또는 `OnDisable`에서 정리한다.
- **이벤트 구독:** `OnEnable`에서 구독하면 `OnDisable`에서 해제한다.
- **서드파티·자동 생성 코드(`Assets/Plugins/**`, `Assets/Script/_/Stuff/CodeMonkey` 등)는 수정하지 않는다.** 꼭 필요하면 문서에 기록하고 최소한으로 고친다.
