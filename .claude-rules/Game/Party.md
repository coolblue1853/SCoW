# 동료 동행·다인원 전투 설계 (PARTY-1)

> 2026-11-21. 근거: 사용자 원문 "전투할 때 동료도 데리고 갈 수 있도록(스토리의 친구 2명), 최대 3명, 친구들도 능력치가 있어 판정에 보탬" + "전투는 다키스트 던전과 비슷". 스토리 근거는 `Game/Story.md`(3절 인물, 6절 키워드 사망, 7절 엔딩), 규칙은 `Game/CocDesign.md`, 큐 P14b 메모.
> 다키스트 던전은 **장르 관례(횡 대열·위치별 행동·턴 순서·스트레스)만** 참고한다. 이름·수치·표현은 따라 하지 않는다(아래 표의 이름·수치는 전부 SCoW 고유).
> 확인하지 못한 것은 "미확인". 코드는 이 문서에서 고치지 않는다. 사용자 판단 질문은 `Docs/OpenQuestions.md` Q14~Q19.

## 0. 결정 요약

| 항목 | 결정 |
|---|---|
| 동료 | **알버트(Albert, 경찰)**, **메이브(Maeve, 마피아 간부·바텐더)**. 둘 다 파비안·알버트·메이브 삼각의 오랜 친구(`Story.md` 3절, 54줄). 파티 = 파비안 + 최대 2 = **3명** |
| 합류 | 알버트: 3일차(t>=9) 경찰서. 메이브: 3일차 오후(t>=10) 바. 둘 다 **비치명 키워드**를 내밀고 청하는 선택지(강제 아님) |
| 전투 | 횡 대열 3칸(앞·중·뒤). 칸마다 가능한 행동이 다름. 속도 게이지로 턴 순서(현 `StepGauge` 확장). 행동마다 플레이어가 직접 지시 |
| SAN | 동료도 SAN 보유. 한 번에 10 이상 손실 + INT 판정 실패 시 **전투 광기(발작)** 1d3 턴. SAN 0 = 이탈 |
| 쓰러짐 | HP<=0 → 쓰러짐(카운트다운) → 응급처치로 안정, 방치하면 전선 이탈. 전투 후 승리 시 부상 회복, 도주·패배 시 사망 |
| 세이브 | `GameState` 의 `Flags`/`Counters`/`Texts` 에 새 키 `party.*` 만 추가. ES3 기존 키·`SchemaVersion` 유지 |
| 단계 | 8단계(PT-0~PT-7), P14b 와 묶어 5절 |

## 1. 동료 합류·이탈 (스토리 정합)

### 1-1. 제약(기존 스토리)

- 알버트·스웨인·데이브는 특정 키워드 누설 후 밤(phase 0, `t>=8`)에 사라진다(`WorldRules` 92~107, `Story.md` 6절). 알버트 치명 키워드: `Root of problem`·`The Culprit`·`Way to Escape`·`Aiden's warning`·`Owner of uniform`. **메이브는 사망 키워드 없음**.
- 에필로그 `Ending_isPromise`(`intelMeiv2`)가 알버트 생존/사망으로 갈린다(`InSewer.json:205-231`, ID 146-161). 동료 사망이 이를 깨면 안 된다 → 5-3절에서 사망 플래그를 **합집합**으로 읽는다.
- 하루 자유 방문 2곳(낮·저녁)뿐(`MapManager` 704~721). 합류가 **추가 이동을 요구하지 않아야** 한다 → 합류 장소는 이미 가는 경찰서·바. 4일차 밤까지 하수도 못 가면 D 엔딩.
- 2일차 낮 `2nd_NoonEvent`(현장)·귀가길 `1st_Battle`(딥원 추격)은 **솔로 고정**(동료 합류 전. 알버트는 NPC 로만 등장).

### 1-2. 합류 표

| 동료 | 장소·시점 | 조건 | 트리거(수첩 키워드 제시) | 합류 동기(대사 방향) |
|---|---|---|---|---|
| 알버트 | Policeoffice, `t>=9`(3일차 이후) | `!albertDeathKeyword`, 플래그 `intelSewerOffice`(하수도 정비소 위치 ●1ⓢ 보유), 미합류 | `Sewerage Maintenance Office`(**치명 아님**: 알버트 143·152 가 이미 주는 키워드) | 비공식 수사. "공식으론 못 간다. 쉬는 날 내가 따라간다"(경찰 권한은 대사 태그로만 사용, 6-2절) |
| 메이브 | Bar, `t>=10`(3일차 오후 이후) | 미합류, `intelSewerOffice` 또는 키워드 `Old route`(SewerImprove/StoryImprove C-2 의 신규 키워드) | `Sewerage Maintenance Office` 또는 `Old route` | 인스머스 루트에 밀수로를 잃음(Bar 104-108, 125-141), 부하가 에이든을 쏜 책임(Bar 71-75). 별 보기 약속(`Promise`) 보유 시 추가 대사 |

- 합류는 **선택지**(수락/보류). 거절하면 `party.<id>.declined` 가 서고 같은 NPC 에게 다음 시간대 다시 청할 수 있다(영구 차단 금지: 막힘 방지 원칙, `StoryImprove.md` 2-1).
- 4일차 낮까지 못 청한 플레이어를 위한 **구제**(P2, M): 알버트 생존·미합류 상태로 하수도 진입(Riverside 맨홀) 시 "따라왔다" 컷으로 합류. 메이브도 동일(진입 직전 바에서 먼저 와 있음). 비용·대사량이 크면 보류.
- 둘 다 합류하면 파티 3인. 한 명만이면 2인. 솔로 클리어도 가능해야 한다(동료는 보조지 필수가 아님). 수치는 솔로 난이도를 그대로 두고 동료는 **편의**를 더하는 쪽(4-6절 밸런스).
- 알버트가 파티에 있는 동안은 치명 키워드 사망 판정을 **적용하지 않는다**(WorldRules 가 `party.albert.joined` 면 `albertDeathKeyword` 소멸 규칙 건너뜀). 합류 전에 누설한 사람은 이미 합류 조건에서 탈락한다. 알버트가 파티에 있을 때 치명 키워드를 내밀면 신뢰 하락 한 줄 대사만(Q15).
- 합류 후 **NPC 자리**: 알버트는 Policeoffice, 메이브는 Bar 에서 사라진다(`WorldRules` 에 `party.<id>.joined` 조건 추가). 두 NPC 와의 대사는 파티 메뉴 "대화"(6-4절)에서 같은 `Keyword.json` 토픽(`Albert/*`, `Maeve/*`)을 재사용하되 죽음 효과(`*DeathKeyword`)는 끈다.
- **이탈**: ① 쓰러짐 방치 후 사망(`party.<id>.dead`). ② SAN 0 이탈(전투 종료 후 귀가, 다음 일차 아침 재합류 가능, 1회성 SAN 회복 +5). ③ 하수도 안에서는 이탈·교체 불가(되돌아올 수 없는 구조, `SewerImprove` 1-1). 하수도 진입 직전(Riverside)에 **대기 지정**(`party.<id>.standby`)으로 뺄 수 있다. 엔딩 진입 시 정산.

### 1-3. 엔딩·에필로그 연결

| 엔딩 | 처리 |
|---|---|
| A 문양 파괴 | 에필로그 앞(`Ending_*` 케이스 맨 앞)에 `Ending_PartyAlbert`/`Ending_PartyMaeve` 변주 추가. 생존 동료 한 줄씩. `albertDead`(= `albertDeathKeyword` OR `party.albert.dead`)로 기존 148-153 분기 재사용. 별 약속 + 알버트 파티 생존 시 현행 146-147 에 합쳐진다(약속 장면에 알버트가 옆에 있음). 메이브 사망 시 새 후일담 1줄 |
| B·C 나쁜 엔딩 | 동료 반응 한 줄(광기·도망). 조건 분기만 추가, 기존 대사 불변 |
| D 악몽 | 영향 없음(동료 합류 전에 4일차 밤 끝나므로) |
| 게임오버 | 파비안 HP·SAN 0(기존) |

