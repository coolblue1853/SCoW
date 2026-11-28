# 신체 손상·절단 시스템 설계 (LIMB-1)

사용자 요청(11-27): 신체 절단 시스템으로 이목을 끌 것. 결손에 따라 그래픽도 달라져야 한다.
범위: 기획·설계만(코드 없음). 수치는 전부 **초안**이며 플레이테스트 조정 대상(미검증).

## 0-0. 11-28 사용자 결정: 범위 축소 (아래 절보다 우선)

픽셀렙 세션 경유로 받은 사용자 결정이다. 1~6절의 머리·몸통 관련 표와 레이어 분리 내용은 **이 절이 대체**한다. 세부 표 재작성은 L0 착수 전에 한다(미착수).

| 항목 | 11-28 결정 |
|---|---|
| 결손 대상 | **양손·양팔·양다리 + 얼굴은 눈·혀만**. 머리·몸통 결손은 뺀다 |
| 부위(임시 가정) | 9칸: `HandR, HandL, ArmR, ArmL, LegR, LegL, EyeR, EyeL, Tongue`. 손 = 손목 아래, 팔 = 팔꿈치 위. 팔 Lost 면 같은 쪽 손도 Lost 로 본다 (Q-LIMB-9) |
| 혀 | 혀 결손 = **대화가 온전히 진행되지 못하는 패널티**. 구현은 나중(후순위). 단계는 Hurt·Lost 두 개만 쓴다는 임시 가정 (Q-LIMB-10) |
| 머리·몸통 | 부위 굴림에서 제거. 중상은 기존 HP 피해만 남는다. 2-2 의 머리·몸통 보정, 2-3 의 머리·몸통 행동 제한, `MaxHpPenalty`, "Torso 로 고정" 재굴림 규칙은 폐기 대상 |
| 그래픽 방식 | **레이어 분리 비추천**(Project B 실패 이력). **통짜 스프라이트 + 스티커 덮기**(붕대·빈 소매·안대 등)를 추천. **사용자 확정 대기** (Q-LIMB-8) |
| 파비안 기준 그림 | `Docs/ArtCandidates/Fabian/noline/1_lineonly.png`(외곽선 없음, 54px). 최종 키는 사용자 미정 |

통짜 + 스티커 방식의 뜻:
- 파비안 스프라이트는 한 장(통짜) 그대로 쓴다. 팔·다리를 레이어로 끄지 않는다.
- 결손은 그 위에 스티커를 덮어 표시한다. 절단은 "없앤 팔"이 아니라 빈 소매·묶은 바지단·그루터기 붕대 스티커로 원래 부위를 가린다.
- 원래 팔이 스티커 밖으로 비칠 수 있다. 그래서 스티커는 팔꿈치 아래·무릎 아래·손목 아래만 덮는 크기로 만들고, 모션 프레임별 앵커 표(데이터)로 따라가게 한다.

## 0. 결정 요약

| 항목 | 결정 |
|---|---|
| 참고 범위 | "부위 손상이 능력·전투·탐색에 영향"이라는 장르 메커니즘만 참고한다. 부위 목록·수치·이름·연출은 SCoW 고유(CoC 7판 중상·응급처치·영구 부상 + 1921년 아캄 탐정 호러) |
| 부위 | 8칸: 머리, 몸통, 오른팔, 왼팔, 오른다리, 왼다리, 오른눈, 왼눈 (1절) |
| 단계 | 0 멀쩡 / 1 부상 / 2 불구 / 3 절단. 머리·몸통은 절단 없음(상한 불구), 눈은 한쪽만 절단 가능 |
| 발생 | **중상**(한 번에 MaxHp 절반 이상 피해 = 7판 Major Wound)과 적 대성공 피격에서만 부위를 굴린다. 평범한 전투는 부위에 무관 |
| 대상 | 파비안은 8칸 전체. 동료(알버트·메이브)는 같은 모델에서 **절단 없음**(불구 상한)·그래픽 공통 붕대 1종 |
| 적 부위 공격 | **넣는다**(후순위 L8). 조준 공격 = 어려운 판정, 머리·팔·다리 3종 (3절) |
| 난이도 | 새 게임 기본 "표준"(절단 포함). "순화"(절단 없음)·"끔"(기록 없음)을 새 게임·설정에서 선택. 구세이브는 키가 없으므로 **끔 = 결손 없음**으로 열린다 (4절) |
| 그래픽 | 몸통 기본 + **부위 오버레이 스티커 + 앵커 표**. 조합 수천 가지를 **정적 오버레이 약 75장**으로 줄인다 (5절). 파비안 새 도트 제작 중이므로 **레이어 분리 규격을 지금 정해야 한다** |

연결 문서: `CocDesign.md`(판정·SAN), `Party.md`(3인 전투·쓰러짐 4-7·세이브 6), `Architecture.md`(2-2 GameState·2-3 저장), `BattleFxProfile.md`(피격 연출), `ArtStyle.md`(6배 격자·비율), `Docs/GraphicsNeeds.md`.

## 1. 부위 모델

### 1-1. 부위와 단계

```
enum LimbPart  { Head, Torso, ArmR, ArmL, LegR, LegL, EyeR, EyeL }
enum LimbState { Sound = 0, Hurt = 1, Crippled = 2, Lost = 3 }
```

