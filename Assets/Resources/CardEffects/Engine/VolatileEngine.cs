using UnityEngine;

public class VolatileEngine : CardEffect
{
    float chance = 0.33f;


    public override void ApplyCardEffectAtEndOfRace() {

        if (Random.value < chance + PersistentData.chanceModifier) {
            Card.isEnabled = false;
            Debug.Log("Extinct!");
        } else {
            Debug.Log("Safe!");
        }
    }

    public override void UpdateCardDescription() {
        Card.cardSchema.cardDescription = "Speed +35, " + (chance+PersistentData.chanceModifier)*100 + "% chance of exploding after race";
    }


}
