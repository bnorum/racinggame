using UnityEngine;

public class LightWheel : CardEffect
{


    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats()
    {

        player.acceleration *= 0.6f;
        RaceManager.Instance.CreateBonusText(0.6f, 2, RaceManager.Instance.accelerationText.gameObject, Card);

    }
}