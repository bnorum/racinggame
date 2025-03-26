using UnityEngine;

public class ReferentialTransmission : CardEffect
{

    public override void ApplyCardEffectWhenRaceBegins() {

        RaceManager.Instance.CreateBonusText((player.maxSpeed / 2)-player.acceleration, 1, RaceManager.Instance.accelerationText.gameObject, Card);
        player.acceleration = player.maxSpeed / 2;
    }


}
