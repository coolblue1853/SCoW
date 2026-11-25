# CoC 규칙 대조·개선 설계 (CocDesign)

> 2026-11-21 작성. 현재 구현은 코드 실측(파일:줄). 원작 수치는 CoC 7판 수호자 규칙서(Keeper Rulebook) 기준이며 장 번호는 기억 기반이라 "확인 필요" 표시. 코드 수정 없음.
> 전제: 트랙 C 리팩터로 판정 규칙이 순수 C#(UnityEngine 비의존)로 분리된 뒤에 구현한다. 아래 4절 인터페이스는 그 결과물 초안.
> 비 UTF-8 파일(`Rollet.cs`, `BattleRollet.cs`, `BattleManager.cs`, `DatabaseManager.cs`)은 grep/Read 로만 확인했다. 줄 번호는 조사 시점.

## 1. 현재 구조 실측

### 1-1. 능력치·기능 (캐릭터 생성)

| 대상 | 공식 | 근거 |
|---|---|---|
| STR/INT/DEX/HP/MP/SAN/LUK | `(1d6+1d6+1d6)*5` (15~90, 5단위). 7판의 STR·DEX 등 3d6x5 와 같은 방식, 단 CON/SIZ/APP/POW/EDU 없음, HP·MP·SAN 도 독립 3d6x5 | `setStat/setStat.cs:47~74`, 재굴림 `:101~128` |
| 재산(weal) | `Random.Range(5,36)` = 5~35 | `setStat.cs:78` |
| 현재 HP/SAN | 시작 = 최대값 | `setStat.cs:82~83` |
| 기능 포인트 풀 | STR/INT/DEX 값이 그대로 각 풀(`strSkillPoint` 등)이 됨. 5점 단위로 기능에 배분, 기능당 상한 90 | `setStat.cs:50~60`, `setSkill.cs:187~228` |
| 기능 기본값 | STR계: 무술25·사격15·검술20·관찰20·수영10 / INT계: 의술10·분석30·청취30·심리치료5·오컬트5 / DEX계: 회피30·손재주20·화술20·은신15·변장10 | `old_Dialog/DatabaseManager.cs:362~383` |
| 직업·배경 | 없음(grep 0건). 기능 목록 고정 | - |

### 1-2. 일반 기능 판정 (`Rollet.cs`)

| 항목 | 현재 구현 | 근거 |
|---|---|---|
| 호출 | `SetRollet(skillName, statLabel, 목표치, subject, enemy)`. 호출처 124건(`Intel_ObtoUI` 59, `BattleManager` 33, `DirectingManager` 23 …) | grep |
| 목표치 | `0~90` 클램프(90 초과 불가) | `Rollet.cs:132~139` |
| 굴리기 | 1의 자리·10의 자리 다이스를 따로 `Random.Range(0,10)`(×10). 00+0 = 100 | `:247~277`, `:283~286` |
| 컨디션(보너스/페널티) | 전역 `DataBaseManager.condition` ∈ Nomal/Good/Best/Bad/Worst (`:159~206`). Good=1의 자리 다이스 2개 중 낮은 값, Best=10의 자리 다이스 2개 중 낮은 값(진짜 보너스 다이스), Bad/Worst=반대(높은 값). 판정 단위가 아니라 **지속 상태**. 판정 시 다이스는 항상 1세트만 굴려 눈으로 보이는 경우가 있음(UI 오브젝트 good1/good10/bad1/bad10) | `:292~373` |
| 성공 등급 | `>=95` Fumble → `<=skill/3f` "Critical Success" → `<=skill` Success → else Failure. 문자열 `"Result : ..."` 로 결과 전달 | `:379~401` |
| 완벽주의 디버프 | Success → Failure 로 강등 | `:389~392` |
| 결과 처리 | `EndRollet()` 가 `subject` 별 분기: `dialog` → `InteractionController.RetrunDialogResult`(문자열 `Sub_Dialog` 71곳 if) / `Item`(구급상자 HP+1d6x5 등) / `MS_attack`(전투) | `:411~490`, `InteractionController.cs:2611~` |
| 성공 확률 표시 | `succesProbability` 로 Very Hard~Very Easy 라벨 | `:207~226` |
| 행운 판정 | 일반 기능과 같은 룰렛에 `DataBaseManager.luk` 를 목표치로 넣음(커피 `Intel_ObtoUI.cs:287`) | |
| 행운 지출 | **없음** | grep luk |
| 밀어붙이기 | **없음** | |
| 기능 성장 | **없음**(기능치를 올리는 코드는 생성 화면 `setSkill` 뿐. 이후는 광기 디버프가 -10 하는 `Rollet.cs:895~935` 뿐) | grep |
| 단서 놓침 | 판정 실패 시 `...JudgeFalse/Fail` 오브젝트의 별도 대사 재생(예 `InteractionController.cs:2621~2624`). 재시도·대체 경로는 장면별로 직접 코딩됨. 규칙 차원의 보장 없음 | |

