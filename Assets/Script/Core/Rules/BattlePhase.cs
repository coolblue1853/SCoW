namespace SCoW.Core.Rules
{
    /// <summary>
    /// 전투 진행 단계(옛 BattleManager.battleState 문자열). 적 슬롯은 단계가 아니라 별도 인덱스로 가진다
    /// (옛 "DeepOneHybrid1Trun"/"DeepOneHybrid1Attack" -> EnemyTurn/EnemyAttack + 적 인덱스).
    /// </summary>
    public enum BattlePhase
    {
        /// <summary>옛 "setTrun": 턴 게이지가 차오르는 중.</summary>
        SetTurn,
        PlayerTurn,
        PlayerChoice,
        PlayerAttack,
        /// <summary>옛 "selectEnemy".</summary>
        SelectEnemy,
        Rollet,
        EnemyTurn,
        EnemyAttack
    }
}
