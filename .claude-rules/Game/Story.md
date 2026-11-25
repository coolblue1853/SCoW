# 스토리 분석 (Story)

> 2026-11-21 대사 CSV 15개·`Assets/Resources/Story/*.json`·`WorldRules`·`MapManager` 를 읽고 정리. 근거는 `CSV파일:ID`(ID 열 값, 파일 줄 번호는 ID+1)로 적는다. JSON 의 `TextKey`(`1st_Bar:42-49`)도 같은 ID 범위다.
> 확인하지 못한 것은 "미확인"이다. 씬(`Main.unity`) 이벤트 배치, `InteractionController`(3200줄) 분기는 통독하지 않았다.
> 개선안은 `Game/StoryImprove.md`. 규칙·광기는 `Game/CocDesign.md`. CSV 서식 오류는 `Docs/Playtest/DialogAudit.md`.
> 출시판 대사는 영문이다(`SCOW_SCV.csv` 만 한글 원본, 도입부 1~17+ 만 있고 코드 미참조). 인물 이름은 영문 표기를 쓴다.

## 1. 한 줄 줄거리와 전체 구조

1921년 7월(신문 헤드라인 몽골 독립·아일랜드 휴전 DetectiveOffice 32-33 으로 추정, 7월 15일 "행성 대직렬") 아캄. 월세가 밀린 사립탐정 **파비안 윌프레드 타일러**가 기자 아내 엘라의 "남편 에이든이 악몽·불면·편집증으로 무너졌다"는 의뢰를 받고, 아캄 하수도에 숨은 **크툴루 강림 의식**(인스머스 딥원이 하수도 관리소를 점령, 7월 15일 행성 직렬에 맞춰 마지막 제물 에이든)을 파헤친다. 파비안 본인도 매일 밤 같은 악몽을 꾸며 광기로 끌려간다. 4일 안에 하수도로 내려가지 않으면 악몽에 먹힌다.

- 막 구성: 1일차(조사) → 2일차 낮(엘라 피살·에이든 납치, 강제 이벤트) → 2~3일차(단서 수렴) → 3~4일차(하수도 `InSewer`) → 엔딩. 4일 안에 못 가면 4일차 밤 악몽 엔딩(`1st_DetectiveOffice:343-361`).
- 이 스토리는 "관계자에게 단서(키워드)를 내밀어 정보를 캐는 탐문 + 판정 + SAN" 구조다. 대사 분기는 장소 CSV 와 `Keyword.json`(인물/키워드 → 노드) 이 정한다.

## 2. 시간 구조

`timeCount`(t) 는 **장소 이동 1회마다 +1**(`MapManager.MoveChar` 704~790줄, 장소별 `timeCount += 1`). 4칸이 하루(`WorldClock.cs`): t%4 = 1 아침, 2 낮, 3 저녁, 0 밤.

| 일차 | t | 시간대 | 일어나는 일 | 근거 |
|---|---|---|---|---|
| 1 | 1 | 아침 | 사무소 시작(1-17). 신문·난로·책상을 보면(`fstDetectivTimeOn` 카운터) 노크 → 엘라 의뢰 | DetectiveOffice 1-76, JSON 4-12 |
| 1 | 2~3 | 낮·저녁 | 자유 조사 **2곳뿐**(이동 2번). 사무소 복귀 시 t=4 | MapManager 704~721 |
| 1 | 4 | 밤 | 사무소 정리(183-207, 키워드별 `♠④`) → 1일차 악몽(208-253) | DetectiveOffice 182-253 |
| 2 | 5 | 아침 | 사무소 | |
| 2 | 6 | 낮 | **강제 이벤트**: 알버트 전화 → 엘라 집 → 시신·목격자·맨홀 (`2nd_NoonEvent`). 이후 `places.json:5` 가 엘라 집 CSV 를 `2nd_NoonEvent` 로 바꿈(`time<6` 아니면) | NoonEvent 1-187, WorldRules 67~73 |
| 2 | 7 | 저녁 | 증인·시신·현장 철거(`t>=7`: 증인·시신 사라짐, 메이브 설득 가능). 인스머스 손님 장면(바) | WorldRules 74~82, Bar 115-124 |
| 2 | 8 | 밤 | 2일차 정리(254-290: 엘라 피살 요약) → 2일차 악몽(291-316). `t==8` 바 손님(BarPeople) 사라짐 | WorldRules 87~91 |
| 3 | 9~12 | | 병원 부검 정보(Hospital 127-148), 알버트 3일차 대사(PoliceOffice 192-212). 하수도 사무소 문은 낮(phase 1·2)에 열림, 저녁·밤에 닫힘(창문·사물함 판정 가능) | WorldRules 117~154 |
| 3 | 12 | 밤 | 3일차 악몽(317-342) | |
| 4 | 13~16 | | 마지막 조사·하수도. 4일차 밤까지 하수도로 못 가면 악몽 엔딩 | DetectiveOffice 343-361 |

