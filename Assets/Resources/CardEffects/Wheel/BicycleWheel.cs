using UnityEngine;

public class BicycleWheel : CardEffect
{

    int numOfWheels = 0;
    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats()
    {
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardType == CardSchema.CardType.WHEEL) {
                numOfWheels++;
            }
        }

        if (numOfWheels == 2) {
            player.maxSpeed *= 1.5f;
            RaceManager.Instance.CreateBonusText(1.5f, 2, RaceManager.Instance.speedText.gameObject, Card);
            player.acceleration *= 1.5f;
            RaceManager.Instance.CreateBonusText(1.5f, 2, RaceManager.Instance.accelerationText.gameObject, Card);
        }

    }
}