| 단계 | 의미 | 되돌림 | 표시 |
|---|---|---|---|
| Sound | 멀쩡 | - | - |
| Hurt 부상 | 타박·열상·골절 의심. 기능 소폭 하락 | 시간 경과로 자연 회복(6절) | 붕대 |
| Crippled 불구 | 부러짐·깊은 열상·시력 손상. 기능 크게 하락 | 응급처치로 악화 정지, 병원 치료로 Hurt. **방치하면 영구 고착(Perm)** | 두꺼운 붕대·팔걸이 |
| Lost 절단 | 부위 상실 | **되돌릴 수 없음**. 의수·의족·안대로 보완만 | 빈 소매·그루터기 |

단계 상한(부위별):

| 부위 | 상한 | 이유 |
|---|---|---|
| Head | Crippled | 머리 절단은 곧 사망이라 이 게임의 쓰러짐(`Downed`) 규칙으로 처리(2-3) |
| Torso | Crippled | 같은 이유 |
| ArmR/ArmL/LegR/LegL | Lost | |
| EyeR/EyeL | Lost, 단 **둘 다 Lost 불가**(남은 눈은 Crippled 상한) | 완전 실명은 판정·그래픽 폭발과 막힘 위험. 이유는 4절 Q-LIMB-3 |

### 1-2. 원인

| 원인 | 조건 | 결과 |
|---|---|---|
| 적 공격 중상 | 한 번의 피격 피해 >= `ceil(MaxHp / 2)` (7판 중상). HP 스케일이 x5 라 파비안 평균 MaxHp 52 → 임계 26. 실측상 **심해인 반격(최대 30)만** 임계를 넘고 혼혈(10~15)은 넘지 못한다 → 절단은 드물다 | 부위 1곳을 굴려 **+1 단계**(2-1) |
| 적 대성공 피격 | 적 판정이 대성공(현행 `Critical Success`, CocDesign R-2 수정 뒤에는 Extreme)이고 피해는 임계 미만 | 부위 굴림, 단 **Hurt 까지만**(Sound→Hurt) |
| 함정 | 하수도 철창·톱니·낙석 등 `LimbTrap` 데이터(부위·단계·회피 판정). L7 | 판정 실패 시 지정 부위 +1~2 단계 |
| 이벤트 | 스토리 장면의 `limb.step` 효과(대사 데이터 onEnter) | 지정 부위 +n 단계. 필수 진행 장면에는 쓰지 않음 |
| 광기 자해 | 광기 단계 3 발현 시 낮은 확률의 `SelfHarm`(HP -5) | 무작위 팔·다리 **Hurt 한정**(절단 금지, 톤은 암시) |
| 쓰러진 자를 또 맞힘 | `Party.md` 4-7 | 즉시 Fallen, 부위 굴림 없음 |

피격 부위 가중치(`EnemyDef.LimbWeights`, 합 100, 공격 종류별 덮어쓰기. 기본값은 아래):

| 공격 | Head | Torso | ArmR | ArmL | LegR | LegL | EyeR | EyeL |
|---|---:|---:|---:|---:|---:|---:|---:|---:|
| 기본 | 10 | 30 | 14 | 14 | 12 | 12 | 4 | 4 |
| 찢기(손톱·물어뜯기) | 8 | 20 | 20 | 20 | 12 | 12 | 4 | 4 |
| 던진 돌 | 25 | 25 | 10 | 10 | 5 | 5 | 10 | 10 |

굴림 결과가 이미 상한이거나(Lost 팔 재차 지정 등) 불가능하면 **최대 3회 다시 굴리고**, 그래도 안 되면 Torso 로 고정한다(2-3).

## 2. 규칙

### 2-1. 단계 진행

| 현재 → 중상 | 결과 |
|---|---|
| Sound → | Hurt |
| Hurt → | Crippled |
| Crippled → (팔·다리·눈) | Lost (표준). **순화 모드는 Crippled 유지 + 해당 전투 종료까지 해당 행동 불가 1턴** |
| Crippled → (머리·몸통) | **Downed 카운트 1 로 즉시 전환**(`Party.md` 4-7, 솔로는 기존대로 HP<=0 게임오버 규칙과 동일 취급) |

- Lost 직후 `Bleeding`: 전투 중 해당 행위자의 차례 시작마다 HP -5. 응급처치(의술) 성공 또는 **전투 종료로 자동 지혈**. 동료가 파비안의 지혈을 도울 근거가 생긴다(`Party.md` 5-4).
- 모드별 차이: 끔 = 부위 기록 없음(HP 피해만), 표준 = 위 표 그대로, 순화 = Lost 불가.

### 2-2. 능력치·기능 보정

보정은 **목표치(기능 포인트)에 더하는 정수**다(`SkillMod`). 하한 5, 상한은 기존 90 클램프(CocDesign R-3 수정 전). 보너스·페널티 다이스(미결 Q-C3)에 의존하지 않는다.

