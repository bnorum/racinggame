using UnityEngine;

public class DirtyTransmission : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats() {
        float totalAcceleration = 1;
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardRarity == CardSchema.CardRarity.COMMON || card.cardSchema.cardRarity == CardSchema.CardRarity.UNCOMMON) {
                totalAcceleration *= 1.2f;
            }
        }
        player.acceleration *= totalAcceleration;
        RaceManager.Instance.CreateBonusText(totalAcceleration, 2, RaceManager.Instance.accelerationText.gameObject, Card);
    }


}