### 1-3. SAN 판정 (`Rollet.cs`, 호출처는 `DirectingManager` 16곳 + `BattleManager.cs:635`)

| 단계 | 현재 구현 | 근거 |
|---|---|---|
| SAN 체크 | `SetRollet("SAN : Check","Sanity", nowSan, id)` = **현재 SAN** 을 목표치로 일반 기능 판정(7판과 동일하게 현재 SAN 대비) | `DirectingManager.cs:215~703` |
| 성공 시 | 후속 룰렛: 변화없음 80 / SAN 회복 15 / 컨디션 상승 5 (%) | `Rollet.cs:735~747`, `:794~810` |
| 실패/대실패 시 | 후속 룰렛 3칸: SAN 감소 / 광기 발현 / 컨디션 하락. 비율은 `debuff`(0~3 단계) 와 컨디션이 Worst 인지에 따라 하드코딩: debuff0 50/30/20, 1 30/50/20, 2 20/60/20, 3 40/-/60. Worst 면 컨디션 칸이 0이 되고 광기 칸이 커짐(debuff3+Worst=SAN감소 100%) | `:610~733` |
| SAN 감소량 | 실패 룰렛이 "SAN 감소" 면 `Random.Range(0,3)==1`→10, 아니면 5 (확률 1/3 로 10). 대사별(CSV) 감소량 차등 **없음** — 모든 SAN 체크가 같은 룰. 성공/실패 감소량 구분(`0/1d3` 식)도 없음 | `:816~831` |
| 광기 발현 | debuff 0→1→2→3 단계를 올리며 단계별 효과 부여. Lv1(재산-5·HP -5·STR -10·INT -10 등 9종) `Range(1,10)`(1~9), Lv2 `Range(1,8)`(1~7, 7종), Lv3 `Range(1,10)`(1~9) 인데 case 10(살인충동 `HomicidalImpulse`)이 있음 → **도달 불가 버그**. Lv3 은 밤공포·청각상실·시야축소·도주불가·난독(수첩 사용불가)·약물공포·인지장애(맵 이동 무작위)·인지붕괴·완벽주의. 해소 수단 **없음**(영구) | `:834~1068`, 효과 표시 `SIS_UIManager` |
| SAN 하락 적용 | `BillowUIManager.SanDown(dmg)`. `mentalWeakness` 면 +5. 하한 클램프 없음 | `BillowUIManager.cs:121~136` |
| SAN 0 | `nowHP<=0 || nowSan<=0` → `endGame` 오브젝트(즉시 게임오버). 일시/무기한 광기 개념 없음 | `DirectingManager.cs:140` |
| 최대 SAN | `san` 고정. 신화 지식으로 감소하는 규칙 없음. 오컬트 기능은 있으나 SAN 상한과 무관 | |
| SAN 회복 | 진정제(Painkillers) 아이템 판정 성공 시 `SanUp(1d3*5)`(실제 `Range(1,4)*5`=5/10/15, 의도 불명) , `Rollet.cs:455~466`. 7판의 "정신분석/휴식 회복" 없음 | |

