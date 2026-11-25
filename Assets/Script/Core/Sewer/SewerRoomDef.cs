using System.Collections.Generic;

namespace SCoW.Core.Sewer
{
    /// <summary>하수도 방의 문. 왼쪽/오른쪽 문과 샛길(Middle, 씬의 SideRoad 문).</summary>
    public enum SewerDoor
    {
        Left,
        Right,
        Middle,
    }

    /// <summary>방 외형 묶음. Normal 은 터널·전구·소품(방 데이터)대로, 나머지는 옛 MakeHouseLand/CageLand/SighnLand 와 같은 고정 외형.</summary>
    public enum SewerLook
    {
        Normal,
        House,
        Cage,
        Symbol,
    }

    public enum SewerTunnel
    {
        Flat,
        Round,
    }

    /// <summary>방 하나의 문 하나. To 는 도착 방 ID.</summary>
    public sealed class SewerExit
    {
        public readonly SewerDoor Door;
        public readonly string To;

        public SewerExit(SewerDoor door, string to)
        {
            Door = door;
            To = to;
        }
    }

    /// <summary>
    /// SEWER-1 S1 방 데이터(구조는 고정, 외형 소품만 시드로 변형). 기존 씬 소품 필드를 켜는 SewerRoomApplier(App)가 읽는다.
    /// Bulb1/Bulb2 는 Flat 이면 burbFlet1/2, Round 이면 burbRound1/2.
    /// </summary>
    public sealed class SewerRoomDef
    {
        private readonly List<SewerExit> _exits = new List<SewerExit>();

        public string Id { get; private set; }
        public string Zone { get; private set; }
        public SewerLook Look { get; internal set; }
        public SewerTunnel Tunnel { get; internal set; }
        public bool Bulb1 { get; internal set; }
        public bool Bulb2 { get; internal set; }
        public bool Pipe { get; internal set; }
        public bool Hole { get; internal set; }
        public bool LadderHigh { get; internal set; }
        /// <summary>적이 나오는 방(조우). 몇 번째 조우인지는 SewerGraph.EncounterRoom 순서.</summary>
        public bool HasEnemy { get; internal set; }

        public IList<SewerExit> Exits
        {
            get { return _exits; }
        }

        /// <summary>샛길 문이 있으면 씬의 SideRoad 소품이 필요하다.</summary>
        public bool SideRoad
        {
            get { return HasDoor(SewerDoor.Middle); }
        }

        internal SewerRoomDef(string id, string zone)
        {
            Id = id;
            Zone = zone;
        }

        public bool HasDoor(SewerDoor door)
        {
            return ExitAt(door) != null;
        }

        public SewerExit ExitAt(SewerDoor door)
        {
            for (int i = 0; i < _exits.Count; i++)
            {
                if (_exits[i].Door == door)
                {
                    return _exits[i];
                }
            }
            return null;
        }

        internal void AddExit(SewerDoor door, string to)
        {
            _exits.Add(new SewerExit(door, to));
        }
    }
}