- 날짜: 신문 "4일 뒤인 7월 15일 시작"(DetectiveOffice:21) → 1일차 = 7월 11일, 4일차 = 7월 14일. 의식은 15일 새벽에 맞춰져 있다(InSewer:22).
- 엘라 집 첫 방문은 `t<6` 안에만 가능하다(`places.json:5`). 1일차에 못 가면 에이든·금고·정신분석 장면(`1st_Client'sHouse`)은 영영 못 본다(자세한 문제는 StoryImprove).
- 시간대 의존 월드(`WorldRules.Apply`): 밤 전구, 하수도 사무소 문·창·사물함(phase), `t>=6` 사건 현장 정리, `t>=7` 증인·시신 철거·Kane/Student 위치 이동, `t==8` 바 손님 퇴장, 알버트/데이브/스웨인 소멸(아래 6절).

## 3. 인물과 관계

| 인물 | 역할 | 만나는 곳 | 비고(근거) |
|---|---|---|---|
| 파비안(Fabian) | 주인공, 보스턴에서 아캄으로 이사 온 탐정, 월세 2개월 밀림(제이크 집주인 압박) | 전부 | DetectiveOffice 3-5, 11 |
| 엘라 트리스(Ella) | 의뢰인, 하즈먼드사 비서, 에이든의 아내. 2일차 낮 피살 | 사무소(1일차), 자택 | DetectiveOffice 58-72 |
| 에이든 트리스(Aiden) | 데일리뉴스 기자 12년차, 악몽·편집증, 금고를 끌어안고 "엘라만 도망치라" 반복. 하수도 괴담을 취재하다 당함. 2일차 낮 납치 | 엘라 자택 2층 | CH 14-60, 198 |
| 버클리 스웨인(Swain) | 신문사 선임 기자(30년차), 에이든 친구. 연속 실종 단독 취재 중 | DailyNews | DailyNews 5, 43, 53 |
| 알버트(Albert) | 경찰, 파비안·메이브의 오랜 친구, 바람기, 실종 사건 비밀 공유자 | Policeoffice | PoliceOffice 13-37 |
| 메이브(Maeve) | 마피아 간부 겸 바텐더, 밀주. 부하가 에이든 어깨를 쏨. 하수도 밀수로를 막혀 인스머스 해초주 루트 개척 | Bar | Bar 70-75, 104-108, 125-141 |
| 케이트 리지(Kate)·앨런 팍스(Alan) | 병원 간호사·의사(불면 유행, 시신 부검) | Hospital | Hospital 3-14, 15-20, 110-148 |
| 케인(Kane) 교수 | 강변에서 수질을 조사하는 노교수(처음 파비안을 제자로 착각). 사건 후 대학으로 이동 | Riverside → University | Riverside 1-6, WorldRules 79~81 |
| 대학생 | 지친 학생, 교수 연구 정보 | University | University 1-42 |
| 더스티 데이브(Dave) | 빈민가 장물아비·정보상(2달러에 한 줄씩). 금고·팔찌 처리. 키워드 후 사망 | Slum | Slum 1-62 |
| 블랙웰(Warden) | 하수도 관리소 소장(Keyword.json 의 `BlackWell`). 가족을 인질로 잡혀 침묵. 열쇠·지도·작업복 | SewerOffice | SewerOffice 31-93 |
| 인스머스 딥원·일지 작성자 | 하수도 점령 세력. 비릿한 냄새의 근원. 일지 작성자(교단 지도자)는 이름·정체 불명 | 하수도, 바 | InSewer 22-37, Bar 127-134 |
| 제이크 | 집주인(월세 독촉, 엔딩의 현실 압박) | 대사로만 | DetectiveOffice 5, InSewer 169 |

