using UnityEngine;

public class TricycleWheel : CardEffect
{


    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats()
    {
        int numOfWheels = 0;
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardType == CardSchema.CardType.WHEEL) {
                numOfWheels++;
            }
        }

        if (numOfWheels == 3) {
            player.maxSpeed *= 1.25f;
            RaceManager.Instance.CreateBonusText(1.25f, 2, RaceManager.Instance.speedText.gameObject, Card);
            player.acceleration *= 1.25f;
            RaceManager.Instance.CreateBonusText(1.25f, 2, RaceManager.Instance.accelerationText.gameObject, Card);
        }

    }
}