## 2. 동료 능력치 (CoC 7판 방식, 이 게임 스케일)

이 게임 능력치는 STR/INT/DEX/HP/MP/SAN/LUK 의 `(3d6)*5`(15~90), 기능 상한 90, 기능 15종 고정(`CocDesign.md` 1-1). 동료는 **고정값**(난수 아님. 매번 같아야 서사·밸런스가 선다). `DataBaseManager.cs:362~383` 의 기능 기본값(무술25·사격15·검술20·관찰20·수영10·의술10·분석30·청취30·심리치료5·오컬트5·회피30·손재주20·화술20·은신15·변장10) 위에 얹은 최종값이다.

### 2-1. 능력치

| 능력치 | 알버트 | 메이브 | 파비안(평균 참고) | 근거 |
|---|---:|---:|---:|---|
| STR | 65 | 45 | 52 | 경찰·몸싸움 / 바텐더·마른 체격(미확인, 대사에 체격 묘사 없음) |
| INT | 55 | 65 | 52 | 메이브: 마피아 간부·밀수 경영 |
| DEX | 55 | 60 | 52 | 속도 게이지(4-3절) |
| MP | 45 | 55 | 52 | 시스템상 미사용(전투 미연동) |
| HP | 75 | 55 | 52 | 알버트 앞열 버팀 |
| SAN | 55 | 65 | 52 | 메이브: 험한 일에 닳음, 알버트: 평범한 경찰 |
| LUK | 40 | 55 | 52 | 알버트: 바람기·운 없음 연출(Story 3절) |

피해에 x5 배율을 쓰므로 HP 단위는 파비안과 같은 5점 단위다(`BattleDamage` 모두 x5).

### 2-2. 기능

굵은 글씨는 **전문 분야**(도움 판정·대신 판정 임계치 40 이상, 4절).

| 기능 | 알버트 | 메이브 | 비고 |
|---|---:|---:|---|
| 무술 | **55** | 30 | 경찰 격투 |
| 사격 | **60** | 45 | 알버트 리볼버, 메이브 소형 권총 |
| 검술 | 20 | 40 | 메이브: 단검(Dagger, 병 깨기 연출) |
| 관찰 | **45** | 40 | |
| 수영 | 25 | 10 | 하수도 수위(미사용 후보) |
| 의술 | 25 | 15 | 응급처치(전투 5-4) 기본 30 이하라 약함. 알버트 경찰 구급 훈련 |
| 분석 | 35 | **50** | 메이브 장부·거래 |
| 청취 | 40 | **55** | 바 손님 정보 |
| 심리치료 | 10 | **45** | 메이브: 술로 달래는 사람. **전투 진정(5-6절) 기능** |
| 오컬트 | 5 | 20 | 메이브: 인스머스 해초주를 직접 다룸(Bar 127-134) |
| 회피 | 35 | **45** | |
| 손재주 | 25 | **50** | 메이브: 자물쇠·도둑질 |
| 화술 | **50** | **70** | 위협(알버트)·협상(메이브) 모두 화술로 처리 |
| 은신 | 15 | **40** | |
| 변장 | 10 | 35 | |

- "법률·위협" 같은 새 기능은 **만들지 않는다**(기능 목록 고정, 세이브 호환). 대신 **태그**로 푼다: 알버트 `Authority`(경찰 권한), 메이브 `Smuggler`·`Underworld`. 태그는 대사 선택지 게이트(`flag:party_albert`, 선택지 `When`)에만 쓰고 판정 수치에는 영향이 없다.
- 장비: 알버트 `Revolver`(1d10+2 x5), 메이브 `SmallPistol`(1d6 x5)+`Dagger`(1d4+2 x5). 모두 기존 `BattleDamage.ForWeapon` 재사용. 탄약은 전투당 6발(재장전 행동 1턴). 동료 탄약은 세이브하지 않는다(전투 시작 시 가득).
- 수치 근거: 평균 파비안과 비슷한 급, 자기 전문 1~3개만 60 안팎. 첫 플레이 솔로 기준 밸런스를 깨지 않도록 90 근처 기능은 없다. 정신 규칙·전투 강도는 플레이테스트로 조정(미검증).

## 3. 탐색 판정 보조 (고유 설계 명시)

7판 근거: 규칙서의 "Bonus and Penalty Dice"에서 도움(협력)에 보너스 다이스를 주는 수호자 재량 관례와, 다른 기능을 합치는 "Combined Rolls"가 있는 것으로 안다. **정확한 장·페이지는 확인 필요**. 아래 세 방식은 이를 모티프로 한 **고유 설계**다.

### 3-1. 세 방식

| 방식 | 효과 | 조건·제한 | 대가 |
|---|---|---|---|
| 도움 | 보너스 다이스 +1(10의 자리 추가 굴림, 낮은 쪽 채택) | 동료의 **해당 기능 >= 40**. 동료가 여러 명이어도 도움 보너스는 최대 +1 | 없음(도움은 판정 1회당 1번) |
| 대신 판정 | 동료가 자기 기능치로 판정. 그 결과가 곧 결과 | 동료 기능 > 파비안 기능 +10 이상일 때 후보 노출. **SAN·행운·밀어붙이기·전투 판정 불가**, 대사 상대가 "파비안 본인에게만 말하는" 장면(`flag` 로 지정) 불가 | 실패 결과(부상 등)는 동료가 받음(`party.<id>.hp` 감소) |
| 동료 전용 선택지 | 대사 선택지·키워드 항목이 열림 | `flag:party_albert`/`party_maeve`(태그) | 없음 |

- 목표치 계산은 기존 `SkillCheck.ClampTarget`(<=90) 그대로. 보너스 다이스는 `SkillCheck` 에 **새 메서드**로 추가한다(7판화 R-5 와 같은 방향, 기존 `Resolve` 시그니처·`Condition` 동작은 불변):

```csharp
// SCoW.Core.Rules.SkillCheck 에 추가 (기존 메서드 변경 없음)
public static int ResolveValueWithBonus(Condition condition, DiceFaces f, int bonusTensCount, int penaltyTensCount, int[] extraTens);
public static RollResult RollWithAssist(IRng rng, int target, Condition condition, int assistBonusDice, bool perfectionism = false);
```
  보너스 다이스 총합 = 컨디션(Best +1, Worst -1) + 도움(+1) 을 -2~+2 로 클램프. Good/Bad 의 1의 자리 변종은 그대로 둔다. 추가 10의 자리는 `rng.Next(0,10)*10` 을 개수만큼 소비(`RollFaces` 소비 순서 뒤에 이어 붙임 → 도움 0개면 기존과 난수 시퀀스 동일, EditMode 동치 테스트 필수).
- **UI**: 일반 룰렛 시작 직전에 "도움 받기 / 대신 시키기" 칩(동료 초상화 + 기능치 표시)을 보이고 기본은 도움 없음(자동 사용 금지: 소요 비용 없이 늘 켜면 판정 의미가 사라진다). 비용을 두고 싶으면 Q16.

### 3-2. 판정 종류별 허용

