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
            RaceManager.Instance.CreateBonusText(2, 2, RaceManager.Instance.speedText.gameObject, Card);
            player.acceleration *= 2;
            RaceManager.Instance.CreateBonusText(2, 2, RaceManager.Instance.accelerationText.gameObject, Card);
        }

    }
}