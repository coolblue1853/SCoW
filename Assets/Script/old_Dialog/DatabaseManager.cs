using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SCoW.Core.State;
public class DataBaseManager : MonoBehaviour
{
    // P3 파사드: 영구 상태(플래그·카운터·텍스트)는 GameRoot.State(GameState)가 보관한다. 아래 프로퍼티는 옛 정적 필드와 같은 이름·소스 호환.
    // 일시·화면·설정 상태는 정적 필드로 남아 있다(ResetTransientStatics). 분류표는 Architecture.md 3절.
    private static GameState S => GameRoot.Instance.State;
    public static bool isNightmare { get => S.GetFlag(FlagKeys.isNightmare); set => S.SetFlag(FlagKeys.isNightmare, value); }

    public static bool endingDoorCheck { get => S.GetFlag(FlagKeys.endingDoorCheck); set => S.SetFlag(FlagKeys.endingDoorCheck, value); }
    public static bool endingDeathCheck { get => S.GetFlag(FlagKeys.endingDeathCheck); set => S.SetFlag(FlagKeys.endingDeathCheck, value); }
    public static int enemyDeathCount { get => S.GetCounter(CounterKeys.enemyDeathCount); set => S.SetCounter(CounterKeys.enemyDeathCount, value); }
    public static bool endingRequestCheck { get => S.GetFlag(FlagKeys.endingRequestCheck); set => S.SetFlag(FlagKeys.endingRequestCheck, value); }
    public static bool endingSwainDeathCheck { get => S.GetFlag(FlagKeys.endingSwainDeathCheck); set => S.SetFlag(FlagKeys.endingSwainDeathCheck, value); }
    public static bool endingAfterPromiseDialog { get => S.GetFlag(FlagKeys.endingAfterPromiseDialog); set => S.SetFlag(FlagKeys.endingAfterPromiseDialog, value); }
    public static bool endingisPromise { get => S.GetFlag(FlagKeys.endingisPromise); set => S.SetFlag(FlagKeys.endingisPromise, value); }
    public static bool endingBreakSymbol { get => S.GetFlag(FlagKeys.endingBreakSymbol); set => S.SetFlag(FlagKeys.endingBreakSymbol, value); }

    public static int endingLadderSanInt { get => S.GetCounter(CounterKeys.endingLadderSanInt); set => S.SetCounter(CounterKeys.endingLadderSanInt, value); }
    public static bool endingLadderSan { get => S.GetFlag(FlagKeys.endingLadderSan); set => S.SetFlag(FlagKeys.endingLadderSan, value); }
    public static bool endingSwin { get => S.GetFlag(FlagKeys.endingSwin); set => S.SetFlag(FlagKeys.endingSwin, value); }

    public static bool sewerMap { get => S.GetFlag(FlagKeys.sewerMap); set => S.SetFlag(FlagKeys.sewerMap, value); }
    public static bool ifGotRequset { get => S.GetFlag(FlagKeys.ifGotRequset); set => S.SetFlag(FlagKeys.ifGotRequset, value); }
    //디버프 3단계
    public static bool sewerFirstIn { get => S.GetFlag(FlagKeys.sewerFirstIn); set => S.SetFlag(FlagKeys.sewerFirstIn, value); }
    public static bool sewerFirstInSanCheck { get => S.GetFlag(FlagKeys.sewerFirstInSanCheck); set => S.SetFlag(FlagKeys.sewerFirstInSanCheck, value); }
    public static bool sewerDeepOneFirstSanCheck { get => S.GetFlag(FlagKeys.sewerDeepOneFirstSanCheck); set => S.SetFlag(FlagKeys.sewerDeepOneFirstSanCheck, value); }

    public static bool isDebuffNightPhobia { get => S.GetFlag(FlagKeys.isDebuffNightPhobia); set => S.SetFlag(FlagKeys.isDebuffNightPhobia, value); }
    public static bool isDebuffDeftness { get => S.GetFlag(FlagKeys.isDebuffDeftness); set => S.SetFlag(FlagKeys.isDebuffDeftness, value); }
    public static bool isDebuffVisionLoss { get => S.GetFlag(FlagKeys.isDebuffVisionLoss); set => S.SetFlag(FlagKeys.isDebuffVisionLoss, value); }
    public static bool isDebuffShortTempred { get => S.GetFlag(FlagKeys.isDebuffShortTempred); set => S.SetFlag(FlagKeys.isDebuffShortTempred, value); }
    public static bool isDebuffDyslexia { get => S.GetFlag(FlagKeys.isDebuffDyslexia); set => S.SetFlag(FlagKeys.isDebuffDyslexia, value); }
    public static bool isDebuffDrugPhobia { get => S.GetFlag(FlagKeys.isDebuffDrugPhobia); set => S.SetFlag(FlagKeys.isDebuffDrugPhobia, value); }
    public static bool isDebuffCognitiveDisorder { get => S.GetFlag(FlagKeys.isDebuffCognitiveDisorder); set => S.SetFlag(FlagKeys.isDebuffCognitiveDisorder, value); }
    public static bool isDebuffCognitiveBreakdown { get => S.GetFlag(FlagKeys.isDebuffCognitiveBreakdown); set => S.SetFlag(FlagKeys.isDebuffCognitiveBreakdown, value); }
    public static bool isDebuffPerfectionism { get => S.GetFlag(FlagKeys.isDebuffPerfectionism); set => S.SetFlag(FlagKeys.isDebuffPerfectionism, value); }

    public static bool sewerPicingSucc { get => S.GetFlag(FlagKeys.sewerPicingSucc); set => S.SetFlag(FlagKeys.sewerPicingSucc, value); }

    public static bool isMorningMapMove { get => S.GetFlag(FlagKeys.isMorningMapMove); set => S.SetFlag(FlagKeys.isMorningMapMove, value); }
    public static bool afterDaveDeathFirstSlum { get => S.GetFlag(FlagKeys.afterDaveDeathFirstSlum); set => S.SetFlag(FlagKeys.afterDaveDeathFirstSlum, value); }

    public static bool iSRoadBattleStart { get => S.GetFlag(FlagKeys.iSRoadBattleStart); set => S.SetFlag(FlagKeys.iSRoadBattleStart, value); }

    public static bool bgmChangeChecker { get => S.GetFlag(FlagKeys.bgmChangeChecker); set => S.SetFlag(FlagKeys.bgmChangeChecker, value); }
    public static bool sewerBattleChecker { get => S.GetFlag(FlagKeys.sewerBattleChecker); set => S.SetFlag(FlagKeys.sewerBattleChecker, value); }
    public static int posionDebuff { get => S.GetCounter(CounterKeys.posionDebuff); set => S.SetCounter(CounterKeys.posionDebuff, value); }

    public static bool symbolStrSanCheck { get => S.GetFlag(FlagKeys.symbolStrSanCheck); set => S.SetFlag(FlagKeys.symbolStrSanCheck, value); }

    public static bool symbolOccultFirst { get => S.GetFlag(FlagKeys.symbolOccultFirst); set => S.SetFlag(FlagKeys.symbolOccultFirst, value); }
    public static bool sewerSymbolOccultCheck { get => S.GetFlag(FlagKeys.sewerSymbolOccultCheck); set => S.SetFlag(FlagKeys.sewerSymbolOccultCheck, value); }

    public static bool bookLookFirst { get => S.GetFlag(FlagKeys.bookLookFirst); set => S.SetFlag(FlagKeys.bookLookFirst, value); }
    public static bool sewerBookOccultCheck { get => S.GetFlag(FlagKeys.sewerBookOccultCheck); set => S.SetFlag(FlagKeys.sewerBookOccultCheck, value); }
    public static bool sewerBookSanCheck { get => S.GetFlag(FlagKeys.sewerBookSanCheck); set => S.SetFlag(FlagKeys.sewerBookSanCheck, value); }

    public static bool journalFirstSanCheck { get => S.GetFlag(FlagKeys.journalFirstSanCheck); set => S.SetFlag(FlagKeys.journalFirstSanCheck, value); }
    public static bool journalSanCheck { get => S.GetFlag(FlagKeys.journalSanCheck); set => S.SetFlag(FlagKeys.journalSanCheck, value); }
    public static bool firstLookJournal { get => S.GetFlag(FlagKeys.firstLookJournal); set => S.SetFlag(FlagKeys.firstLookJournal, value); }

    public static bool gainOldMap { get => S.GetFlag(FlagKeys.gainOldMap); set => S.SetFlag(FlagKeys.gainOldMap, value); }

    public static bool safeDocument { get => S.GetFlag(FlagKeys.safeDocument); set => S.SetFlag(FlagKeys.safeDocument, value); }
    public static bool insmusRhtoric { get => S.GetFlag(FlagKeys.insmusRhtoric); set => S.SetFlag(FlagKeys.insmusRhtoric, value); }
    public static bool isInsmusMeetSewer { get => S.GetFlag(FlagKeys.isInsmusMeetSewer); set => S.SetFlag(FlagKeys.isInsmusMeetSewer, value); }

    public static bool sewerBattleEndCheck { get => S.GetFlag(FlagKeys.sewerBattleEndCheck); set => S.SetFlag(FlagKeys.sewerBattleEndCheck, value); }
    public static bool inSewerStealthFail { get => S.GetFlag(FlagKeys.inSewerStealthFail); set => S.SetFlag(FlagKeys.inSewerStealthFail, value); }
    public static bool inSewerStealthSucc { get => S.GetFlag(FlagKeys.inSewerStealthSucc); set => S.SetFlag(FlagKeys.inSewerStealthSucc, value); }
    public static bool startSewerBattle { get => S.GetFlag(FlagKeys.startSewerBattle); set => S.SetFlag(FlagKeys.startSewerBattle, value); }

    public static bool sewerHouseIn { get => S.GetFlag(FlagKeys.sewerHouseIn); set => S.SetFlag(FlagKeys.sewerHouseIn, value); }
    public static int sewerEnemyCounter { get => S.GetCounter(CounterKeys.sewerEnemyCounter); set => S.SetCounter(CounterKeys.sewerEnemyCounter, value); }

    public static bool potalWait = false;
    public static bool blackRechSuccCanTalck { get => S.GetFlag(FlagKeys.blackRechSuccCanTalck); set => S.SetFlag(FlagKeys.blackRechSuccCanTalck, value); }
    public static bool blackDeftness { get => S.GetFlag(FlagKeys.blackDeftness); set => S.SetFlag(FlagKeys.blackDeftness, value); }
    public static bool blackRhethic { get => S.GetFlag(FlagKeys.blackRhethic); set => S.SetFlag(FlagKeys.blackRhethic, value); }

