using UnityEngine;

public class Booty : EntityType
{
    public override void OnHitFinishLine()
    {
        PlayerBonus.Instance.ResetBootiesCollected();
    }
}