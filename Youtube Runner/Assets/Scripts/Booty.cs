using UnityEngine;

public class Booty : EntityType
{
    public override void OnHitFinishLine()
    {
        base.OnHitFinishLine();
        DontLoseBootyAchievement.Instance.OnPlayerLoseBooty();
        PlayerBonus.Instance.ResetBootiesCollected();
    }
}