// 자동 생성(Architecture.md P3, 생성 스크립트는 저장소에 없음). 키 = 옛 DataBaseManager 정적 필드명 그대로(오타 포함). 한 번 낸 키는 바꾸지 않고 추가만 한다.
using System.Collections.Generic;

namespace SCoW.Core.State
{
    /// <summary>영구 bool 플래그 키(393). true 인 키만 GameState.Flags 에 보관한다.</summary>
    public static class FlagKeys
    {
        public const string isNightmare = "isNightmare";
        public const string endingDoorCheck = "endingDoorCheck";
        public const string endingDeathCheck = "endingDeathCheck";
        public const string endingRequestCheck = "endingRequestCheck";
        public const string endingSwainDeathCheck = "endingSwainDeathCheck";
        public const string endingAfterPromiseDialog = "endingAfterPromiseDialog";
        public const string endingisPromise = "endingisPromise";
        public const string endingBreakSymbol = "endingBreakSymbol";
        public const string endingLadderSan = "endingLadderSan";
        public const string endingSwin = "endingSwin";
        public const string sewerMap = "sewerMap";
        public const string ifGotRequset = "ifGotRequset";
        public const string sewerFirstIn = "sewerFirstIn";
        public const string sewerFirstInSanCheck = "sewerFirstInSanCheck";
        public const string sewerDeepOneFirstSanCheck = "sewerDeepOneFirstSanCheck";
        public const string isDebuffNightPhobia = "isDebuffNightPhobia";
        public const string isDebuffDeftness = "isDebuffDeftness";
        public const string isDebuffVisionLoss = "isDebuffVisionLoss";
        public const string isDebuffShortTempred = "isDebuffShortTempred";
        public const string isDebuffDyslexia = "isDebuffDyslexia";
        public const string isDebuffDrugPhobia = "isDebuffDrugPhobia";
        public const string isDebuffCognitiveDisorder = "isDebuffCognitiveDisorder";
        public const string isDebuffCognitiveBreakdown = "isDebuffCognitiveBreakdown";
        public const string isDebuffPerfectionism = "isDebuffPerfectionism";
        public const string sewerPicingSucc = "sewerPicingSucc";
        public const string isMorningMapMove = "isMorningMapMove";
        public const string afterDaveDeathFirstSlum = "afterDaveDeathFirstSlum";
        public const string iSRoadBattleStart = "iSRoadBattleStart";
        public const string bgmChangeChecker = "bgmChangeChecker";
        public const string sewerBattleChecker = "sewerBattleChecker";
        public const string symbolStrSanCheck = "symbolStrSanCheck";
        public const string symbolOccultFirst = "symbolOccultFirst";
        public const string sewerSymbolOccultCheck = "sewerSymbolOccultCheck";
        public const string bookLookFirst = "bookLookFirst";
        public const string sewerBookOccultCheck = "sewerBookOccultCheck";
        public const string sewerBookSanCheck = "sewerBookSanCheck";
        public const string journalFirstSanCheck = "journalFirstSanCheck";
        public const string journalSanCheck = "journalSanCheck";
        public const string firstLookJournal = "firstLookJournal";
        public const string gainOldMap = "gainOldMap";
        public const string safeDocument = "safeDocument";
        public const string insmusRhtoric = "insmusRhtoric";
        public const string isInsmusMeetSewer = "isInsmusMeetSewer";
        public const string sewerBattleEndCheck = "sewerBattleEndCheck";
        public const string inSewerStealthFail = "inSewerStealthFail";
        public const string inSewerStealthSucc = "inSewerStealthSucc";
        public const string startSewerBattle = "startSewerBattle";
        public const string sewerHouseIn = "sewerHouseIn";
        public const string blackRechSuccCanTalck = "blackRechSuccCanTalck";
        public const string blackDeftness = "blackDeftness";
        public const string blackRhethic = "blackRhethic";
        public const string blackFirstDialog = "blackFirstDialog";
        public const string mapLooked = "mapLooked";
        public const string mapFirstDialog = "mapFirstDialog";
        public const string sewerageKey = "sewerageKey";
        public const string judgeSafeTryOpen = "judgeSafeTryOpen";
        public const string judgeCoatTryDisguise = "judgeCoatTryDisguise";
        public const string gainCoat = "gainCoat";
        public const string gainMap = "gainMap";
        public const string wearCoat = "wearCoat";
        public const string isSewerOffice = "isSewerOffice";
        public const string isSewerOfficeBrokenWindow = "isSewerOfficeBrokenWindow";
        public const string tryBrokenWindow = "tryBrokenWindow";
        public const string tryObserLokcer = "tryObserLokcer";
        public const string moveSewerOfficeWindow = "moveSewerOfficeWindow";
        public const string morningMove2nd = "morningMove2nd";
        public const string morningMove4th = "morningMove4th";
        public const string day4Case1San = "day4Case1San";
        public const string day4Case1Health = "day4Case1Health";
        public const string sewerDoorOpen = "sewerDoorOpen";
        public const string sewerDoorOpenDeftness = "sewerDoorOpenDeftness";
        public const string sewerDoorOpenCheck = "sewerDoorOpenCheck";
        public const string morningMove_3rd = "morningMove_3rd";
        public const string thirdDayDreamSan = "thirdDayDreamSan";
        public const string thirdDayAlbertDialog = "thirdDayAlbertDialog";
        public const string thirdDayPoliceADialog = "thirdDayPoliceADialog";
        public const string thirdDayPoliceBDialog = "thirdDayPoliceBDialog";
        public const string thirdDayAlanDialog = "thirdDayAlanDialog";
        public const string barDrinkSanCheck = "barDrinkSanCheck";
        public const string barMove = "barMove";
        public const string barArrive = "barArrive";
        public const string daveDeathKeyword = "daveDeathKeyword";
        public const string albertDeathKeyword = "albertDeathKeyword";
        public const string swainDeathKeyword = "swainDeathKeyword";
        public const string albertFirstAfterEvent = "albertFirstAfterEvent";
        public const string daveSafeOpen = "daveSafeOpen";
        public const string daveAfterSafe = "daveAfterSafe";
        public const string daveAfterBreclet = "daveAfterBreclet";
        public const string daveCheck = "daveCheck";
        public const string secondDayDialog = "secondDayDialog";
        public const string afterSanCheck = "afterSanCheck";
        public const string case2IntCheck = "case2IntCheck";
        public const string case2SanCheck = "case2SanCheck";
        public const string swainDie = "swainDie";
        public const string kaneSlimeSan = "kaneSlimeSan";
        public const string noonMorningMove = "noonMorningMove";
        public const string secondDetectiveNoonEvent = "secondDetectiveNoonEvent";
        public const string noonEventMovetoClient = "noonEventMovetoClient";
        public const string noonEventClientsFirst = "noonEventClientsFirst";
        public const string noonEventAccessAuthorization = "noonEventAccessAuthorization";
        public const string witnessFirstDailog = "witnessFirstDailog";
        public const string doorPoliceFirstDailog = "doorPoliceFirstDailog";
        public const string noonEventFirstDoor = "noonEventFirstDoor";
        public const string noonEventSanCheck = "noonEventSanCheck";
        public const string insidePoliceFirstDailog = "insidePoliceFirstDailog";
        public const string deadBodyFirstLook = "deadBodyFirstLook";
        public const string deadBodyLookMissingDialog = "deadBodyLookMissingDialog";
        public const string deadBodyLookFishySmellDialog = "deadBodyLookFishySmellDialog";
        public const string deadBodyMedicine = "deadBodyMedicine";
        public const string deadBodyObservational = "deadBodyObservational";
        public const string deadBodyAnalyzing = "deadBodyAnalyzing";
        public const string deadBodyDeftness = "deadBodyDeftness";
        public const string deadBodyGotoSwere = "deadBodyGotoSwere";
        public const string siteAnalyzing = "siteAnalyzing";
        public const string thinWallets = "thinWallets";
        public const string carelessness = "carelessness";
        public const string debilitation = "debilitation";
        public const string dizziness = "dizziness";
        public const string sprains = "sprains";
        public const string unlucky = "unlucky";
        public const string musclePain = "musclePain";
        public const string migraines = "migraines";
        public const string eyeDisease = "eyeDisease";
        public const string masochism = "masochism";
        public const string careless = "careless";
        public const string mentalWeakness = "mentalWeakness";
        public const string helplessness = "helplessness";
        public const string extravagant = "extravagant";
        public const string panicAttack = "panicAttack";
        public const string medicaldistrust = "medicaldistrust";
        public const string hallucinations = "hallucinations";
        public const string tightwad = "tightwad";
        public const string nightPhobia = "nightPhobia";
        public const string deafness = "deafness";
        public const string visionLoss = "visionLoss";
        public const string shortTempered = "shortTempered";
        public const string dyslexia = "dyslexia";
        public const string drugPhobia = "drugPhobia";
        public const string cognitiveDisorder = "cognitiveDisorder";
        public const string cognitiveBreakdown = "cognitiveBreakdown";
        public const string homicidalImpulse = "homicidalImpulse";
        public const string perfectionism = "perfectionism";
        public const string isUseSafe = "isUseSafe";
        public const string battleSan = "battleSan";
        public const string isBattleBeforeDialog = "isBattleBeforeDialog";
        public const string afterBattle = "afterBattle";
        public const string endBattle = "endBattle";
        public const string roadBattleEnd = "roadBattleEnd";
        public const string firstClientsHouse = "firstClientsHouse";
        public const string firstClientsHouseEnd = "firstClientsHouseEnd";
        public const string isBar = "isBar";
        public const string maeveFirstDialog = "maeveFirstDialog";
        public const string workerFirstDialog = "workerFirstDialog";
        public const string daveFirstDialog = "daveFirstDialog";
        public const string aFirstDialog = "aFirstDialog";
        public const string bFirstDialog = "bFirstDialog";
        public const string albertFirstDialog = "albertFirstDialog";
        public const string alanFirstDialog = "alanFirstDialog";
        public const string kateDisapear = "kateDisapear";
        public const string kateNoonFirstDialog = "kateNoonFirstDialog";
        public const string alanNoonFirstDialog = "alanNoonFirstDialog";
        public const string kaneFirstDialog = "kaneFirstDialog";
        public const string sewerFirstDialog = "sewerFirstDialog";
        public const string sewerObservation = "sewerObservation";
        public const string sewerObservationEnd = "sewerObservationEnd";
        public const string sewerSan = "sewerSan";
        public const string univFirstDialog = "univFirstDialog";
        public const string swainFirstDialog = "swainFirstDialog";
        public const string isSwainTalkMissingpeople = "isSwainTalkMissingpeople";
        public const string aidenFirstDialog = "aidenFirstDialog";
        public const string aidenObservation = "aidenObservation";
        public const string aidenMedicine = "aidenMedicine";
        public const string aidenPsychotherapy = "aidenPsychotherapy";
        public const string ellaEndDialog = "ellaEndDialog";
        public const string isFirstDoor = "isFirstDoor";
        public const string isFirst2st = "isFirst2st";
        public const string isFirstRoom = "isFirstRoom";
        public const string getSafe = "getSafe";
        public const string strDialogOn = "strDialogOn";
        public const string failTwoKeyword = "failTwoKeyword";
        public const string endDemo = "endDemo";
        public const string endDay = "endDay";
        public const string connectKey = "connectKey";
        public const string fstDetectiveEndFirstDialog = "fstDetectiveEndFirstDialog";
        public const string fstDetectiveNockDialog = "fstDetectiveNockDialog";
        public const string fstDetectiveAfterSelect = "fstDetectiveAfterSelect";
        public const string fstDetectiveAfterSelectNo = "fstDetectiveAfterSelectNo";
        public const string fstDetectiveEndSelect = "fstDetectiveEndSelect";
        public const string fstDetectiveNewspaper = "fstDetectiveNewspaper";
        public const string intelAiden1 = "intelAiden1";
        public const string intelAiden2 = "intelAiden2";
        public const string intelAiden3 = "intelAiden3";
        public const string intelAiden4 = "intelAiden4";
        public const string intelAiden5 = "intelAiden5";
        public const string intelAiden6 = "intelAiden6";
        public const string intelElla1 = "intelElla1";
        public const string intelElla2 = "intelElla2";
        public const string intelElla3 = "intelElla3";
        public const string intelElla4 = "intelElla4";
        public const string intelElla5 = "intelElla5";
        public const string intelElla6 = "intelElla6";
        public const string intelSwain1 = "intelSwain1";
        public const string intelSwain2 = "intelSwain2";
        public const string intelSwain3 = "intelSwain3";
        public const string intelSwain4 = "intelSwain4";
        public const string intelSwain5 = "intelSwain5";
        public const string intelSwain6 = "intelSwain6";
        public const string intelCane1 = "intelCane1";
        public const string intelCane2 = "intelCane2";
        public const string intelCane3 = "intelCane3";
        public const string intelCane4 = "intelCane4";
        public const string intelCane5 = "intelCane5";
        public const string intelCane6 = "intelCane6";
        public const string intelAlbert1 = "intelAlbert1";
        public const string intelAlbert2 = "intelAlbert2";
        public const string intelAlbert3 = "intelAlbert3";
        public const string intelAlbert4 = "intelAlbert4";
        public const string intelAlbert5 = "intelAlbert5";
        public const string intelAlbert6 = "intelAlbert6";
        public const string intelMeiv1 = "intelMeiv1";
        public const string intelMeiv2 = "intelMeiv2";
        public const string intelMeiv3 = "intelMeiv3";
        public const string intelMeiv4 = "intelMeiv4";
        public const string intelMeiv5 = "intelMeiv5";
        public const string intelMeiv6 = "intelMeiv6";
        public const string intelBlackWell1 = "intelBlackWell1";
        public const string intelBlackWell2 = "intelBlackWell2";
        public const string intelBlackWell3 = "intelBlackWell3";
        public const string intelBlackWell4 = "intelBlackWell4";
        public const string intelBlackWell5 = "intelBlackWell5";
        public const string intelBlackWell6 = "intelBlackWell6";
        public const string intelPlanetaryParade1 = "intelPlanetaryParade1";
        public const string intelPlanetaryParade2 = "intelPlanetaryParade2";
        public const string intelPlanetaryParade3 = "intelPlanetaryParade3";
        public const string intelPlanetaryParade4 = "intelPlanetaryParade4";
        public const string intelPlanetaryParade5 = "intelPlanetaryParade5";
        public const string intelPlanetaryParade6 = "intelPlanetaryParade6";
        public const string intelNightmare1 = "intelNightmare1";
        public const string intelNightmare2 = "intelNightmare2";
        public const string intelNightmare3 = "intelNightmare3";
        public const string intelNightmare4 = "intelNightmare4";
        public const string intelNightmare5 = "intelNightmare5";
        public const string intelNightmare6 = "intelNightmare6";
        public const string intelParanoia1 = "intelParanoia1";
        public const string intelParanoia2 = "intelParanoia2";
        public const string intelParanoia3 = "intelParanoia3";
        public const string intelParanoia4 = "intelParanoia4";
        public const string intelParanoia5 = "intelParanoia5";
        public const string intelParanoia6 = "intelParanoia6";
        public const string intelInsomnia1 = "intelInsomnia1";
        public const string intelInsomnia2 = "intelInsomnia2";
        public const string intelInsomnia3 = "intelInsomnia3";
        public const string intelInsomnia4 = "intelInsomnia4";
        public const string intelInsomnia5 = "intelInsomnia5";
        public const string intelInsomnia6 = "intelInsomnia6";
        public const string intelSewerGhostStory1 = "intelSewerGhostStory1";
        public const string intelSewerGhostStory2 = "intelSewerGhostStory2";
        public const string intelSewerGhostStory3 = "intelSewerGhostStory3";
        public const string intelSewerGhostStory4 = "intelSewerGhostStory4";
        public const string intelSewerGhostStory5 = "intelSewerGhostStory5";
        public const string intelSewerGhostStory6 = "intelSewerGhostStory6";
        // SEWER S2 조사 포인트 단서(추가만).
        public const string intelSewerClueGraffiti = "intelSewerClueGraffiti";
        public const string intelSewerClueWorkwear = "intelSewerClueWorkwear";
        public const string intelSewerClueLog = "intelSewerClueLog";
        public const string intelSewerClueFootprints = "intelSewerClueFootprints";
        public const string intelSewerClueSignboard = "intelSewerClueSignboard";
        public const string intelMissingPeople1 = "intelMissingPeople1";
        public const string intelMissingPeople2 = "intelMissingPeople2";
        public const string intelMissingPeople3 = "intelMissingPeople3";
        public const string intelMissingPeople4 = "intelMissingPeople4";
        public const string intelMissingPeople5 = "intelMissingPeople5";
        public const string intelMissingPeople6 = "intelMissingPeople6";
        public const string intelFishySmell1 = "intelFishySmell1";
        public const string intelFishySmell2 = "intelFishySmell2";
        public const string intelFishySmell3 = "intelFishySmell3";
        public const string intelFishySmell4 = "intelFishySmell4";
        public const string intelFishySmell5 = "intelFishySmell5";
        public const string intelFishySmell6 = "intelFishySmell6";
        public const string intelRiverWaterQuality1 = "intelRiverWaterQuality1";
        public const string intelRiverWaterQuality2 = "intelRiverWaterQuality2";
        public const string intelRiverWaterQuality3 = "intelRiverWaterQuality3";
        public const string intelRiverWaterQuality4 = "intelRiverWaterQuality4";
        public const string intelRiverWaterQuality5 = "intelRiverWaterQuality5";
        public const string intelRiverWaterQuality6 = "intelRiverWaterQuality6";
        public const string intelCreepyEyes1 = "intelCreepyEyes1";
        public const string intelCreepyEyes2 = "intelCreepyEyes2";
        public const string intelCreepyEyes3 = "intelCreepyEyes3";
        public const string intelCreepyEyes4 = "intelCreepyEyes4";
        public const string intelCreepyEyes5 = "intelCreepyEyes5";
        public const string intelCreepyEyes6 = "intelCreepyEyes6";
        public const string intelSewerWorker1 = "intelSewerWorker1";
        public const string intelSewerWorker2 = "intelSewerWorker2";
        public const string intelSewerWorker3 = "intelSewerWorker3";
        public const string intelSewerWorker4 = "intelSewerWorker4";
        public const string intelSewerWorker5 = "intelSewerWorker5";
        public const string intelSewerWorker6 = "intelSewerWorker6";
        public const string intelDave1 = "intelDave1";
        public const string intelDave2 = "intelDave2";
        public const string intelDave3 = "intelDave3";
        public const string intelDave4 = "intelDave4";
        public const string intelDave5 = "intelDave5";
        public const string intelDave6 = "intelDave6";
        public const string intelSafe1 = "intelSafe1";
        public const string intelSafe2 = "intelSafe2";
        public const string intelSafe3 = "intelSafe3";
        public const string intelSafe4 = "intelSafe4";
        public const string intelSafe5 = "intelSafe5";
        public const string intelSafe6 = "intelSafe6";
        public const string intelMurderCase1 = "intelMurderCase1";
        public const string intelMurderCase2 = "intelMurderCase2";
        public const string intelMurderCase3 = "intelMurderCase3";
        public const string intelMurderCase4 = "intelMurderCase4";
        public const string intelMurderCase5 = "intelMurderCase5";
        public const string intelMurderCase6 = "intelMurderCase6";
        public const string intelDetectiveOffice1 = "intelDetectiveOffice1";
        public const string intelDetectiveOffice2 = "intelDetectiveOffice2";
        public const string intelDetectiveOffice3 = "intelDetectiveOffice3";
        public const string intelDetectiveOffice4 = "intelDetectiveOffice4";
        public const string intelDetectiveOffice5 = "intelDetectiveOffice5";
        public const string intelDetectiveOffice6 = "intelDetectiveOffice6";
        public const string intelHospital1 = "intelHospital1";
        public const string intelHospital2 = "intelHospital2";
        public const string intelHospital3 = "intelHospital3";
        public const string intelHospital4 = "intelHospital4";
        public const string intelHospital5 = "intelHospital5";
        public const string intelHospital6 = "intelHospital6";
        public const string intelNewspaper1 = "intelNewspaper1";
        public const string intelNewspaper2 = "intelNewspaper2";
        public const string intelNewspaper3 = "intelNewspaper3";
        public const string intelNewspaper4 = "intelNewspaper4";
        public const string intelNewspaper5 = "intelNewspaper5";
        public const string intelNewspaper6 = "intelNewspaper6";
        public const string intelQuestHouse1 = "intelQuestHouse1";
        public const string intelQuestHouse2 = "intelQuestHouse2";
        public const string intelQuestHouse3 = "intelQuestHouse3";
        public const string intelQuestHouse4 = "intelQuestHouse4";
        public const string intelQuestHouse5 = "intelQuestHouse5";
        public const string intelQuestHouse6 = "intelQuestHouse6";
        public const string intelStation1 = "intelStation1";
        public const string intelStation2 = "intelStation2";
        public const string intelStation3 = "intelStation3";
        public const string intelStation4 = "intelStation4";
        public const string intelStation5 = "intelStation5";
        public const string intelStation6 = "intelStation6";
        public const string intelSewer1 = "intelSewer1";
        public const string intelSewer2 = "intelSewer2";
        public const string intelSewer3 = "intelSewer3";
        public const string intelSewer4 = "intelSewer4";
        public const string intelSewer5 = "intelSewer5";
        public const string intelSewer6 = "intelSewer6";
        public const string intelUniversity1 = "intelUniversity1";
        public const string intelUniversity2 = "intelUniversity2";
        public const string intelUniversity3 = "intelUniversity3";
        public const string intelUniversity4 = "intelUniversity4";
        public const string intelUniversity5 = "intelUniversity5";
        public const string intelUniversity6 = "intelUniversity6";
        public const string intelRiverside1 = "intelRiverside1";
        public const string intelRiverside2 = "intelRiverside2";
        public const string intelRiverside3 = "intelRiverside3";
        public const string intelRiverside4 = "intelRiverside4";
        public const string intelRiverside5 = "intelRiverside5";
        public const string intelRiverside6 = "intelRiverside6";
        public const string intelBar1 = "intelBar1";
        public const string intelBar2 = "intelBar2";
        public const string intelBar3 = "intelBar3";
        public const string intelBar4 = "intelBar4";
        public const string intelBar5 = "intelBar5";
        public const string intelBar6 = "intelBar6";
        public const string intelSlum1 = "intelSlum1";
        public const string intelSlum2 = "intelSlum2";
        public const string intelSlum3 = "intelSlum3";
        public const string intelSlum4 = "intelSlum4";
        public const string intelSlum5 = "intelSlum5";
        public const string intelSlum6 = "intelSlum6";
        public const string intelPoliceOffice1 = "intelPoliceOffice1";
        public const string intelPoliceOffice2 = "intelPoliceOffice2";
        public const string intelPoliceOffice3 = "intelPoliceOffice3";
        public const string intelPoliceOffice4 = "intelPoliceOffice4";
        public const string intelPoliceOffice5 = "intelPoliceOffice5";
        public const string intelPoliceOffice6 = "intelPoliceOffice6";
        public const string intelGunsmith1 = "intelGunsmith1";
        public const string intelGunsmith2 = "intelGunsmith2";
        public const string intelGunsmith3 = "intelGunsmith3";
        public const string intelGunsmith4 = "intelGunsmith4";
        public const string intelGunsmith5 = "intelGunsmith5";
        public const string intelGunsmith6 = "intelGunsmith6";
        public const string intelSewageMaintenanceOffice1 = "intelSewageMaintenanceOffice1";
        public const string intelSewageMaintenanceOffice2 = "intelSewageMaintenanceOffice2";
        public const string intelSewageMaintenanceOffice3 = "intelSewageMaintenanceOffice3";
        public const string intelSewageMaintenanceOffice4 = "intelSewageMaintenanceOffice4";
        public const string intelSewageMaintenanceOffice5 = "intelSewageMaintenanceOffice5";
        public const string intelSewageMaintenanceOffice6 = "intelSewageMaintenanceOffice6";
        public const string intelWharf1 = "intelWharf1";
        public const string intelWharf2 = "intelWharf2";
        public const string intelWharf3 = "intelWharf3";
        public const string intelWharf4 = "intelWharf4";
        public const string intelWharf5 = "intelWharf5";
        public const string intelWharf6 = "intelWharf6";

