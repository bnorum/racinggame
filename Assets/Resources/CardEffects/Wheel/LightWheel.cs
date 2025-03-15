using UnityEngine;

public class LightWheel : CardEffect
{


    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats()
    {

        player.acceleration *= 0.6f;
    }
}