| 부위 | 영향 기능 | Hurt | Crippled | Lost | 계산 |
|---|---|---:|---:|---:|---|
| 팔(R·L) | 손재주, 맨손, 근접, 사격, 던지기, 응급처치(의술), 완력(Str) | -10 | -25 | -40 | 한 손 행동: **덜 다친 팔** 기준. 다른 팔도 Crippled 이상이면 추가 -10. 완력·양손 행동: 더 다친 팔 기준 |
| 다리(R·L) | 회피, 수영, 은신, 도주 | -5 | -15 | -30 | 두 다리 **합산**, 하한 -50. 보조 소품이 있으면 합산을 절반으로 |
| 눈(R·L) | 관찰, 사격 | -5 | -10 | -20 | 두 눈 합산(최대 -30, 둘 다 Lost 불가) |
| 머리 | 관찰, 화술, 분석 | -5 | -20 | - | |
| 몸통 | 건강(Health), 완력 | -5 | -15 | - | + `MaxHp` -10 / -25 (현재 HP 도 새 최대치로 깎음) |

기능 ID 는 builder 가 `Rollet.cs` 목표치 계산부에서 실제 문자열을 확인해 표에 대응시킨다(`Observation`·`Rhetoric`·`Swin`·`Deftness`·`Evasion`·`Medicine`·`Str`·`Health` 등. 추측 금지).
적용 위치는 **한 곳**: 목표치 산출 직후 `LimbRules.SkillMod(set, skillId)` 를 더한다. 호출처마다 즉석 보정하지 않는다(CocDesign R-7 재발 방지).

### 2-3. 전투 행동 제한

| 상태 | 제한 |
|---|---|
| 무기 손 | 무기 `Hands`(데이터 추가): 한 손 = 단검·권총·리볼버·던지기(돌·화염병). 양손 = 소총·산탄·도끼·배트. **성한(Hurt 이하) 팔이 하나뿐이면 양손 무기 불가**. 팔 하나 Crippled 이상이면 재장전 2턴. 두 팔 모두 Crippled 이상이면 **무기·던지기·응급처치 불가**(가능한 행동은 Guard·Swap·Run·Calm 뿐). 갈고리 의수는 한 손 무기는 못 쥐나 양손 무기 **받침**으로 인정(양손 무기 허용, -10 추가) |
| 다리 | `SpeedFactor`(두 다리 곱): 한 다리 Hurt 0.95, Crippled 0.85, Lost 0.70, 하한 0.50. 보조 소품이 감소분의 절반을 돌려줌. **전투 게이지 속도**에는 감소분의 절반만 적용(`1 - (1 - f) / 2`, `Party.md` 4-4 의 `StepGauge`). 한 다리 Lost + 보조 소품 없음: **교환(Swap)·도주 불가**. 도주 판정은 `DEX x SpeedFactor / 적 수` |
| 눈 | 한쪽 Lost: 2-2 의 사격·관찰 보정만. 시선 SAN 공격(`SanAttack`) 피해는 **그대로**(눈이 하나 없어도 시선은 닿음, 톤). 대상 제한 없음 |
| 머리 | Crippled: 첫 차례 게이지 지연(시작 게이지 -30) |
| 몸통 | Crippled: 앞 칸(근접 위치) 배치 시 첫 피격 피해 +5(몸이 약해 보임, 선택적, 초안) |
| 솔로 | 같은 규칙. 솔로 전투는 기존대로 HP<=0 즉시 게임오버(Q17) |

PT 3인 전투: `BattleField.ValidTargets`/`Resolve` 에서 `LimbRules.CanUse(set, ActionKind, weaponHands)` 를 질의해 행동 메뉴를 비활성화(회색+사유 한 줄). 동료도 같은 규칙. 동료는 Lost 불가라 "양팔 Crippled" 이 사실상 최악.

### 2-4. 탐색·대화·월드

| 영역 | 규칙 |
|---|---|
| 월드 이동 | 이동 속도 x `SpeedFactor`(하한 0.5). 하수도·지도 이동 시간(타임카운트)은 **바꾸지 않는다**(시간 압박 규칙 불변) |
| 하수도 시야 | 눈 한쪽 Lost 시 시야 마스크 반경 -10%(S3 `SewerVision`, 눈 Crippled 도 -5%). 구현은 SEWER-1 S3 이 있을 때 연결 |
| 탐색 판정 | 2-2 의 SkillMod. 동료 도움·대신 판정(`Party.md` 3절)은 동료 자신의 부상 보정을 받음 |
| 선택지 | 몇몇 대사 선택지에 `requires: limb.armFree >= 1` 같은 조건 키로 "한 손이 필요한" 행동(사다리·자물쇠·상자 열기)을 막는다. **필수 진행 경로는 막지 않는다**: 각 장면에 결손 없이도 열리는 대체 경로(동료·시간·다른 단서)를 둔다 |
| 대화 | 얼굴 손상(머리 Crippled, 눈 Lost)과 눈에 띄는 절단은 NPC 첫 대면 대사 한 줄 반응(`Limb_*` 대사 데이터, 지어내지 않고 사용자/스토리 담당이 작성) + 화술 -5 한정. 의수·안대는 반응이 줄어듦 |
| 엔딩 | 에필로그에 결손 한 줄(`Limb_Epilogue_*`). 결말 분기는 바꾸지 않는다 |

### 2-5. SAN 충격

| 사건 | 판정 | 성공 | 실패 |
|---|---|---:|---:|
| 불구(Crippled)가 됨 | SAN 판정 | 0 | 5 |
| 절단(Lost)이 됨 | SAN 판정 | 5 | 10 |
| 동료가 불구·절단되는 것을 봄 | 파비안 SAN 판정(개별) | 0 | 5 |
| 자신의 자해 | 광기 규칙 | 광기 자체의 손실 | |