        public static readonly string[] All =
        {
            isNightmare,
            endingDoorCheck,
            endingDeathCheck,
            endingRequestCheck,
            endingSwainDeathCheck,
            endingAfterPromiseDialog,
            endingisPromise,
            endingBreakSymbol,
            endingLadderSan,
            endingSwin,
            sewerMap,
            ifGotRequset,
            sewerFirstIn,
            sewerFirstInSanCheck,
            sewerDeepOneFirstSanCheck,
            isDebuffNightPhobia,
            isDebuffDeftness,
            isDebuffVisionLoss,
            isDebuffShortTempred,
            isDebuffDyslexia,
            isDebuffDrugPhobia,
            isDebuffCognitiveDisorder,
            isDebuffCognitiveBreakdown,
            isDebuffPerfectionism,
            sewerPicingSucc,
            isMorningMapMove,
            afterDaveDeathFirstSlum,
            iSRoadBattleStart,
            bgmChangeChecker,
            sewerBattleChecker,
            symbolStrSanCheck,
            symbolOccultFirst,
            sewerSymbolOccultCheck,
            bookLookFirst,
            sewerBookOccultCheck,
            sewerBookSanCheck,
            journalFirstSanCheck,
            journalSanCheck,
            firstLookJournal,
            gainOldMap,
            safeDocument,
            insmusRhtoric,
            isInsmusMeetSewer,
            sewerBattleEndCheck,
            inSewerStealthFail,
            inSewerStealthSucc,
            startSewerBattle,
            sewerHouseIn,
            blackRechSuccCanTalck,
            blackDeftness,
            blackRhethic,
            blackFirstDialog,
            mapLooked,
            mapFirstDialog,
            sewerageKey,
            judgeSafeTryOpen,
            judgeCoatTryDisguise,
            gainCoat,
            gainMap,
            wearCoat,
            isSewerOffice,
            isSewerOfficeBrokenWindow,
            tryBrokenWindow,
            tryObserLokcer,
            moveSewerOfficeWindow,
            morningMove2nd,
            morningMove4th,
            day4Case1San,
            day4Case1Health,
            sewerDoorOpen,
            sewerDoorOpenDeftness,
            sewerDoorOpenCheck,
            morningMove_3rd,
            thirdDayDreamSan,
            thirdDayAlbertDialog,
            thirdDayPoliceADialog,
            thirdDayPoliceBDialog,
            thirdDayAlanDialog,
            barDrinkSanCheck,
            barMove,
            barArrive,
            daveDeathKeyword,
            albertDeathKeyword,
            swainDeathKeyword,
            albertFirstAfterEvent,
            daveSafeOpen,
            daveAfterSafe,
            daveAfterBreclet,
            daveCheck,
            secondDayDialog,
            afterSanCheck,
            case2IntCheck,
            case2SanCheck,
            swainDie,
            kaneSlimeSan,
            noonMorningMove,
            secondDetectiveNoonEvent,
            noonEventMovetoClient,
            noonEventClientsFirst,
            noonEventAccessAuthorization,
            witnessFirstDailog,
            doorPoliceFirstDailog,
            noonEventFirstDoor,
            noonEventSanCheck,
            insidePoliceFirstDailog,
            deadBodyFirstLook,
            deadBodyLookMissingDialog,
            deadBodyLookFishySmellDialog,
            deadBodyMedicine,
            deadBodyObservational,
            deadBodyAnalyzing,
            deadBodyDeftness,
            deadBodyGotoSwere,
            siteAnalyzing,
            thinWallets,
            carelessness,
            debilitation,
            dizziness,
            sprains,
            unlucky,
            musclePain,
            migraines,
            eyeDisease,
            masochism,
            careless,
            mentalWeakness,
            helplessness,
            extravagant,
            panicAttack,
            medicaldistrust,
            hallucinations,
            tightwad,
            nightPhobia,
            deafness,
            visionLoss,
            shortTempered,
            dyslexia,
            drugPhobia,
            cognitiveDisorder,
            cognitiveBreakdown,
            homicidalImpulse,
            perfectionism,
            isUseSafe,
            battleSan,
            isBattleBeforeDialog,
            afterBattle,
            endBattle,
            roadBattleEnd,
            firstClientsHouse,
            firstClientsHouseEnd,
            isBar,
            maeveFirstDialog,
            workerFirstDialog,
            daveFirstDialog,
            aFirstDialog,
            bFirstDialog,
            albertFirstDialog,
            alanFirstDialog,
            kateDisapear,
            kateNoonFirstDialog,
            alanNoonFirstDialog,
            kaneFirstDialog,
            sewerFirstDialog,
            sewerObservation,
            sewerObservationEnd,
            sewerSan,
            univFirstDialog,
            swainFirstDialog,
            isSwainTalkMissingpeople,
            aidenFirstDialog,
            aidenObservation,
            aidenMedicine,
            aidenPsychotherapy,
            ellaEndDialog,
            isFirstDoor,
            isFirst2st,
            isFirstRoom,
            getSafe,
            strDialogOn,
            failTwoKeyword,
            endDemo,
            endDay,
            connectKey,
            fstDetectiveEndFirstDialog,
            fstDetectiveNockDialog,
            fstDetectiveAfterSelect,
            fstDetectiveAfterSelectNo,
            fstDetectiveEndSelect,
            fstDetectiveNewspaper,
            intelAiden1,
            intelAiden2,
            intelAiden3,
            intelAiden4,
            intelAiden5,
            intelAiden6,
            intelElla1,
            intelElla2,
            intelElla3,
            intelElla4,
            intelElla5,
            intelElla6,
            intelSwain1,
            intelSwain2,
            intelSwain3,
            intelSwain4,
            intelSwain5,
            intelSwain6,
            intelCane1,
            intelCane2,
            intelCane3,
            intelCane4,
            intelCane5,
            intelCane6,
            intelAlbert1,
            intelAlbert2,
            intelAlbert3,
            intelAlbert4,
            intelAlbert5,
            intelAlbert6,
            intelMeiv1,
            intelMeiv2,
            intelMeiv3,
            intelMeiv4,
            intelMeiv5,
            intelMeiv6,
            intelBlackWell1,
            intelBlackWell2,
            intelBlackWell3,
            intelBlackWell4,
            intelBlackWell5,
            intelBlackWell6,
            intelPlanetaryParade1,
            intelPlanetaryParade2,
            intelPlanetaryParade3,
            intelPlanetaryParade4,
            intelPlanetaryParade5,
            intelPlanetaryParade6,
            intelNightmare1,
            intelNightmare2,
            intelNightmare3,
            intelNightmare4,
            intelNightmare5,
            intelNightmare6,
            intelParanoia1,
            intelParanoia2,
            intelParanoia3,
            intelParanoia4,
            intelParanoia5,
            intelParanoia6,
            intelInsomnia1,
            intelInsomnia2,
            intelInsomnia3,
            intelInsomnia4,
            intelInsomnia5,
            intelInsomnia6,
            intelSewerGhostStory1,
            intelSewerGhostStory2,
            intelSewerGhostStory3,
            intelSewerGhostStory4,
            intelSewerGhostStory5,
            intelSewerGhostStory6,
            intelMissingPeople1,
            intelMissingPeople2,
            intelMissingPeople3,
            intelMissingPeople4,
            intelMissingPeople5,
            intelMissingPeople6,
            intelFishySmell1,
            intelFishySmell2,
            intelFishySmell3,
            intelFishySmell4,
            intelFishySmell5,
            intelFishySmell6,
            intelRiverWaterQuality1,
            intelRiverWaterQuality2,
            intelRiverWaterQuality3,
            intelRiverWaterQuality4,
            intelRiverWaterQuality5,
            intelRiverWaterQuality6,
            intelCreepyEyes1,
            intelCreepyEyes2,
            intelCreepyEyes3,
            intelCreepyEyes4,
            intelCreepyEyes5,
            intelCreepyEyes6,
            intelSewerWorker1,
            intelSewerWorker2,
            intelSewerWorker3,
            intelSewerWorker4,
            intelSewerWorker5,
            intelSewerWorker6,
            intelDave1,
            intelDave2,
            intelDave3,
            intelDave4,
            intelDave5,
            intelDave6,
            intelSafe1,
            intelSafe2,
            intelSafe3,
            intelSafe4,
            intelSafe5,
            intelSafe6,
            intelMurderCase1,
            intelMurderCase2,
            intelMurderCase3,
            intelMurderCase4,
            intelMurderCase5,
            intelMurderCase6,
            intelDetectiveOffice1,
            intelDetectiveOffice2,
            intelDetectiveOffice3,
            intelDetectiveOffice4,
            intelDetectiveOffice5,
            intelDetectiveOffice6,
            intelHospital1,
            intelHospital2,
            intelHospital3,
            intelHospital4,
            intelHospital5,
            intelHospital6,
            intelNewspaper1,
            intelNewspaper2,
            intelNewspaper3,
            intelNewspaper4,
            intelNewspaper5,
            intelNewspaper6,
            intelQuestHouse1,
            intelQuestHouse2,
            intelQuestHouse3,
            intelQuestHouse4,
            intelQuestHouse5,
            intelQuestHouse6,
            intelStation1,
            intelStation2,
            intelStation3,
            intelStation4,
            intelStation5,
            intelStation6,
            intelSewer1,
            intelSewer2,
            intelSewer3,
            intelSewer4,
            intelSewer5,
            intelSewer6,
            intelSewerClueGraffiti,
            intelSewerClueWorkwear,
            intelSewerClueLog,
            intelSewerClueFootprints,
            intelSewerClueSignboard,
            intelUniversity1,
            intelUniversity2,
            intelUniversity3,
            intelUniversity4,
            intelUniversity5,
            intelUniversity6,
            intelRiverside1,
            intelRiverside2,
            intelRiverside3,
            intelRiverside4,
            intelRiverside5,
            intelRiverside6,
            intelBar1,
            intelBar2,
            intelBar3,
            intelBar4,
            intelBar5,
            intelBar6,
            intelSlum1,
            intelSlum2,
            intelSlum3,
            intelSlum4,
            intelSlum5,
            intelSlum6,
            intelPoliceOffice1,
            intelPoliceOffice2,
            intelPoliceOffice3,
            intelPoliceOffice4,
            intelPoliceOffice5,
            intelPoliceOffice6,
            intelGunsmith1,
            intelGunsmith2,
            intelGunsmith3,
            intelGunsmith4,
            intelGunsmith5,
            intelGunsmith6,
            intelSewageMaintenanceOffice1,
            intelSewageMaintenanceOffice2,
            intelSewageMaintenanceOffice3,
            intelSewageMaintenanceOffice4,
            intelSewageMaintenanceOffice5,
            intelSewageMaintenanceOffice6,
            intelWharf1,
            intelWharf2,
            intelWharf3,
            intelWharf4,
            intelWharf5,
            intelWharf6,
        };

