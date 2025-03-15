using UnityEngine;

public class CleanTransmission : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats() {
        float totalAcceleration = 0;
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardRarity == CardSchema.CardRarity.RARE || card.cardSchema.cardRarity == CardSchema.CardRarity.EPIC || card.cardSchema.cardRarity == CardSchema.CardRarity.LEGENDARY) {
                totalAcceleration += 5f;
            }
        }
        player.acceleration += totalAcceleration;
        RaceManager.Instance.CreateBonusText(totalAcceleration, 1, RaceManager.Instance.accelerationText.gameObject, Card);
    }


}
