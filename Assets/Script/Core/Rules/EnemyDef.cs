namespace SCoW.Core.Rules
{
    public enum EnemyKind { DeepOneHybrid, DeepOne }

    /// <summary>적이 자기 차례에 고르는 행동. 옛 BattleManager.EnemySetAction 의 분기.</summary>
    public enum EnemyIntent { Normal, Deftness, SanAttack, PoisonAttack }

    /// <summary>
    /// 행동 종류별 가중치. 합이 0 이면 난수 없이 일반 공격(옛 심해인 혼혈 슬롯).
    /// 선택은 Range(0, 합) 한 번: 앞에서부터 이성 -> 독 -> 일반 순으로 구간을 나눈다(옛 Range(0,4) 의 0/1/그 외와 동치).
    /// </summary>
    public readonly struct IntentWeights
    {
        public readonly int SanAttack;
        public readonly int PoisonAttack;
        public readonly int Normal;

        public IntentWeights(int sanAttack, int poisonAttack, int normal)
        {
            SanAttack = sanAttack;
            PoisonAttack = poisonAttack;
            Normal = normal;
        }

        public int Total => SanAttack + PoisonAttack + Normal;
    }

    /// <summary>난수 구간. Random.Range(int,int) 와 같이 상한은 제외값.</summary>
    public readonly struct IntRange
    {
        public readonly int Min;
        public readonly int MaxExclusive;

        public IntRange(int min, int maxExclusive)
        {
            Min = min;
            MaxExclusive = maxExclusive;
        }

        public int Roll(IRng rng)
        {
            return rng.Next(Min, MaxExclusive);
        }
    }

    /// <summary>적 한 슬롯의 정의(데이터). 옛 EnemyClass 의 SetStat* 상수와 BattleManager 의 슬롯별 분기를 옮긴 것.</summary>
    public sealed class EnemyDef
    {
        public string Id;
        public EnemyKind Kind;
        public IntRange Str;
        public IntRange Intl;
        public IntRange Dex;
        public IntRange Mp;
        public IntRange Hp;
        public IntRange San;
        public int MartialArts;
        public int Swordsmanship;
        public int Evasion;
        /// <summary>이 슬롯의 일반 공격 피해(플레이어가 회피·반격·공격에 실패했을 때 받는 값).</summary>
        public DiceExpr AttackDamage;
        public bool UsesDeftness;
        /// <summary>행동 종류 가중치(ChooseIntent 가 읽는다). UsesDeftness 슬롯은 쓰지 않는다.</summary>
        public IntentWeights Intent;
        /// <summary>
        /// 아군 칸(앞·중·뒤) 가중치 3종. PT-2(다인원 전투)에서 사용, 현재 미사용(1:1 전투는 읽지 않는다).
        /// 일반 공격 5:3:2.
        /// </summary>
        public int[] TargetWeights = { 5, 3, 2 };
        /// <summary>이성 공격(시선)은 전 칸 무작위.</summary>
        public int[] SanTargetWeights = { 1, 1, 1 };
        /// <summary>독 공격은 앞 칸 우선.</summary>
        public int[] PoisonTargetWeights = { 1, 0, 0 };

        public int[] TargetWeightsFor(EnemyIntent intent)
        {
            switch (intent)
            {
                case EnemyIntent.SanAttack: return SanTargetWeights;
                case EnemyIntent.PoisonAttack: return PoisonTargetWeights;
                default: return TargetWeights;
            }
        }
    }

    /// <summary>현재 출시판 5종. All 의 순서(0~4)가 BattleManager 의 적 배열 인덱스와 같다.</summary>
    public static class EnemyDefs
    {
        public static readonly EnemyDef DeepOneHybrid1 = Hybrid("DeepOneHybrid1", BattleDamage.Hit1D3, false);
        public static readonly EnemyDef DeepOneHybrid2 = Hybrid("DeepOneHybrid2", BattleDamage.Hit1D3Plus1, false);
        public static readonly EnemyDef DeepOneHybrid3 = Hybrid("DeepOneHybrid3", BattleDamage.Hit1D3, true);
        public static readonly EnemyDef DeepOneHybrid4 = Hybrid("DeepOneHybrid4", BattleDamage.Hit1D3Plus1, false);

        public static readonly EnemyDef DeepOne1 = new EnemyDef
        {
            Id = "DeepOne1",
            Kind = EnemyKind.DeepOne,
            Str = new IntRange(60, 80),
            Intl = new IntRange(55, 75),
            Dex = new IntRange(45, 55),
            Mp = new IntRange(40, 60),
            Hp = new IntRange(100, 120),
            San = new IntRange(30, 60),
            MartialArts = 45,
            Swordsmanship = 50,
            Evasion = 25,
            AttackDamage = BattleDamage.Hit2D3,
            Intent = new IntentWeights(1, 1, 2)
        };

        /// <summary>인덱스 = 적 배열 슬롯. 0~3 혼혈 1~4, 4 심해인 1.</summary>
        public static readonly EnemyDef[] All = { DeepOneHybrid1, DeepOneHybrid2, DeepOneHybrid3, DeepOneHybrid4, DeepOne1 };

        /// <summary>Id 로 정의를 찾는다. 없으면 null.</summary>
        public static EnemyDef Get(string id)
        {
            for (int i = 0; i < All.Length; i++)
            {
                if (All[i].Id == id)
                {
                    return All[i];
                }
            }
            return null;
        }

        /// <summary>Id 로 All 인덱스를 찾는다. 없으면 -1.</summary>
        public static int IndexOf(string id)
        {
            for (int i = 0; i < All.Length; i++)
            {
                if (All[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        private static EnemyDef Hybrid(string id, DiceExpr damage, bool deftness)
        {
            return new EnemyDef
            {
                Id = id,
                Kind = EnemyKind.DeepOneHybrid,
                Str = new IntRange(45, 80),
                Intl = new IntRange(45, 80),
                Dex = new IntRange(45, 80),
                Mp = new IntRange(5, 10),
                Hp = new IntRange(40, 55),
                San = new IntRange(30, 60),
                MartialArts = 50,
                Swordsmanship = 0,
                Evasion = 25,
                AttackDamage = damage,
                UsesDeftness = deftness
            };
        }
    }

    /// <summary>전투 중 적 한 마리의 가변 상태. 재굴림은 같은 객체를 덮어쓴다(옛 SetStat* 와 같은 순서로 난수 소비).</summary>
    public sealed class EnemyState
    {
        public readonly EnemyDef Def;
        public int Str;
        public int Intl;
        public int Dex;
        public int Mp;
        public int FullHp;
        public int NowHp;
        public int San;
        public int MartialArts;
        public int Swordsmanship;
        public int Evasion;
        /// <summary>사망 처리(오브젝트·턴 심볼 끄기, 사망 수 집계)를 이미 했는가. 재굴림하면 지워진다.</summary>
        public bool DeathHandled;

        public EnemyState(EnemyDef def)
        {
            Def = def;
        }

        public bool IsDead => NowHp <= 0;

        /// <summary>str, intl, dex, mp, fullHP, san 순으로 난수 6회.</summary>
        public void Roll(IRng rng)
        {
            Str = Def.Str.Roll(rng);
            Intl = Def.Intl.Roll(rng);
            Dex = Def.Dex.Roll(rng);
            Mp = Def.Mp.Roll(rng);
            FullHp = Def.Hp.Roll(rng);
            NowHp = FullHp;
            San = Def.San.Roll(rng);
            MartialArts = Def.MartialArts;
            Swordsmanship = Def.Swordsmanship;
            Evasion = Def.Evasion;
            DeathHandled = false;
        }

        public void TakeDamage(int damage)
        {
            NowHp -= damage;
        }
    }
}