        /// <summary>초기값이 true 인 플래그(New() 가 넣는다).</summary>
        public static readonly string[] DefaultTrue = { bgmChangeChecker };
    }

    /// <summary>영구 int 키(59). 0 은 보관하지 않는다.</summary>
    public static class CounterKeys
    {
        public const string enemyDeathCount = "enemyDeathCount";
        public const string endingLadderSanInt = "endingLadderSanInt";
        public const string posionDebuff = "posionDebuff";
        public const string sewerEnemyCounter = "sewerEnemyCounter";
        public const string battleEnemyCount = "battleEnemyCount";
        public const string debuff = "debuff";
        public const string bullet = "bullet";
        public const string shotgun = "shotgun";
        public const string rifle = "rifle";
        public const string revolver = "revolver";
        public const string smallPistol = "smallPistol";
        public const string bat = "bat";
        public const string axe = "axe";
        public const string molotov = "molotov";
        public const string dagger = "dagger";
        public const string pistolAmmo = "pistolAmmo";
        public const string rifleAmmo = "rifleAmmo";
        public const string shotgunAmmo = "shotgunAmmo";
        public const string firstaidkit = "firstaidkit";
        public const string bandages = "bandages";
        public const string painkillers = "painkillers";
        public const string aidenKeywordCount = "aidenKeywordCount";
        public const string nowSmallPistol = "nowSmallPistol";
        public const string nowRevolver = "nowRevolver";
        public const string nowRifle = "nowRifle";
        public const string nowShotgun = "nowShotgun";
        public const string timeCount = "timeCount";
        public const string fstDetectivTimeOn = "fstDetectivTimeOn";
        public const string lockerTryTime = "lockerTryTime";
        public const string str = "str";
        public const string intl = "intl";
        public const string dex = "dex";
        public const string hp = "hp";
        public const string mp = "mp";
        public const string san = "san";
        public const string luk = "luk";
        public const string weal = "weal";
        public const string nowHP = "nowHP";
        public const string nowSan = "nowSan";
        public const string strSkillPoint = "strSkillPoint";
        public const string martialArtsPoint = "martialArtsPoint";
        public const string gunShotPoint = "gunShotPoint";
        public const string swordPoint = "swordPoint";
        public const string observationPoint = "observationPoint";
        public const string swimingPoint = "swimingPoint";
        public const string intSkillPoint = "intSkillPoint";
        public const string medicinePoint = "medicinePoint";
        public const string analysisPoint = "analysisPoint";
        public const string listeningPoint = "listeningPoint";
        public const string psychotherapyPoint = "psychotherapyPoint";
        public const string occultPoint = "occultPoint";
        public const string dexSkillPoint = "dexSkillPoint";
        public const string evasionPoint = "evasionPoint";
        public const string deftnessPoint = "deftnessPoint";
        public const string rhetoricPoint = "rhetoricPoint";
        public const string stealthPoint = "stealthPoint";
        public const string disguisePoint = "disguisePoint";
        public const string gainCharacterInt = "gainCharacterInt";
        public const string gainEventInt = "gainEventInt";
        public const string gainPlaceInt = "gainPlaceInt";

