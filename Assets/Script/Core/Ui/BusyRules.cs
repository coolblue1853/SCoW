namespace SCoW.Core.Ui
{
    /// <summary>
    /// 연출·대기 플래그 묶음(읽기 전용 스냅샷). 옛 DataBaseManager 정적 bool 들의 값을 App 의 UiGate 가 채워 넘긴다.
    /// </summary>
    public struct BusyFlags
    {
        public bool Dialog1;          // isActiveDialog1
        public bool Dialog2;          // isActiveDialog2
        public bool StoryDirecting;   // storyDirecting
        public bool SecondDirecting;  // secondisDirecting
        public bool Directing;        // isDirecting
        public bool Rollet;           // isRollet
        public bool BattleRollet;     // isBattleRollet
        public bool PortalWait;       // potalWait
        public bool MapMoving;        // isOpenMap(지도 이동 연출 중. 지도 패널과 다름)
        public bool Scene;            // isScene
        public bool Select;           // isSelect
        public bool SelectionOn;      // selectionOn
        public bool BattleSan;        // battleSan
        public bool StrDialogOn;      // strDialogOn
        public bool BarMove;          // barMove
        public bool BattleBeforeDialog; // isBattleBeforeDialog
        public bool AfterBattle;      // afterBattle
    }

    /// <summary>
    /// "지금 무엇이 막히나" 규칙 한 곳(P5). 옛 코드의 8플래그 AND 복제(설정·정보·지도·인벤·상점·이동·포털·상호작용)를 옮긴 것.
    /// 기존 막힘 목록은 Tests/EditMode/BusyRulesTests 로 고정되어 있다. 바꾸려면 테스트부터 바꾼다.
    /// </summary>
    public static class BusyRules
    {
        public const string BattleRoadPlace = "BattleRoad";

        /// <summary>저장·불러오기를 막는 "연출·대기 중". 열린 패널(UI)은 보지 않는다(설정창이 저장 창을 여는 쪽이라).</summary>
        public static bool IsBusy(BusyFlags f)
        {
            return f.Dialog1 || f.Dialog2 || f.StoryDirecting || f.SecondDirecting || f.Directing
                || f.Rollet || f.BattleRollet || f.PortalWait || f.MapMoving
                || f.Scene || f.Select || f.SelectionOn || f.BattleSan || f.StrDialogOn || f.BarMove
                || f.BattleBeforeDialog || f.AfterBattle;
        }

        /// <summary>
        /// 패널을 열 수 있나. place 는 현재 장소 이름, dyslexia 는 난독 디버프(정보창만 막음).
        /// 설정창은 대사·스토리 연출 중에도 열 수 있다(Audit B-4). 판정·포털·연출·지도 이동 중에는 여전히 막는다.
        /// </summary>
        public static bool CanOpen(PanelKind kind, BusyFlags f, UiStack stack, string place, bool dyslexia)
        {
            switch (kind)
            {
                case PanelKind.Setting:
                    return !stack.Any && !f.Rollet && !f.PortalWait && !f.Directing && !f.MapMoving;
                case PanelKind.Intel:
                    return !dyslexia && ModalFree(f, stack) && !f.MapMoving;
                case PanelKind.Map:
                case PanelKind.Inventory:
                    return ModalFree(f, stack) && !f.MapMoving && place != BattleRoadPlace;
                case PanelKind.Shop:
                    return !stack.Any && !f.Dialog1 && !f.Rollet && !f.Directing;
                case PanelKind.SlotMenu:
                    // 설정창 위에서만(타이틀은 스택이 빈 채로 연다). 이미 떠 있으면 중복 금지.
                    return !stack.Contains(PanelKind.SlotMenu) && (!stack.Any || stack.IsTop(PanelKind.Setting));
                default:
                    return true;
            }
        }

        // 정보·지도·인벤 공통: 대사·스토리·포털·2차 연출·판정·연출이 없고 다른 패널이 없다.
        private static bool ModalFree(BusyFlags f, UiStack stack)
        {
            return !stack.Any && !f.Dialog1 && !f.StoryDirecting && !f.PortalWait && !f.SecondDirecting && !f.Rollet && !f.Directing;
        }

        /// <summary>플레이어 이동(Player.cs).</summary>
        public static bool CanMove(BusyFlags f, UiStack stack)
        {
            return !stack.Any && !f.Dialog1 && !f.StoryDirecting && !f.Rollet && !f.Directing;
        }

        /// <summary>맵 오브젝트 상호작용(Intel_ObtoUI).</summary>
        public static bool CanInteractWorld(BusyFlags f, UiStack stack)
        {
            return !stack.Any && !f.Dialog2 && !f.StoryDirecting && !f.Directing;
        }

        /// <summary>포탈 이동 키(PotalPoint).</summary>
        public static bool CanUsePortal(BusyFlags f)
        {
            return !f.Directing && !f.Dialog1 && !f.PortalWait;
        }
    }
}
