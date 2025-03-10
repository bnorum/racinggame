using UnityEngine;

public class VolatileEffect : CardEffect
{
    float chance = 0.33f;


    public override void ApplyCardEffectAtEndOfRace() {
        if (Random.value < chance) {
            Card.isEnabled = false;
            Debug.Log("Extinct!");
        } else {
            Debug.Log("Safe!");
        }
    }
}
