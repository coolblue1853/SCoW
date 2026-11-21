namespace SCoW.Core.World
{
    /// <summary>월드 오브젝트 식별자(WorldRules 의 목표). 씬 연결은 앱 층(WorldBinder)이 한다.</summary>
    public static class WorldIds
    {
        /// <summary>상태 키가 아닌 "월드를 다시 평가하라" 알림 키(GameRoot.NotifyTransient). 다른 스크립트가 월드 쪽 값(canJudge 등)을 직접 되돌렸을 때 올린다.</summary>
        public const string RefreshKey = "world:refresh";

        public const string DetectiveNightBulb = "detectiveNightBulb";
        public const string EndDoorDetective = "endDoorDetective";
        public const string Manhole = "Manhole";
        public const string RiverActive = "riverActive";
        public const string RiverPotal = "riverPotal";
        public const string BlackWell = "blackWell";
        public const string BlackWellOb = "blackWellOb";
        public const string SewerDoorPotal = "sewerDoorPotal";
        public const string SewerDoorDialog = "sewerDoorDialog";
        public const string SewerOfficeWindow = "sewerOfficeWindow";
        public const string SewerOfficeLocker = "sewerOfficeLocker";
        public const string Meave = "meave";
        public const string BfNoonEvent = "bfNoonEvent";
        public const string AfNoonEvent = "afNoonEvent";
        public const string PoliceLine = "policeLine";
        public const string Witness = "witness";
        public const string DeadBody = "deadBody";
        public const string Site = "site";
        public const string Kane = "kane";
        public const string Student = "student";
        public const string BarPeople1 = "barPeople1";
        public const string BarPeople2 = "barPeople2";
        public const string Albert = "albert";
        public const string Dave = "dave";
        public const string Swain = "swain";
        public const string Journalist = "journalist";
    }

    /// <summary>WorldRules 가 읽고 쓰는 씬의 얇은 창구(순수 C# 테스트에서는 가짜로 대체).</summary>
    public interface IWorldScene
    {
        bool IsActive(string id);
        void SetActive(string id, bool active);
        void MoveTo(string id, float x, float y, float z);
        float LocalY(string id);

        /// <summary>Intel_ObtoUI 의 canJudge / canKeyword.</summary>
        bool GetCanJudge(string id);
        void SetCanJudge(string id, bool value);
        bool GetCanKeyword(string id);
        void SetCanKeyword(string id, bool value);

        /// <summary>LightColorController.time.</summary>
        void SetLightTime(float time);

        /// <summary>하루가 지나 어질러진 의뢰인의 집 방 스프라이트로 바꾼다.</summary>
        void SetClientsHouseRoomMessy();
    }
}