- 친구 삼각(파비안·알버트·메이브): 별 보기 약속(`Promise` 키워드, `intelMeiv2`)이 엔딩 에필로그를 가른다(6절).
- 에이든 ↔ 메이브: 마피아에게 총상(DetectiveOffice 67, Bar 71-75). 초반 "마피아 소행" 가설(DetectiveOffice 77-88)의 붉은 청어(red herring).

## 4. 장소별 역할 (`places.json`)

| 장소 id | CSV | 이야기 역할 | 시간·조건 |
|---|---|---|---|
| DetectiveOffice | 1st_DetectiveOffice | 도입·의뢰·밤 정리·악몽 4회 | 항상 |
| Client'shouse | 1st_Client'sHouse(`time<6`) → 2nd_NoonEvent | 에이든 대면·금고 / 2일차 낮 살인 현장 | `places.json:5` |
| DailyNews | 1st_DailyNews | 스웨인: 에이든 배경, 연속 실종 정보 | 스웨인 사망 후 기자로 교체 |
| University | 1st_University | 학생·케인(사건 후): 수질·점액 | 학생은 저녁에 없음(`MapManager.cs:754`) |
| Riverside | 1st_Riverside | 케인(사건 전)·하수도 입구 관찰(섬뜩한 눈) | 케인은 밤에 있음 |
| Hospital | 1st_Hospital | 불면 유행 정보, 3일차 부검 | 케이트(오전)·앨런(오후) 시간대 차 |
| Policeoffice | 1st_PoliceOffice | 알버트: 실종 6건 비밀·사건 요약 | 시기별 3종 첫 대사 |
| Bar | 1st_Bar | 메이브: 밀수, 인스머스 루트, 술 판정 | 인스머스 손님 장면(2일차 저녁) |
| Slum | 1st_Slum | 데이브: 금고 개봉, 팔찌 매각 | 유료 대화(`★③`) |
| NoonEvent / BattleRoad | 2nd_NoonEvent / 1st_Battle | 살인 현장 조사·귀가길 딥원 추격(전투) | 강제 이벤트 |
| SewerOffice | SewerOffice | 소장 설득/절도, 창문·사물함, 열쇠·지도·작업복 | 낮 소장 있음, 밤 비어 있음 |
| InSewer | InSewer | 변장·은밀·전투, 일지·마도서·문양, 엔딩 | 열쇠·맨홀·강변 자물쇠로 진입 |
| (Item) | 1st_Item(SafeLook.json) | 금고·팔찌·코트·열쇠·지도·서류 설명 | 소지품 |

## 5. 단서 → 키워드 → 수첩 → 결론

`●n` 표식(예 `ⓡ(●1ⓟplanetary parade)ⓦ`)이 키워드 획득 지점이다(수첩 `IntelManager`/`KeyWordManager`(K7(6f896de)에서 제거, 대체: KeywordBook/KeywordPicker)). 장소 CSV 에서 단서를 얻고, **수첩에서 키워드를 골라 NPC 에 내밀면** `Keyword.json` 의 `인물/키워드 → 노드` 로 답이 나온다. 매핑 없는 조합은 `인물/*` 로 떨어져 "모른다"(`kaneNothing` 등).