### 1-4. 전투 (`BattleManager`, `BattleRollet`)

| 항목 | 현재 구현 | 근거 |
|---|---|---|
| 구조 | 턴제. 플레이어 행동 → 대립 판정(`BattleRollet.SetBattleRollet(내 기능, 목표, subject, 적 기능…)`) → 등급 매트릭스 하드코딩 | `BattleManager.cs:580~702`, `BattleRollet.cs:145`, `:449~` |
| 대립 판정 | 양쪽 각자 d100 굴려 등급(Fumble/Failure/Success/CritSuccess) 산출 후 `subject` 별 if 매트릭스. 예: 회피는 내가 크리티컬이면 항상 성공, 둘 다 성공/실패여도 성공 등으로 "동급이면 방어측 유리". 7판의 "성공 단계 높은 쪽 승, 동급이면 기능치 높은 쪽" 규칙과 다름 | `BattleRollet.cs:402~490` |
| 도주 | `DEX / enemyCount` vs 적 DEX 대립 판정 | `BattleManager.cs:580` |
| 피해 | 각 적·행동마다 `Random.Range(a,b)` 하드코딩, 대부분 `*5`. 주석 `1D4+2`=`Range(1,5)+2`(맞음), 주석 `1D8`=`Range(1,7)+2`(틀림, 1d6+2), 주석 `1D6`=`Range(1,6)×4합`(틀림, 4d5), `Range(1,4)`는 1d3 (플레이어 피해가 대부분 이것) | `:331`, `:760~2468` |
| 방어구·피해 보너스·관통·치명타 | 없음(대성공 = 일반 성공과 다른 피해 보너스 확인 못 함, 확인 필요) | |
| HP 공식 | HP=3d6*5(15~90). 7판 HP=(CON+SIZ)/10 와 스케일이 완전히 다름. 피해에 `*5` 를 곱해 스케일을 맞춰 둠 | `setStat.cs:62` |
| 행운 | 전투에서 미사용. 대신 `DirectingManager.cs:601~605` 에서 `Range(1,luk) <= luk` 는 항상 참 → 행운 방어 판정 **무의미(버그)** | |

### 1-5. 조사·대화

| 항목 | 현재 구현 | 근거 |
|---|---|---|
| 조사 | `InteractionController`가 장소·오브젝트 클릭 → CSV 대사 재생 → 필요한 곳에서 `Intel_ObtoUI` 가 기능 판정 호출 → 성공/실패 대사 오브젝트 분기. 수사 수첩(`IntelUI`)이 키워드(인물/장소/사건)를 모음 | `Intel_ObtoUI.cs:275~317`, `KeyWordManager`(K7(6f896de)에서 제거, 대체: KeywordBook/KeywordPicker) |
| 심문·설득 | `selectionUIManager` 에서 화술/은신 등 일반 기능 판정(예 `:127` 설득, `:164` 은신). 상대 기능과의 **대립 판정 없음**(고정 난이도 = 내 기능치 그대로) | |
| 직업·배경 선택지 | 없음. 선택지는 진행 플래그·키워드·아이템 소지로만 열림 | grep |
| 대성공/대실패 연출 | 대사 분기는 성공(대성공 포함)/실패(대실패 포함) 이분. 대성공·대실패 전용 분기는 확인 못 함 | `InteractionController.cs:2617~2623` |

### 1-6. 실측으로 찾은 규칙 버그 (설계 선행 수정 후보, 개선점검표 A-4·A-8 연계)