- SAN 판정은 기존 `Rollet` SAN 체크 재사용(현재 SAN 목표). 손실량은 위 표 값을 쓰는 새 호출(`SanityLoss` 형태, `CocDesign.md` 4절)로 쓰되, 연결 전까지는 기존 3칸 룰렛(SAN 감소/광기/컨디션)을 그대로 쓰고 손실량만 넘긴다.
- 한 번에 10 이상 손실 + INT 실패 → 전투 광기 `BoutKind`(`Party.md` 4-6)가 이미 있으므로 **절단 실패가 곧 발작 조건**이 된다. 별도 규칙 없음.
- 같은 부위의 두 번째 충격에는 SAN 판정 없음(적응). 새 부위만 판정.

### 2-6. 시간·치료·보조 소품 (1921년 소품)

| 항목 | 규칙 |
|---|---|
| 자연 회복 | Hurt 는 **4 타임카운트**(1일) 지나면 Sound. 비밀기지 휴식이 생기면(Q-S2) 휴식 1회 = 2 타임 단축 |
| 응급처치 | 의술 판정(`Party.md` 5-4). 성공: Hurt 회복 시간 절반, **Crippled 의 `Untreated` 해제**(고착 시한 정지), Lost 의 지혈. 붕대(소모품 `Gauze`) 1개 소비. 붕대 없으면 판정 -20 |
| 고착(Perm) | Crippled 가 `Untreated` 로 **8 타임카운트** 지나면 영구 불구. 병원이 못 고치고, 의수·지팡이로만 보완 |
| 병원 치료 | 병원(Kate, 낮에만): 시간 1 소모 + 치료비. 성공 판정 없이 Crippled → Hurt(1회에 한 부위). 비용은 현행 "재산" 개념이 있으면 사용(Q-LIMB-5) |
| 절단 | 치료 불가. 병원에서 그루터기 정리를 받으면 `Bleeding` 재발 없음(이미 전투 종료로 지혈이라 의미는 연출뿐) |
| 지팡이 `Cane` | 다리 Hurt~Crippled: 합산 절반 + 이동 보정. **한 손 점유**(전투 중에는 점유 안 함) |
| 목발 `Crutch` | 다리 Lost: 교환·도주 불가 해제, `SpeedFactor` 0.70 → 0.85. 한 손 점유 → 양손 무기 불가 |
| 의족 `Peg` | 다리 Lost: 합산 -30 → -15, `SpeedFactor` 0.85, 손 점유 없음. 목발과 택일 아닌 **교체** |
| 갈고리 의수 `Hook` | 팔 Lost: -40 → -25, 양손 무기 받침(-10), 맨손·한 손 무기 불가 |
| 나무 의수 `WoodHand` | 팔 Lost: -40 → -35, 외관 보정(대화 화술 -5 면제). 전투 효과 거의 없음 |
| 안대 `Patch` | 눈 Lost: 능력 변화 없음, 대화 한 줄 반응 감소. 순수 외형 |
| 획득 | 기존 상점 UI 재사용. 임시 가정: 병원 = 붕대·지팡이·목발, 총포상(또는 잡화) = 갈고리·나무 의수·의족·안대. 단 4일짜리 게임이라 **절단 후 의수 구입은 1회 방문**으로 끝나게 한다(Q-LIMB-5) |

- 되돌릴 수 없는 것: 절단(Lost), 영구 불구(Perm), MaxHp 감소 중 Perm 부위분. 회복되는 것: 부상, 방치 전의 불구.
- 소품 ID 는 새 아이템·I2 새 키(`Limb_*`)로 **추가만**. 기존 아이템·용어 키 불변.

### 2-7. 동료

- 동료 `PartyMember` 도 `LimbSet` 보유(키는 `party.<id>.limb.<part>`, 7절). 절단 불가(Lost→Crippled 유지), 눈은 부위 굴림에서 제외(Head 로 합산).
- 동료 Crippled: 해당 행동 -25 + **전투 후 `Recovering` 1 타임**(`Party.md` 4-7 연계). Perm 고착은 동료에게 없음(병원 치료 권장 대사만).
- 동료는 그래픽 변형이 없다: 전투 초상에 공통 붕대 오버레이 1~2종(5절)만.

## 3. 적 쪽 부위 공격 (추천: 넣는다, 후순위 L8)

| 항목 | 내용 |
|---|---|
| 추천 | **넣되 단순하게**: 새 행동 `Aimed`(조준), 부위 3종(머리·팔·다리), 모든 근접·사격·던지기에 적용 |
| 근거 | (1) `BATTLE-BAL-1`/Q-BAL1: 적 HP(혼혈 54·심해인 104)가 높아 HP 깎기만으로는 승산이 낮다. 조준은 HP 외의 수단을 준다 (2) 부상이 플레이어만의 위협이 아니라 쌍방 규칙이 되어 "싸우지 말고 피하라"는 CoC 톤을 지키면서 전술 폭을 준다 (3) 대열 전투에서 "누구를 먼저 막나"라는 판단이 생긴다 |
| 비용 | `EnemyDef.Parts`(데이터), 판정 -50%, 행동 1개 추가. 적 그래픽 변형은 **없이** 시작(상태 아이콘·피 튐 이펙트만) |
| 판정 | 기능 목표치의 **절반**(어려운 성공)을 굴려 성공하면 부위 효과, 실패해도 일반 명중 판정 한 번은 인정(실패 시 빗나감, 행동은 소모) |

