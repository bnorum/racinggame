using UnityEngine;

public class VolatileEngine : CardEffect
{
    float chance = 0.33f;


    public override void ApplyCardEffectAtEndOfRace() {
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardEffectName == "FuzzyDice") {
                chance += .20f;
            }
        }
        if (Random.value < chance) {
            Card.isEnabled = false;
            Debug.Log("Extinct!");
        } else {
            Debug.Log("Safe!");
        }
    }
}