| ID | 내용 | 근거 |
|---|---|---|
| R-1 | Fumble 이 `>=95` 고정 → 기능 90일 때 95~100(6%) 대실패. 7판은 100(기능<50이면 96~100) | `Rollet.cs:379`, `BattleRollet.cs:405` |
| R-2 | "Critical Success" 가 `<=skill/3`(실제 7판의 어려움 성공 ≈ 절반, 극단 ≈ 1/5). 7판 대성공은 01 만. 이름과 규칙이 다름 | `:383` |
| R-3 | 기능 목표치 상한 90 클램프 → 성장 도입 시 91+ 불가 | `:132~135` |
| R-4 | 광기 Lv3 case 10 도달 불가, Lv1 `Range(1,10)`/Lv2 `Range(1,8)` 상한 제외 오용 | `:837,945,1006` |
| R-5 | 컨디션 Good 이 1의 자리 보너스(비표준), 보너스 다이스는 10의 자리여야 함. 확률 표시식과 실제 굴림 불일치 가능 | `:175`, `:292~310` |
| R-6 | `DeathCheck`/`LuckInt` 항상 참(`Range(1,luk)<=luk`) | `DirectingManager.cs:601~605` |
| R-7 | 심리치료 `/2` 같은 보정이 호출처마다 즉석 계산 | `Intel_ObtoUI.cs:304` |

## 2. CoC 7판 규칙 대조

원작 근거는 7판 수호자 규칙서의 장 이름 수준. 장 번호·정확 페이지는 **확인 필요**.