        public static readonly string[] All =
        {
            enemyDeathCount,
            endingLadderSanInt,
            posionDebuff,
            sewerEnemyCounter,
            battleEnemyCount,
            debuff,
            bullet,
            shotgun,
            rifle,
            revolver,
            smallPistol,
            bat,
            axe,
            molotov,
            dagger,
            pistolAmmo,
            rifleAmmo,
            shotgunAmmo,
            firstaidkit,
            bandages,
            painkillers,
            aidenKeywordCount,
            nowSmallPistol,
            nowRevolver,
            nowRifle,
            nowShotgun,
            timeCount,
            fstDetectivTimeOn,
            lockerTryTime,
            str,
            intl,
            dex,
            hp,
            mp,
            san,
            luk,
            weal,
            nowHP,
            nowSan,
            strSkillPoint,
            martialArtsPoint,
            gunShotPoint,
            swordPoint,
            observationPoint,
            swimingPoint,
            intSkillPoint,
            medicinePoint,
            analysisPoint,
            listeningPoint,
            psychotherapyPoint,
            occultPoint,
            dexSkillPoint,
            evasionPoint,
            deftnessPoint,
            rhetoricPoint,
            stealthPoint,
            disguisePoint,
            gainCharacterInt,
            gainEventInt,
            gainPlaceInt,
        };

