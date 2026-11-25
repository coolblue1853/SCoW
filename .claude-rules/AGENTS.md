# SCoW 규칙 인덱스

- 모든 대화·문서는 **한글**로 쓴다.
- 이 파일은 **짧은 인덱스**다(매 세션 로드). 항목마다 "어느 문서를 읽을지" 판단할 한 줄만 적고, 항목은 **백틱**으로 적는다(링크 금지).
- 문서는 `Framework/`(범용 규약) 또는 `Game/`(게임 고유)에 둔다. 한 문서는 500줄 이내를 권장한다.

## Project Overview

**SCoW (Shadow Cast on Water)** — 사용자가 예전에 출시한 크툴루의 부름(CoC) 기반 TRPG 게임. 개선 작업 중.

- **Unity:** 6000.4.0f1 / URP 17.4 / 2D (11-21 2021.3.21f1 에서 업그레이드, 되돌릴 지점 태그 `pre-unity6`)
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
| `Game/CocDesign.md` | CoC 판정·SAN·전투 실측, 7판 규칙 대조, 우선순위, 순수 C# 규칙 인터페이스 초안 |
| `Game/ArtStyle.md` | 아트 스타일 초안: 1921년 아캄 도트 탐정 호러, 320×180·6배 격자, 팔레트 램프, 명암·외곽선, 캐릭터 비율, UI 톤, 림버스식 연출, PixelLab 사용 원칙(11-25 이후·생성 전 질문) |
| `Game/Story.md` | 스토리 분석: 줄거리·인물·장소·시간(timeCount)·단서→키워드→수첩 사슬·판정·엔딩 분기(CSV ID 근거) |
| `Game/StoryImprove.md` | 스토리 개선안 P0/P1/P2: 설정 모순·막힘·흐름·자유도·SAN/광기 장면별 연출, 데이터 주도 얹는 법 |
| `Game/Architecture.md` | 코드 전면 재작성 설계: 구조 실측, 상태 객체·저장·데이터 주도 대화·Core asmdef·GameInput, 단계 P0~P16(스트랭글러), UTF-8 변환 방침 |
| `Game/Party.md` | 동료 동행(PARTY-1): 알버트·메이브 합류·능력치, 탐색 판정 보조, 3칸 대열 전투·SAN 발작·쓰러짐, 엔진 인터페이스, 세이브·입력, 단계 PT-0~7 |
| `Game/KeywordUX.md` | KEYWORD-1: 키워드 시스템 현행 조작 단계·문제(파일:줄), 카탈로그·KeywordPicker·3경로 입력·횟수/오답·새 키워드 알림 개선 설계, 단계 K1~K7 |
| `Game/UxImprove.md` | UX-1: 화면별 UI/UX 실측(타이틀·설정·생성·월드·대사·수첩·룰렛·HUD·전투·저장·사망)·장르 관례 대비, 개선안 P0~P2 표(3경로), 시간·목표 HUD·설정 재구성·HintBar·저장 토스트, 단계 UX-1~12 |
| `Game/SewerImprove.md` | SEWER-1: 하수도 내부 현행 실측(랜덤 방·운 판정·조명/소리 없음)과 가설, 고정 그래프·조사 포인트·SAN 연동 공포 연출·조우 리듬, 단계 S0~S9 |

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
| `Docs/AssetPlan.md` *(저장소 루트)* | 보유 에셋 SCoW 기준 재분류(적용·후보·안 맞음), 적용할 파일 경로, 트랙 C 도구 판단(Dialogue System·Master Audio 불채택), Unity 6 위험도 |
| `Docs/ArtInventory.md` *(저장소 루트)* | 그래픽 현황 실측(폴더별 장수·해상도·도트 배율·PPU·필터·색 수), 불일치, 다듬기 우선순위 표 |
| `Docs/GraphicsNeeds.md` *(저장소 루트)* | 기능 작업 중 생긴 임시 그래픽·필요 리소스 목록(ART-2 전달용, 금요일 픽셀렙 세션 전달, 자동 생성 금지) |