| # | 규칙 | 원작(수치·근거) | 현재 | 차이 |
|---|---|---|---|---|
| 1 | 성공 단계 | 보통(<=기능), 어려움(<=1/2), 극단(<=1/5). 대성공 01, 대실패 100(기능<50이면 96~100). 규칙서 "Game System: Skill rolls / Degrees of success" | 보통/"Critical"(=1/3)/Fumble(>=95) | 단계 3개가 아니라 기준이 다름(R-1,R-2) |
| 2 | 기능 성장 | 세션 중 성공한 기능에 체크, 세션 끝에 d100 > 기능치(또는 >95)이면 +1d10, 성공 시 체크 해제. 규칙서 "Improvement Phase / Development Phase" | 없음 | 완전 부재. 캐릭터 단일 캠페인이라 "챕터 끝 성장" 으로 대체 가능 |
| 3 | 밀어붙이기 | 실패한 판정을 재굴림, 실패하면 수호자가 정한 심각한 결과. 전투·SAN·행운 판정엔 불가. 규칙서 "Pushing the Roll" | 없음 | 부재 |
| 4 | 행운 소모 | 판정 후 행운을 1:1 로 써 결과 수치를 낮춰 성공. 행운 판정·SAN·피해엔 불가, 행운은 회복 안 됨(별도 회복 판정). 규칙서 "Spending Luck" | LUK 는 자원이 아니라 이벤트 판정용 값(`Intel_ObtoUI.cs:287`, `TimeManagere.cs:148~153`) | 자원 규칙 없음 |
| 5 | 보너스/페널티 다이스 | 10의 자리 다이스를 추가로 굴려 낮은/높은 쪽 선택. 최대 2개. 규칙서 "Bonus and Penalty Dice" | 컨디션 5단계(비표준, 1의 자리 변종 포함) | 구조적으로 유사. 컨디션을 "스토리 지속 보정", 판정 단위는 별도로 분리하는 편이 7판과 호환 |
| 6 | 대립 판정 | 양측 굴려 성공 단계가 높은 쪽 승, 동급이면 기능치(또는 DEX) 높은 쪽. 규칙서 "Opposed Rolls" | 전투만 하드코딩 매트릭스, 심문은 없음 | 매트릭스 교체 필요 |
| 7 | SAN 판정 | `SAN손실 "성공/실패"` 표기(예 `0/1d3`). 성공 시 앞값, 실패 시 뒷값만큼 SAN 감소. 규칙서 "Sanity: Sanity Rolls" | 성공 시 소량 회복 가능, 실패 시 3칸 룰렛(5 또는 10, 광기, 컨디션) | 손실량이 장면 데이터가 아님. 광기·컨디션 룰렛은 고유 설계(유지 여부 미결) |
| 8 | 일시 광기 | 한 번에 5 이상 SAN 손실 + 지능(INT) 판정 실패 → 일시 광기. 광기 발작 1d10 라운드(전투) 또는 1d10 시간(요약). 규칙서 "Sanity: Temporary Insanity / Bout of Madness" | 영구 디버프(`debuff` 단계) 를 광기와 동일시. 일시 개념 없음 | 영구 디버프를 "무기한 광기" 로 두고 "일시 광기 발작" 을 새로 추가하는 구조가 자연스러움 |
| 9 | 무기한 광기 | 하루(게임 내) 동안 현재 SAN 의 1/5 이상 손실 시 무기한 광기. 몇 달간 지속(치료 판정). 규칙서 "Indefinite Insanity" | 광기 발현 룰렛(확률) 로만 진입. 하루 누적 규칙 없음 | 하루 누적 SAN 손실 추적 필요(게임 내 `timeCount`/낮밤 존재 `TimeManagere`) |
| 10 | 광기 발작 표 | 1d10 표(기억상실·도주·폭력 등). 규칙서 "Bouts of Madness" 표(확인 필요: 발작 표 번호) | 디버프 효과 표(Lv1 9종·Lv2 7종·Lv3 9종) | 효과 표는 이미 있으나 "일시 vs 무기한" 구분 없음 |
| 11 | 신화 지식 | 크툴루 신화 기능이 오르면 최대 SAN = 99 - 신화. 신화 조우 첫 SAN 체크 시 신화 기능 획득. 규칙서 "Cthulhu Mythos skill" | 없음(`occult` 기능은 일반 기능) | 최대 SAN 개념 있음(`san`) → 감소 로직 추가 가능 |
| 12 | 단서 놓침 | 핵심 단서(core clue)는 판정 없이 제공하거나 실패해도 다른 경로로. 실패하면 '대가'를 치르고 진행. 수호자 장 "Clues / Failure" (확인 필요: 장 번호) | 실패 → 실패 대사. 핵심 단서 실패 시 대체 경로 여부는 장면마다 다름(미확인) | 규칙으로 보장 안 됨. 핵심 단서 판정 목록 전수 조사 필요 |
| 13 | 직업·배경 | 직업별 기능 포인트(EDU*2 + 직업 특성치*2 등)·신용도·배경(Backstory). 대화에서 직업이 해결 수단이 됨. 규칙서 "Creating Investigators" | 직업 없음, 주인공 고정(탐정) | 기능 + 배경 태그로 선택지 열기 가능 |
| 14 | 행운 회복·소모 | 판정에 행운 소모, 세션마다 회복 판정(d100 > 행운이면 2d10 회복). | 해당 없음 | 4번과 함께 |
| 15 | HP·피해 | HP=(CON+SIZ)/10. 피해 d3/d4/d6/d8 등, 대성공 시 최대+, 극단 성공 시 관통. | 위 1-4 | 스케일 방식 고유. 주사위식만 정정 |

## 3. 항목별 효과·비용·우선순위

크기 S(소규모)/M/L. 스토리 영향: 없음/소(대사 몇 줄)/중(장면 분기 추가)/대(각본 수정).

