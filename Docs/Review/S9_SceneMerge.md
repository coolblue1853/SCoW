# SEWER S9 씬 복제 합치기 판정 (11-27)

## 결론

- 복제 쌍 15개(`TimeManagere` 의 `X` / `XBattle` 필드) 중 **합친 것 2개**(`ladderBattle`, `sideRoadBattle`: 항상 꺼진 죽은 오브젝트라 삭제).
- 나머지 **13쌍은 합치지 않는다.** 한 스프라이트가 월드 두 곳에 동시에 있을 수 없고, 합치려면 전투 카메라·`Battle/Character`·`BattleCanvus`·조명 전부를 탐험 월드 좌표로 옮겨야 한다. 이득은 오브젝트 13개뿐이고 위험은 전투 화면 전체다.

## 실측

- `Map/Map/Sewer/...`(탐험)와 `Battle/Map/Sewer/...`(전투)는 부모 아래 **로컬 좌표·스케일·레이어·컴포넌트가 전부 같고**, 월드 좌표만 정확히 (+443.9, +315.6, 0) 다르다(예: Ground_Nomal (-1219.12,-199.96) vs (-775.22,115.64)).
- 전투 카메라는 `BattleCameraMove`(originPoint 120.1 / originPointx -774.6)로 전투 구역 좌표에 있고, 비하수도 전투는 이 좌표를 DOTween 으로 직접 움직인다. 전투 쪽 `Battle/Character`(6)·`BattleCanvus`·Battle_* 지점도 같은 좌표계다.
- 탐험 쪽에는 2D 조명(`Map/Lights` 15개, `Global`)과 플레이어·NPC·건물이 있어, 전투 카메라를 그쪽으로 옮기면 전투 장면에 같이 보인다.
- 두 벌을 참조하는 코드는 `TimeManagere` 인스펙터 필드와 `SewerRoomApplier.SyncBattle`(탐험 소품 상태를 `CopyActive` 로 복제에 복사) 뿐이다. 즉 복제 유지 비용은 코드 한 표(13줄)이고 이미 한곳에 모였다.

## 쌍별 표

| # | 필드 | 탐험 월드 위치 | 전투 월드 위치 | 로컬 차이 | 참조 | 판정 |
|---|------|---------------|---------------|-----------|------|------|
| 1 | groundWater | (-1219.1,-200.0) | (-775.2,115.6) | 없음 | TimeManagere, SyncBattle | 유지(카메라 좌표 종속) |
| 2 | groundWaterLand2 | (-1205.0,-200.0) | (-761.1,115.6) | 없음 | 위와 같음 | 유지 |
| 3 | sewerFlat | (-1219.1,-191.3) | (-775.2,124.4) | 없음 | 위와 같음 | 유지 |
| 4 | sewerRound | (-1219.1,-191.3) | (-775.2,124.4) | 없음 | 위와 같음 | 유지 |
| 5 | burbFlet1 | (-1234.2,-190.4) | (-790.3,125.2) | 자식 1 동일 | 위와 같음 | 유지(램프 자식 조명 포함) |
| 6 | burbFlet2 | (-1199.1,-190.4) | (-755.2,125.2) | 자식 2 동일 | 위와 같음 | 유지 |
| 7 | burbRound1 | (-1232.6,-188.0) | (-788.7,127.6) | 자식 1 동일 | 위와 같음 | 유지 |
| 8 | burbRound2 | (-1201.2,-188.0) | (-757.3,127.6) | 자식 1 동일 | 위와 같음 | 유지 |
| 9 | hole | (-1217.4,-188.6) | (-773.5,127.0) | 없음 | 위와 같음 | 유지 |
| 10 | pipe | (-1219.1,-182.4) | (-775.2,133.2) | 없음 | 위와 같음 | 유지 |
| 11 | house | (-1230.7,-190.4) | (-786.8,125.2) | **자식 3 vs 1**(탐험 쪽 자식이 더 많음) | 위와 같음 | 유지(구성이 다름) |
| 12 | cage | (-1236.7,-193.3) | (-792.8,122.4) | 없음 | 위와 같음 | 유지 |
| 13 | land2 | (-1228.7,-197.4) | (-784.8,118.2) | 없음 | 위와 같음 | 유지 |
| 14 | ladder / ladderBattle | y 35.30 | y 8.79(월드 127.5) | 위치 다름, 탐험 쪽만 `Intel_ObtoUI`·`Rigidbody2D`·`BoxCollider2D` | `SyncBattle` 이 항상 `SetActive(false)` | **삭제**(전투 복제는 한 번도 켜진 적 없음) |
| 15 | sideRoad / sideRoadBattle | (-1218.1,-191.3) | (-774.2,124.4) | 자식 1 동일 | `SyncBattle` 이 항상 `SetActive(false)` | **삭제** |

(주: 요청서의 "14쌍" 은 13쌍 + 사다리·샛길 = 15 로 센 결과다.)

## 한 변경

- `Main.unity`: `Battle/Map/Sewer/Ladder`, `.../SideRoad`(자식 1) 삭제, `TimeManagere` 의 두 참조를 null 로. 에디터 API(`DestroyImmediate`+`SaveScene`)로만 수정, 씬 diff 는 삭제 396줄·참조 2줄.
- `TimeManagere.cs`: `ladderBattle`·`sideRoadBattle` 필드 삭제. `SewerRoomApplier.cs`: 대응 2줄 삭제.

## 남은 13쌍을 합치려면(보류 근거, 할 때의 조건)

- 전투 카메라(`BattleCameraMove` 상수 3개, 비하수도 전투 DOTween 대상), `Battle/Character` 배치, `BattleCanvus` 월드 기준 요소, 전투 연출용 `Battle_*` 지점을 탐험 하수도 좌표로 옮기고, 전투 중 탐험 플레이어·조명 가리기가 필요하다. 비하수도 전투(강가 등)는 배경이 따로라 하수도만 따로 옮기면 두 좌표계가 공존한다.
- 따라서 전투 전반 재작업(큐 신규) 때 함께 한다. 지금은 `SyncBattle` 표 하나가 유지 비용의 전부다.

## 검증

- 전·후 스샷(`Logs/playtest/`): `s9_before_solo.png`/`s9_after_solo.png`(하수도 솔로 전투 E1), `s9_before_party.png`/`s9_after_party.png`(3인 파티 E2), `s9_before_room.png`/`s9_after_room.png`(하수도 z0→우→우 방 이동). 솔로·파티 전투 배경과 배치는 동일하다(파티는 무작위 HP·턴 순서만 다름). 방 이동 스샷은 방 외형이 무작위 시드라 램프 유무가 다르다(변경과 무관, 이동·표시 자체는 정상).
- 컴파일 error 0, EditMode 503/503 통과. `Main` 씬 Missing script 5개는 기존(BillowUI HPBar 4·End_Look 1)이며 삭제한 오브젝트와 무관. 세이브 슬롯 쓰기 없음(`SaveSnapshot` 확인용 읽기만).