| 판정 | 도움 | 대신 | 예 |
|---|---|---|---|
| 관찰·분석·청취·손재주·의술·오컬트·화술(조사·대화) | O | O | 소장 설득 `Persuasion`(화술: 메이브 70), 현장 의학(알버트는 25라 후보 안 됨) |
| 힘(창문 `tryBrokenWindow`) | O | O | 알버트 65 이면 대신 판정 후보(실패 시 알버트 HP 손실) |
| 은신(하수도 진입) | **단체 판정**(아래) | X | 3-3절 |
| SAN 판정 | X(개별 판정, 3-4절) | X | |
| 행운 | X | X | 파비안 전용 |

### 3-3. 하수도 은밀·위장(`BattleEngine.SewerDisguiseFails`)

현행은 파비안 DEX 1회. 파티가 있으면: **전원 중 가장 낮은 은신치 기준**으로 판정(단체 은신 관례; 7판 "Group rolls" 와 같은 방향이나 이름·세부는 확인 필요, 고유 설계). 알버트 은신 15가 걸림돌이라 변장(작업복·코트)이 있으면 계산에서 제외. 메이브가 파티에 있으면 대신 판정으로 "내가 앞장서겠다"(은신 40) 선택지를 연다. 시그니처: `SewerDisguiseFails(int dex, IRng rng)` 는 그대로 두고 `SewerPartyDisguiseFails(int[] groupLowestDexes, IRng rng)` 신규.

### 3-4. 동료 SAN·반응 대사

- SAN 체크 장면은 **전원 각자 판정**(자기 SAN 목표치). 손실량은 현행 실패 룰렛과 같은 분포: 5(2/3) 또는 10(1/3)(`SanCheck.RollLoss`, 호출처 확인 필요). 성공 시 0. 파비안은 기존 룰렛 연출 유지, 동료는 **자동 판정 후 결과 한 줄**.
- **제외 장면**(파비안 단독): 악몽 4회(`DetectiveOffice` 208-253, 291-316, 317-342, 343-361), 꿈·환각. 동료 SAN 은 `SanCheckSource.Shared`/`Solo` 플래그로 구분(`StoryNode`/호출 메타에 `shared` 속성 추가, 기본 `Solo`: **기존 장면 변동 없음**, 하수도 장면 5곳과 전투만 `Shared` 로 이행).
- **반응 대사**: 새 CSV `Party.csv`(ID 1~, `TextKey = "Party:n-m"`). 키: 판정 종류별 성공·실패 한 줄(알버트 "..." / 메이브 "..."). 영문, 한글 열은 I2 키가 아니라 CSV 열 추가(`StoryImprove` 5절 4항). **I2 새 키는 만들지 않아도 된다**(코드가 I2 를 안 씀). I2 를 쓰는 UI 문구(파티 메뉴 등)만 새 키 `Party_*` 로 추가.

## 4. 전투 (다인원·횡 대열)

### 4-1. 현재 실측(P14a)

- `BattleEngine`(순수): `StepGauge`(파비안 속도 2, 적 1.9, 130 임계), `ChooseIntent`, `ResolveContest`, 피해식. `EnemyState`(Def+가변)·`EnemyDefs`(5종). **다인원은 없음**: 적 슬롯이 `battleState` 문자열 + `EnemyState` 5필드 + 이름이 박힌 분기 46곳+(`BattleManager`), `BattleManager.cs:595` 에 `DeepOne1` 하드코딩.
- 플레이어 행동은 `PlayerSetAttack/Swords/Markmenship/Deftness/Reload/RunAway` → 룰렛(`Rollet`) 대립 판정 → `RetrunRolletResult` 3800줄 분기. 파비안 한 명을 전제로 한 UI.

### 4-2. 대열

| 항목 | 값 |
|---|---|
| 아군 칸 | 3(`Rank 0` 앞, `1` 중, `2` 뒤). 인원 < 3 이면 앞에서부터 채움. 기본 배치: 알버트 앞, 파비안 중, 메이브 뒤 |
| 적 칸 | 3(앞·중·뒤). 시나리오가 지정. 현 적 2마리 구조(`enemyOb`/`enemy2Ob`)는 앞·중에 둔다 |
| 교체 | 인접 칸과 위치 교환. **그 행동이 곧 자기 차례**(교환 상대 차례는 유지). 쓰러짐·광기 상태도 교환 가능 |
| 사라진 칸 | 쓰러짐/도망 시 칸은 비우고 뒤가 앞당겨진다(앞이 비면 압축) |

### 4-3. 위치별 행동

| 행동 | 앞(0) | 중(1) | 뒤(2) | 대상 |
|---|:-:|:-:|:-:|---|
| 근접(무술·검술·단검·배트·도끼) | O | O(적 앞·중만) | X | 적 앞 또는 중 |
| 사격(소형·리볼버·소총·산탄) | O(불리 다이스 +1) | O | O | 적 전 칸. 앞 칸에 적이 있으면 뒤 칸 적은 보너스 -없음(단순화) |
| 던지기(돌·화염병) | O | O | O | 전 칸 |
| 방어(회피 태세) | O | O | O | 자신: 다음 피격 대립 판정에 보너스 다이스 +1(차례 끝까지) |
| 응급처치 | O | O | O | 아군(쓰러짐 우선), 의술 판정 |
| 진정 | X | O | O | 광기 아군, 심리치료·화술 |
| 위치 교환 | O | O | O | 인접 아군 |
| 도주 | 파비안만 | 파비안만 | 파비안만 | 파티 전체(5-5) |

- 근접 제한은 "적 뒤 칸은 앞 칸을 지나야 닿는다"는 장르 관례를 쓴 **고유 값**이다. 적 앞 칸이 살아 있는 한 근접은 앞·중만 닿는다(`ValidTargets` 의 규칙, 데이터가 아니라 코드 상수).
- 적 행동의 사정권: 일반 공격은 아군 앞(0)·중(1) 우선(가중 5:3:2, 빈 칸이면 합산 정규화), `SanAttack` 은 **전 칸 무작위**(시선), `PoisonAttack` 은 앞(0) 우선. 정확한 가중치는 `EnemyDef` 데이터(`TargetWeights`)로 뺀다(P14b "ChooseIntent 종류 분기 → EnemyDef 가중치 데이터"와 같은 방향, 4-8절).

### 4-4. 턴 순서(속도 게이지)

- 현행 단일 규칙을 일반화: 행위자(Actor)마다 `Gauge`. 한 "틱"에 모든 행위자가 `StepGauge` 와 같은 식(`+= (1 + dex/100) * speed`)으로 오르고, **130 이상이 된 행위자가 차례를 받는다**. 속도 상수: 아군 2.0, 적 1.9(현행 유지). 동시 도달 시 DEX 높은 쪽 → 아군 우선 → 슬롯 번호.
- 현행 `StepGauge` 는 도달 시 -130 으로 리셋하는 형태이므로 **1:1 전투에서 옛 로그와 동치**가 되도록 틱 안의 처리 순서(아군 슬롯 0..2, 적 슬롯 0..2)를 고정한다. 옛 `SymbolUp`(`BattleManager.cs:266`) 의 호출 순서를 builder 가 먼저 읽고 맞춘다(**미확인 항목**).
- 순수 클래스 `BattleTimeline`: `Actor Next(IRng, IList<IBattleUnit>)`, `IEnumerable<Actor> Preview(int n)`(상단 순서 바에 쓸 다음 n 명, 게이지 복사본을 시뮬레이트).
- 상태 이상: **Downed 는 게이지가 돈다**(자기 차례 도달이 카운트다운이라서, 행동은 못 함). Stable·Broken·Fallen 은 게이지 정지. 광기는 차례는 받되 행동만 자동(`BeginTurn` false, 차례 연출 유지).
- 연출: 차례를 받는 행위자가 앞으로 반 걸음(기존 `SymbolUp` 아이콘 대신 순서 바 + 초상화 하이라이트).