### 5-1. 핵심 사슬(결론까지)

```
[불면·악몽 증상]            신문(DetectiveOffice 35-39) → 병원(Hospital 31-84) → 학생(University 24-31)
   └ 물·바다 악몽 공통        Alan 53-57, 66-67 / Albert 69-76 / Swain 84-91
[비릿한 냄새 (원인 3키워드)]   Swain 26, Dave 11·33-37, 엘라(CH 204), 바 손님(Bar 81-89), Maeve 66-69, 시신(NoonEvent 131)
   └ 근원 = 하수도 노동자/하수도  Dave 35 ●3ⓦ, Kane 46, Item 서류 21
[연속 실종 6건(남4 여2)]      Swain 53(●1ⓜ) ⟷ Albert 25(●2ⓜ), 비밀 유지 강요
   └ 같은 시기 시작             Swain 98-101: 냄새·실종 동시 → "하수도 정비 시설" ●1ⓢ
[하수도 괴담(악어)]          엘라(CH 141) / 데이브 24-29 / 서류 17 → 알버트 59-62(실제로는 불가) → 눈(Riverside 16-23)
[섬뜩한 눈]                 Riverside 하수도 관찰(♠② 관찰력 → ♠③ SAN) → 케이트(Hospital 91-98)·메이브(Bar 63-65) 아무도 안 믿음 → 에이든 "하수도 가까이 가지 마라"(CH 191-199)
[2일차 낮 살인]              목격자(●1ⓩ 작업복) → 시신(사인 ●4ⓩ, 물갈퀴 ●5ⓩ) → 현장(맨홀 ●6ⓩ)
   └ 결론 "범인 = 하수도 노동자" Fab_Witness~SwainSewer(DetectiveOffice 257-290)
[하수도 정비 사무소 위치 ●1ⓢ] 위 단서 어느 것으로나 획득(Swain 101·152·186, Albert 143, Kate 115, Dave 37, Maeve 175, Item 27)
   → 소장(SewerOffice) → 열쇠·지도·작업복 → 강변 하수도/맨홀 → 일지(InSewer 22-37) → 문양 파괴 or 피 → 엔딩
```

### 5-2. 키워드 → NPC 반응 표 규모

- `Keyword.json` 인물 11명: Ella, Aiden, Swain, University student, Kane, Albert, Kate, Alan, Maeve, Dave, BlackWell, Symbol. 토픽 약 150개.
- 같은 키워드가 3개 이름으로 갈라져 같은 노드를 부르는 곳이 많다: "Source speculation / Sewer & FishySmell / Cause of Fishy smell"(Aiden 21-23, Swain 40-42, Albert 80-82, Maeve 144-146, Student 64-66, BlackWell 153-155).
- 키워드로 NPC 가 **죽는/사라지는** 것(`*DeathKeyword`, 6절): Swain(`Manhole`·`Aiden's warning`·`Witness Testimony`·`The Culprit`, Keyword.json:48-49,52-53, 57), Albert(`Root of problem`·`The Culprit`·`Way to Escape`·`Aiden's warning`·`Owner of Uniform`, :86,90,93-95), Dave(Slum 45 `♣⑥`, 금고 전달 직후).

### 5-3. 핵심 판정(주요)