| 부위 | 효과(전투 종료까지, 중첩 없음) |
|---|---|
| 머리·눈 | 적의 시선 공격(`SanAttack`) 2턴 불발, 적 명중 -10 |
| 팔 | 적 `AttackDamage` x0.7(반올림) |
| 다리 | 적 게이지 속도 x0.8, 파비안 도주 판정 +1 보너스 다이스(미결 시 +20 목표치) |

- 특수 적(보스·`SanAttack` 면역·부위 없음)은 `EnemyDef.Parts` 가 비어 있어 `Aimed` 메뉴가 숨는다.
- 끔 모드에서는 `Aimed` 도 숨김(시스템 일관).
- 순수 C#: `BattleCommand` 에 `AimPart` 필드 추가(기본 `None`). 기존 호출 불변.

## 4. 출시작 호환·난이도 옵션

| 항목 | 규칙 |
|---|---|
| 구세이브 | `GameState` 에 `limb.*` 키가 없다 = 전부 Sound·`limb.mode` 0(끔). `SchemaVersion` 1 유지, `SaveMigrator` 변경 없음(`SaveCodec` 가 새 키를 그대로 저장·복원, `Party.md` 6절과 같은 근거) |
| 새 게임 | `limb.mode = 1`(표준) 로 시작. 캐릭터 생성 화면에 3택(표준·순화·끔) + 한 줄 설명 |
| 설정 | 슬롯 무관 `settings` 가 아니라 **슬롯(GameState)별**. 진행 중에도 **더 쉬운 쪽으로만** 바꿀 수 있다(표준→순화→끔). 끔에서 켜기는 안내 팝업과 함께 허용(현재 부위는 모두 Sound 로 시작) |
| 끔의 의미 | 중상·대성공 피격 때 부위 굴림·SAN 충격·`Aimed`·결손 대사 모두 없음. 월드·전투는 출시판과 동일 |
| 되돌리기 | 모드를 낮추면 기존 Lost 는 **Crippled 로 승격 표기 없이 그대로 유지**하되 기능 보정은 Crippled 값 적용(결손 그래픽은 유지). 끔으로 내리면 모든 보정 해제·그래픽은 Sound |
| I2 | 새 키 `Limb_*` 로만 추가. 기존 용어 키 불변 |
| ES3 | 기존 ES3 키 불변(`CodeStyle.md` 3절) |

난이도 추천 근거: 출시작 유저가 이미 가진 세이브를 원치 않게 어렵게 하지 않고, 새 게임은 사용자가 원한 "이목을 끄는 요소"가 기본값으로 켜지게 한다. 영구 손실이 불편한 유저에게는 순화·끔이 있다(장르 관례: 영구 손실은 선택지가 되어야 함).

## 5. 그래픽 요구

> 11-28: 0-0절이 우선한다. 머리·몸통 오버레이(월드 4장, 초상·일러스트의 머리 붕대)는 빠진다. 손·혀 스티커가 새로 들어간다. 레이어 분리 대신 통짜 + 스티커(사용자 확정 대기). 아래 표는 11-27 원안이며, 개정 장수는 `Docs/GraphicsNeeds.md` 신체 손상 절을 따른다.

### 5-1. 조합 폭발과 해법

파비안 월드 스프라이트 기준 **풀 스프라이트 조합**: 팔 2칸x4단계 x 다리 2칸x4단계 x 눈 2칸x3(멀쩡·부상·안대) x 머리·몸통 3x3 = 4^4 x 3^2 x 9 = **20,736 조합**(x 모션 3종). 불가능하다.
이 중 게임에서 실제 도달하는 조합이 적더라도 어느 것이 나올지 모르므로 **레이어 합성**으로 간다.

| 방식 | 내용 |
|---|---|
| 몸통 기본 + 오버레이 스티커 | 몸통(머리·몸통·코트), 팔R, 팔L, 다리R, 다리L 을 **레이어로 분리 제작**. 부상·불구·절단·보조 소품은 별도 **정적 스티커**로 위에 얹거나 해당 레이어를 끈다 |
| 앵커 표 | 프레임마다 부위 앵커 좌표(손목·팔꿈치·무릎·머리·눈·가슴)를 **데이터 표(그림 아님)**로 둔다. 스티커는 앵커를 따라가므로 모션 프레임마다 스티커를 따로 그리지 않는다 |
| 절룩임·한 팔 모션 | 다리 불구는 걸음 주기 느리게 + 몸통 상하 1px 리듬(**코드**)으로 표현, 그림 추가 없음 |
| 팔레트 교체 | 피 얼룩·진흙·젖음은 코트 팔레트 램프 교체 2종(그림 추가 없음). 포인트 색 탁한 적색 `#7a1a1a`(ArtStyle 3절) |
| 좌우 | 스프라이트는 오른쪽 향함 기준 한 방향만 제작. 왼쪽은 flipX 이므로 화면 가까운 쪽·먼 쪽 팔이 R/L 에 대응이 바뀜. 앵커 표에 near/far 매핑을 둔다 |

### 5-2. 필요 변형 수

월드(파비안, 모션: 대기·걷기·달리기 3종. 프레임 수는 **미확인**, 앵커 표 행 수만 영향):