    public static bool blackFirstDialog { get => S.GetFlag(FlagKeys.blackFirstDialog); set => S.SetFlag(FlagKeys.blackFirstDialog, value); }
    public static bool mapLooked { get => S.GetFlag(FlagKeys.mapLooked); set => S.SetFlag(FlagKeys.mapLooked, value); }
    public static bool mapFirstDialog { get => S.GetFlag(FlagKeys.mapFirstDialog); set => S.SetFlag(FlagKeys.mapFirstDialog, value); }
    public static bool sewerageKey { get => S.GetFlag(FlagKeys.sewerageKey); set => S.SetFlag(FlagKeys.sewerageKey, value); }
    public static bool judgeSafeTryOpen { get => S.GetFlag(FlagKeys.judgeSafeTryOpen); set => S.SetFlag(FlagKeys.judgeSafeTryOpen, value); }
    public static bool judgeCoatTryDisguise { get => S.GetFlag(FlagKeys.judgeCoatTryDisguise); set => S.SetFlag(FlagKeys.judgeCoatTryDisguise, value); }
    public static bool gainCoat { get => S.GetFlag(FlagKeys.gainCoat); set => S.SetFlag(FlagKeys.gainCoat, value); }
    public static bool gainMap { get => S.GetFlag(FlagKeys.gainMap); set => S.SetFlag(FlagKeys.gainMap, value); }
    public static bool wearCoat { get => S.GetFlag(FlagKeys.wearCoat); set => S.SetFlag(FlagKeys.wearCoat, value); }
    public static bool setIntelCharDetail = false;

    public static bool isOpenMap = false;
    public static bool secondisDirecting = false;
    public static bool storyDirecting = false;

    public static bool isSewerOffice { get => S.GetFlag(FlagKeys.isSewerOffice); set => S.SetFlag(FlagKeys.isSewerOffice, value); }
    public static bool isSewerOfficeBrokenWindow { get => S.GetFlag(FlagKeys.isSewerOfficeBrokenWindow); set => S.SetFlag(FlagKeys.isSewerOfficeBrokenWindow, value); }
    public static bool tryBrokenWindow { get => S.GetFlag(FlagKeys.tryBrokenWindow); set => S.SetFlag(FlagKeys.tryBrokenWindow, value); }
    public static bool tryObserLokcer { get => S.GetFlag(FlagKeys.tryObserLokcer); set => S.SetFlag(FlagKeys.tryObserLokcer, value); }
    public static bool moveSewerOfficeWindow { get => S.GetFlag(FlagKeys.moveSewerOfficeWindow); set => S.SetFlag(FlagKeys.moveSewerOfficeWindow, value); }

    // 4일차MorningMove_2nd
    public static int battleEnemyCount { get => S.GetCounter(CounterKeys.battleEnemyCount); set => S.SetCounter(CounterKeys.battleEnemyCount, value); }
    public static bool morningMove2nd { get => S.GetFlag(FlagKeys.morningMove2nd); set => S.SetFlag(FlagKeys.morningMove2nd, value); }
    public static bool morningMove4th { get => S.GetFlag(FlagKeys.morningMove4th); set => S.SetFlag(FlagKeys.morningMove4th, value); }
    public static bool day4Case1San { get => S.GetFlag(FlagKeys.day4Case1San); set => S.SetFlag(FlagKeys.day4Case1San, value); }
    public static bool day4Case1Health { get => S.GetFlag(FlagKeys.day4Case1Health); set => S.SetFlag(FlagKeys.day4Case1Health, value); }

    public static bool sewerDoorOpen { get => S.GetFlag(FlagKeys.sewerDoorOpen); set => S.SetFlag(FlagKeys.sewerDoorOpen, value); }
    public static bool sewerDoorOpenDeftness { get => S.GetFlag(FlagKeys.sewerDoorOpenDeftness); set => S.SetFlag(FlagKeys.sewerDoorOpenDeftness, value); }
    public static bool sewerDoorOpenCheck { get => S.GetFlag(FlagKeys.sewerDoorOpenCheck); set => S.SetFlag(FlagKeys.sewerDoorOpenCheck, value); }
    //
    public static bool morningMove_3rd { get => S.GetFlag(FlagKeys.morningMove_3rd); set => S.SetFlag(FlagKeys.morningMove_3rd, value); }
    public static bool thirdDayDreamSan { get => S.GetFlag(FlagKeys.thirdDayDreamSan); set => S.SetFlag(FlagKeys.thirdDayDreamSan, value); }
    public static bool thirdDayAlbertDialog { get => S.GetFlag(FlagKeys.thirdDayAlbertDialog); set => S.SetFlag(FlagKeys.thirdDayAlbertDialog, value); }
    public static bool thirdDayPoliceADialog { get => S.GetFlag(FlagKeys.thirdDayPoliceADialog); set => S.SetFlag(FlagKeys.thirdDayPoliceADialog, value); }
    public static bool thirdDayPoliceBDialog { get => S.GetFlag(FlagKeys.thirdDayPoliceBDialog); set => S.SetFlag(FlagKeys.thirdDayPoliceBDialog, value); }
    public static bool thirdDayAlanDialog { get => S.GetFlag(FlagKeys.thirdDayAlanDialog); set => S.SetFlag(FlagKeys.thirdDayAlanDialog, value); }
    //2일차
    public static bool barDrinkSanCheck { get => S.GetFlag(FlagKeys.barDrinkSanCheck); set => S.SetFlag(FlagKeys.barDrinkSanCheck, value); }
    // P4: 연출 대기 신호(DialogManager→DirectingManager 1프레임)라 저장하지 않는다. 일시 정적 필드(ResetTransientStatics 에서 초기화).
    public static bool barMove = false;
    public static bool barArrive { get => S.GetFlag(FlagKeys.barArrive); set => S.SetFlag(FlagKeys.barArrive, value); }

    public static bool daveDeathKeyword { get => S.GetFlag(FlagKeys.daveDeathKeyword); set => S.SetFlag(FlagKeys.daveDeathKeyword, value); }
    public static bool albertDeathKeyword { get => S.GetFlag(FlagKeys.albertDeathKeyword); set => S.SetFlag(FlagKeys.albertDeathKeyword, value); }
    /// <summary>알버트가 죽은 것으로 보는 합집합(PT-3): 치명 키워드 밤 소멸 OR 파티 사망. 에필로그 분기가 읽는다.</summary>
    public static bool albertDead { get => albertDeathKeyword || S.GetFlag(SCoW.Core.State.PartyKeys.Dead(SCoW.Core.Rules.CompanionDefs.AlbertId)); }
    public static bool swainDeathKeyword { get => S.GetFlag(FlagKeys.swainDeathKeyword); set => S.SetFlag(FlagKeys.swainDeathKeyword, value); }

    public static bool albertFirstAfterEvent { get => S.GetFlag(FlagKeys.albertFirstAfterEvent); set => S.SetFlag(FlagKeys.albertFirstAfterEvent, value); }

    public static bool daveSafeOpen { get => S.GetFlag(FlagKeys.daveSafeOpen); set => S.SetFlag(FlagKeys.daveSafeOpen, value); }
    public static bool daveAfterSafe { get => S.GetFlag(FlagKeys.daveAfterSafe); set => S.SetFlag(FlagKeys.daveAfterSafe, value); }
    public static bool daveAfterBreclet { get => S.GetFlag(FlagKeys.daveAfterBreclet); set => S.SetFlag(FlagKeys.daveAfterBreclet, value); }
    public static bool daveCheck { get => S.GetFlag(FlagKeys.daveCheck); set => S.SetFlag(FlagKeys.daveCheck, value); }

    public static bool secondDayDialog { get => S.GetFlag(FlagKeys.secondDayDialog); set => S.SetFlag(FlagKeys.secondDayDialog, value); }

    public static bool afterSanCheck { get => S.GetFlag(FlagKeys.afterSanCheck); set => S.SetFlag(FlagKeys.afterSanCheck, value); }
    public static bool case2IntCheck { get => S.GetFlag(FlagKeys.case2IntCheck); set => S.SetFlag(FlagKeys.case2IntCheck, value); }
    public static bool case2SanCheck { get => S.GetFlag(FlagKeys.case2SanCheck); set => S.SetFlag(FlagKeys.case2SanCheck, value); }

    public static bool swainDie { get => S.GetFlag(FlagKeys.swainDie); set => S.SetFlag(FlagKeys.swainDie, value); }
    public static bool kaneSlimeSan { get => S.GetFlag(FlagKeys.kaneSlimeSan); set => S.SetFlag(FlagKeys.kaneSlimeSan, value); }

    //낮 강제 이벤트
    public static bool noonMorningMove { get => S.GetFlag(FlagKeys.noonMorningMove); set => S.SetFlag(FlagKeys.noonMorningMove, value); }
    public static bool secondDetectiveNoonEvent { get => S.GetFlag(FlagKeys.secondDetectiveNoonEvent); set => S.SetFlag(FlagKeys.secondDetectiveNoonEvent, value); }
    public static bool noonEventMovetoClient { get => S.GetFlag(FlagKeys.noonEventMovetoClient); set => S.SetFlag(FlagKeys.noonEventMovetoClient, value); }
    public static bool noonEventClientsFirst { get => S.GetFlag(FlagKeys.noonEventClientsFirst); set => S.SetFlag(FlagKeys.noonEventClientsFirst, value); }
    public static bool noonEventAccessAuthorization { get => S.GetFlag(FlagKeys.noonEventAccessAuthorization); set => S.SetFlag(FlagKeys.noonEventAccessAuthorization, value); }
    public static bool witnessFirstDailog { get => S.GetFlag(FlagKeys.witnessFirstDailog); set => S.SetFlag(FlagKeys.witnessFirstDailog, value); }
    public static bool doorPoliceFirstDailog { get => S.GetFlag(FlagKeys.doorPoliceFirstDailog); set => S.SetFlag(FlagKeys.doorPoliceFirstDailog, value); }
    public static bool noonEventFirstDoor { get => S.GetFlag(FlagKeys.noonEventFirstDoor); set => S.SetFlag(FlagKeys.noonEventFirstDoor, value); }
    public static bool noonEventSanCheck { get => S.GetFlag(FlagKeys.noonEventSanCheck); set => S.SetFlag(FlagKeys.noonEventSanCheck, value); }
    public static bool insidePoliceFirstDailog { get => S.GetFlag(FlagKeys.insidePoliceFirstDailog); set => S.SetFlag(FlagKeys.insidePoliceFirstDailog, value); }
    public static bool deadBodyFirstLook { get => S.GetFlag(FlagKeys.deadBodyFirstLook); set => S.SetFlag(FlagKeys.deadBodyFirstLook, value); }
    public static bool deadBodyLookMissingDialog { get => S.GetFlag(FlagKeys.deadBodyLookMissingDialog); set => S.SetFlag(FlagKeys.deadBodyLookMissingDialog, value); }
    public static bool deadBodyLookFishySmellDialog { get => S.GetFlag(FlagKeys.deadBodyLookFishySmellDialog); set => S.SetFlag(FlagKeys.deadBodyLookFishySmellDialog, value); }