| # | 항목 | 효과(체감) | 구현 비용 | 스토리 영향 | 우선 |
|---|---|---|---|---|---|
| 1 | 성공 단계 7판화(보통/어려움/극단/대성공/대실패 + 버그 R-1·R-2·R-3) | 룰 일치·판정 신뢰 | M (핵심 enum + 문자열 `"Result : ..."` 호환 어댑터) | 소: 문자열 호환 유지하면 기존 분기 영향 없음 | P0 |
| 2 | 보너스/페널티 다이스 정정(R-5) | 룰 일치 | S | 없음 | P0 |
| 3 | 규칙 버그 수정(R-4, R-6, 피해식 A-4) | 정확성 | S | 없음 | P0 |
| 4 | 행운 소모 | 실패 구제, 선택의 긴장. 플레이어 만족 큼 | M (LUK 자원화, UI 버튼, 세이브 필드) | 소 | P1 |
| 5 | 밀어붙이기 | 서스펜스·"재시도 가능" 불만 해소 | M (대가 연출을 장면별로 정의: 실패 대사 확장) | 중: 밀어붙이기 대가 대사 판정마다 필요 | P1 |
| 6 | 기능 성장(챕터/일차 끝) | 진행감, 재플레이 가치 | M (성공 체크 저장 + 성장 연출 UI) | 소(성장 연출 대사) | P1 |
| 7 | SAN 손실 데이터화(`성공/실패` 값 CSV·코드 지정) + 일시/무기한 광기 구분, 하루 누적 1/5 규칙, 광기 발작 | 공포감 균형 핵심, 7판 광기 정체성 | L (SAN 룰렛 재설계, 광기 상태 모델, 해소 수단 설계) | 대: 모든 SAN 체크 장면(16곳+전투)의 손실값 결정, 광기 중 대사 변형 | P1 |
| 8 | 최대 SAN·신화 지식 | 크툴루 분위기 강화, 정보 획득의 대가 | M (신화 점수 + 상한) | 중: 금서·크리처 조우 장면에 신화 +n 지정 | P2 |
| 9 | 단서 놓침 대체 경로 | 막힘 해소(체감 큼) | M (핵심 단서 목록 + 실패 시 대체 정보 제공 규칙) | 대: 핵심 판정 N곳 각본 점검 | P1 |
| 10 | 직업·배경이 선택지를 여는 구조 | 재플레이 가치, 롤플레이 | L (생성 화면 선택, 태그 게이트, 대사 분기) | 대 | P2 |
| 11 | 대립 판정(심문·설득) | 심문 긴장감 | M (판정 룰 순수 C# 에 있으면 UI 연결만. 상대 기능치 데이터 필요) | 중: 상대별 화술/심리학 수치, 대사 | P2 |
| 12 | 전투 대립 매트릭스 7판화 | 규칙 일치 | M (매트릭스 → 단계 비교로 교체) | 없음 | P1 |
| 13 | 판정 단위 컨디션 vs 지속 컨디션 분리 | 규칙 명확성 | S (트랙 C 에서 분리하면 자연 달성) | 없음 | P2 |

### 추천 구현 순서
1. P0 묶음(1~3): 트랙 C 순수 C# 코어에 `SkillCheck` 와 단계 계산, 컨디션→보너스/페널티 변환, 호환 어댑터(`"Result : X"` 문자열), EditMode 테스트 분포 검증. 스토리 변경 0.
2. 12(전투 대립) → 4(행운 소모) → 5(밀어붙이기): 코어 위에 얹는 UI 작업. 4·5 는 같은 "판정 후 선택" UI 를 쓰므로 한 번에.
3. 6(기능 성장): 4·5 가 성공 체크 데이터를 쓰므로 그 뒤. 세이브 설계(큐 B-1)와 같이.
4. 9(단서 놓침 점검): 각본 점검이라 코드와 병행 가능. 코드는 "핵심 단서" 플래그와 실패 시 대체 대사 연결.
5. 7(SAN·광기 재설계): 가장 크다. 설계 문서 별도 작성 후(광기 영구 디버프의 해소 수단·일시 광기 연출) 진행.
6. P2(8, 10, 11, 13): 보류, 각본 영향이 큼.

## 4. 순수 C# 인터페이스 초안 (시그니처 수준)

위치 제안: `Assets/Script/Rules/` (어셈블리 `Scow.Rules`, UnityEngine 참조 없음). 난수는 주입(테스트 가능).

```csharp
namespace Scow.Rules
{
    public interface IDice
    {
        int D(int sides);          // 1..sides 포함
        int D100();                // 1..100
        int Tens();                // 0,10,..,90 (10의 자리 다이스)
        int Units();               // 0..9
    }

    public enum SuccessLevel { Fumble, Failure, Regular, Hard, Extreme, Critical }

    public enum DiceBonus { None = 0, Bonus1 = 1, Bonus2 = 2, Penalty1 = -1, Penalty2 = -2 }

    public readonly struct RollResult
    {
        public int Value;          // 최종 d100 결과(1~100)
        public int Target;         // 사용된 목표치
        public SuccessLevel Level;
        public bool Success => Level >= SuccessLevel.Regular;
        public string LegacyString { get; } // "Result : Success" 등 기존 InteractionController 호환
    }

    public static class SkillCheck
    {
        public static SuccessLevel Evaluate(int value, int target);          // 규칙 1 (Fumble: 100, target<50이면 96~100)
        public static RollResult Roll(IDice dice, int target, DiceBonus bonus);
        public static RollResult Push(IDice dice, int target, DiceBonus bonus, RollResult first); // 밀어붙이기 재굴림
        public static RollResult SpendLuck(RollResult r, ref int luck, int spend); // 행운 소모, 1:1 낮춤
        public static float Probability(int target, DiceBonus bonus, SuccessLevel atLeast); // UI 표시용
    }

    public static class OpposedCheck
    {
        // 승자: >0 A승, <0 B승, 0 무승부. 동급이면 targetA/targetB 비교
        public static int Resolve(RollResult a, RollResult b, int targetA, int targetB);
    }

    public sealed class SanityRule
    {
        public SanityOutcome Check(IDice dice, SanityState state, SanityLoss loss, RollContext ctx);
    }
    public readonly struct SanityLoss { public string OnSuccess; public string OnFailure; } // "0", "1d3", "1d6+1"
    public sealed class SanityState
    {
        public int Current; public int Max; public int MythosSkill; public int LostToday;
        public int EffectiveMax => 99 - MythosSkill;          // 규칙 11 (최대 SAN 은 기존 san 과 min)
    }
    public enum InsanityKind { None, Temporary, Indefinite }
    public readonly struct SanityOutcome { public int Loss; public InsanityKind Insanity; public int BoutDuration; }

    public interface ISkillGrowth
    {
        void MarkSuccess(string skillId);                       // 성공 시 체크
        IReadOnlyList<GrowthResult> Resolve(IDice dice, IDictionary<string,int> skills); // 세션/일차 끝
    }
}
```

연결점(어댑터, 구현은 트랙 C 이후):
- `Rollet.SetRollet`/`BattleRollet` 는 위 `SkillCheck.Roll` 만 호출하고 연출(UI·사운드)만 담당. `EndRollet` 의 `"Result : ..."` 문자열은 `RollResult.LegacyString` 로 유지해 `InteractionController.RetrunDialogResult` 71곳을 건드리지 않는다.
- `DataBaseManager.condition` 문자열 ↔ `DiceBonus` 변환 함수 하나만 두고 전역 문자열은 점차 제거(Nomal/Good/Best/Bad/Worst → 0/+1(1의 자리? 7판은 10의 자리라 Good 의미 재정의 필요, 미결 Q-C3)).
- 세이브(ES3): 성공 체크 목록, 행운 사용분, 오늘 SAN 손실·광기 상태는 새 키로 **추가**만.

## 5. 함정
- 대성공 이름 변경 시 `"Result : Critical Success"` 문자열이 `InteractionController`·`BattleManager`·`Rollet.EndRollet` 곳곳의 비교에 쓰임(예 `BattleManager.cs:2387` 등 다수). 어댑터로 문자열은 그대로 두고 내부 enum 만 확장.
- Fumble 규칙 변경은 플레이 밸런스를 바꾼다(기능 90일 때 6%→1%). 대실패 이벤트가 사실상 안 나오는지 확인 필요.
- 기능 90 클램프 제거 시 보너스 다이스 확률표(`succesProbability`)와 UI 오버플로.
- `Range(1,N)` 상한 제외: 모든 주사위를 `IDice.D(n)` 로 통일하면서 기존 `Range(1,4)*5`(1d3) 를 1d4 로 정정하면 플레이어 피해 기댓값 상향(2→2.5, x5 스케일). 밸런스 확인 필요.
- SAN 룰렛 UI 는 문자열(`sanName.text == "!Sanity check failed!"`)로 분기한다(`Rollet.cs:762`, `:794`). 리팩터 때 텍스트 비교 의존 제거.
- 광기 Lv3 효과(난독=수첩 불가, 인지장애=맵 이동 무작위, 밤공포)는 진행 막힘 소지. 해소 수단이 없으면 일시/무기한 구분 도입 시 해소 규칙을 같이 설계.
- 일부 비 UTF-8 파일 수정 시 인코딩 변환 합의 필요(개선점검표 미결 M5).

## 6. 검증 시나리오 (프로브)
1. EditMode 테스트(`Scow.Rules`): 시드 고정 `IDice` 로 `Evaluate` 경계값 — target 40: 100→Fumble, 96→Fumble, 41→Failure, 40→Regular, 20→Hard, 8→Extreme, 1→Critical. target 60: 96~99 는 Failure, 100 만 Fumble.
2. 분포 테스트: `Roll` 을 100000회 돌려 보너스/페널티 1개·2개의 성공 확률이 7판 표(target 50 / 보너스 1개 ≈ 0.7? 확인 필요, 계산식으로 도출)와 ±1% 일치.
3. 호환: `LegacyString` 이 기존 분기 문자열 4종과 같은지 단위 테스트, 그리고 플레이 모드에서 `Newspaper : Read` 등 기존 판정 1건 성공/실패 대사 재생 확인.
4. SAN: `SanityRule.Check` 로 `0/1d3` 1000회 — 성공 시 손실 0, 실패 시 1~3, 하루 누적 1/5 넘으면 `Indefinite`, 한 번에 5+ & INT 실패면 `Temporary`.
5. 행운 소모/밀어붙이기: 소모 후 값이 `target` 이하가 되는지, 행운 부족 시 거부, SAN·행운·전투 판정엔 푸시 불가.
6. 전투: 대립 판정 매트릭스 교체 후 DeepOne 전투 1회 완주, 데미지 로그로 최소·최대 확인.
7. 플레이 모드 스크린샷: 판정 UI 에 "어려움/극단" 표기, 행운 소모 버튼 노출.

## 미결 (임시 가정)

- Q-C1 7판 규칙을 얼마나 충실히 따를지(고유 룰렛·컨디션 5단계 유지 여부) — 가정: 판정 단계·대실패·보너스 다이스는 7판화, SAN 후속 룰렛·광기 디버프·컨디션 표시는 고유 설계로 유지하되 일시/무기한 구분만 추가.
- Q-C2 출시판 세이브 호환(세이브가 없으므로 문제 없음, `GameOverview` 4-1) — 가정: 세이브 키는 이번 규칙 도입 시 새로 추가.
- Q-C3 `condition` Good/Bad 를 7판 보너스 다이스(10의 자리)로 통일할지, 1의 자리 변종 유지할지 — 가정: 통일(Good=보너스1개, Best=보너스2개, Bad/Worst=페널티).
- Q-C4 대실패 기준을 7판식(100 / 96~100)으로 바꿀지 — 가정: 7판식 적용(현재 6% → 1~5%로 하락, 난이도 완화).
- Q-C5 밀어붙이기 대가의 종류(HP 손실·SAN 손실·적 선공 등) — 가정: 판정마다 정의가 필요해 P1 이후, 기본값은 "실패 대사 + SAN -d3".
- Q-C6 일시 광기 발작 연출(조작 불가 몇 턴 등) — 가정: 1d10 라운드 전투/1d10 시간 대신 "하루 단위 상태 + 대사 변형" 으로 단순화.
- Q-C7 직업·배경 시스템 도입 여부 — 가정: 보류(P2), 주인공 고정이면 필요도 낮음.
- Q-C8 규칙서 장 번호·표 번호 — 가정: 본 문서의 장 이름은 기억 기반이라 구현 전 규칙서 원문 대조 필요.