| 오버레이 | 장수 |
|---|---:|
| 팔: 부상 붕대 R/L 2, 불구 팔걸이(팔 레이어 대체) R/L 2, 절단 빈 소매 R/L 2, 갈고리 의수 R/L 2, 나무 의수 R/L 2 | 10 |
| 다리: 부상 붕대 R/L 2, 불구 보조대 R/L 2, 절단 그루터기 R/L 2, 의족 R/L 2, 지팡이 소품 1, 목발 소품 1 | 10 |
| 눈: 부상 붕대 R/L 2, 안대 R/L 2 | 4 |
| 머리: 부상 붕대, 불구(두꺼운 붕대+출혈) | 2 |
| 몸통: 부상 붕대(가슴), 불구(피 얼룩 번짐) | 2 |
| **월드 소계** | **28** |

| 화면 | 방식 | 장수 |
|---|---|---:|
| 전투 초상(`PartyBattlePanel` 칩·순서 바 64px) | 파비안: 머리 2 + 눈 4 + 목깃 피 1 + 팔걸이 어깨 띠 2 = 9. 알버트·메이브: 공통 붕대 머리 1 + 안대 1 = 각 2 (동료 눈 Lost 없음이라 안대 대신 눈 붕대) | 13 |
| 전투 전신(파비안 `battlePlayer` 자세 N 개) | 월드 오버레이 28장을 **자세별 앵커 표**로 재사용. 절단은 팔 전체가 아니라 **팔꿈치 아래·무릎 아래**만 소매·바지 스티커로 가림. 완전 레이어 분리는 하지 않음(위험: 공격 자세에서 원래 팔이 비칠 수 있음 → L6 에서 프로브) | 0(재사용), 앵커 표 N행(**자세 수 미확인**) |
| 대화 일러스트(80x80 가슴 위) | 파비안: 머리 2 + 눈 4 + 어깨 띠 2 = 8. 알버트·메이브: 붕대 머리 1 + 안대 1 + 눈 붕대 1 = 각 3. NPC 10명은 대상 아님 | 14 |
| HUD 인체 실루엣(수첩·상태창) | 실루엣 1 + 부위 마스크 8. 단계 색은 코드 틴트 | 9 |
| 단계 아이콘(토스트·메뉴) | 4(부상·불구·절단·지혈) | 4 |
| 소품 인벤 아이콘 | 붕대·지팡이·목발·의족·갈고리·나무 의수·안대 | 7 |
| 적 부위 손상(L8, **P2**) | 적 5종 x 3(팔·다리·머리) 오버레이. 시작은 아이콘·이펙트만 | 15 |
| **합계(적 제외)** | | **75** |

- 비교: 풀 조합 20,736(x3 모션) 대비 약 75장 + 앵커 표. 
- 파비안 원본 도트 확정 전에 **레이어 분리 규격**을 못 박아야 한다. 현재 시안 C·D 진행 중(`GraphicsNeeds.md`). 몸통·팔R·팔L·다리R·다리L 5레이어로 내보내게 하고, 다리 불구 연출을 위해 대기 모션의 몸통 흔들림이 코드와 겹치지 않는지 확인.
- 모든 새 그래픽은 `GraphicsNeeds.md` 의 "신체 손상" 절에 쌓는다. 자동 생성 금지, 사용자 검토 후 적용(`graphics-user-review`). 구현 중에는 색 사각형 임시 오버레이.

## 6. 순수 C# 엔진 인터페이스 (Core/Rules, `SCoW.Core.Rules`, UnityEngine 참조 없음)

새 파일(`BattleEngine.cs` 불변, `Party.md` 4-8 방식): `Core/Rules/Limb/LimbPart.cs`, `LimbSet.cs`, `LimbRules.cs`, `LimbDefs.cs`, 테스트 `Tests/EditMode/LimbRulesTests.cs`. 난수는 기존 `IRng`, 피해식 표기는 기존 `DiceExpr` 재사용.