        /// <summary>초기값이 0 이 아닌 카운터.</summary>
        public static readonly KeyValuePair<string, int>[] NonZeroDefaults =
        {
            new KeyValuePair<string, int>(timeCount, 1),
            new KeyValuePair<string, int>(martialArtsPoint, 25),
            new KeyValuePair<string, int>(gunShotPoint, 15),
            new KeyValuePair<string, int>(swordPoint, 20),
            new KeyValuePair<string, int>(observationPoint, 20),
            new KeyValuePair<string, int>(swimingPoint, 10),
            new KeyValuePair<string, int>(medicinePoint, 10),
            new KeyValuePair<string, int>(analysisPoint, 30),
            new KeyValuePair<string, int>(listeningPoint, 30),
            new KeyValuePair<string, int>(psychotherapyPoint, 5),
            new KeyValuePair<string, int>(occultPoint, 5),
            new KeyValuePair<string, int>(evasionPoint, 30),
            new KeyValuePair<string, int>(deftnessPoint, 20),
            new KeyValuePair<string, int>(rhetoricPoint, 20),
            new KeyValuePair<string, int>(stealthPoint, 15),
            new KeyValuePair<string, int>(disguisePoint, 10),
        };
    }

    /// <summary>영구 string 키(3). 빈 문자열은 보관하지 않는다.</summary>
    public static class TextKeys
    {
        public const string nowPlace = "nowPlace";
        public const string potalPlace = "potalPlace";
        public const string battleWeapon = "battleWeapon";

        public static readonly string[] All =
        {
            nowPlace,
            potalPlace,
            battleWeapon,
        };

        /// <summary>초기값이 빈 문자열이 아닌 텍스트.</summary>
        public static readonly KeyValuePair<string, string>[] NonEmptyDefaults =
        {
            new KeyValuePair<string, string>(nowPlace, "DetectiveOffice"),
        };
    }
}