### 4-5. 행동 해결(대립 판정)

- 파비안: 기존 룰렛 UI 유지. 동료·적의 판정은 **헤드리스 자동**(`SkillCheck.Roll`) 후 결과창 한 줄 + 팝업.
- 아군이 맞을 때: 기존 회피·반격 선택 대신, 동료는 `Evasion`(회피 기능) 자동 대립으로 처리(`ResolveContest(Evasion, allySuccess, enemySuccess)` 그대로). 파비안은 현행(플레이어 선택).
- 방어구·피해 보너스 없음(`CocDesign` 1-4 와 동일, 이번 범위 아님).

### 4-6. SAN·광기가 전투에 미치는 영향

| 항목 | 규칙(고유 설계, CocDesign 7번 `일시 광기` 모티프) |
|---|---|
| 피해원 | `SanAttack`(심해인 시선), 아군이 동료를 쓰러뜨림·사망 목격(SAN -5, 아군 전원 개별 판정), 새 요소 없음 |
| 판정 | 대상이 SAN 판정(목표 = 현재 SAN). 실패 시 손실 5 또는 10(`RollLoss`). 현행 파비안의 3칸 룰렛(SAN 감소/광기 발현/컨디션)은 전투 SAN 공격에서도 **파비안에게는 그대로**(회귀 방지) |
| 발작 조건 | 한 번에 **10 이상** 손실 + **INT 판정 실패**(7판 "5 이상 + INT"를 이 게임 스케일 x5 로 환산) → 전투 광기 `BoutKind` 1d3 턴 |
| 발작 종류(1d4) | 1 도주(뒤 칸으로 강제 교환, 차례 건너뜀) / 2 얼어붙음(차례 건너뜀) / 3 난동(무작위 아군 1명 공격, 상대 SAN -5) / 4 낙담(방어만 가능, 인접 아군 보너스 다이스 -1) |
| 해소 | 지속 턴 소진, 또는 **진정**(5-6) 성공 시 즉시 해소 + SAN +5 |
| SAN 0 | 동료: `Broken`(전투 종료까지 행동 불가, 종료 후 파티 이탈, 다음 일차 아침 재합류 가능). 파비안: 기존 게임오버 |
| 파비안 발작 | 연출 위주. `madStage >= 2`(`StoryImprove` 4-1, `madTemp`)일 때만 전투 시작에 1회 발작(1d3 턴). 1턴 스킵·방어만 둘 중 하나로 한정, 진행 막힘은 없다(Q12·Q13 가정과 같은 방향) |
| 장점 | 앞 칸 알버트는 SAN 공격에 덜 맞음(가중치 5:3:2 와 별개로, SanAttack 은 전 칸 균등) → 심리적 부담을 뒤 칸에 분산 |

- 숫자는 전부 초안. 플레이테스트 조정 대상(미검증).

### 4-7. 쓰러짐·사망·부상

| 상태 | 진입 | 행동 | 종료 |
|---|---|---|---|
| Ready | 기본 | 정상 | |
| Downed(쓰러짐) | HP <= 0 | 행동 불가, 칸 유지, 카운트다운 3(자기 게이지 도달 3회) | **응급처치**(의술 판정, 보조 행동 1턴) 성공 시 Stable(HP 5, 행동 불가 유지) → 전투 후 회복. 카운트 0 → Fallen |
| Fallen(전선 이탈) | 카운트 0 | 칸에서 제거 | 전투 승리: `Recovering`(HP 1, 다음 시간대까지 불참). 도주·패배: **영구 사망**(`party.<id>.dead`) |
| Broken | SAN 0 | 행동 불가 | 4-6 |

- 쓰러진 아군을 또 맞히면 즉시 Fallen(7판의 "죽어가는 자 추가 피해" 대응, 고유 규칙).
- 파비안이 쓰러지면 **아군이 한 명이라도 서 있을 때** Downed 카운트다운(동일 규칙). 아군 전원 쓰러지거나 파비안 카운트 0 → 기존 게임오버(`DirectingManager.cs:140` 호출 경로). 솔로 전투는 기존대로 HP<=0 즉시 게임오버(회귀 방지, Q17).
- 동료 사망은 **영구**: 스토리 플래그(`party.<id>.dead`)로 에필로그 연결(1-3절). 해당 동료가 있는 모든 이후 장면은 `Dead` 변형 없이 대사를 건너뜀. 임시 가정: 가끔 유저에게 힘든 영구 사망이라 Q17.

### 4-8. 엔진 인터페이스 (Core/Rules, 순수 C#, 새 파일)

```csharp
namespace SCoW.Core.Rules {
  public enum Side { Ally, Enemy }
  public enum UnitStatus { Ready, Downed, Stable, Fallen, Broken, Recovering }
  public enum BoutKind { None, Flee, Freeze, Lash, Despair }
  public enum ActionKind { Melee, Ranged, Throw, Guard, FirstAid, Calm, Swap, Special, Reload, Run, Item }

  /// 파비안/동료/적 공통. 파비안의 HP·SAN 은 IVitals 어댑터로 기존 전역(DataBaseManager)에 연결.
  public interface IVitals { int Hp {get;set;} int MaxHp {get;} int San {get;set;} int MaxSan {get;} }
  public interface IBattleUnit { string Id {get;} Side Side {get;} int Dex {get;} bool IsActing {get;} /* Ready 만 true */ float Gauge {get;set;} int Rank {get;set;} }

  public sealed class CompanionDef { /* Id, 능력치 고정값, Skills(Dictionary<string,int>), WeaponId, SpecialIds, Tags */ }
  public sealed class PartyMember : IBattleUnit { /* Def, IVitals, Status, DownedCountdown, Bout, BoutTurns, IsLeader */ }
  public sealed class BattleSlot { public Side Side; public int Rank; public IBattleUnit Unit; }

  public sealed class BattleField {
    public BattleSlot[] Allies = new BattleSlot[3];
    public BattleSlot[] Enemies = new BattleSlot[3];           // EnemyState 는 IBattleUnit 어댑터 EnemyUnit 로 감쌈
    public IBattleUnit NextActor(IRng rng);                    // BattleTimeline 위임
    public IReadOnlyList<int> ValidTargets(IBattleUnit actor, ActionKind kind);  // 4-3 표
    public BattleEvent[] Resolve(BattleCommand cmd, IRng rng); // 헤드리스 해결(자동 판정 포함)
    public bool Swap(int rankA, int rankB, Side side);
    public bool IsOver(out BattleOutcome outcome);             // Win / Loss / Fled
  }
  public readonly struct BattleCommand { public IBattleUnit Actor; public ActionKind Kind; public int TargetRank; public Side TargetSide; public string Special; }
  public readonly struct BattleEvent { public string Type; public string ActorId; public string TargetId; public int Value; public string Text; }  // Damage, SanLoss, BoutStart, Down, Fallen, Swap ...
}
```

- `EnemyState`(이미 순수)는 `EnemyUnit : IBattleUnit`(래퍼)로 감싸 **EnemyState[]** 로 보유. `EnemyDef` 에 `TargetWeights int[3]`, `IntentWeights`(종류별 가중치)를 추가해 `ChooseIntent` 의 종류 분기를 데이터화(P14b 메모). 기존 `EnemyDefs` 정적 인스턴스·`Roll` 난수 소비 순서 불변.
- `BattleEngine`(정적)은 파티 무관 순수 함수(`ResolveContest`, 피해식, `StepGauge`)로 유지. 파티 규칙은 **새 파일**(`BattleField.cs`, `PartyMember.cs`, `BattleTimeline.cs`, `CompanionDefs.cs`)로 분리 — `BattleEngine.cs` 는 현재 200줄이지만 책임이 달라서다.
- `BattleManager`(View): 행동 해결을 `BattleField.Resolve` 의 `BattleEvent[]` 를 받아 재생. 기존 `RetrunRolletResult` 3800줄 분기는 **P14b 에서 BattleEvent 재생으로 대체**(단계적: 파비안 룰렛 결과만 `BattleCommand` 로 변환해 넘기는 어댑터부터).
- **P14b 메모 반영 표**:

| P14b 메모 | 파티 설계에서 처리 |
|---|---|
| EnemyState 5필드·이름 슬롯 분기 46곳+ → `EnemyState[]`+인덱스 루프 | `BattleField.Enemies[]` 가 `EnemyUnit` 보유. PT-1 의 선행 작업 |
| `BattleManager.cs:595` `EnemyDefs.DeepOne1` 하드코딩 → 슬롯 `state.Def` | 위와 함께(`SetEnemy(string)` 가 `EnemyDefs.Get(id)` 로) |
| `ChooseIntent` 종류 분기 → `EnemyDef` 가중치 | 4-3·4-8 `IntentWeights`/`TargetWeights` |
| 피해식 `Def.AttackDamage` 통일 | 동료 피격에도 동일 |
| `PoisonTickDamage`·`StepGauge out` 미사용 정리 | `BattleTimeline` 이 `StepGauge` 를 직접 쓰므로 out 은 쓰임(유지 가능), `PoisonTickDamage` 는 **아군 전원**에게 틱(`Poisoned` 스택을 `IBattleUnit` 상태로) |
| `battleState` 문자열 → enum | `BattlePhase { PlayerSelect, Resolve, EnemyTurn, End }` enum |
| `SymbolUp` 매 프레임 문자열 비교 → bool | `BattleTimeline.NextActor` 로 대체 |
| `EnemyClass` 빈 껍데기 삭제(P15) | 변경 없음 |

## 5. 전투 행동 상세

| 번호 | 행동 | 판정 | 비고 |
|---|---|---|---|
| 5-1 | 공격(근접·사격·던지기) | 해당 기능 vs 적 방어(`ResolveContest`) | 동료 무기는 2-2절. 대성공 피해 보너스 없음(현행) |
| 5-2 | 방어(태세) | - | 회피 보너스 다이스 +1, 다음 자기 차례까지 |
| 5-3 | 특수(알버트) | 아래 | 전투당 `Cover` 1회, `Warning` 2회 |
| 5-4 | 응급처치 | 의술 vs 목표치 | 쓰러짐 → Stable, 일반 HP 회복 +5(성공)/+0(실패). 알버트 25·메이브 15·파비안 10 로 **전부 낮다** → 성공률 낮음은 의도(쓰러짐 방지는 대열 관리로) |
| 5-5 | 도주 | 파비안 `DEX / 적 수` vs 적 DEX(현행 `RunAway`) | 쓰러짐·Fallen 은 두고 간다(사망, 4-7). 광기 `Flee` 는 도주 판정과 별개 |
| 5-6 | 진정(`Calm`) | 심리치료(메이브 45, 파비안 5) 또는 화술(알버트 50) vs 7판 고정 목표 | 광기 아군 해소 + SAN +5. 메이브 특수 `Stiff drink` 는 판정 없이 SAN +5, 전투당 1회 |
| 5-7 | 특수(메이브) | | `Dirty trick`(손재주 vs 적 회피, 성공 시 적 1턴 스킵, 심해인은 면역) / `Stiff drink` |
| 5-8 | 특수(알버트) | | `Cover`(인접 아군 1명 피격을 대신 맞는 태세, 다음 자기 차례까지, 회피 판정은 알버트 기준) / `Warning`(화술 vs 적 INT, 심해인 하이브리드만 1턴 스킵) |

특수기는 **고유 이름·효과**로 만든 것이며 DD 의 특정 스킬을 따라 하지 않았다. 1인칭 대사는 `Party.csv` 에 ID 를 둔다.

## 6. 세이브

| 항목 | 방식 | 키 |
|---|---|---|
| 합류·사망·대기 | `GameState.Flags` | `party.albert.joined` `party.albert.dead` `party.albert.declined` `party.albert.standby` (maeve 동일) |
| 능력치 변동 | `GameState.Counters` | `party.<id>.hp` `party.<id>.san` `party.<id>.status`(UnitStatus 정수) `party.<id>.backAt`(Recovering 복귀 `timeCount`) |
| 대열·순서 | `GameState.Texts` | `party.order`(`albert,fabian,maeve` 같은 콤마 목록) |
| 전투 중 상태 | 저장 안 함 | 전투 종료 시에만 위 키로 환원. 전투 중 저장은 현행처럼 막힘(`BusyRules`) |

- `SaveCodec`(`Core/Save/SaveCodec.cs`)는 `Flags`/`Counters`/`Texts` 를 키 정렬 후 일반 레코드로 기록하므로 **코드 변경 없이** 새 키가 저장·복원된다. 구세이브는 키가 없으면 `false`/0/빈 문자열 → "파티 없음"(`SchemaVersion` 1 유지, `SaveMigrator` 변경 불필요).
- 동료 HP·SAN 이 0이어서 "키 없음"과 구별되지 않는 문제: `party.<id>.joined` 가 있을 때만 `hp/san` 을 읽고, 없는 키는 `MaxHp/MaxSan` 으로 초기화.
- ES3 기존 키(옛 `DataBaseManager` 직렬화 경로)는 건드리지 않는다. ES3 → `GameState` 이행 상태는 `Architecture.md` P5·P7 을 따르며, 이 문서의 키는 `GameState` 쪽에만 둔다.
- 플래그·카운터 키 상수는 `FlagKeys.cs`/`CounterKeys` 에 **추가만**.
- I2: 새 UI 문구(파티 패널, 행동 이름 등)만 `Party_*` 새 키로 추가. 기존 용어 키 변경 없음.

## 7. UI·입력 (마우스·키보드·패드 3경로)

모든 입력은 `GameInput`(Core 가 입력을 모름, `Architecture.md` 2-7절)을 거치며 **`UnityEngine.Input` 직접 호출 금지**(`.claude/rules/csharp.md`). 새 액션은 `Assets/Resources/Input/*.inputactions` 의 `Battle` 맵에 추가.

| 동작 | 마우스 | 키보드 | 패드 |
|---|---|---|---|
| 행동 선택(공격·방어·특수·교환…) | 버튼 클릭 | 1~6 / 방향키+Enter | 십자키·왼 스틱 + A, B 취소 |
| 대상 선택 | 적·아군 초상화 클릭, 호버 시 하이라이트 | 좌우 방향키(칸 이동)+Enter | 왼 스틱 좌/우 + A. **전투 적 선택 패드 불가였던 문제**(`Architecture.md` P2 행 "전투 적 선택 패드 불가")를 이 기회에 해소 |
| 대열 교체 | 초상화 드래그 또는 `Swap` 선택 후 인접 칸 클릭 | `Q`/`E`: 현재 행위자를 좌/우 인접 칸으로 | LB/RB: 좌/우 교환 |
| 순서 바 보기 | 항상 표시 | - | - |
| 파티 메뉴(수첩 옆, 대화·도움 선택) | 칩 클릭 | `Tab` | Y |
| 취소 | 우클릭 | Esc | B |