```csharp
namespace SCoW.Core.Rules {
  public enum LimbPart { Head, Torso, ArmR, ArmL, LegR, LegL, EyeR, EyeL }
  public enum LimbState { Sound = 0, Hurt = 1, Crippled = 2, Lost = 3 }
  public enum LimbMode { Off = 0, Standard = 1, Mild = 2 }
  public enum LimbCause { EnemyMajor, EnemyCritical, Trap, SelfHarm, Event }
  public enum LimbAid { None, Hook, WoodHand, Peg, Patch }   // 팔·다리·눈 한 칸당 하나
  public enum SupportAid { None, Cane, Crutch }               // 지팡이·목발(손 점유)

  public sealed class LimbSet {
    public bool IsParty;                                     // true 면 Lost 불가, 눈 굴림 제외
    public LimbState Get(LimbPart p);
    public bool IsPerm(LimbPart p);                          // 고착
    public bool IsUntreated(LimbPart p);
    public int SinceTime(LimbPart p);                        // 마지막 악화 timeCount
    public LimbAid AidOf(LimbPart p); public SupportAid Support { get; set; }
    public void Set(LimbPart p, LimbState s, int timeCount); // 테스트·로드용
    public int ArmFree { get; }                              // 성한(Hurt 이하) 팔 수 0~2
    public LimbSet Clone();
  }

  public readonly struct LimbHit { public LimbPart Part; public LimbState Before, After; public bool Bleeding; public bool Rerolled; }
  public readonly struct LimbTrap { public LimbPart Part; public int Steps; public int AvoidTarget; }

  public static class LimbRules {
    // 부위를 굴릴지: damage >= ceil(maxHp/2) 또는 enemyCritical(Hurt 한정). Off 면 항상 false.
    public static bool ShouldRoll(LimbMode mode, int damage, int maxHp, bool enemyCritical);
    // 가중치 8개(합 100)로 부위 선택. 상한 불가 부위는 최대 3회 재굴림 후 Torso.
    public static LimbPart PickPart(LimbSet set, int[] weights8, IRng rng);
    // steps 단계 올림(모드·부위 상한·눈 둘 다 Lost 금지·머리/몸통 Crippled→Downed 신호 반영)
    public static LimbHit Apply(LimbSet set, LimbPart part, int steps, LimbMode mode, int timeCount, out bool escalateToDowned);
    public static int SkillMod(LimbSet set, string skillId, bool twoHanded);
    public static float SpeedFactor(LimbSet set);            // 1.0~0.5
    public static float GaugeFactor(LimbSet set);            // 1 - (1-SpeedFactor)/2
    public static int MaxHpPenalty(LimbSet set);             // 몸통
    public static bool CanUse(LimbSet set, ActionKind kind, int weaponHands); // 팔 규칙 + 교환·도주 불가
    public static (int onSuccess, int onFail) ShockLoss(LimbHit hit);        // 2-5 표. 새 부위 판정만
    public static void Tick(LimbSet set, int timeCount);     // 자연 회복·Perm 고착. 시간 경과 시 1회
    public static bool FirstAid(LimbSet set, LimbPart p, bool success, bool hasGauze);
    public static bool HospitalTreat(LimbSet set, LimbPart p, int timeCount); // Crippled→Hurt
    public static string LegacySummary(LimbSet set);         // 디버그·토스트용 한 줄
  }
}
```

연결점(어댑터, View 쪽):

| 위치 | 연결 |
|---|---|
| `PartyBattleRules`/`BattleFieldActions.cs`(적 공격 피해 적용부, 273·283줄 근처) | 피해 확정 후 `ShouldRoll → PickPart → Apply` → `BattleEvent` 신규 타입 `Limb`(부위·단계). `EnemyDef.LimbWeights`(데이터 추가, 기본값 1-2 표) |
| `BattleManager`(구 View) | 파비안 피격은 `BattleEvent.Limb` 를 받아 소리·텍스트·`PlayerHitted*` 후 부위 스티커 갱신. `BattleFxProfile.md` 의 피격 연출은 불변, 마지막에 한 줄 얹는다 |
| `Rollet.cs` 목표치 산출 | `SkillMod` 더하기 한 곳 |
| `BattleField.ValidTargets`·행동 메뉴 | `CanUse` |
| `BattleTimeline.StepGauge` | `GaugeFactor` |
| 월드 이동 속도·`SewerVision` | `SpeedFactor`, 눈 보정 |
| 시간 경과(`TimeManagere`·`TimeCount` 증가 지점) | `LimbRules.Tick` 1회 |
| 대사 데이터 조건·효과 | 조건 키 `limb.<part>.state`, `limb.armFree`, 효과 `limb.step:<part>:<n>`. 실제 onEnter 문법은 builder 가 `Architecture.md` 2-4 로 확인 |
| 인벤토리 | 소품 아이템 장착 → `LimbSet.AidOf`/`Support` |

GameState 새 키(**추가만**, 키 이름 변경 금지, `FlagKeys`/`CounterKeys` 상수화):

| 종류 | 키 | 값 |
|---|---|---|
| Counter | `limb.mode` | 0 끔 / 1 표준 / 2 순화. 없으면 0 |
| Counter | `limb.<part>` | `LimbState` 정수. `<part>` = head, torso, armr, arml, legr, legl, eyer, eyel. 없으면 0 |
| Counter | `limb.<part>.since` | 마지막 악화 `timeCount` |
| Flag | `limb.<part>.perm` | 영구 고착 |
| Flag | `limb.<part>.untreated` | 불구 방치 중 |
| Text | `limb.aid.<part>` | 소품 ID(`hook`/`woodhand`/`peg`/`patch`). 없으면 없음 |
| Text | `limb.support` | `cane`/`crutch`. 없으면 없음 |
| Counter | `party.<id>.limb.<part>` | 동료 부위(`Party.md` 6절과 같은 규약), 키 없음 = Sound |
| Flag | `limb.seen.<part>` | 해당 부위 충격(SAN) 판정을 이미 한 부위 |

- `SaveCodec` 는 `Flags`/`Counters`/`Texts` 를 일반 레코드로 기록하므로 코드 변경 없이 저장된다. 로드 후 `LimbSet` 은 `GameState` 에서 **재구성**(상태 사본을 별도로 저장하지 않음).
- 전투 중 `Bleeding`은 저장하지 않는다(전투 중 저장 금지, `BusyRules`).

## 7. 함정

