using UnityEngine;

public class SilverWheel : CardEffect
{


    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats()
    {
        player.acceleration += PersistentData.playerMoney;
        RaceManager.Instance.CreateBonusText(PersistentData.playerMoney, 1, RaceManager.Instance.accelerationText.gameObject, Card);
    }


}