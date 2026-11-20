# SCoW 규칙 인덱스

- 모든 대화·문서는 **한글**로 쓴다.
- 이 파일은 **짧은 인덱스**다(매 세션 로드). 항목마다 "어느 문서를 읽을지" 판단할 한 줄만 적고, 항목은 **백틱**으로 적는다(링크 금지).
- 문서는 `Framework/`(범용 규약) 또는 `Game/`(게임 고유)에 둔다. 한 문서는 500줄 이내를 권장한다.

## Project Overview

**SCoW (Shadow Cast on Water)** — 사용자가 예전에 출시한 크툴루의 부름(CoC) 기반 TRPG 게임. 개선 작업 중.

- **Unity:** 2021.3.21f1 / URP 12 / 2D
- **주요 플러그인:** DOTween(Demigiant), Easy Save 3, I2 Localization, Text Animator, DamageNumbersPro
- **씬:** `Assets/Scenes/Main.unity`, `Assets/Scenes/SetCharcter/SetCharacter.unity`, `Assets/Scenes/Insert.unity`
- **코드:** `Assets/Script/`(UI·dialogue·IntelUI·Battle·setStat·Map·Rollet·Character 등)

## 프레임워크 규약

| 파일 | 설명 |
|------|------|
| `Framework/TokenEconomy.md` | **절약 대원칙·세션 담당자 운영** |
| `Framework/CodeStyle.md` | 코드 스타일·커밋 |
| `Framework/ContextEconomy.md` | 컨텍스트 절약 |
| `Framework/Dispatch.md` | 업무 생성·전달 규칙(중복 방지, 큐 원본) |
| `Framework/AutoRun.md` | 자동 진행 `/loop` 틱 절차(Project B 원본, 플레이 봇 부분은 해당 없음) |
| `Framework/EditorAutomation.md` | unity-mcp 설치·컴파일·플레이 확인 |

## 게임 기획 / 스펙

| 파일 | 설명 |
|------|------|
| `Game/GameOverview.md` | 게임 개요·코드 구조 지도(첫 세션에서 채움) |

## 레퍼런스 게임 분석 (Project B 에서 옮김, 원본 추출 자료는 ProjectB 쪽 경로)

| 파일 | 설명 |
|------|------|
| `Reference/SkulReference.md` | 스컬: 전투 모션·이펙트 분석 |
| `Reference/SkulAssetLibrary.md` | 스컬 원본 추출 자료 분류·위치 |
| `Reference/SkulMotionMap.md` | 스컬 애니 ↔ 동작 대응표 |
| `Reference/DungeonSlasherReference.md` | 던전 슬래셔 APK 추출·분석 |
| `Reference/LimbusReference.md` | 림버스 컴퍼니: 이펙트·연출(UI 연출·대사 연출 참고 가치 큼) |
| `Reference/BackgroundRefPlan.md` | 던전 슬래셔 기반 배경 후보 |
| `Reference/MotionReferenceMaple.md`·`MotionReferenceSurvey.md`·`MotionGapSurvey.md` | 메이플 외 동작 레퍼런스 조사 |
| `Reference/LevelDesignRef.md` | 메랜 DB 레벨 구간 대조 |
| `Reference/Maple/MapleMaps.md`·`MapleMobs.md`·`MapleQuests.md` | 메이플 맵·몹·퀘스트 실측 요약(퀘스트 구조·말투 참고) |
| `Reference/AssetInventory.md` | 사용자 보유 에셋스토어 86종 분류(SCoW 에도 쓸 수 있는 것 확인용) |

## 운영

| 파일 | 설명 |
|------|------|
| `Docs/Handoff/queue.md` *(저장소 루트)* | 작업 큐 원본 |
| `Docs/Handoff/state.md` *(저장소 루트)* | 현재 상태 |
| `Docs/OpenQuestions.md` *(저장소 루트)* | 사용자 확인이 필요한 기획 질문과 임시 가정 |
| `Docs/Handoff/PortedFromProjectB.md` *(저장소 루트)* | Project B 에서 옮긴 것·안 옮긴 것 목록 |