    public static bool deadBodyMedicine { get => S.GetFlag(FlagKeys.deadBodyMedicine); set => S.SetFlag(FlagKeys.deadBodyMedicine, value); }
    public static bool deadBodyObservational { get => S.GetFlag(FlagKeys.deadBodyObservational); set => S.SetFlag(FlagKeys.deadBodyObservational, value); }
    public static bool deadBodyAnalyzing { get => S.GetFlag(FlagKeys.deadBodyAnalyzing); set => S.SetFlag(FlagKeys.deadBodyAnalyzing, value); }
    public static bool deadBodyDeftness { get => S.GetFlag(FlagKeys.deadBodyDeftness); set => S.SetFlag(FlagKeys.deadBodyDeftness, value); }
    public static bool deadBodyGotoSwere { get => S.GetFlag(FlagKeys.deadBodyGotoSwere); set => S.SetFlag(FlagKeys.deadBodyGotoSwere, value); }

    public static bool siteAnalyzing { get => S.GetFlag(FlagKeys.siteAnalyzing); set => S.SetFlag(FlagKeys.siteAnalyzing, value); }
    //Sanity
    public static int debuff { get => S.GetCounter(CounterKeys.debuff); set => S.SetCounter(CounterKeys.debuff, value); }
    public static bool thinWallets { get => S.GetFlag(FlagKeys.thinWallets); set => S.SetFlag(FlagKeys.thinWallets, value); }
    public static bool carelessness { get => S.GetFlag(FlagKeys.carelessness); set => S.SetFlag(FlagKeys.carelessness, value); }
    public static bool debilitation { get => S.GetFlag(FlagKeys.debilitation); set => S.SetFlag(FlagKeys.debilitation, value); }
    public static bool dizziness { get => S.GetFlag(FlagKeys.dizziness); set => S.SetFlag(FlagKeys.dizziness, value); }
    public static bool sprains { get => S.GetFlag(FlagKeys.sprains); set => S.SetFlag(FlagKeys.sprains, value); }
    public static bool unlucky { get => S.GetFlag(FlagKeys.unlucky); set => S.SetFlag(FlagKeys.unlucky, value); }
    public static bool musclePain { get => S.GetFlag(FlagKeys.musclePain); set => S.SetFlag(FlagKeys.musclePain, value); }
    public static bool migraines { get => S.GetFlag(FlagKeys.migraines); set => S.SetFlag(FlagKeys.migraines, value); }
    public static bool eyeDisease { get => S.GetFlag(FlagKeys.eyeDisease); set => S.SetFlag(FlagKeys.eyeDisease, value); }

    public static bool masochism { get => S.GetFlag(FlagKeys.masochism); set => S.SetFlag(FlagKeys.masochism, value); }
    public static bool careless { get => S.GetFlag(FlagKeys.careless); set => S.SetFlag(FlagKeys.careless, value); }
    public static bool mentalWeakness { get => S.GetFlag(FlagKeys.mentalWeakness); set => S.SetFlag(FlagKeys.mentalWeakness, value); }
    public static bool helplessness { get => S.GetFlag(FlagKeys.helplessness); set => S.SetFlag(FlagKeys.helplessness, value); }
    public static bool extravagant { get => S.GetFlag(FlagKeys.extravagant); set => S.SetFlag(FlagKeys.extravagant, value); }
    public static bool panicAttack { get => S.GetFlag(FlagKeys.panicAttack); set => S.SetFlag(FlagKeys.panicAttack, value); }
    public static bool medicaldistrust { get => S.GetFlag(FlagKeys.medicaldistrust); set => S.SetFlag(FlagKeys.medicaldistrust, value); }
    public static bool hallucinations { get => S.GetFlag(FlagKeys.hallucinations); set => S.SetFlag(FlagKeys.hallucinations, value); }
    public static bool tightwad { get => S.GetFlag(FlagKeys.tightwad); set => S.SetFlag(FlagKeys.tightwad, value); }

    public static bool nightPhobia { get => S.GetFlag(FlagKeys.nightPhobia); set => S.SetFlag(FlagKeys.nightPhobia, value); }
    public static bool deafness { get => S.GetFlag(FlagKeys.deafness); set => S.SetFlag(FlagKeys.deafness, value); }
    public static bool visionLoss { get => S.GetFlag(FlagKeys.visionLoss); set => S.SetFlag(FlagKeys.visionLoss, value); }
    public static bool shortTempered { get => S.GetFlag(FlagKeys.shortTempered); set => S.SetFlag(FlagKeys.shortTempered, value); }
    public static bool dyslexia { get => S.GetFlag(FlagKeys.dyslexia); set => S.SetFlag(FlagKeys.dyslexia, value); }
    public static bool drugPhobia { get => S.GetFlag(FlagKeys.drugPhobia); set => S.SetFlag(FlagKeys.drugPhobia, value); }
    public static bool cognitiveDisorder { get => S.GetFlag(FlagKeys.cognitiveDisorder); set => S.SetFlag(FlagKeys.cognitiveDisorder, value); }
    public static bool cognitiveBreakdown { get => S.GetFlag(FlagKeys.cognitiveBreakdown); set => S.SetFlag(FlagKeys.cognitiveBreakdown, value); }
    public static bool homicidalImpulse { get => S.GetFlag(FlagKeys.homicidalImpulse); set => S.SetFlag(FlagKeys.homicidalImpulse, value); }
    public static bool perfectionism { get => S.GetFlag(FlagKeys.perfectionism); set => S.SetFlag(FlagKeys.perfectionism, value); }

    //Inventory
    private static bool _isItemUI = false;
    /// <summary>GameRoot.StateChanged 로 알리는 일시 키 이름(isItemUI).</summary>
    public const string ItemUiKey = "isItemUI";
    /// <summary>인벤토리(아이템 UI) 열림. 바뀔 때 GameRoot.StateChanged(ItemUiKey)를 올린다(DialogDatabaseManager 가 아이템 CSV 로 바꿨다 되돌린다, C-2 P12·P13).</summary>
    public static bool isItemUI
    {
        get => _isItemUI;
        set
        {
            if (_isItemUI == value) { return; }
            _isItemUI = value;
            GameRoot.NotifyTransient(ItemUiKey);
        }
    }
    public static bool isUseSafe { get => S.GetFlag(FlagKeys.isUseSafe); set => S.SetFlag(FlagKeys.isUseSafe, value); }

    public static string nowItem = "";
    public static int bullet { get => S.GetCounter(CounterKeys.bullet); set => S.SetCounter(CounterKeys.bullet, value); }
    public static int shotgun { get => S.GetCounter(CounterKeys.shotgun); set => S.SetCounter(CounterKeys.shotgun, value); }
    public static int rifle { get => S.GetCounter(CounterKeys.rifle); set => S.SetCounter(CounterKeys.rifle, value); }
    public static int revolver { get => S.GetCounter(CounterKeys.revolver); set => S.SetCounter(CounterKeys.revolver, value); }
    public static int smallPistol { get => S.GetCounter(CounterKeys.smallPistol); set => S.SetCounter(CounterKeys.smallPistol, value); }
    public static int bat { get => S.GetCounter(CounterKeys.bat); set => S.SetCounter(CounterKeys.bat, value); }
    public static int axe { get => S.GetCounter(CounterKeys.axe); set => S.SetCounter(CounterKeys.axe, value); }
    public static int molotov { get => S.GetCounter(CounterKeys.molotov); set => S.SetCounter(CounterKeys.molotov, value); }
    public static int dagger { get => S.GetCounter(CounterKeys.dagger); set => S.SetCounter(CounterKeys.dagger, value); }

    public static int pistolAmmo { get => S.GetCounter(CounterKeys.pistolAmmo); set => S.SetCounter(CounterKeys.pistolAmmo, value); }
    public static int rifleAmmo { get => S.GetCounter(CounterKeys.rifleAmmo); set => S.SetCounter(CounterKeys.rifleAmmo, value); }
    public static int shotgunAmmo { get => S.GetCounter(CounterKeys.shotgunAmmo); set => S.SetCounter(CounterKeys.shotgunAmmo, value); }

    public static int firstaidkit { get => S.GetCounter(CounterKeys.firstaidkit); set => S.SetCounter(CounterKeys.firstaidkit, value); }
    public static int bandages { get => S.GetCounter(CounterKeys.bandages); set => S.SetCounter(CounterKeys.bandages, value); }
    public static int painkillers { get => S.GetCounter(CounterKeys.painkillers); set => S.SetCounter(CounterKeys.painkillers, value); }

    public static bool conntectMouse = false;

    //SoundManager
    public static float soundVolume = 0.5f;
    public static float sfxVolume = 0.5f;
    public static bool workSound = false;

    //Battle
    public static bool battleSan = false; // P4: 일시(저장 제외)
    public static bool isBattleBeforeDialog { get => S.GetFlag(FlagKeys.isBattleBeforeDialog); set => S.SetFlag(FlagKeys.isBattleBeforeDialog, value); }
    public static bool afterBattle { get => S.GetFlag(FlagKeys.afterBattle); set => S.SetFlag(FlagKeys.afterBattle, value); }
    public static bool isBattleRollet = false;
    public static bool endBattle { get => S.GetFlag(FlagKeys.endBattle); set => S.SetFlag(FlagKeys.endBattle, value); }
    public static bool roadBattleEnd { get => S.GetFlag(FlagKeys.roadBattleEnd); set => S.SetFlag(FlagKeys.roadBattleEnd, value); }

    // Map
    // nowPlace 가 바뀌면 GameState 가 변경을 알린다(GameRoot.StateChanged, C-2 P13). DialogDatabaseManager·WorldBinder 가 구독.
    public static string nowPlace { get => S.GetText(TextKeys.nowPlace); set => S.SetText(TextKeys.nowPlace, value); }
    public static string potalPlace { get => S.GetText(TextKeys.potalPlace); set => S.SetText(TextKeys.potalPlace, value); }

    //keyword
    public static string keywordDowner;

    //Directing
    public static bool isDirecting = false;
    public static bool firstClientsHouse { get => S.GetFlag(FlagKeys.firstClientsHouse); set => S.SetFlag(FlagKeys.firstClientsHouse, value); }
    public static bool firstClientsHouseEnd { get => S.GetFlag(FlagKeys.firstClientsHouseEnd); set => S.SetFlag(FlagKeys.firstClientsHouseEnd, value); }

    //1일차 술집
    public static bool isBar { get => S.GetFlag(FlagKeys.isBar); set => S.SetFlag(FlagKeys.isBar, value); }
    public static bool maeveFirstDialog { get => S.GetFlag(FlagKeys.maeveFirstDialog); set => S.SetFlag(FlagKeys.maeveFirstDialog, value); }
    public static bool workerFirstDialog { get => S.GetFlag(FlagKeys.workerFirstDialog); set => S.SetFlag(FlagKeys.workerFirstDialog, value); }

    //1일차 빈민가
    public static bool daveFirstDialog { get => S.GetFlag(FlagKeys.daveFirstDialog); set => S.SetFlag(FlagKeys.daveFirstDialog, value); }

    //1일차 경찰서
    public static bool aFirstDialog { get => S.GetFlag(FlagKeys.aFirstDialog); set => S.SetFlag(FlagKeys.aFirstDialog, value); }
    public static bool bFirstDialog { get => S.GetFlag(FlagKeys.bFirstDialog); set => S.SetFlag(FlagKeys.bFirstDialog, value); }
    public static bool albertFirstDialog { get => S.GetFlag(FlagKeys.albertFirstDialog); set => S.SetFlag(FlagKeys.albertFirstDialog, value); }

