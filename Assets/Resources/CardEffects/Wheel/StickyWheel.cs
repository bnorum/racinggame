using UnityEngine;

public class StickyWheel : CardEffect
{
    public override void ApplyCardEffectWhenRaceBegins() {
        if (player.revInPlaceTimer < 0.5f)
        player.revInPlaceTimer = 0.5f;
    }

}