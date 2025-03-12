using UnityEngine;

public class WheelieCool : CardEffect
{

    int numOfWheels = 0;
    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats()
    {
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardType == CardSchema.CardType.WHEEL) {
                numOfWheels++;
            }
        }

        if (numOfWheels <= 1) {
            player.maxSpeed *= 2;
            player.acceleration *= 2;
        }

    }
}