- 포커스 링: 선택 가능한 대상에 윤곽 표시, 방향키/스틱으로 이동 시 `EventSystem.SetSelectedGameObject`(패드·키보드 공용). 마우스 호버는 동일 선택 상태로 반영(이중 하이라이트 방지).
- 판정 중 UI(룰렛)는 현행 입력 우선순위(`UiStack`·`BusyRules`) 유지.
- 해상도·가독성: 3명 초상화 + 적 3 + 순서 바가 한 화면에 들어가야 한다 → 도트 UI 레이아웃은 builder 가 스크린샷(`unity-render`)으로 확정, 그래픽 변경은 사용자 검토 후 적용(메모리 `graphics-user-review`).

## 8. 단계·비용·검증

비용: S(1세션 이내) / M(1~2세션) / L(3세션+). builder 한 단위 = 한 단계. P14b 와 묶는 순서: **P14b-a(엔진 리팩터)가 PT-1 의 전제**.

| 단계 | 내용 | 비용 | 선행 | 검증 기준 |
|---|---|:-:|---|---|
| PT-0 (완료 11-21) | 데이터·세이브 | S | 없음 | `CompanionDefs`(2명 고정값), `FlagKeys` 키, `party.*` 저장·로드 EditMode 테스트, 구세이브 로드 시 파티 없음·크래시 없음 |
| PT-1 | **P14b-a**: `EnemyState[]`+`EnemyUnit`, enum `BattlePhase`, `IntentWeights`/`TargetWeights` 데이터화, `DeepOne1` 하드코딩 제거. **1:1 전투 외형·시드 로그 동치** | M | | 시드 고정 전투 로그가 리팩터 전후 같음, 하수도 전투·귀가길 전투 플레이 모드 확인 |
| PT-2 (완료 11-21) | Core 파티: `PartyMember`/`BattleSlot`/`BattleField`/`BattleTimeline`, 위치 규칙, 쓰러짐·광기, 응급처치·진정 | L | PT-1 | EditMode: ①1v1 이 옛 `StepGauge` 와 같은 차례 순서 ②3v3 시드 로그 ③근접 사거리·교환 ④쓰러짐 카운트·Fallen·승/도주 사망 ⑤발작 판정(분포) |
| PT-3 | 합류·이탈 스토리 연결(JSON·`WorldRules`): 알버트 합류 노드, 메이브 합류, NPC 자리 숨김, 키워드 대사 재사용, `Ending_Party*` 변주 | M | PT-0, 키워드 `Old route`(KEYWORD/C-2) | 플레이 모드: 3일차 경찰서에서 키워드 제시→합류→NPC 사라짐→키워드 사망 안 일어남. 엔딩 A 에서 `albertDead` 합집합 분기 검증(V-10 회귀 포함) |
| PT-4 | 탐색 보조: `SkillCheck` 보너스 다이스 메서드, "도움/대신" 칩 UI, 동료 SAN 자동 판정, `Party.csv` 반응 대사 | M | PT-0 | EditMode: 도움 0개에서 난수 소비 동일(회귀), 보너스 다이스 분포(성공률 상승), 플레이 모드 소장 설득에 메이브 도움 |
| PT-5 | **P14b-b 전투 View 다인원**: 3칸 초상화·순서 바·행동/대상 선택, 동료 자동 판정 재생, `BattleEvent` 재생기 | L | PT-2 | 플레이 모드: 하수도 3v3 전투 한 판 스크린샷 연속 캡처, 파비안 룰렛 정상 동작(회귀) |
| PT-6 | 입력 3경로(7절): GameInput 액션·포커스 링·대열 교체 | M | PT-5 | 마우스·키보드·가상 패드(에디터)로 대상·행동·교환 전부 가능, `Input.*` grep 0 |
| PT-7 | 하수도 연동: 단체 은신, 방별 조우 전 파티 상태 환원, 동료 쓰러짐·SAN 환원, 밸런스 1차 | M | PT-3~6, SEWER-1 S1 | 하수도 끝까지 3인 파티 플레이, 솔로 대비 승률 비교, 구세이브 로드 |

- 순서 요약: PT-0 → PT-1 → PT-2(병렬: PT-3·PT-4) → PT-5 → PT-6 → PT-7.
- 같은 파일(`BattleManager.cs`, `InteractionController`)을 만지는 PT-1·PT-5 와 PT-3 은 **직렬**(`TokenEconomy.md` 2절).

## 9. 함정

- **턴 칸 매핑(PT-1 잔여)**: `BattleManager` 의 적 인덱스 <-> 턴 게이지 칸 표(`RoadSymbolEnemy`/`SewerSymbolEnemy`/`EnemySymbolSlot`)는 1:1 외형 유지용 장소별 고정표라 하수도에서 혼혈4·심해인1 이 칸0·1 을 겹쳐 쓴다. PT-2 에서 `BattleField.Enemies[3]`/`Rank` 로 "등장하는 적 -> 칸" 을 전투마다 만들어 이 표를 없앤다.
- **`BattleManager` 3900줄**: 46곳 분기를 한 번에 바꾸려 하지 말고 PT-1 에서 `EnemyState[]` 로 바꾼 뒤 View 재생기(PT-5)에서 분기를 줄인다.
- `Random.Range(int,int)` 상한 제외, 새 보너스 다이스 난수 소비 순서는 기존 시퀀스 뒤에 붙인다(시드 동치).
- 알버트 치명 키워드 로직(`WorldRules`)과 `party.albert.joined` 우선순위: 합류 후 `albertDeathKeyword` 가 서 있어도 소멸시키지 않음(대신 대사 1줄 변주). 에필로그는 `albertDead` 합집합을 **새 케이스 맨 앞**에서 평가(기존 케이스는 뒤에 보존).
- Albert NPC 는 2일차 낮 현장·귀가길 이전에는 NPC, 3일차부터 합류 대상. `Policeoffice` 3종 첫 대사(`thirdDayPoliceADialog` "휴가")와 합류 대사가 겹치지 않게 조건(`!party.albert.joined`) 처리.
- 동료 SAN 자동 판정은 `Solo` 장면에서 일어나면 안 된다(기본 `Solo`, `Shared` 는 명시 이행).
- `DirectingManager.cs:140` 게임오버 경로: 파비안 다운 카운트다운 규칙은 파티 전투에서만 켠다(`BattleField.LeaderDownRule`). 솔로·기존 경로는 불변.
- 동료 영구 사망은 되돌릴 수 없으므로 `party.<id>.dead` 저장 시 확인(자동 저장 직전 전투 승리 후 정산) — 도주 시 사망 정산을 전투 종료 시점 한 곳에서만 수행.
- 3명 초상화·전투 UI 는 현 `Main.unity` 씬 레이아웃(씬 미통독)과 겹칠 수 있다. 씬 수정은 프리팹 단위로, `*.unity` 통독 금지.
- 그래픽·초상화 도트는 PixelLab 예산(메모리 `budget-priority`)이 걸려 있어 **PT-5 이전에 초상화 제작 여부를 사용자에게 확인**(Q19).

## 10. 검증 시나리오(프로브)