    //1일차 병원
    public static bool alanFirstDialog { get => S.GetFlag(FlagKeys.alanFirstDialog); set => S.SetFlag(FlagKeys.alanFirstDialog, value); }
    public static bool kateDisapear { get => S.GetFlag(FlagKeys.kateDisapear); set => S.SetFlag(FlagKeys.kateDisapear, value); }
    public static bool kateNoonFirstDialog { get => S.GetFlag(FlagKeys.kateNoonFirstDialog); set => S.SetFlag(FlagKeys.kateNoonFirstDialog, value); }
    public static bool alanNoonFirstDialog { get => S.GetFlag(FlagKeys.alanNoonFirstDialog); set => S.SetFlag(FlagKeys.alanNoonFirstDialog, value); }

    //1일차 강가
    public static bool kaneFirstDialog { get => S.GetFlag(FlagKeys.kaneFirstDialog); set => S.SetFlag(FlagKeys.kaneFirstDialog, value); }
    public static bool sewerFirstDialog { get => S.GetFlag(FlagKeys.sewerFirstDialog); set => S.SetFlag(FlagKeys.sewerFirstDialog, value); }
    public static bool sewerObservation { get => S.GetFlag(FlagKeys.sewerObservation); set => S.SetFlag(FlagKeys.sewerObservation, value); }
    public static bool sewerObservationEnd { get => S.GetFlag(FlagKeys.sewerObservationEnd); set => S.SetFlag(FlagKeys.sewerObservationEnd, value); }
    public static bool sewerSan { get => S.GetFlag(FlagKeys.sewerSan); set => S.SetFlag(FlagKeys.sewerSan, value); }

    //1일차 대학
    public static bool univFirstDialog { get => S.GetFlag(FlagKeys.univFirstDialog); set => S.SetFlag(FlagKeys.univFirstDialog, value); }

    //1일차 신문사
    public static bool swainFirstDialog { get => S.GetFlag(FlagKeys.swainFirstDialog); set => S.SetFlag(FlagKeys.swainFirstDialog, value); }
    public static bool isSwainTalkMissingpeople { get => S.GetFlag(FlagKeys.isSwainTalkMissingpeople); set => S.SetFlag(FlagKeys.isSwainTalkMissingpeople, value); }

    //1일차 의뢰자의 집
    public static bool aidenFirstDialog { get => S.GetFlag(FlagKeys.aidenFirstDialog); set => S.SetFlag(FlagKeys.aidenFirstDialog, value); }
    public static bool aidenObservation { get => S.GetFlag(FlagKeys.aidenObservation); set => S.SetFlag(FlagKeys.aidenObservation, value); }
    public static bool aidenMedicine { get => S.GetFlag(FlagKeys.aidenMedicine); set => S.SetFlag(FlagKeys.aidenMedicine, value); }
    public static bool aidenPsychotherapy { get => S.GetFlag(FlagKeys.aidenPsychotherapy); set => S.SetFlag(FlagKeys.aidenPsychotherapy, value); }
    public static bool ellaEndDialog { get => S.GetFlag(FlagKeys.ellaEndDialog); set => S.SetFlag(FlagKeys.ellaEndDialog, value); }
    public static bool isFirstDoor { get => S.GetFlag(FlagKeys.isFirstDoor); set => S.SetFlag(FlagKeys.isFirstDoor, value); }
    public static bool isFirst2st { get => S.GetFlag(FlagKeys.isFirst2st); set => S.SetFlag(FlagKeys.isFirst2st, value); }
    public static bool isFirstRoom { get => S.GetFlag(FlagKeys.isFirstRoom); set => S.SetFlag(FlagKeys.isFirstRoom, value); }
    public static bool getSafe { get => S.GetFlag(FlagKeys.getSafe); set => S.SetFlag(FlagKeys.getSafe, value); }
    public static bool strDialogOn = false; // P4: 일시(저장 제외)
    public static int aidenKeywordCount { get => S.GetCounter(CounterKeys.aidenKeywordCount); set => S.SetCounter(CounterKeys.aidenKeywordCount, value); }
    public static bool failTwoKeyword { get => S.GetFlag(FlagKeys.failTwoKeyword); set => S.SetFlag(FlagKeys.failTwoKeyword, value); }

    //1일차 오전 탐정사무소
    public static bool endDemo { get => S.GetFlag(FlagKeys.endDemo); set => S.SetFlag(FlagKeys.endDemo, value); }
    public static bool endDay { get => S.GetFlag(FlagKeys.endDay); set => S.SetFlag(FlagKeys.endDay, value); }
    public static bool connectKey { get => S.GetFlag(FlagKeys.connectKey); set => S.SetFlag(FlagKeys.connectKey, value); }
    public static bool fstDetectiveEndFirstDialog { get => S.GetFlag(FlagKeys.fstDetectiveEndFirstDialog); set => S.SetFlag(FlagKeys.fstDetectiveEndFirstDialog, value); }
    public static bool fstDetectiveNockDialog { get => S.GetFlag(FlagKeys.fstDetectiveNockDialog); set => S.SetFlag(FlagKeys.fstDetectiveNockDialog, value); }
    public static bool fstDetectiveAfterSelect { get => S.GetFlag(FlagKeys.fstDetectiveAfterSelect); set => S.SetFlag(FlagKeys.fstDetectiveAfterSelect, value); }
    public static bool fstDetectiveAfterSelectNo { get => S.GetFlag(FlagKeys.fstDetectiveAfterSelectNo); set => S.SetFlag(FlagKeys.fstDetectiveAfterSelectNo, value); }
    public static bool fstDetectiveEndSelect { get => S.GetFlag(FlagKeys.fstDetectiveEndSelect); set => S.SetFlag(FlagKeys.fstDetectiveEndSelect, value); }

    //Select
    public static bool selectionOn = false;

    //Judge
    public static bool fstDetectiveNewspaper { get => S.GetFlag(FlagKeys.fstDetectiveNewspaper); set => S.SetFlag(FlagKeys.fstDetectiveNewspaper, value); }

    //Battle
    public static string battleWeapon { get => S.GetText(TextKeys.battleWeapon); set => S.SetText(TextKeys.battleWeapon, value); }
    public static int nowSmallPistol { get => S.GetCounter(CounterKeys.nowSmallPistol); set => S.SetCounter(CounterKeys.nowSmallPistol, value); }
    public static int nowRevolver { get => S.GetCounter(CounterKeys.nowRevolver); set => S.SetCounter(CounterKeys.nowRevolver, value); }
    public static int nowRifle { get => S.GetCounter(CounterKeys.nowRifle); set => S.SetCounter(CounterKeys.nowRifle, value); }
    public static int nowShotgun { get => S.GetCounter(CounterKeys.nowShotgun); set => S.SetCounter(CounterKeys.nowShotgun, value); }

    //Map
    public static int timeCount { get => S.GetCounter(CounterKeys.timeCount); set => S.SetCounter(CounterKeys.timeCount, value); }
    public static int fstDetectivTimeOn { get => S.GetCounter(CounterKeys.fstDetectivTimeOn); set => S.SetCounter(CounterKeys.fstDetectivTimeOn, value); }

    //Rollet
    public static bool isRollet;
    public static string condition = "Nomal";

    //dialog
    public static string selectObject;
    public static bool isSelect;
    public static bool cancelJudge;
    public static bool isKeyword;
    public static bool isJudge;
    public static string lookObject = "";
    public static string nowSelecter = "End"; // End_Look_Judge 존재
    public static bool isScene;
    public static bool isPresentation;
    public static bool isOption;
    public static bool endDialogLine;
    public static bool skipActive;
    public static float textDelay = 0.07f;
    public static bool isActiveDialog1;
    public static bool isActiveDialog2;

    // stat
    public static int str { get => S.GetCounter(CounterKeys.str); set => S.SetCounter(CounterKeys.str, value); }
    public static int intl { get => S.GetCounter(CounterKeys.intl); set => S.SetCounter(CounterKeys.intl, value); }
    public static int dex { get => S.GetCounter(CounterKeys.dex); set => S.SetCounter(CounterKeys.dex, value); }
    public static int hp { get => S.GetCounter(CounterKeys.hp); set => S.SetCounter(CounterKeys.hp, value); }
    public static int mp { get => S.GetCounter(CounterKeys.mp); set => S.SetCounter(CounterKeys.mp, value); }
    public static int san { get => S.GetCounter(CounterKeys.san); set => S.SetCounter(CounterKeys.san, value); }
    public static int luk { get => S.GetCounter(CounterKeys.luk); set => S.SetCounter(CounterKeys.luk, value); }
    public static int weal { get => S.GetCounter(CounterKeys.weal); set => S.SetCounter(CounterKeys.weal, value); }
    public static int nowHP { get => S.GetCounter(CounterKeys.nowHP); set => S.SetCounter(CounterKeys.nowHP, value); }
    public static int nowSan { get => S.GetCounter(CounterKeys.nowSan); set => S.SetCounter(CounterKeys.nowSan, value); }

    //skill -str
    public static int strSkillPoint { get => S.GetCounter(CounterKeys.strSkillPoint); set => S.SetCounter(CounterKeys.strSkillPoint, value); }
    public static int martialArtsPoint { get => S.GetCounter(CounterKeys.martialArtsPoint); set => S.SetCounter(CounterKeys.martialArtsPoint, value); }
    public static int gunShotPoint { get => S.GetCounter(CounterKeys.gunShotPoint); set => S.SetCounter(CounterKeys.gunShotPoint, value); }
    public static int swordPoint { get => S.GetCounter(CounterKeys.swordPoint); set => S.SetCounter(CounterKeys.swordPoint, value); }
    public static int observationPoint { get => S.GetCounter(CounterKeys.observationPoint); set => S.SetCounter(CounterKeys.observationPoint, value); }
    public static int swimingPoint { get => S.GetCounter(CounterKeys.swimingPoint); set => S.SetCounter(CounterKeys.swimingPoint, value); }

    //skill -int
    public static int intSkillPoint { get => S.GetCounter(CounterKeys.intSkillPoint); set => S.SetCounter(CounterKeys.intSkillPoint, value); }
    public static int medicinePoint { get => S.GetCounter(CounterKeys.medicinePoint); set => S.SetCounter(CounterKeys.medicinePoint, value); }
    public static int analysisPoint { get => S.GetCounter(CounterKeys.analysisPoint); set => S.SetCounter(CounterKeys.analysisPoint, value); }
    public static int listeningPoint { get => S.GetCounter(CounterKeys.listeningPoint); set => S.SetCounter(CounterKeys.listeningPoint, value); }
    public static int psychotherapyPoint { get => S.GetCounter(CounterKeys.psychotherapyPoint); set => S.SetCounter(CounterKeys.psychotherapyPoint, value); }
    public static int occultPoint { get => S.GetCounter(CounterKeys.occultPoint); set => S.SetCounter(CounterKeys.occultPoint, value); }

