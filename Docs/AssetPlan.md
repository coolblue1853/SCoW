# 보유 에셋 재분류 (SCoW 기준, 2026-11-21)

> `.claude-rules/Reference/AssetInventory.md` 는 Project B(횡스크롤 ARPG) 기준 분류다. SCoW 는 **1921년 아캄 항구 도시 배경의 도트 탐정 호러(크툴루의 부름 TRPG)** 라서 판단이 뒤집힌 것이 많다(예: Project B 에서 "해당 없음"이던 Horror SFX 는 SCoW 의 핵심 자원이다).
> 조사 방법: Unity Asset Store 캐시 `C:\Users\user\AppData\Roaming\Unity\Asset Store-5.x\<퍼블리셔>\**\*.unitypackage` 안의 `pathname` 목록만 읽었다(풀지 않음, 임포트 없음). 아래 "패키지 내 경로"는 임포트할 때 쓸 원본 경로다.
> **그래픽 교체·신규 이미지 적용은 사용자 검토 후에만 한다.** 이 문서는 선택 근거이며 에셋을 프로젝트에 넣지 않았다. 사운드·글꼴도 "적용할 것"은 사용자 승인 뒤 임포트한다(스타일 기준은 `Game/ArtStyle.md`, 현황은 `Docs/ArtInventory.md`).
> 대용량 팩 통째 임포트 금지. 필요한 파일만 `Assets/ThirdParty/<이름>/` 로 (Project B 규칙 유지: 필요 파일만 골라 .unitypackage 의 asset/meta 를 직접 풀거나 `AssetDatabase.ImportPackage` 후 불필요 폴더 삭제).

## 1. 보유 현황

| 위치 | 에셋 |
|---|---|
| SCoW `Assets/Plugins` 에 이미 있음 | DOTween + DOTweenPro (Demigiant), Easy Save 3, I2 Localization, Text Animator(`Febucci.UI`, asmdef 있음), DamageNumbersPro, `External_rostCrypt` |
| SCoW `Packages` | 2D Pixel Perfect 5.1.1, 2D Animation 14.0.3, Cinemachine 2.10.6, Timeline 1.8.11, Input System 1.19.0, URP 17.4.0, TMP(ugui 2.0.0) |
| 캐시에 .unitypackage 있음(미임포트) | Fantasy Music Suite 2, Free Fantasy Adventure Music Pack, **Horror Bundle - Sound Effects(2.4GB)**, **Pro Sound Collection(2GB)**, **Ultimate Game Music Collection(4.4GB)**, **Game Music Treasury Vol I(4.1GB)**, Ultimate SFX Music Bundle(10GB, 목록 미조사), SoundBits Free SFX(0.6GB, 목록 미조사), Free Frost Mage SFX, 8-Bit Sfx, Smart Lighting 2D, Feel, Corgi Engine, Quest Machine, Magic Slashes FX, Particle Pack Starter Assets, All In 1 Sprite Shader, Odin Inspector, AnyPortrait, 2D Pixel Unit Maker(SPUM), Magic Cliffs, Underwater Fantasy, Bringer Of Death, Evil Wizard 2, Martial Hero, Medieval King Pack 2, MonstersCreaturesFantasy, 2D Pixel - RPG Monsters Icon, Free Pixel Font - Thaleah |
| 계정에는 있다고 기록, 캐시·SCoW 에 **없음** | Dialogue System for Unity, Master Audio 2022, Pro Camera 2D, Behavior Designer, Pixel Perfect Camera(구) — 쓰려면 My Assets 에서 다시 받아야 함. 아래 4절에서 도입 안 하기로 한 이유 |

## 2. 항목별 분류

판정: **적용** = 필요한 파일을 골라 쓴다. **후보** = 조건이 맞으면. **안 맞음** = SCoW 톤·장르 불일치.

### 2-1. UI 프레임

| 에셋 | 판정 | 이유 |
|---|---|---|
| (보유 에셋 중 해당 팩 없음) | — | 대화상자·HUD·수첩 프레임에 쓸 도트 UI 팩은 보유 목록에 없다. Cartoon GUI Pack 은 카툰풍이라 안 맞음. **자체 제작**(ArtInventory 우선순위 1번, PixelLab 11-25 이후 또는 직접 도트)로 간다 |
| Pro Sound `User_Interface_Menu`·`Retro_Classic/Menu_UI_Beeps`(소리만) | 후보 | 4절 사운드 참고 |
| UI Particle Image | 안 맞음 | 보유 확인 불가, 우선순위 낮음 |