| # | 무엇을 | 기대 |
|---|---|---|
| V-P1 | 1일차·2일차 | 동료 합류 선택지 없음. 2일차 낮 현장·귀가길 전투는 솔로(회귀 V-10) |
| V-P2 | 3일차 경찰서, 알버트 생존 + `Sewerage Maintenance Office` 제시 | 합류 선택지, 수락 시 NPC 사라짐, 파티 패널 2인 |
| V-P3 | 알버트 치명 키워드를 합류 전에 사용 → 3일차 | 알버트 소멸, 합류 선택지 없음, 메이브는 가능, 에필로그 148-153 유지 |
| V-P4 | 메이브 합류 → 하수도 진입 | 3인 파티, 단체 은신 판정, 동료 위치 초상화 |
| V-P5 | 판정: 메이브 도움 | 성공률 상승(보너스 다이스), 도움 없음 시 난수 시퀀스 동일 |
| V-P6 | 전투 3v3 | 순서 바, 앞 칸만 근접, 뒤 칸 근접 불가, 교환, 시드 로그 재현 |
| V-P7 | SAN 공격 연속 | 동료 SAN 감소, 10 이상+INT 실패 시 발작, 진정으로 해소 |
| V-P8 | 쓰러짐 | 카운트다운, 응급처치 성공 시 Stable, 승리 후 Recovering, 도주 시 사망 → 에필로그 변주 |
| V-P9 | 마우스·키보드·패드 | 대상·행동·교환 3경로 모두 완주 |
| V-P10 | 구세이브·ES3 | 새 키 없음 = 파티 없음, 저장→로드 후 파티 상태 동일 |

- PT-0 구현 메모: 키는 자동생성 `FlagKeys.cs` 를 건드리지 않고 `Core/State/PartyState.cs` 의 `PartyKeys` 에 둔다. 합류 후의 HP 0 은 진짜 0(`Join` 이 최대값을 기록, 읽기는 `joined` 여부만 본다). `Join` 은 처음이면 `party.order` 에 `fabian` 을 먼저 넣는다.

- PT-2 구현 메모(고유 설계로 정한 값, 전부 초안·플레이테스트 조정 대상). 코드: `Core/Rules/` `BattleUnits.cs`(enum·`IBattleUnit`·`EnemyUnit`·`BattleCommand`·`BattleEvent`), `PartyMember.cs`, `BattleTimeline.cs`, `BattleField.cs`(구조·대상·승패·정산)·`BattleFieldActions.cs`(차례 시작·행동 해결), `PartyBattleRules.cs`. 테스트 `PartyBattleTests`(30개, 골든 `PartyBattlePT2.golden.txt`).
  - **턴 순서**: 한 틱에 아군 칸 0..2 -> 적 칸 0..2 순서로 보고, 먼저 130 이상인 한 명만 차례를 받는다(뒤 순서는 그 틱에 오르지 않음). 옛 `SymbolUp` 과 동치(1v1 테스트 통과). 4-4절의 "DEX 높은 쪽 우선" 타이브레이크는 쓰지 않는다(칸 순서). `Next(order)`·`Preview(order, n)` 는 난수 인자가 없다.
  - **Downed 는 게이지가 돈다**(자기 차례 도달 3회가 카운트다운. 4-4절 "쓰러짐은 게이지 정지"와 충돌해 카운트다운 쪽을 택함). Stable·Broken·Fallen 은 정지.
  - **판정**: 아군 공격 = 기능 판정 vs 적 `RollPlain`(근접은 적 무술, 사격은 적 회피, 난수 2개). 실패는 빗나감(반격 없음). 앞 칸 사격 = 불리 다이스(`Condition.Worst`), 방어 태세 = 보너스(`Best`), 절망 이웃 = 불리. 다중 보너스 다이스는 PT-4.
  - **적 행동**: `DecideEnemy` = `ChooseIntent` -> `TargetWeightsFor`. 점유 칸만 가중 합산, 점유 칸 하나면 난수 없음, 가중 합 0 이면 균등. `Deftness` 의도는 일반 공격으로 처리(옛 의미 미확인). 독 = 3틱, 자기 차례 시작 1d2x5(Ready 만, 쓰러지면 해제).
  - **쓰러짐·정산**: Downed 또는 Stable 을 또 맞히면 즉시 Fallen. 동료 Fallen 은 칸에서 빠지고(압축) 남은 Ready 아군이 목격 SAN 판정(실패 -5, 발작 없음). 솔로(`LeaderDownRule` 꺼짐) 파비안 HP 0 = 즉시 Loss. 파비안 Fallen·SAN 0·행동할 Ready 아군 없음 = Loss. `Settle()` 은 한 번만: 승리 -> Fallen=Recovering(HP 1), Downed·Stable=Ready(HP 최소 5). 도주·패배 -> Fallen·Downed·Stable 동료 `Dead`.
  - **발작**: 손실 10 이상 + INT 판정 실패(`RollBout`). 파비안은 `LeaderBoutEnabled`(기본 꺼짐)일 때만. 광기 차례는 지시 없이 자동(`BeginTurn` false): Flee = 맨 뒤와 교환, Freeze = 건너뜀, Lash = 무작위 아군 무술 피해 + 상대 SAN -5, Despair = 방어만. 응급처치 = 의술 판정(Downed->Stable HP 5, 그 외 +5). 진정 = max(심리치료, 화술) 판정, 중·뒤 칸만, 성공 시 발작 해소 + SAN +5.
  - **리뷰 반영(11-21)**: `BattleCommand` 에 `TargetSide`(행동 종류로 정해짐)·`Special`·`PreRolled`(`SuccessLevel?`, 행위자 자신의 판정을 굴리지 않고 룰렛 결과 주입) 추가, `ActionKind` 에 Throw/Special/Reload/Item 자리(Resolve 는 Invalid). 파비안 대응 선택은 `BattleOptions.ManualLeaderDefense` 일 때 Resolve 가 `ResponseNeeded` 로 멈추고 `ResolveResponse(DefenseChoice, preRolled)` 로 이어진다(반격 성공 = 파비안이 적을 침, 실패 = 맞음).
  - **BattleEvent**: 판정 단계는 `ActorLevel`/`TargetLevel`(SuccessLevel 정수, 그 이벤트의 행위자·대상 기준), `Rank`/`Hp`/`San` = 대상의 변화 후 값, `Detail` enum(문자열 대신), `SanLoss.ActorId` = 원인. 새 종류: `Compact`(슬롯 압축, Value = 옛 칸), `Departed`, `ActionStart`(적 이성·독 공격 시작), `ResponseNeeded`. 피해 이벤트는 쓰러짐(Down/Fallen)보다 앞선다. `Resolve`·`Settle` 모두 호출자 버퍼 `List<BattleEvent>` 에 쌓는다(반환값 없음). `BattleOptions`(LeaderDownRule/LeaderBoutEnabled/ManualLeaderDefense)는 생성 시에만 정한다.
  - **환원**: `Core/State/PartyBattleBridge`. `BuildRoster(state, leader, timeCount)` = `party.order` 순서(최대 3, standby·dead 제외, 복귀 시각 지난 Recovering·Broken 은 되살림: Recovering HP 최소 MaxHp/2, Broken SAN +5). `WriteBack` = Settle 뒤 HP·SAN·상태·`backAt`(Recovering = timeCount+1, Broken = 다음 일차 아침) 기록. 사망 원본은 `PartyMember.Dead`(Died 이벤트는 알림). Broken 은 `Departed` 이벤트, 어느 결과든 파티 이탈. 파비안은 Settle 에서 승리 시 Downed·Stable -> Ready(HP 5 이상), 패배·도주는 그대로.
  - 골든(`PartyBattlePT2.golden.txt`)은 이벤트 형식(판정 단계·r/hp/san 열)과 순서(Hit -> Down, Compact 추가) 변경으로 재생성했다. 규칙·난수 소비 순서는 그대로.
  - **미구현(PT-5/7)**: 던지기·특수(Cover/Warning/Dirty trick/Stiff drink)·재장전·탄약·아이템 구현·턴 칸 매핑 표 제거(View 쪽).