| 장면 | 판정 | 성공 | 실패 | 재시도 |
|---|---|---|---|---|
| 신문 읽기(DetectiveOffice 18) | 관찰력(추정) | 불면 기사 → 병원 힌트(35-39) | "특별한 것 없음"(40) | 신문 재확인 가능(미확인) |
| 난로(41) | 행운 | 컨디션 상승(43-45) | 컨디션 하락 느낌(46-48) | 미확인 |
| 에이든 금고(CH 62-96) | 힘 / 언변 / 정신분석(매우 어려움) / 안 건드림 | 금고 획득(`getSafe`) | 등 부상·쫓겨남(75-78, 91-95) | 금고 선택지 1회성(추정) |
| 에이든 의학 진단 | 의학 | 불면 인과 의문(97-99) | 100 | |
| 시신·현장(NoonEvent 128-187) | 의학·관찰력·자료조사·손재주(팔찌 절도) | 사인·물갈퀴·맨홀·팔찌 | 단서 없음(155-156,161,169,173) | 현장 다시 와서 자료조사 재시도(180-187) |
| 경찰 설득(NoonEvent 96-113) | 언변 | 집 안 진입(104-110) | 못 들어감 → 목격자 엿듣기(듣기)만 | |
| 바 술(Bar 183-199) | 언변 | 인스머스 술 한 모금, SAN(`※③`) | 못 마심(199) | |
| 하수도 관찰(Riverside 15-28) | 관찰력 | 눈 키워드, SAN | "너무 어두워 안 보임"(26) | 이후 `sewerObservationEnd` |
| 소장(SewerOffice 42, 74) | 언변 / 손재주 | 열쇠·지도 / 열쇠만 | 열쇠 못 얻음(72,76) | |
| 창문·사물함(SewerOffice 9-24) | 힘(창문) / 관찰력(사물함) | 침입·작업복·열쇠(22-23) | 부상·1회성(`tryBrokenWindow`,`tryObserLokcer`: WorldRules 145~146) | **재시도 불가** |
| 하수도 은밀(InSewer 1-13) | 은신 / 언변(변장 없을 때) | 통과 | 전투 | |
| 문양 파괴(54-77) | 오컬트 / 힘 | 문양 인식 / 파괴 | 부상 | |

## 6. 월드 변화·NPC 소멸 (키워드 누설의 대가)

| 트리거(플래그) | 효과 | 근거 |
|---|---|---|
| `swainDeathKeyword` + phase 0(밤) | 스웨인 소멸, 기자(Journalist)가 대신 자리, 에필로그에 "교살된 채 발견" | WorldRules 103~107, InSewer 162-163, DailyNews 225-230 |
| `albertDeathKeyword` + `t>=8` phase 0 | 알버트 소멸, 3일차 경찰 A/B 대사 "휴가" (`thirdDayPoliceADialog`) | WorldRules 92~97, PoliceOffice 225-232 |
| `daveDeathKeyword` + phase 0 | 데이브 소멸, "쓰레기통 뒤지러 갔나?" | WorldRules 98~102, Slum 61-62 |
| 금고 획득 `getSafe` | 엘라 귀가 대사 분기(`ella_end_getsafe/nosafe`), 정신분석 성공 시 에이든 대화 2종 | CH 124-136, 149-190 |
| 사건 후 `intelMissingPeople1/2` | 스웨인·알버트 대사 변경 | Keyword.json 33-34 |

플레이어는 "누설하면 죽는다"를 사전에 알 수 없다. 단서 사용 뒤 밤이 지나야 결과가 나온다(StoryImprove 참조).

## 7. 엔딩 조건과 분기

| 엔딩 | 조건 | 장면 | 비고 |
|---|---|---|---|
| A 문양 파괴(해결) | 하수도에서 문양을 파괴(오컬트/힘 판정) 후 탈출 | InSewer 139-142 → 에필로그 143-182 | 물은 차오르지만(57) 의식이 막힘 |
| B 피의 엔딩 | 문양 앞 SAN 판정 실패 → 홀린 듯 손에 피를 묻힘(74-76, 126-138) 또는 "Symbol/Advent" 키워드(`bloodEnding`, Keyword.json:161) | 크툴루 강림: 홍수, 거대 존재, 대사 118-125 | 나쁜 엔딩 |
| C 사다리 엔딩 | 문양 처리 안 하고 사다리로 탈출(91-105) | 도시 침수·SAN 9연속(`endingLadderSanInt` 0~8, Judge.json:391-428) → B 와 같은 결말 | 나쁜 엔딩 |
| D 악몽 엔딩(시간 초과) | 4일차 밤까지 하수도로 못 감 | 4번째 악몽 → 큰 눈 "Welcome home"(DetectiveOffice 343-361) | 게임 오버성 엔딩 |
| 게임 오버 | HP·SAN 0 (`DirectingManager.cs:140`) | | |
| 체포(변주) | 하수도 노동자를 죽임(`Ending_Killing`) | 경찰이 새벽에 체포(InSewer 183-190) | A 에필로그 뒤에 붙음 |