### 2-2. 폰트(한글 포함)

| 에셋 | 판정 | 이유·경로 |
|---|---|---|
| **Galmuri11 Bold**(OFL, 스토어 아님) | **적용** | 현재 TMP 글꼴 `ThaleahFat`·`pcsenior` 둘 다 한글 글리프가 없고(`Assets/Font/Thaleah_PixelFont/`), `MapoAgape.ttf` 는 `Assets/Sprite/Note/` 에 TTF 만 있고 TMP 에셋이 없다. Project B 에서 이미 구성한 한글 도트 폰트: `C:\Users\user\ProjectB\Assets\ThirdParty\Font\Galmuri\`(`Galmuri11-Bold.ttf`, `Galmuri11Bold_TMP.asset`, `OFL-LICENSE.txt`). SCoW 로 복사 후 `ThaleahFat` TMP 에셋의 **Fallback Font Asset** 에 연결하면 한글만 Galmuri 로 그려진다. 저장소에는 빈 아틀라스로 커밋(Dynamic, 빌드 시 비움). **글꼴 시안은 사용자 검토 필요** |
| Free Pixel Font - Thaleah | 이미 있음 | 영문·숫자 전용. 1920년대 탐정 톤과는 약간 게임스러움 — 타이틀·숫자(데미지·주사위)에 한정 |
| MapoAgape(마포애가페) | 후보 | 이미 프로젝트에 TTF 있음. 수첩(Note) 손글씨 용도로 TMP 동적 에셋화 후보(수첩 필기체 분위기에 맞음). 라이선스 확인 필요(마포구 무료 폰트, 상업 이용 가능 여부 확인 전까지 보류) |
| 둥근모꼴·NeoDunggeunmo | 후보 | 스토어 아님. Galmuri 가 안 맞을 때 대안 |

### 2-3. BGM

현재 SCoW 에는 장소별 곡 13개가 `Assets/Sound/Bgam/` 에 있다(술집·탐정·총포상·병원·하수도 입구·신문·악몽·경찰·강변·캐릭터 생성·하수도 관리소·슬럼·대학). **갈아엎지 않고 빈 곳(하수도 깊은 구역·공포 단계·전투·엔딩)을 메운다.**

| 에셋 | 판정 | 이유·경로 |
|---|---|---|
| **Ultimate Game Music Collection**(John Leonard French) | **적용(소수)** | 호러·미스터리 앰비언스와 "Dark Jazz Club"(술집·1920년대 톤)이 있다. 패키지 내 경로 `Assets/Ultimate Game Music Collection/` 아래 — **하수도**: `Dungeons/Dark Dungeon AMBIENT LOOP.wav`, `Dungeons/Murky Dungeon LOOP.wav`, `Ambience/Creepy Ambience LOOP.wav`, `Ambience/Sinister Ambience LOOP.wav` / **SAN 위험·딥원 조우**: `Ambience/Shimmering Horror Ambience LOOP.wav`, `Ambience/Horror Ambience LOOP.wav` / **조사·잠입 긴장**: `Sneaking/Lurking LOOP.wav`(+Layers 3개로 층 쌓기) / **병원·고독**: `Ambience/Loneliness LOOP.wav`, `Ambience/Music Box Edits/Music Box SIMPLE LOOP.wav` / **술집**: `Locations/Dark Jazz Club Loop.wav` / **엔딩 패배**: `Short Cues/Losing/Dark Defeat 1.wav`. 모두 약 20곡 이내만 OGG 변환 임포트 |
| **Game Music Treasury Vol I**(Steven Melin) | 후보 | 분위기곡이 있음(`LOOP_Darkened Past`, `LOOP_Slow-Burning Ambience`, `LOOP_Tension Underground`, `LOOP_Derelict Shrine` 등, `Tactics Adventure`·`JRPG` 번들 안, 정확한 하위 경로는 임포트 시 확인). 위 팩으로 부족할 때만 |
| Fantasy Music Suite 2 | 후보(소수) | `Place of Shadows.wav`, `Embark at Dusk Loopable.wav` 정도. 나머지는 판타지(엘프·주점)라 안 맞음 |
| Free Fantasy Adventure Music Pack | 안 맞음 | 전투·영웅 판타지 곡(`Time for Battle`, `Final Glorious Triumph`) |
| SAGA Complete Collection | 후보 보류 | 캐시 0MB(다운로드 안 됨). 필요 시 재다운로드 |
| Horror Bundle `PACKHorrorMusic`(25곡) | 후보 | 아래 2-4 참고, 단발 공포 음악 |

### 2-4. 효과음

| 에셋 | 판정 | 이유·경로 |
|---|---|---|
| **Horror Bundle - Sound Effects**(AD Sounds, wav 4,382개) | **적용(소수 수십 개)** | Project B 의 "해당 없음(Horror SFX)" 판정을 뒤집는다. `SewerImprove.md` 3-3 의 공포 레이어(발소리·물소리·전조·환청·SAN 연동)와 정확히 맞는다. 패키지 내 `Assets/HorrorBundleSFX/` 아래 — **SAN 심박**: `PACKHauntedHouse/OldSounds/Heart Beat/SFX_HeartBeatLoopPulseFaster.wav`, `SFX_HeartBeatSingleBeatLowPulseSlower.wav` / **물방울**: `PACKBundle/SmallCinematicScenarios/house_06/sfx_water_drip_03_wav.wav`, `_04` / **긴장 스팅어**: `PACKBundle/StingerAndTransitions/sfx_stinger_*.wav`(약 49개 중 3~5개), 라이저 `sfx_riser_low_to_*` / **무드·환청**: `PACKBundle/MoodAndAmbienceDefiners/`(153개 중 10개 이내), 속삭임·호흡은 `PACKBundle/HumanVoices/Woman`·`CreatureVoices`에서 검색 / **하수도 괴물 전조**: `PACKMonsterSFX/PackCinematicMonsters`(206개 중 후보 5개), `PACKCinematicHorror/Monsters` / **문**: `PACKDoors/Mockup_Files/Door_Squeak`, `Keys` / **소리 풍경 루프**: `PACKBundle/SoundScapes`(23개) |
| **Pro Sound Collection**(Gamemaster Audio, 약 6,100개) | **적용(소수)** | 현실 계열 일반 효과음 — 총기·발소리·UI·물. 패키지 내 `Assets/Gamemaster Audio - Pro Sound Collection/` 아래 — **발소리**: `Footsteps/footstep_concrete_*`(콘크리트 60), `footstep_wood_*`(55), `footstep_gravel_*`(51), `footstep_water_splash_med_wading_*`(24, 하수도 침수 구간) 각 4~6개 / **물**: `Water/water_drop_drip_single_05·07·09·10·22.wav`, `water_drops_drips_multiple_06.wav`, `water_splash_object_body_02.wav` / **앰비언트**: `Animals_Nature_Ambiences/river_stream_flowing_water_loop_06_short_version.wav`(강변), `rain_heavy_loop_02.wav`, `beach_waves_ocean_calm_loop_01_long_version.WAV`(항구) / **권총**: `Guns_Weapons/Guns/gun_revolver_pistol_cock_04·05.wav`, `gun_revolver_pistol_dry_fire_05.wav`, `gun_silenced_pistol1_shot_01.wav` + 총포상 소품 / **맨홀·문**: `Doors/manhole_metal_slide_door_open_close_5.wav`, `Doors/door_old_wooden_close_thud_01.wav`, `door_hatch_squeak_open_01.wav` / **UI**: `User_Interface_Menu/ui_button_simple_click_01.wav`, `ui_menu_popup_message_reward_01.wav` 등 6개 |
| Ultimate Game Music Collection `Short Cues` | **적용(소수)** | 공포 스팅: `Short Cues/Scares/Scratching SCARE.wav`, `Piercing SCARE.wav`, `Slow Scratch RISER.wav`, `Short Cues/Sound Effects/Thunder 4.wav`, `Draughty House Loop.wav`(외풍 앰비언트) |
| Ultimate SFX Music Bundle(10GB)·SoundBits Free SFX(0.6GB) | 후보 | 목록 미조사. Horror Bundle·Pro Sound 로 부족할 때만 목록 확인 |
| 8-Bit Sfx(Little Robot) | 안 맞음 | 레트로 비프(Jump·Pickup·Explosion)는 1920년대 호러 톤과 충돌 |
| Free Frost Mage SFX | 안 맞음 | 판타지 마법 |
| Magic_Spells(Pro Sound 안) | 안 맞음 | |

### 2-5. 이펙트(VFX)

| 에셋 | 판정 | 이유 |
|---|---|---|
| All In 1 Sprite Shader | **후보(핵심)** | 피격 플래시·디졸브·아웃라인·글리치(SAN 연출, 림버스식 포스트 효과의 도트판) — URP 2D 지원(Project B 17MB 임포트 이력, Demo 제외). 다만 SCoW 는 UI(Canvas) 스프라이트 중심이라 `Image` 머티리얼 적용 가능한지 먼저 시험. Unity 6000.4 호환은 실측 필요 |
| Feel(More Mountains) | 후보 | 화면 흔들림·플래시. 현재 DOTween 으로 충분(아래 4절), 전투 View(P14b)에서 필요할 때만 |
| Magic Slashes FX / Particle Pack Starter Assets | 안 맞음 | 판타지 마법·환타지 슬래시 |
| Smart Lighting 2D | 안 맞음(보류) | Project B 는 미도입 결정. SCoW 는 UI 스프라이트 기반이라 2D 광원 시스템이 필요 없고, 시야 마스크는 방사 그라디언트로 충분(`SewerImprove.md` 3-3) |
| DamageNumbersPro | 이미 있음 | 판정 숫자·SAN 감소 숫자에 쓰는 중, 변경 없음 |

### 2-6. 대화·연출 도구

| 에셋 | 판정 | 이유 |
|---|---|---|
| **Text Animator**(Febucci) | **유지(이미 사용 중)** | `DialogManager.cs` 의 `TextAnimatorPlayer`·`SkipTypewriter`. 태그 연출(`<shake>`, `<wave>`)을 SAN 하락·공포 대사에 쓴다. 옛 CSV 태그 `ⓖ(..)ⓦ` 는 `Architecture.md` 2-4 대로 View 가 변환 |
| **DOTween Pro** | **유지(이미 있음)** | UI 트윈·전투 View 연출 전담. `DOTweenAnimation`(에디터 컴포넌트)은 단순 UI 호흡·버튼 반응용으로 쓸 수 있다. 코드는 `SetLink`/`Kill` 규약 |
| **I2 Localization** | 유지(이미 있음) | 문구는 새 키 추가만(`CodeStyle.md`) |
| Dialogue System for Unity | **안 씀**(4절) | |
| Master Audio | **안 씀(당장)**(4절) | |
| Quest Machine | 안 맞음 | 퀘스트 로그형 RPG 구조, SCoW 는 단서·시간 진행 구조 |
| Behavior Designer·Pro Camera 2D | 안 맞음 | 실시간 AI·횡스크롤 카메라, SCoW 는 턴 전투·고정 장면 |
| Odin Inspector | 후보 | 대화 JSON 검증 창 정도. 필수 아님(Odin 이 Unity 6 에서 별도 버전 필요할 수 있음) |

### 2-7. 도트 배경·캐릭터

| 에셋 | 판정 | 이유 |
|---|---|---|
| Magic Cliffs(GothicVania, Ansimuz) | 안 맞음 | 중세 판타지 절벽. 1921년 도시 아님. 어두운 청록 톤만 참고(`Docs/ArtInventory.md` 팔레트) |
| Underwater Fantasy(Ansimuz) | 후보(소) | 하수도 수중·침수 연출용 패럴랙스 `far`/`sand`/`foreground-1·2` 4장만, 색 조정 전제. 도트 크기가 SCoW 배경과 다르므로 사용자 검토 필수 |
| Monsters_Creatures_Fantasy / Evil Wizard 2 / Bringer of Death / Martial Hero / Medieval King Pack 2 | 안 맞음 | 중세 판타지. 딥원·하수도 괴물은 자체 도트(PixelLab) |
| 2D Pixel - RPG Monsters Icon(LAYERLAB) | 안 맞음 | 판타지 몬스터 아이콘 |
| SPUM / Pixel Hero Maker | 안 맞음 | Project B 에서도 "너무 안 어울린다" 반려 이력 |
| AnyPortrait | 후보 | 인물 일러스트 리깅. 현재 도트 스프라이트 구조와 맞지 않아 지금은 안 씀 |

### 2-8. 기타

| 에셋 | 판정 | 이유 |
|---|---|---|
| Easy Save 3 | 이미 있음 | 세이브 키는 추가만 |
| Corgi Engine | 안 맞음 | 횡스크롤 플랫포머 |
| 2D Pixel Perfect(패키지, 설치됨) | 후보 | 씬 적용 여부 미확인. 도트 배율 통일(`Docs/ArtInventory.md` 5절 2번)과 함께 검토 |

## 3. 적용할 것 상위 (우선순위)

1. **Galmuri11 Bold 한글 TMP 폴백** — Project B 에서 복사(위 2-2). 한글 글꼴 부재 해소. 사용자 글꼴 시안 검토 후.
2. **Horror Bundle 공포 레이어 소수 선별**(약 30개) — `SewerImprove.md` S6 소리 레이어, SAN 연동 심박·스팅어.
3. **Pro Sound Collection 발소리·물·맨홀·권총·UI 소수 선별**(약 40개) — 하수도·총포상·강변 소리 갭.
4. **Ultimate Game Music Collection 하수도·공포·병원 BGM**(약 12곡 OGG 변환) — 하수도 깊은 구역과 SAN 위험 단계 곡.
5. **Text Animator·DOTween Pro 유지 + SAN 연출 태그 세트 정의** — 신규 임포트 없음. 비용 0.

(그래픽 쪽은 보유 에셋에 쓸 만한 것이 없다. UI 프레임·딥원·수첩은 `ArtInventory.md` 5절의 자체 제작 목록을 따른다.)

## 4. 트랙 C 도구 판단 (`Architecture.md` 트랙 C 재설계 기준)

**결론: Dialogue System for Unity 는 도입하지 않고 현 설계(Core `DialogueRunner` + Topic JSON + Text Animator View)를 유지한다. Master Audio 는 지금 도입하지 않고, 오디오는 작은 `AudioService` 로 정리한다. DOTween Pro·Text Animator 는 그대로 쓴다.**

근거:
1. **중복·충돌**: P8~P12(이행 완료)에서 `DialogueRunner`·Topic JSON·`StoryDatabase`·`Condition`/`Effect`/`ConditionParser`·백로그·읽은 대사 스킵까지 순수 C#(Core)로 만들고 동치 테스트를 끝냈다. Dialogue System 은 자체 DB(ScriptableObject)·Lua 조건·Dialogue UI·세이브 시스템(Pixel Crushers)을 가져와 구조가 겹치고, 변환 비용이 이미 지불한 설계 비용을 넘는다.
2. **세이브 호환**: 출시판 Easy Save 3 키를 깨지 않는 것이 최우선이다. Dialogue System 의 변수·세이브는 ES3 와 브리지가 필요해 새 실패 지점을 만든다. 현재 `GameState`(Core) → `SaveService`(ES3) 한 경로가 더 단순하다.
3. **Core 순수성**: `SCoW.Core` 는 `noEngineReferences` 라서 Dialogue System 타입을 못 본다. 이를 쓰면 `DialogueRunner` 를 Assembly-CSharp 으로 내려야 해 테스트(`dotnet test`)를 잃는다.
4. **가진 도구로 충분**: 타자기·태그(Text Animator), 선택지·초상 연출(DOTween), 문구(I2). 부족분은 CoC 판정 연출(주사위 롤렛)처럼 게임 고유라서 어차피 직접 구현이다.
5. **Unity 6 호환 미검증**: Dialogue System·Master Audio 는 SCoW 에 없고 캐시에도 없어 호환을 확인한 적이 없다(Project B 는 Unity 2021 계열).
6. **Master Audio**: 현재 `SoundManager`(463줄, `AudioClip` 필드 57개)는 인스펙터 참조로 묶여 있다. Master Audio 는 버스·플레이리스트·덕킹·거리 감쇠를 주지만, 필요한 기능(SAN 별 BGM 전환, 하수도 소리 레이어 3~4종, 대사 중 덕킹)은 Unity `AudioMixer`(그룹 + `SetFloat`)와 오브젝트 풀 `AudioSource` 로 충분하고 추가 의존성이 없다. 소리 레이어가 7종을 넘거나 3D 거리 감쇠가 필요해지면(`SewerImprove.md` S6 구현 중) 그때 재평가한다.

### Architecture.md 에 넣을 문장 초안 (직접 고치지 않음)

2-4(대화 데이터 주도) 끝에 추가:

> - **대화 미들웨어는 쓰지 않는다.** Dialogue System for Unity·Yarn Spinner 류는 도입하지 않는다. `DialogueRunner`(Core)가 진행·조건·효과를 소유하고, 표현은 View 가 `TextAnimatorPlayer`(타자기·태그 연출)와 DOTween(선택지·초상 연출, 모든 트윈 `SetLink`)으로 한정한다. 근거: Core 순수성(`noEngineReferences`), ES3 세이브 단일 경로, 동치 테스트 유지. 근거 상세는 `Docs/AssetPlan.md` 4절.
> - **SAN 연출 태그 세트**: SAN 단계(안정/불안/위험)가 View 에 `TextStyle`(예: 기본 / `<wave a=.2>` / `<shake a=.6>`+글리치 색)을 전달한다. 상태는 View 가 읽기만 한다.

2-6(장르 기본 기능) 또는 신규 2-9(사운드)에 추가:

> - **사운드**: 호출은 `ISoundService`(전역 정적 `SoundManager` 직접 호출 금지)로 모으고 구현은 `AudioMixer` 그룹(Master/Bgm/Sfx/Ambience/Voice)과 `AudioSource` 풀로 한다. 볼륨은 `SettingsData.SoundVolume`·`SfxVolume`(기존 키 유지)을 믹서에 적용한다. 대사 중 BGM 덕킹은 `DialogueRunner` 의 시작·종료 이벤트를 `WorldBinder` 가 받아 믹서 스냅샷을 전환하는 방식이다. 새 클립은 `Assets/Sound/<Bgm|Sfx|Ambience>/` 에 필요한 것만 두고 `ArtStyle.md`·`AssetPlan.md` 3절 목록을 따른다. Master Audio 는 도입하지 않는다(소리 레이어 7종 초과 또는 3D 거리 감쇠가 필요해질 때 재평가).

## 5. Unity 6(6000.4) 호환 위험도

| 에셋 | 위험 | 근거·메모 |
|---|---|---|
| Text Animator(Febucci) | **낮음** | 이미 SCoW 에서 동작(P2b 서드파티 대응 완료), asmdef 있음 |
| DOTween·DOTweenPro | 낮음 | 이미 사용 중. DOTween Pro 설치됨(`Assets/Plugins/Demigiant/DOTweenPro`) |
| I2 Localization | 낮음 | 이미 사용 중 |
| Easy Save 3 | 낮음 | 이미 사용 중 |
| 오디오 팩(wav) 전부 | **없음** | 에셋은 데이터라 호환 문제 없음. 크기만 주의(wav 는 OGG 변환 + Vorbis 압축 + 스트리밍) |
| Galmuri TMP 폴백 | 낮음 | Project B 에서 Unity 2021 기준으로 만든 TMP 에셋이므로 Unity 6 의 TMP 3.x/ugui 2.0 에서 **재생성 권장**(저장소는 빈 아틀라스로 커밋) |
| All In 1 Sprite Shader | 중간 | URP 2D 17.4 에서 셰이더 그래프 변환·`Image` 머티리얼 호환 미검증. 시험 후 도입 |
| Feel | 중간 | 에디터 확장이 많아 Unity 6 경고·API 변경 가능성. 필요 시 부분 임포트 |
| Odin Inspector | 중간 | Unity 6 용 3.3.x 가 필요, 캐시 버전 확인 필요 |
| Smart Lighting 2D | 높음 | 자체 렌더링·구형 URP 코드. 도입 안 함 |
| Dialogue System·Master Audio | 미확인 | 도입 안 함(4절) |

## 6. 진행 절차

1. 사용자가 이 문서의 "적용할 것"(3절)과 글꼴 시안(2-2)을 승인.
2. 승인된 파일만 `.unitypackage` 에서 필요한 `asset`/`asset.meta` 를 꺼내 `Assets/ThirdParty/<이름>/`(오디오는 `Assets/Sound/Ambience|Sfx|Bgm/`)에 둔다. 임포트 전에 `Assets/Sound` 폴더 구조를 `unity-traps` 로 확인(meta GUID·개행).
3. 소리는 연결 코드(S6 등)와 같이 넣고, 플레이 모드에서 청취 확인(미청취는 "미검증"으로 기록).
4. 그래픽·글꼴은 스크린샷 비교 후 사용자 승인 뒤에만 기존 에셋을 교체.
5. 결과를 `Docs/Handoff/state.md` 에 3~5줄로 남기고 `Reference/AssetInventory.md` 가 아닌 이 문서를 갱신한다(원본 인벤토리는 Project B 기록으로 남김).

## 7. 미확인·한계

- Ultimate SFX Music Bundle(10GB)·SoundBits(0.6GB)는 목록 미조사.
- Game Music Treasury 곡의 정확한 폴더 하위 경로는 임포트 시 확인.
- 오디오는 이름으로만 판단(청취 안 함). 후보는 임포트 후 사용자가 듣고 고르는 것이 전제.
- 라이선스는 Asset Store 표준 EULA 로 가정(상업 사용 가능). MapoAgape·Galmuri 는 별도 라이선스 확인 필요(Galmuri 는 OFL 라이선스 파일이 Project B 에 있음).