- PT-3 구현 메모: 합류 트리거는 카탈로그의 `Location`(상위 Sewage MA Office, 플래그 intelSewageMaintenanceOffice1) 제시로 `Keyword.json` 의 `Albert Bradley/Location`(time>8)·`Maeve/Location`(time>9) 토픽이 `pre: join:id` 효과를 실행한다. 수락/보류 선택지 UI 와 `Old route` 는 미구현(키워드 제시 = 수락, 임시 가정). 새 대사 CSV: 1st_PoliceOffice·1st_Bar 322~, InSewer 416~425(엔딩 변주 3종). 플레이 모드 검증은 미실시.
- PT-5 구현 메모(11-25): View `Battle/Party/`(PartyBattleView·Panel·BattleEventPlayer·PartyBattleDirector), 순수 연출 순서 `Core/Ui/BattleCue.cs`(BattleCueBuilder), 예상 성공률 `BattleField.PreviewAttack`, 툴팁 `UI/TooltipView·TooltipTarget`(UX-11 공유). 진입: `BattleManager.Update` 가 전투 시작 때 한 번 `PartyBattleDirector.TryStart` 를 불러 명단(파비안+동료)이 2명 이상이면 View 가 전투를 넘겨받고, 1명(솔로)이면 옛 경로 그대로. 종료는 `BattleManager.PartyBattleEnd`(죽은 적 정산·승리 시 턴 심볼 끄기·도주는 옛 도주 흐름). 파비안 판정은 `Rollet.SetRollet(..., "PT_Fabian")` -> `EndRollet` 이 `PartyBattleDirector.OnLeaderRoll` 로 PreRolled 주입. 임시 가정: 동료 초상화는 색 사각형+이름, 순서 바는 `Preview(8)`, 동료 사격 탄은 무제한(Core 미추적), 대열 교체는 행동 메뉴 Swap 만(드래그·Q/E 는 PT-6), 하수도 옛 심볼 매핑상 적은 2마리까지(3v3 는 `Begin` 직접 호출). 플레이 모드 3v3 한 판(룰렛·압축·승리) 확인, 솔로 경로는 옛 틱 동작 확인.
- PT-6 구현 메모(11-25): 순수 규칙 `Core/Ui/BattleSelectNav.cs`(메뉴 이동·대상 이동·교체 이웃 칸), 입력 `PartyBattleView.Input.cs`. 대열 교체 단축키는 `UI` 맵의 `SwapLeft`/`SwapRight` 액션: 키보드 **Q/R**(E 는 Interact 확정과 충돌해 R 로 변경), 패드 LB/RB. 행동 메뉴에서 누르면 Swap 줄+이웃 칸을 한 번에 확정(`ViewPick.QuickRank`). 화면 방향 기준(아군은 칸 번호가 클수록 왼쪽 → 좌우 이동 시 칸 증감 반전, 이전에는 아군 대상 좌우가 거꾸로였다). Q 는 수첩 TodoTab 과 같은 키지만 수첩이 열릴 때만 읽혀 전투 View 와 겹치지 않는다. HintBar: 메뉴 `Navigate/Confirm/Swap`, 대상 `Adjust/Confirm/Cancel`(장치별 글리프 `HintGlyphs`). 숫자키 1~6 행동 선택은 1/2/3 이 OpenIntel/Map/SIS 와 겹쳐 보류. 포커스 링은 PT-5 `PartyBattlePanel.SetFocus` 재사용(새 그래픽 없음). 검증: EditMode 441 통과, 플레이 모드 3v3 에서 `DebugSwap` 으로 교체·교체 대상 선택 확인. 에디터 비포커스라 실제 키·패드 이벤트는 처리되지 않아 **키/패드 실입력은 미검증**, 마우스 클릭 취소도 미검증.
- PT-7 구현 메모(11-25): 코드 `Core/Sewer/SewerPartyRules.cs`(은신 값 모으기·파티 인원·조우별 적 구성), `BattleEngine.SewerPartyDisguiseFails`, `PartyBattleBridge.RestoreForSewer`. 연결: `InSewerEnemy`(입구 은신), `DirectingManager.GotoBattleSewer`(조우 전 환원 + 적 구성 + 턴 심볼), `BattleManager.SewerSymbolEnemy` 3칸째 = 혼혈3. 전투 진입은 PT-5 `TryStart` 그대로(명단 2명 이상이면 다인원 View, 1명이면 옛 솔로 경로).
  - **단체 은신(임시 가정)**: 값 = 파비안 DEX(옛 판정) + 동료 은신 기능치(알버트 15·메이브 40), 가장 낮은 값 하나로 `SewerDisguiseFails` 한 번. 코트(`wearCoat`) 착용이면 동료는 변장으로 가려져 파비안 값만. 죽은·대기 동료는 제외. 값이 하나면 옛 판정과 난수 소비까지 같다(구세이브·솔로 동치). 메이브 "내가 앞장서겠다"(대신 판정) 선택지는 대사 작업이 필요해 **미구현**.
  - **방별 조우 전 환원(임시 가정)**: 하수도 안은 시간이 안 흐르고 이탈·귀가가 안 되므로(1-2절) 복귀 시각을 기다리지 않는다. 조우 시작 때 Recovering 은 HP 최소 MaxHp/2(복귀 HP 절반 그대로), Broken 은 SAN +5(한 번)로 Ready 가 된다. Downed·Stable 이 전투 밖에 남아 있으면 Ready(HP 최소 5). 죽은·대기 동료는 그대로. 전투 후 쓰러짐 정산(승리 Recovering, 도주·패배 사망)은 PT-2 그대로라 한 전투 뒤 다음 조우에서 되살아난다.
  - **조우 적 구성(임시 가정)**: 솔로는 옛 구성(E1 혼혈4, E2 심해인1, E3 둘). 파티(2명 이상)는 한 마리 추가: E1 혼혈4+혼혈3, E2 심해인1+혼혈4, E3 혼혈4+심해인1+혼혈3. 하수도 턴 심볼은 3칸이고 칸마다 적 하나(혼혈4 칸1·심해인1 칸2·혼혈3 칸3)라 더 늘릴 수 없다.
  - **밸런스 1차(EditMode `PartySewerTests`, 각 200판·시드 고정·승리 수)**: 파비안 = HP50·SAN52·DEX52·무술50·권총(사격40, 탄 무제한이라 솔로에 유리), 정책은 자동(응급·진정·사격·근접 순). 연속 = E1→E2→E3, 사이에 환원.

    | 조우 | 솔로 | 2인 | 3인 |
    |---|---|---|---|
    | E1 | 198 | 200 | 200 |
    | E2 | 162 | 185 | 196 |
    | E3 | 76 | 160 | 188 |
    | E1~E3 연속 | 19 | 102 | 161 |

    판독: 적 한 마리 추가에도 동료는 확실한 편의(연속 클리어 솔로 9.5% → 3인 80.5%). 솔로는 탄·룰렛·도주가 없는 모의라 실제와 다르다. 3인이 너무 쉬우면 적 HP 보정을 검토(칸 한계로 마릿수는 못 늘림). 플레이 감으로 재조정.
  - **구세이브**: 파티 키가 없으면 인원 1·환원 0·은신 값 [dex]·구성 솔로(`OldSave_WithoutPartyKeysRunsAsSolo`).
  - **검증**: EditMode 454(새 13) 통과. 플레이 모드: Recovering 알버트 + 메이브 합류 상태로 `GotoBattleSewer`(E1) -> 환원(알버트 HP 1 -> 37, Ready) 후 아군 3 대 적 2 다인원 View 가 메뉴 모드까지 진행, 콘솔 error 0. **미검증**: 전투를 끝까지 이어 다음 조우로 넘어가는 연속 플레이, 입구 단체 은신 대사 실제 호출, 3칸째 적(혼혈3)의 전투 맵 오브젝트 표시.