    //skill -dex
    public static int dexSkillPoint { get => S.GetCounter(CounterKeys.dexSkillPoint); set => S.SetCounter(CounterKeys.dexSkillPoint, value); }
    public static int evasionPoint { get => S.GetCounter(CounterKeys.evasionPoint); set => S.SetCounter(CounterKeys.evasionPoint, value); }
    public static int deftnessPoint { get => S.GetCounter(CounterKeys.deftnessPoint); set => S.SetCounter(CounterKeys.deftnessPoint, value); }
    public static int rhetoricPoint { get => S.GetCounter(CounterKeys.rhetoricPoint); set => S.SetCounter(CounterKeys.rhetoricPoint, value); }
    public static int stealthPoint { get => S.GetCounter(CounterKeys.stealthPoint); set => S.SetCounter(CounterKeys.stealthPoint, value); }
    public static int disguisePoint { get => S.GetCounter(CounterKeys.disguisePoint); set => S.SetCounter(CounterKeys.disguisePoint, value); }

    [SerializeField] string csvFileName;
    Dictionary<int, Dialog> dialogDic = new Dictionary<int, Dialog>();
    public static bool isFinish;
    static public DataBaseManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Persist.Keep(this.gameObject);
            instance = this;
        }
        isPresentation = false;

    }

    public string[] varName;
    public float[] var;

    public string[] switchName;
    public bool[] switches;

    //Event
    // 현재 획득한 사건정보가지수
    // 행성대직렬 현재 페이지
    //행성대직렬 정보
    //인물정보
    public static int nowPageElla = 1;
    public static int nowPageAiden = 1;
    public static int nowPageCane = 1;
    public static int nowPageAlbert = 1;
    public static int nowPageMeiv = 1;
    public static int nowPageBlackWell = 1;
    public static int nowPageSwain = 1;

    public static int gainCharacterInt { get => S.GetCounter(CounterKeys.gainCharacterInt); set => S.SetCounter(CounterKeys.gainCharacterInt, value); }

    public static int buttonCountElla = -1;
    public static int buttonCountAiden = -1;
    public static int buttonCountCane = -1;
    public static int buttonCountAlbert = -1;
    public static int buttonCountMeiv = -1;
    public static int buttonCountBlackWell = -1;
    public static int buttonCountSwain = -1;

    public static bool intelAiden1 { get => S.GetFlag(FlagKeys.intelAiden1); set => S.SetFlag(FlagKeys.intelAiden1, value); }
    public static bool intelAiden2 { get => S.GetFlag(FlagKeys.intelAiden2); set => S.SetFlag(FlagKeys.intelAiden2, value); }
    public static bool intelAiden3 { get => S.GetFlag(FlagKeys.intelAiden3); set => S.SetFlag(FlagKeys.intelAiden3, value); }
    public static bool intelAiden4 { get => S.GetFlag(FlagKeys.intelAiden4); set => S.SetFlag(FlagKeys.intelAiden4, value); }
    public static bool intelAiden5 { get => S.GetFlag(FlagKeys.intelAiden5); set => S.SetFlag(FlagKeys.intelAiden5, value); }
    public static bool intelAiden6 { get => S.GetFlag(FlagKeys.intelAiden6); set => S.SetFlag(FlagKeys.intelAiden6, value); }

    public static bool intelElla1 { get => S.GetFlag(FlagKeys.intelElla1); set => S.SetFlag(FlagKeys.intelElla1, value); }
    public static bool intelElla2 { get => S.GetFlag(FlagKeys.intelElla2); set => S.SetFlag(FlagKeys.intelElla2, value); }
    public static bool intelElla3 { get => S.GetFlag(FlagKeys.intelElla3); set => S.SetFlag(FlagKeys.intelElla3, value); }
    public static bool intelElla4 { get => S.GetFlag(FlagKeys.intelElla4); set => S.SetFlag(FlagKeys.intelElla4, value); }
    public static bool intelElla5 { get => S.GetFlag(FlagKeys.intelElla5); set => S.SetFlag(FlagKeys.intelElla5, value); }
    public static bool intelElla6 { get => S.GetFlag(FlagKeys.intelElla6); set => S.SetFlag(FlagKeys.intelElla6, value); }

    public static bool intelSwain1 { get => S.GetFlag(FlagKeys.intelSwain1); set => S.SetFlag(FlagKeys.intelSwain1, value); }
    public static bool intelSwain2 { get => S.GetFlag(FlagKeys.intelSwain2); set => S.SetFlag(FlagKeys.intelSwain2, value); }
    public static bool intelSwain3 { get => S.GetFlag(FlagKeys.intelSwain3); set => S.SetFlag(FlagKeys.intelSwain3, value); }
    public static bool intelSwain4 { get => S.GetFlag(FlagKeys.intelSwain4); set => S.SetFlag(FlagKeys.intelSwain4, value); }
    public static bool intelSwain5 { get => S.GetFlag(FlagKeys.intelSwain5); set => S.SetFlag(FlagKeys.intelSwain5, value); }
    public static bool intelSwain6 { get => S.GetFlag(FlagKeys.intelSwain6); set => S.SetFlag(FlagKeys.intelSwain6, value); }

    public static bool intelCane1 { get => S.GetFlag(FlagKeys.intelCane1); set => S.SetFlag(FlagKeys.intelCane1, value); }
    public static bool intelCane2 { get => S.GetFlag(FlagKeys.intelCane2); set => S.SetFlag(FlagKeys.intelCane2, value); }
    public static bool intelCane3 { get => S.GetFlag(FlagKeys.intelCane3); set => S.SetFlag(FlagKeys.intelCane3, value); }
    public static bool intelCane4 { get => S.GetFlag(FlagKeys.intelCane4); set => S.SetFlag(FlagKeys.intelCane4, value); }
    public static bool intelCane5 { get => S.GetFlag(FlagKeys.intelCane5); set => S.SetFlag(FlagKeys.intelCane5, value); }
    public static bool intelCane6 { get => S.GetFlag(FlagKeys.intelCane6); set => S.SetFlag(FlagKeys.intelCane6, value); }

    public static bool intelAlbert1 { get => S.GetFlag(FlagKeys.intelAlbert1); set => S.SetFlag(FlagKeys.intelAlbert1, value); }
    public static bool intelAlbert2 { get => S.GetFlag(FlagKeys.intelAlbert2); set => S.SetFlag(FlagKeys.intelAlbert2, value); }
    public static bool intelAlbert3 { get => S.GetFlag(FlagKeys.intelAlbert3); set => S.SetFlag(FlagKeys.intelAlbert3, value); }
    public static bool intelAlbert4 { get => S.GetFlag(FlagKeys.intelAlbert4); set => S.SetFlag(FlagKeys.intelAlbert4, value); }
    public static bool intelAlbert5 { get => S.GetFlag(FlagKeys.intelAlbert5); set => S.SetFlag(FlagKeys.intelAlbert5, value); }
    public static bool intelAlbert6 { get => S.GetFlag(FlagKeys.intelAlbert6); set => S.SetFlag(FlagKeys.intelAlbert6, value); }

    public static bool intelMeiv1 { get => S.GetFlag(FlagKeys.intelMeiv1); set => S.SetFlag(FlagKeys.intelMeiv1, value); }
    public static bool intelMeiv2 { get => S.GetFlag(FlagKeys.intelMeiv2); set => S.SetFlag(FlagKeys.intelMeiv2, value); }
    public static bool intelMeiv3 { get => S.GetFlag(FlagKeys.intelMeiv3); set => S.SetFlag(FlagKeys.intelMeiv3, value); }
    public static bool intelMeiv4 { get => S.GetFlag(FlagKeys.intelMeiv4); set => S.SetFlag(FlagKeys.intelMeiv4, value); }
    public static bool intelMeiv5 { get => S.GetFlag(FlagKeys.intelMeiv5); set => S.SetFlag(FlagKeys.intelMeiv5, value); }
    public static bool intelMeiv6 { get => S.GetFlag(FlagKeys.intelMeiv6); set => S.SetFlag(FlagKeys.intelMeiv6, value); }

    public static bool intelBlackWell1 { get => S.GetFlag(FlagKeys.intelBlackWell1); set => S.SetFlag(FlagKeys.intelBlackWell1, value); }
    public static bool intelBlackWell2 { get => S.GetFlag(FlagKeys.intelBlackWell2); set => S.SetFlag(FlagKeys.intelBlackWell2, value); }
    public static bool intelBlackWell3 { get => S.GetFlag(FlagKeys.intelBlackWell3); set => S.SetFlag(FlagKeys.intelBlackWell3, value); }
    public static bool intelBlackWell4 { get => S.GetFlag(FlagKeys.intelBlackWell4); set => S.SetFlag(FlagKeys.intelBlackWell4, value); }
    public static bool intelBlackWell5 { get => S.GetFlag(FlagKeys.intelBlackWell5); set => S.SetFlag(FlagKeys.intelBlackWell5, value); }
    public static bool intelBlackWell6 { get => S.GetFlag(FlagKeys.intelBlackWell6); set => S.SetFlag(FlagKeys.intelBlackWell6, value); }

    //사건 정보
    public static int nowPagePlanetaryParade = 1;
    public static int nowPageNightmare = 1;
    public static int nowPageParanoia = 1;
    public static int nowPageInsomnia = 1;
    public static int nowPageSewerGhostStory = 1;
    public static int nowPageMissingPeople = 1;
    public static int nowPageFishySmell = 1;
    public static int nowPageRiverWaterQuality = 1;
    public static int nowPageCreepyEyes = 1;
    public static int nowPageSewerWorker = 1;
    public static int nowPageDave = 1;
    public static int nowPageSafe = 1;
    public static int nowPageMurderCase = 1;

    public static int gainEventInt { get => S.GetCounter(CounterKeys.gainEventInt); set => S.SetCounter(CounterKeys.gainEventInt, value); }

    public static int buttonCountPlanetaryParade = -1;
    public static int buttonCountNightmare = -1;
    public static int buttonCountParanoia = -1;
    public static int buttonCountInsomnia = -1;
    public static int buttonCountSewerGhostStory = -1;
    public static int buttonCountMissingPeople = -1;
    public static int buttonCountFishySmell = -1;
    public static int buttonCountRiverWaterQuality = -1;
    public static int buttonCountCreepyEyes = -1;
    public static int buttonCountSewerWorker = -1;
    public static int buttonCountDave = -1;
    public static int buttonCountSafe = -1;
    public static int buttonCountMurderCase = -1;

    public static bool intelPlanetaryParade1 { get => S.GetFlag(FlagKeys.intelPlanetaryParade1); set => S.SetFlag(FlagKeys.intelPlanetaryParade1, value); }
    public static bool intelPlanetaryParade2 { get => S.GetFlag(FlagKeys.intelPlanetaryParade2); set => S.SetFlag(FlagKeys.intelPlanetaryParade2, value); }
    public static bool intelPlanetaryParade3 { get => S.GetFlag(FlagKeys.intelPlanetaryParade3); set => S.SetFlag(FlagKeys.intelPlanetaryParade3, value); }
    public static bool intelPlanetaryParade4 { get => S.GetFlag(FlagKeys.intelPlanetaryParade4); set => S.SetFlag(FlagKeys.intelPlanetaryParade4, value); }
    public static bool intelPlanetaryParade5 { get => S.GetFlag(FlagKeys.intelPlanetaryParade5); set => S.SetFlag(FlagKeys.intelPlanetaryParade5, value); }
    public static bool intelPlanetaryParade6 { get => S.GetFlag(FlagKeys.intelPlanetaryParade6); set => S.SetFlag(FlagKeys.intelPlanetaryParade6, value); }

    public static bool intelNightmare1 { get => S.GetFlag(FlagKeys.intelNightmare1); set => S.SetFlag(FlagKeys.intelNightmare1, value); }
    public static bool intelNightmare2 { get => S.GetFlag(FlagKeys.intelNightmare2); set => S.SetFlag(FlagKeys.intelNightmare2, value); }
    public static bool intelNightmare3 { get => S.GetFlag(FlagKeys.intelNightmare3); set => S.SetFlag(FlagKeys.intelNightmare3, value); }
    public static bool intelNightmare4 { get => S.GetFlag(FlagKeys.intelNightmare4); set => S.SetFlag(FlagKeys.intelNightmare4, value); }
    public static bool intelNightmare5 { get => S.GetFlag(FlagKeys.intelNightmare5); set => S.SetFlag(FlagKeys.intelNightmare5, value); }
    public static bool intelNightmare6 { get => S.GetFlag(FlagKeys.intelNightmare6); set => S.SetFlag(FlagKeys.intelNightmare6, value); }

    public static bool intelParanoia1 { get => S.GetFlag(FlagKeys.intelParanoia1); set => S.SetFlag(FlagKeys.intelParanoia1, value); }
    public static bool intelParanoia2 { get => S.GetFlag(FlagKeys.intelParanoia2); set => S.SetFlag(FlagKeys.intelParanoia2, value); }
    public static bool intelParanoia3 { get => S.GetFlag(FlagKeys.intelParanoia3); set => S.SetFlag(FlagKeys.intelParanoia3, value); }
    public static bool intelParanoia4 { get => S.GetFlag(FlagKeys.intelParanoia4); set => S.SetFlag(FlagKeys.intelParanoia4, value); }
    public static bool intelParanoia5 { get => S.GetFlag(FlagKeys.intelParanoia5); set => S.SetFlag(FlagKeys.intelParanoia5, value); }
    public static bool intelParanoia6 { get => S.GetFlag(FlagKeys.intelParanoia6); set => S.SetFlag(FlagKeys.intelParanoia6, value); }

    public static bool intelInsomnia1 { get => S.GetFlag(FlagKeys.intelInsomnia1); set => S.SetFlag(FlagKeys.intelInsomnia1, value); }
    public static bool intelInsomnia2 { get => S.GetFlag(FlagKeys.intelInsomnia2); set => S.SetFlag(FlagKeys.intelInsomnia2, value); }
    public static bool intelInsomnia3 { get => S.GetFlag(FlagKeys.intelInsomnia3); set => S.SetFlag(FlagKeys.intelInsomnia3, value); }
    public static bool intelInsomnia4 { get => S.GetFlag(FlagKeys.intelInsomnia4); set => S.SetFlag(FlagKeys.intelInsomnia4, value); }
    public static bool intelInsomnia5 { get => S.GetFlag(FlagKeys.intelInsomnia5); set => S.SetFlag(FlagKeys.intelInsomnia5, value); }
    public static bool intelInsomnia6 { get => S.GetFlag(FlagKeys.intelInsomnia6); set => S.SetFlag(FlagKeys.intelInsomnia6, value); }

    public static bool intelSewerGhostStory1 { get => S.GetFlag(FlagKeys.intelSewerGhostStory1); set => S.SetFlag(FlagKeys.intelSewerGhostStory1, value); }
    public static bool intelSewerGhostStory2 { get => S.GetFlag(FlagKeys.intelSewerGhostStory2); set => S.SetFlag(FlagKeys.intelSewerGhostStory2, value); }
    public static bool intelSewerGhostStory3 { get => S.GetFlag(FlagKeys.intelSewerGhostStory3); set => S.SetFlag(FlagKeys.intelSewerGhostStory3, value); }
    public static bool intelSewerGhostStory4 { get => S.GetFlag(FlagKeys.intelSewerGhostStory4); set => S.SetFlag(FlagKeys.intelSewerGhostStory4, value); }
    public static bool intelSewerGhostStory5 { get => S.GetFlag(FlagKeys.intelSewerGhostStory5); set => S.SetFlag(FlagKeys.intelSewerGhostStory5, value); }
    public static bool intelSewerGhostStory6 { get => S.GetFlag(FlagKeys.intelSewerGhostStory6); set => S.SetFlag(FlagKeys.intelSewerGhostStory6, value); }

    public static bool intelMissingPeople1 { get => S.GetFlag(FlagKeys.intelMissingPeople1); set => S.SetFlag(FlagKeys.intelMissingPeople1, value); }
    public static bool intelMissingPeople2 { get => S.GetFlag(FlagKeys.intelMissingPeople2); set => S.SetFlag(FlagKeys.intelMissingPeople2, value); }
    public static bool intelMissingPeople3 { get => S.GetFlag(FlagKeys.intelMissingPeople3); set => S.SetFlag(FlagKeys.intelMissingPeople3, value); }
    public static bool intelMissingPeople4 { get => S.GetFlag(FlagKeys.intelMissingPeople4); set => S.SetFlag(FlagKeys.intelMissingPeople4, value); }
    public static bool intelMissingPeople5 { get => S.GetFlag(FlagKeys.intelMissingPeople5); set => S.SetFlag(FlagKeys.intelMissingPeople5, value); }
    public static bool intelMissingPeople6 { get => S.GetFlag(FlagKeys.intelMissingPeople6); set => S.SetFlag(FlagKeys.intelMissingPeople6, value); }

    public static bool intelFishySmell1 { get => S.GetFlag(FlagKeys.intelFishySmell1); set => S.SetFlag(FlagKeys.intelFishySmell1, value); }
    public static bool intelFishySmell2 { get => S.GetFlag(FlagKeys.intelFishySmell2); set => S.SetFlag(FlagKeys.intelFishySmell2, value); }
    public static bool intelFishySmell3 { get => S.GetFlag(FlagKeys.intelFishySmell3); set => S.SetFlag(FlagKeys.intelFishySmell3, value); }
    public static bool intelFishySmell4 { get => S.GetFlag(FlagKeys.intelFishySmell4); set => S.SetFlag(FlagKeys.intelFishySmell4, value); }
    public static bool intelFishySmell5 { get => S.GetFlag(FlagKeys.intelFishySmell5); set => S.SetFlag(FlagKeys.intelFishySmell5, value); }
    public static bool intelFishySmell6 { get => S.GetFlag(FlagKeys.intelFishySmell6); set => S.SetFlag(FlagKeys.intelFishySmell6, value); }

    public static bool intelRiverWaterQuality1 { get => S.GetFlag(FlagKeys.intelRiverWaterQuality1); set => S.SetFlag(FlagKeys.intelRiverWaterQuality1, value); }
    public static bool intelRiverWaterQuality2 { get => S.GetFlag(FlagKeys.intelRiverWaterQuality2); set => S.SetFlag(FlagKeys.intelRiverWaterQuality2, value); }
    public static bool intelRiverWaterQuality3 { get => S.GetFlag(FlagKeys.intelRiverWaterQuality3); set => S.SetFlag(FlagKeys.intelRiverWaterQuality3, value); }
    public static bool intelRiverWaterQuality4 { get => S.GetFlag(FlagKeys.intelRiverWaterQuality4); set => S.SetFlag(FlagKeys.intelRiverWaterQuality4, value); }
    public static bool intelRiverWaterQuality5 { get => S.GetFlag(FlagKeys.intelRiverWaterQuality5); set => S.SetFlag(FlagKeys.intelRiverWaterQuality5, value); }
    public static bool intelRiverWaterQuality6 { get => S.GetFlag(FlagKeys.intelRiverWaterQuality6); set => S.SetFlag(FlagKeys.intelRiverWaterQuality6, value); }

    public static bool intelCreepyEyes1 { get => S.GetFlag(FlagKeys.intelCreepyEyes1); set => S.SetFlag(FlagKeys.intelCreepyEyes1, value); }
    public static bool intelCreepyEyes2 { get => S.GetFlag(FlagKeys.intelCreepyEyes2); set => S.SetFlag(FlagKeys.intelCreepyEyes2, value); }
    public static bool intelCreepyEyes3 { get => S.GetFlag(FlagKeys.intelCreepyEyes3); set => S.SetFlag(FlagKeys.intelCreepyEyes3, value); }
    public static bool intelCreepyEyes4 { get => S.GetFlag(FlagKeys.intelCreepyEyes4); set => S.SetFlag(FlagKeys.intelCreepyEyes4, value); }
    public static bool intelCreepyEyes5 { get => S.GetFlag(FlagKeys.intelCreepyEyes5); set => S.SetFlag(FlagKeys.intelCreepyEyes5, value); }
    public static bool intelCreepyEyes6 { get => S.GetFlag(FlagKeys.intelCreepyEyes6); set => S.SetFlag(FlagKeys.intelCreepyEyes6, value); }

    public static bool intelSewerWorker1 { get => S.GetFlag(FlagKeys.intelSewerWorker1); set => S.SetFlag(FlagKeys.intelSewerWorker1, value); }
    public static bool intelSewerWorker2 { get => S.GetFlag(FlagKeys.intelSewerWorker2); set => S.SetFlag(FlagKeys.intelSewerWorker2, value); }
    public static bool intelSewerWorker3 { get => S.GetFlag(FlagKeys.intelSewerWorker3); set => S.SetFlag(FlagKeys.intelSewerWorker3, value); }
    public static bool intelSewerWorker4 { get => S.GetFlag(FlagKeys.intelSewerWorker4); set => S.SetFlag(FlagKeys.intelSewerWorker4, value); }
    public static bool intelSewerWorker5 { get => S.GetFlag(FlagKeys.intelSewerWorker5); set => S.SetFlag(FlagKeys.intelSewerWorker5, value); }
    public static bool intelSewerWorker6 { get => S.GetFlag(FlagKeys.intelSewerWorker6); set => S.SetFlag(FlagKeys.intelSewerWorker6, value); }

    public static bool intelDave1 { get => S.GetFlag(FlagKeys.intelDave1); set => S.SetFlag(FlagKeys.intelDave1, value); }
    public static bool intelDave2 { get => S.GetFlag(FlagKeys.intelDave2); set => S.SetFlag(FlagKeys.intelDave2, value); }
    public static bool intelDave3 { get => S.GetFlag(FlagKeys.intelDave3); set => S.SetFlag(FlagKeys.intelDave3, value); }
    public static bool intelDave4 { get => S.GetFlag(FlagKeys.intelDave4); set => S.SetFlag(FlagKeys.intelDave4, value); }
    public static bool intelDave5 { get => S.GetFlag(FlagKeys.intelDave5); set => S.SetFlag(FlagKeys.intelDave5, value); }
    public static bool intelDave6 { get => S.GetFlag(FlagKeys.intelDave6); set => S.SetFlag(FlagKeys.intelDave6, value); }

    public static bool intelSafe1 { get => S.GetFlag(FlagKeys.intelSafe1); set => S.SetFlag(FlagKeys.intelSafe1, value); }
    public static bool intelSafe2 { get => S.GetFlag(FlagKeys.intelSafe2); set => S.SetFlag(FlagKeys.intelSafe2, value); }
    public static bool intelSafe3 { get => S.GetFlag(FlagKeys.intelSafe3); set => S.SetFlag(FlagKeys.intelSafe3, value); }
    public static bool intelSafe4 { get => S.GetFlag(FlagKeys.intelSafe4); set => S.SetFlag(FlagKeys.intelSafe4, value); }
    public static bool intelSafe5 { get => S.GetFlag(FlagKeys.intelSafe5); set => S.SetFlag(FlagKeys.intelSafe5, value); }
    public static bool intelSafe6 { get => S.GetFlag(FlagKeys.intelSafe6); set => S.SetFlag(FlagKeys.intelSafe6, value); }

    public static bool intelMurderCase1 { get => S.GetFlag(FlagKeys.intelMurderCase1); set => S.SetFlag(FlagKeys.intelMurderCase1, value); }
    public static bool intelMurderCase2 { get => S.GetFlag(FlagKeys.intelMurderCase2); set => S.SetFlag(FlagKeys.intelMurderCase2, value); }
    public static bool intelMurderCase3 { get => S.GetFlag(FlagKeys.intelMurderCase3); set => S.SetFlag(FlagKeys.intelMurderCase3, value); }
    public static bool intelMurderCase4 { get => S.GetFlag(FlagKeys.intelMurderCase4); set => S.SetFlag(FlagKeys.intelMurderCase4, value); }
    public static bool intelMurderCase5 { get => S.GetFlag(FlagKeys.intelMurderCase5); set => S.SetFlag(FlagKeys.intelMurderCase5, value); }
    public static bool intelMurderCase6 { get => S.GetFlag(FlagKeys.intelMurderCase6); set => S.SetFlag(FlagKeys.intelMurderCase6, value); }

    //장소정보
    public static int buttonCountDetectiveOffice = -1;
    public static int buttonCountHospital = -1;
    public static int buttonCountNewspaper = -1;
    public static int buttonCountQuestHouse = -1;
    public static int buttonCountStation = -1;
    public static int buttonCountSewer = -1;
    public static int buttonCountUniversity = -1;
    public static int buttonCountRiverside = -1;
    public static int buttonCountBar = -1;
    public static int buttonCountSlum = -1;
    public static int buttonCountPoliceOffice = -1;
    public static int buttonCountGunsmith = -1;
    public static int buttonCountSewageMaintenanceOffice = -1;
    public static int buttonCountWharf = -1;

    public static int gainPlaceInt { get => S.GetCounter(CounterKeys.gainPlaceInt); set => S.SetCounter(CounterKeys.gainPlaceInt, value); }

    public static int nowPageDetectiveOffice = 1;
    public static int nowPageHospital = 1;
    public static int nowPageNewspaper = 1;
    public static int nowPageQuestHouse = 1;
    public static int nowPageStation = 1;
    public static int nowPageSewer = 1;
    public static int nowPageUniversity = 1;
    public static int nowPageRiverside = 1;
    public static int nowPageBar = 1;
    public static int nowPageSlum = 1;
    public static int nowPagePoliceOffice = 1;
    public static int nowPageGunsmith = 1;
    public static int nowPageSewageMaintenanceOffice = 1;
    public static int nowPageWharf = 1;

    public static bool intelDetectiveOffice1 { get => S.GetFlag(FlagKeys.intelDetectiveOffice1); set => S.SetFlag(FlagKeys.intelDetectiveOffice1, value); }
    public static bool intelDetectiveOffice2 { get => S.GetFlag(FlagKeys.intelDetectiveOffice2); set => S.SetFlag(FlagKeys.intelDetectiveOffice2, value); }
    public static bool intelDetectiveOffice3 { get => S.GetFlag(FlagKeys.intelDetectiveOffice3); set => S.SetFlag(FlagKeys.intelDetectiveOffice3, value); }
    public static bool intelDetectiveOffice4 { get => S.GetFlag(FlagKeys.intelDetectiveOffice4); set => S.SetFlag(FlagKeys.intelDetectiveOffice4, value); }
    public static bool intelDetectiveOffice5 { get => S.GetFlag(FlagKeys.intelDetectiveOffice5); set => S.SetFlag(FlagKeys.intelDetectiveOffice5, value); }
    public static bool intelDetectiveOffice6 { get => S.GetFlag(FlagKeys.intelDetectiveOffice6); set => S.SetFlag(FlagKeys.intelDetectiveOffice6, value); }

    public static bool intelHospital1 { get => S.GetFlag(FlagKeys.intelHospital1); set => S.SetFlag(FlagKeys.intelHospital1, value); }
    public static bool intelHospital2 { get => S.GetFlag(FlagKeys.intelHospital2); set => S.SetFlag(FlagKeys.intelHospital2, value); }
    public static bool intelHospital3 { get => S.GetFlag(FlagKeys.intelHospital3); set => S.SetFlag(FlagKeys.intelHospital3, value); }
    public static bool intelHospital4 { get => S.GetFlag(FlagKeys.intelHospital4); set => S.SetFlag(FlagKeys.intelHospital4, value); }
    public static bool intelHospital5 { get => S.GetFlag(FlagKeys.intelHospital5); set => S.SetFlag(FlagKeys.intelHospital5, value); }
    public static bool intelHospital6 { get => S.GetFlag(FlagKeys.intelHospital6); set => S.SetFlag(FlagKeys.intelHospital6, value); }

    public static bool intelNewspaper1 { get => S.GetFlag(FlagKeys.intelNewspaper1); set => S.SetFlag(FlagKeys.intelNewspaper1, value); }
    public static bool intelNewspaper2 { get => S.GetFlag(FlagKeys.intelNewspaper2); set => S.SetFlag(FlagKeys.intelNewspaper2, value); }
    public static bool intelNewspaper3 { get => S.GetFlag(FlagKeys.intelNewspaper3); set => S.SetFlag(FlagKeys.intelNewspaper3, value); }
    public static bool intelNewspaper4 { get => S.GetFlag(FlagKeys.intelNewspaper4); set => S.SetFlag(FlagKeys.intelNewspaper4, value); }
    public static bool intelNewspaper5 { get => S.GetFlag(FlagKeys.intelNewspaper5); set => S.SetFlag(FlagKeys.intelNewspaper5, value); }
    public static bool intelNewspaper6 { get => S.GetFlag(FlagKeys.intelNewspaper6); set => S.SetFlag(FlagKeys.intelNewspaper6, value); }

    public static bool intelQuestHouse1 { get => S.GetFlag(FlagKeys.intelQuestHouse1); set => S.SetFlag(FlagKeys.intelQuestHouse1, value); }
    public static bool intelQuestHouse2 { get => S.GetFlag(FlagKeys.intelQuestHouse2); set => S.SetFlag(FlagKeys.intelQuestHouse2, value); }
    public static bool intelQuestHouse3 { get => S.GetFlag(FlagKeys.intelQuestHouse3); set => S.SetFlag(FlagKeys.intelQuestHouse3, value); }
    public static bool intelQuestHouse4 { get => S.GetFlag(FlagKeys.intelQuestHouse4); set => S.SetFlag(FlagKeys.intelQuestHouse4, value); }
    public static bool intelQuestHouse5 { get => S.GetFlag(FlagKeys.intelQuestHouse5); set => S.SetFlag(FlagKeys.intelQuestHouse5, value); }
    public static bool intelQuestHouse6 { get => S.GetFlag(FlagKeys.intelQuestHouse6); set => S.SetFlag(FlagKeys.intelQuestHouse6, value); }

    public static bool intelStation1 { get => S.GetFlag(FlagKeys.intelStation1); set => S.SetFlag(FlagKeys.intelStation1, value); }
    public static bool intelStation2 { get => S.GetFlag(FlagKeys.intelStation2); set => S.SetFlag(FlagKeys.intelStation2, value); }
    public static bool intelStation3 { get => S.GetFlag(FlagKeys.intelStation3); set => S.SetFlag(FlagKeys.intelStation3, value); }
    public static bool intelStation4 { get => S.GetFlag(FlagKeys.intelStation4); set => S.SetFlag(FlagKeys.intelStation4, value); }
    public static bool intelStation5 { get => S.GetFlag(FlagKeys.intelStation5); set => S.SetFlag(FlagKeys.intelStation5, value); }
    public static bool intelStation6 { get => S.GetFlag(FlagKeys.intelStation6); set => S.SetFlag(FlagKeys.intelStation6, value); }

    public static bool intelSewer1 { get => S.GetFlag(FlagKeys.intelSewer1); set => S.SetFlag(FlagKeys.intelSewer1, value); }
    public static bool intelSewer2 { get => S.GetFlag(FlagKeys.intelSewer2); set => S.SetFlag(FlagKeys.intelSewer2, value); }
    public static bool intelSewer3 { get => S.GetFlag(FlagKeys.intelSewer3); set => S.SetFlag(FlagKeys.intelSewer3, value); }
    public static bool intelSewer4 { get => S.GetFlag(FlagKeys.intelSewer4); set => S.SetFlag(FlagKeys.intelSewer4, value); }
    public static bool intelSewer5 { get => S.GetFlag(FlagKeys.intelSewer5); set => S.SetFlag(FlagKeys.intelSewer5, value); }
    public static bool intelSewer6 { get => S.GetFlag(FlagKeys.intelSewer6); set => S.SetFlag(FlagKeys.intelSewer6, value); }

    public static bool intelUniversity1 { get => S.GetFlag(FlagKeys.intelUniversity1); set => S.SetFlag(FlagKeys.intelUniversity1, value); }
    public static bool intelUniversity2 { get => S.GetFlag(FlagKeys.intelUniversity2); set => S.SetFlag(FlagKeys.intelUniversity2, value); }
    public static bool intelUniversity3 { get => S.GetFlag(FlagKeys.intelUniversity3); set => S.SetFlag(FlagKeys.intelUniversity3, value); }
    public static bool intelUniversity4 { get => S.GetFlag(FlagKeys.intelUniversity4); set => S.SetFlag(FlagKeys.intelUniversity4, value); }
    public static bool intelUniversity5 { get => S.GetFlag(FlagKeys.intelUniversity5); set => S.SetFlag(FlagKeys.intelUniversity5, value); }
    public static bool intelUniversity6 { get => S.GetFlag(FlagKeys.intelUniversity6); set => S.SetFlag(FlagKeys.intelUniversity6, value); }

    public static bool intelRiverside1 { get => S.GetFlag(FlagKeys.intelRiverside1); set => S.SetFlag(FlagKeys.intelRiverside1, value); }
    public static bool intelRiverside2 { get => S.GetFlag(FlagKeys.intelRiverside2); set => S.SetFlag(FlagKeys.intelRiverside2, value); }
    public static bool intelRiverside3 { get => S.GetFlag(FlagKeys.intelRiverside3); set => S.SetFlag(FlagKeys.intelRiverside3, value); }
    public static bool intelRiverside4 { get => S.GetFlag(FlagKeys.intelRiverside4); set => S.SetFlag(FlagKeys.intelRiverside4, value); }
    public static bool intelRiverside5 { get => S.GetFlag(FlagKeys.intelRiverside5); set => S.SetFlag(FlagKeys.intelRiverside5, value); }
    public static bool intelRiverside6 { get => S.GetFlag(FlagKeys.intelRiverside6); set => S.SetFlag(FlagKeys.intelRiverside6, value); }

    public static bool intelBar1 { get => S.GetFlag(FlagKeys.intelBar1); set => S.SetFlag(FlagKeys.intelBar1, value); }
    public static bool intelBar2 { get => S.GetFlag(FlagKeys.intelBar2); set => S.SetFlag(FlagKeys.intelBar2, value); }
    public static bool intelBar3 { get => S.GetFlag(FlagKeys.intelBar3); set => S.SetFlag(FlagKeys.intelBar3, value); }
    public static bool intelBar4 { get => S.GetFlag(FlagKeys.intelBar4); set => S.SetFlag(FlagKeys.intelBar4, value); }
    public static bool intelBar5 { get => S.GetFlag(FlagKeys.intelBar5); set => S.SetFlag(FlagKeys.intelBar5, value); }
    public static bool intelBar6 { get => S.GetFlag(FlagKeys.intelBar6); set => S.SetFlag(FlagKeys.intelBar6, value); }

    public static bool intelSlum1 { get => S.GetFlag(FlagKeys.intelSlum1); set => S.SetFlag(FlagKeys.intelSlum1, value); }
    public static bool intelSlum2 { get => S.GetFlag(FlagKeys.intelSlum2); set => S.SetFlag(FlagKeys.intelSlum2, value); }
    public static bool intelSlum3 { get => S.GetFlag(FlagKeys.intelSlum3); set => S.SetFlag(FlagKeys.intelSlum3, value); }
    public static bool intelSlum4 { get => S.GetFlag(FlagKeys.intelSlum4); set => S.SetFlag(FlagKeys.intelSlum4, value); }
    public static bool intelSlum5 { get => S.GetFlag(FlagKeys.intelSlum5); set => S.SetFlag(FlagKeys.intelSlum5, value); }
    public static bool intelSlum6 { get => S.GetFlag(FlagKeys.intelSlum6); set => S.SetFlag(FlagKeys.intelSlum6, value); }

    public static bool intelPoliceOffice1 { get => S.GetFlag(FlagKeys.intelPoliceOffice1); set => S.SetFlag(FlagKeys.intelPoliceOffice1, value); }
    public static bool intelPoliceOffice2 { get => S.GetFlag(FlagKeys.intelPoliceOffice2); set => S.SetFlag(FlagKeys.intelPoliceOffice2, value); }
    public static bool intelPoliceOffice3 { get => S.GetFlag(FlagKeys.intelPoliceOffice3); set => S.SetFlag(FlagKeys.intelPoliceOffice3, value); }
    public static bool intelPoliceOffice4 { get => S.GetFlag(FlagKeys.intelPoliceOffice4); set => S.SetFlag(FlagKeys.intelPoliceOffice4, value); }
    public static bool intelPoliceOffice5 { get => S.GetFlag(FlagKeys.intelPoliceOffice5); set => S.SetFlag(FlagKeys.intelPoliceOffice5, value); }
    public static bool intelPoliceOffice6 { get => S.GetFlag(FlagKeys.intelPoliceOffice6); set => S.SetFlag(FlagKeys.intelPoliceOffice6, value); }

    public static bool intelGunsmith1 { get => S.GetFlag(FlagKeys.intelGunsmith1); set => S.SetFlag(FlagKeys.intelGunsmith1, value); }
    public static bool intelGunsmith2 { get => S.GetFlag(FlagKeys.intelGunsmith2); set => S.SetFlag(FlagKeys.intelGunsmith2, value); }
    public static bool intelGunsmith3 { get => S.GetFlag(FlagKeys.intelGunsmith3); set => S.SetFlag(FlagKeys.intelGunsmith3, value); }
    public static bool intelGunsmith4 { get => S.GetFlag(FlagKeys.intelGunsmith4); set => S.SetFlag(FlagKeys.intelGunsmith4, value); }
    public static bool intelGunsmith5 { get => S.GetFlag(FlagKeys.intelGunsmith5); set => S.SetFlag(FlagKeys.intelGunsmith5, value); }
    public static bool intelGunsmith6 { get => S.GetFlag(FlagKeys.intelGunsmith6); set => S.SetFlag(FlagKeys.intelGunsmith6, value); }

    public static bool intelSewageMaintenanceOffice1 { get => S.GetFlag(FlagKeys.intelSewageMaintenanceOffice1); set => S.SetFlag(FlagKeys.intelSewageMaintenanceOffice1, value); }
    public static bool intelSewageMaintenanceOffice2 { get => S.GetFlag(FlagKeys.intelSewageMaintenanceOffice2); set => S.SetFlag(FlagKeys.intelSewageMaintenanceOffice2, value); }
    public static bool intelSewageMaintenanceOffice3 { get => S.GetFlag(FlagKeys.intelSewageMaintenanceOffice3); set => S.SetFlag(FlagKeys.intelSewageMaintenanceOffice3, value); }
    public static bool intelSewageMaintenanceOffice4 { get => S.GetFlag(FlagKeys.intelSewageMaintenanceOffice4); set => S.SetFlag(FlagKeys.intelSewageMaintenanceOffice4, value); }
    public static bool intelSewageMaintenanceOffice5 { get => S.GetFlag(FlagKeys.intelSewageMaintenanceOffice5); set => S.SetFlag(FlagKeys.intelSewageMaintenanceOffice5, value); }
    public static bool intelSewageMaintenanceOffice6 { get => S.GetFlag(FlagKeys.intelSewageMaintenanceOffice6); set => S.SetFlag(FlagKeys.intelSewageMaintenanceOffice6, value); }

    public static bool intelWharf1 { get => S.GetFlag(FlagKeys.intelWharf1); set => S.SetFlag(FlagKeys.intelWharf1, value); }
    public static bool intelWharf2 { get => S.GetFlag(FlagKeys.intelWharf2); set => S.SetFlag(FlagKeys.intelWharf2, value); }
    public static bool intelWharf3 { get => S.GetFlag(FlagKeys.intelWharf3); set => S.SetFlag(FlagKeys.intelWharf3, value); }
    public static bool intelWharf4 { get => S.GetFlag(FlagKeys.intelWharf4); set => S.SetFlag(FlagKeys.intelWharf4, value); }
    public static bool intelWharf5 { get => S.GetFlag(FlagKeys.intelWharf5); set => S.SetFlag(FlagKeys.intelWharf5, value); }
    public static bool intelWharf6 { get => S.GetFlag(FlagKeys.intelWharf6); set => S.SetFlag(FlagKeys.intelWharf6, value); }
    public void ResetData()
    {
        GameRoot.Instance.NewGame();
        ResetTransientStatics();
    }

    // 옛 ResetData 가 되돌리던 비영구(일시·화면·설정) 정적 필드. 영구 상태는 GameRoot.NewGame 이 맡는다.
    public static void ResetTransientStatics()
    {
        barMove = false;
        battleSan = false;
        strDialogOn = false;
        potalWait = false;
        setIntelCharDetail = false;
        isOpenMap = false;
        secondisDirecting = false;
        storyDirecting = false;
        isItemUI = false;
        conntectMouse = false;
        workSound = false;
        isBattleRollet = false;
        isDirecting = false;
        selectionOn = false;
        UiGate.Clear();
        isRollet = false;
        isSelect = false;
        cancelJudge = false;
        isKeyword = false;
        isJudge = false;
        isScene = false;
        isPresentation = false;
        isOption = false;
        endDialogLine = false;
        skipActive = false;
        isActiveDialog1 = false;
        isActiveDialog2 = false;
        nowItem = "";
        condition = "Nomal";
        selectObject = "";
        lookObject = "";
        nowSelecter = "End";
        nowPageElla = 1;
        nowPageAiden = 1;
        nowPageCane = 1;
        nowPageAlbert = 1;
        nowPageMeiv = 1;
        nowPageBlackWell = 1;
        nowPageSwain = 1;
        buttonCountElla = -1;
        buttonCountAiden = -1;
        buttonCountCane = -1;
        buttonCountAlbert = -1;
        buttonCountMeiv = -1;
        buttonCountBlackWell = -1;
        buttonCountSwain = -1;
        nowPagePlanetaryParade = 1;
        nowPageNightmare = 1;
        nowPageParanoia = 1;
        nowPageInsomnia = 1;
        nowPageSewerGhostStory = 1;
        nowPageMissingPeople = 1;
        nowPageFishySmell = 1;
        nowPageRiverWaterQuality = 1;
        nowPageCreepyEyes = 1;
        nowPageSewerWorker = 1;
        nowPageDave = 1;
        nowPageSafe = 1;
        nowPageMurderCase = 1;
        buttonCountPlanetaryParade = -1;
        buttonCountNightmare = -1;
        buttonCountParanoia = -1;
        buttonCountInsomnia = -1;
        buttonCountSewerGhostStory = -1;
        buttonCountMissingPeople = -1;
        buttonCountFishySmell = -1;
        buttonCountRiverWaterQuality = -1;
        buttonCountCreepyEyes = -1;
        buttonCountSewerWorker = -1;
        buttonCountDave = -1;
        buttonCountSafe = -1;
        buttonCountMurderCase = -1;
        buttonCountDetectiveOffice = -1;
        buttonCountHospital = -1;
        buttonCountNewspaper = -1;
        buttonCountQuestHouse = -1;
        buttonCountStation = -1;
        buttonCountSewer = -1;
        buttonCountUniversity = -1;
        buttonCountRiverside = -1;
        buttonCountBar = -1;
        buttonCountSlum = -1;
        buttonCountPoliceOffice = -1;
        buttonCountGunsmith = -1;
        buttonCountSewageMaintenanceOffice = -1;
        buttonCountWharf = -1;
        nowPageDetectiveOffice = 1;
        nowPageHospital = 1;
        nowPageNewspaper = 1;
        nowPageQuestHouse = 1;
        nowPageStation = 1;
        nowPageSewer = 1;
        nowPageUniversity = 1;
        nowPageRiverside = 1;
        nowPageBar = 1;
        nowPageSlum = 1;
        nowPagePoliceOffice = 1;
        nowPageGunsmith = 1;
        nowPageSewageMaintenanceOffice = 1;
        nowPageWharf = 1;
        textDelay = SettingsService.Current.TextDelay; // 설정은 새 게임·타이틀 복귀로 되돌리지 않는다
    }
}