A 에필로그 변주(`Ending_isPromise`, `Ending_RequestCheck`, `Ending_SwainDeathCheck`; InSewer.json:205-231):

| 조건 | 문장 | ID |
|---|---|---|
| 별 보기 약속(`intelMeiv2`) 있고 알버트 생존 | 약속에 늦어 혼남, 용서받음 | 146-147 |
| 약속 있고 알버트 사망(`albertDeathKeyword`) | 알버트 사망, 메이브의 복수 다짐, 파비안의 죄책감 | 148-153 |
| 약속 없음 | 편하게 잠. 알버트 사망이면 157-161 반복 | 154-161 |
| 스웨인 사망 | 하수도에서 교살체 발견 | 162-163 |
| 의뢰 수락(`ifGotRequset`) | 에이든 생존·사례금으로 월세 해결 | 164-172 |
| 의뢰 거절 | 에이든 생존·"그때 맡을걸" 후회 | 173-182 |

- 에필로그에 데이브·소장(블랙웰)·케인·엘라·메이브 약속 위치 후일담은 없다(StoryImprove).
- 에이든 생존·구출 장면은 에필로그 한 줄이라 하수도 안에서 에이든을 만나지 못한다(InSewer 어디에도 에이든 대사 없음).

## 8. SAN 체크 장면(광기 연출 후보 위치) — 14곳+전투

`♣/♠/◈/◐` 표식(SAN 판정 호출)과 `Judge.json` `SanCheck/*` 로 확인.

| 번호 | 장면 | 근거 | 상황 |
|---|---|---|---|
| 1 | 1일차 밤 악몽 직후 | DetectiveOffice 227, 239(지능 판정 분기) | 에이든과 같은 증상 자각 |
| 2 | 2일차 밤 악몽 직후 | 309 `※④`, 312 | 구토 |
| 3 | 3일차 악몽 후 | 334-338 (의뢰 수락 여부로 분기) | 건강 판정 |
| 4 | 4일차 악몽 | 343-361 | 엔딩 |
| 5 | 강변 하수도의 눈 | Riverside 23 `♠③` | 첫 괴물 |
| 6 | 케인 점액·인어 | University 63 `♣⑤` | |
| 7 | 바 술 | Bar 192 `※③` | 악몽 플래시백 |
| 8 | 엘라 시신 | NoonEvent 119 `♣②` | |
| 9 | 퇴근길 딥원 | Battle 5 `♠⑥`, 8(SAN -10 하드코딩 표기) | |
| 10~14 | 하수도: 일지 38, 마도서 45, 문양 66·71, 진입 82, 딥원 88, 사다리 92·112-125 | InSewer | 마지막 9연타(`◈①`) |

## 9. 확인하지 못한 것

- 에이든 금고 "혼자 열기"(Judge `Safe : Open`) 성공 후 흐름. `1st_Item:5` 는 "데모 버전" 문구(도달 불가, DialogAudit 6절).
- `aidenLookJudge` 씬 연결 누락(DialogAudit C트랙, 관찰 판정 시 NRE 가능).
- 사무소 노크 트리거 카운터 임계값, 밤 연출의 정확한 시작 조건(`MapManager`/`DirectingManager`).
- 하수도 맵 생성(`TimeManagere` MakeSewer*) 의 방 구성·적 배치.
- `InteractionController` 안의 판정 난이도(기능, 보정), 전투 밸런스.
