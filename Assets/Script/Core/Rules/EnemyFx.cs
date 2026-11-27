namespace SCoW.Core.Rules
{
    /// <summary>BattleCameraMove.ZoomUp/ZoomMidle/ZoomDown. None 은 줌 호출 없음.</summary>
    public enum CamZoom
    {
        None,
        Up,
        Midle,
        Down,
    }

    /// <summary>플레이어가 맞을 때 battlePlayer.PlayerHitted* 호출. None 은 호출 없음.</summary>
    public enum PlayerHitStyle
    {
        None,
        Dagger,
    }

    /// <summary>적이 플레이어를 때리는 연출의 슬롯별 값(출시판 BattleManager.RetrunRolletResult 실측).</summary>
    public readonly struct PlayerHitReaction
    {
        public readonly PlayerHitStyle PlayerHit;
        /// <summary>SoundManager.BattleSound 키. 출시판 리소스 키라 바꾸지 않는다.</summary>
        public readonly string HitSound;
        /// <summary>OnSpriteChangeComplete(playerRender, hitted) 호출 여부.</summary>
        public readonly bool HittedSprite;
        /// <summary>battlePlayer.resetPlayer() 호출 여부.</summary>
        public readonly bool ResetPlayer;

        public PlayerHitReaction(PlayerHitStyle playerHit, string hitSound, bool hittedSprite, bool resetPlayer)
        {
            PlayerHit = playerHit;
            HitSound = hitSound;
            HittedSprite = hittedSprite;
            ResetPlayer = resetPlayer;
        }
    }

    /// <summary>
    /// P14b F0: 슬롯(EnemyDefs.All 과 같은 인덱스)별 연출 값. 현재 값 그대로이며 아직 BattleManager 가 쓰지 않는다.
    /// 부호(회전)는 행동이 정하므로 RotateDeg 는 크기(양수)다.
    /// </summary>
    public readonly struct EnemyFxProfile
    {
        public readonly CamZoom Zoom;
        public readonly float RotateDeg;
        /// <summary>evasion·counterattack 실패(플레이어가 맞음).</summary>
        public readonly PlayerHitReaction EvadeFailHit;
        /// <summary>MA_attack_Counter 실패.</summary>
        public readonly PlayerHitReaction MartialFailHit;
        /// <summary>SS_attack_Counter 실패(무기 3종 공통).</summary>
        public readonly PlayerHitReaction MeleeFailHit;
        /// <summary>evasion 성공 때 플레이어 이동 시간(초).</summary>
        public readonly float EvadeMoveSec;
        /// <summary>evasion 성공 이동 y 를 적 오브젝트 y 로 계산하는 옛 동작(H1 만).</summary>
        public readonly bool EvadeYFromEnemy;
        /// <summary>evasion 실패 블록에서 줌 호출(H4 는 주석 처리되어 false). Zoom 이 None 이면 의미 없음.</summary>
        public readonly bool ZoomInEvadeFail;
        /// <summary>MA·SS·MS·Deftness 마무리에 CamRotate(0) 호출(H1~H3 는 없음, TurnEnd 의 ResetCam 에 의존). evasion·counter 는 전 슬롯 있음.</summary>
        public readonly bool CamRotateResetOnAttack;

        public EnemyFxProfile(
            CamZoom zoom,
            float rotateDeg,
            PlayerHitReaction evadeFailHit,
            PlayerHitReaction martialFailHit,
            PlayerHitReaction meleeFailHit,
            float evadeMoveSec,
            bool evadeYFromEnemy,
            bool zoomInEvadeFail,
            bool camRotateResetOnAttack)
        {
            Zoom = zoom;
            RotateDeg = rotateDeg;
            EvadeFailHit = evadeFailHit;
            MartialFailHit = martialFailHit;
            MeleeFailHit = meleeFailHit;
            EvadeMoveSec = evadeMoveSec;
            EvadeYFromEnemy = evadeYFromEnemy;
            ZoomInEvadeFail = zoomInEvadeFail;
            CamRotateResetOnAttack = camRotateResetOnAttack;
        }
    }

    /// <summary>슬롯별 연출 표. 인덱스는 EnemyDefs.All 과 같다(H1, H2, H3, H4, D1).</summary>
    public static class EnemyFxProfiles
    {
        private const string Martial = "MartialArts";
        private const string Dagger = "Dagger_Attack";

        public static readonly EnemyFxProfile[] All =
        {
            // DeepOneHybrid1
            new EnemyFxProfile(
                CamZoom.Midle, 5f,
                new PlayerHitReaction(PlayerHitStyle.None, Martial, true, false),
                new PlayerHitReaction(PlayerHitStyle.None, Martial, true, false),
                new PlayerHitReaction(PlayerHitStyle.None, Martial, true, false),
                0.7f, true, true, false),
            // DeepOneHybrid2
            new EnemyFxProfile(
                CamZoom.Down, 5f,
                new PlayerHitReaction(PlayerHitStyle.Dagger, Dagger, true, true),
                new PlayerHitReaction(PlayerHitStyle.None, Dagger, true, true),
                new PlayerHitReaction(PlayerHitStyle.None, Martial, true, true),
                1f, false, true, false),
            // DeepOneHybrid3
            new EnemyFxProfile(
                CamZoom.Up, 5f,
                new PlayerHitReaction(PlayerHitStyle.None, Martial, true, false),
                new PlayerHitReaction(PlayerHitStyle.None, Martial, true, false),
                new PlayerHitReaction(PlayerHitStyle.None, Martial, true, false),
                1f, false, true, false),
            // DeepOneHybrid4
            new EnemyFxProfile(
                CamZoom.Up, 1f,
                new PlayerHitReaction(PlayerHitStyle.Dagger, Dagger, false, true),
                new PlayerHitReaction(PlayerHitStyle.Dagger, Dagger, false, true),
                new PlayerHitReaction(PlayerHitStyle.Dagger, Dagger, false, true),
                1f, false, false, true),
            // DeepOne1
            new EnemyFxProfile(
                CamZoom.None, 1f,
                new PlayerHitReaction(PlayerHitStyle.Dagger, Dagger, true, true),
                new PlayerHitReaction(PlayerHitStyle.None, Dagger, true, true),
                new PlayerHitReaction(PlayerHitStyle.None, Dagger, true, true),
                1f, false, false, true),
        };
    }
}
