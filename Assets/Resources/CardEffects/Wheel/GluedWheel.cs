using UnityEngine;

public class GluedWheel : CardEffect
{
    public override void ApplyCardEffectWhenRaceBegins() {
        if (player.revInPlaceTimer < 1f)
        player.revInPlaceTimer = 1f;
    }

}