- `Rollet.cs` 상한 90 클램프(R-3)와 하한 5 를 SkillMod 합산 **뒤**에 적용한다(순서가 바뀌면 Lost 보정이 무의미해짐).
- 대칭 키: R/L 은 **캐릭터 기준**. 월드에서 flip 되면 화면 쪽이 바뀐다(5-1). 앵커 near/far 매핑을 데이터 한 곳에서만 계산.
- 중상 임계는 `ceil(MaxHp/2)`. MaxHp 가 몸통 부상으로 줄면 임계도 같이 변한다 → **임계 계산에는 `Torso` 감소 전 MaxHp** 를 쓴다(자기 강화 루프 방지).
- 심해인 반격 최대 30 이 임계 경계라 `MaxHp` 가 60 이하면 항상 중상이다. 균형 판단(BATTLE-BAL-1)과 함께 본다.
- 동료 Lost 불가 때문에 파비안과 규칙이 달라진다. 도움말에 명시.
- 끔 모드 로드 시 `limb.<part>` 값이 남아 있으면 그래픽은 Sound 로, 보정은 0 으로 **View 에서 무시**(데이터는 보존: 다시 켜면 복원).
- 새 UI 문구는 I2 `Limb_*` 새 키만. 한글은 글꼴에 글리프가 있을 때만(`SaveText.IsKorean` 패턴).
- 입력은 마우스·키보드·패드 3경로(설정·치료·소품 장착 메뉴). `gamepad-always` 원칙.

## 8. 단계·검증

| 단계 | 내용 | 검증 기준(프로브) |
|---|---|---|
| L0 | 순수 엔진 `LimbSet`·`LimbRules`·`LimbDefs`·`EnemyDef.LimbWeights`·EditMode 테스트 | `ShouldRoll` 경계(임계 -1/임계/임계+1, 모드 3종), `PickPart` 시드 고정 분포(1만 회에서 가중치 ±3%), 상한·눈 금지·재굴림, `Apply` 표 전수, `SkillMod`·`SpeedFactor` 표 전수, `Tick` 4/8 타임 경계. 테스트 통과 + 콘솔 에러 0 |
| L1 | GameState 키·`SaveCodec` 왕복·구세이브 | 새 키 있는 슬롯 저장→로드 동일, 키 없는 구 슬롯 로드→전부 Sound·mode 0, 임의 키 정렬 무관. 슬롯 목록 `ReadMeta` 불변 |
| L2 | 판정·전투·월드 연동(`SkillMod`·`CanUse`·`GaugeFactor`·`SpeedFactor`·`MaxHp`) | 팔 Crippled 로 세팅 후 사격 목표치 -25 확인, 양손 무기 메뉴 회색, 다리 Lost 도주 불가, 월드 이동 속도 0.7 배(실측 시간) |
| L3 | 피격 연동 + SAN 충격 | 시드 고정 시뮬: 심해인 반격 30 → 부위 1곳 단계+1, BattleEvent `Limb` 1건, SAN 판정 1회. 혼혈 15 → 부위 굴림 없음. 순화·끔 모드 분기 |
| L4 | UI: 부위 실루엣 HUD·토스트·난이도 선택(새 게임·설정)·3경로 입력 | 스크린샷으로 실루엣 8부위 색 확인, 패드·키보드만으로 모드 변경, 토스트가 대사·전투 UI 와 안 겹침 |
| L5 | 응급처치·병원·소품·자연 회복·고착 + 새 상점 항목 | 붕대 소비·Untreated 해제, 8 타임 방치 시 Perm, 병원 치료 1회 1부위, 의수 장착 보정 확인, 시간 경과 `Tick` 1회만 호출 |
| L6 | 오버레이 View(`LimbOverlayView`) + 앵커 표(임시 색 사각형) | 월드·초상·대화에서 28/13/14 오버레이가 앵커를 따라 붙는지 스크린샷. 공격 자세에서 원래 팔이 비치는지 확인(5-2 위험). 사용자 그래픽 검토 후 교체(ART-2 연계) |
| L7 | 이벤트·함정·광기 자해·동료 `LimbSet` | 함정 판정 실패 시 지정 부위 +n, 광기 자해 Hurt 한정, 동료 절단 불가, 동료 Crippled → `Recovering` |
| L8 | 적 부위 조준 `Aimed`(EnemyDef.Parts) | 판정 1/2, 3종 효과 확인, Parts 빈 적 메뉴 숨김, 끔에서 숨김, 시뮬(PT-7)으로 솔로 E1 승률 변화 측정 |
| L9 | 통합: 처음부터 엔딩까지 플레이, 구세이브 호환, 리뷰 | 구세이브 3종 로드 정상, 끔 모드 플레이가 출시판과 동일 로그(`unity-verify`, 개행·BOM 점검), reviewer-arch·code 필수 항목 반영 |

- 각 단계마다 컴파일·EditMode 테스트·`unity-verify`. 확인 못 하면 커밋 메시지·문서에 "미검증".
- L0~L1 은 그래픽 없이 가능(그래픽 대기와 무관). L6 은 사용자 검토 전 임시 그래픽.

## 9. 미결 (임시 가정 → `Docs/OpenQuestions.md` Q-LIMB-*)

Q-LIMB-1 기본 난이도, Q-LIMB-2 중상 임계, Q-LIMB-3 양눈 실명 금지, Q-LIMB-4 동료 절단 금지, Q-LIMB-5 치료·소품 획득처·비용, Q-LIMB-6 Perm 시한, Q-LIMB-7 적 부위 공격, Q-LIMB-8 레이어 분리 규격. 상세는 해당 파일.
