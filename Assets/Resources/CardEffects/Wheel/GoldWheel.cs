using UnityEngine;

public class GoldWheel : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats()
    {
        player.maxSpeed += PersistentData.playerMoney;
        RaceManager.Instance.CreateBonusText(PersistentData.playerMoney, 1, RaceManager.Instance.speedText.gameObject, Card);
    }




}