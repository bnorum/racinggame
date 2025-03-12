using UnityEngine;

public class LoadBearingWheel : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats()
    {
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if ((card.cardSchema.cardQuantityModified == CardSchema.CardQuantityModified.SPEED
            || card.cardSchema.cardQuantityModified == CardSchema.CardQuantityModified.BOTH)
            && card.cardSchema.cardType == CardSchema.CardType.WHEEL) {
                player.acceleration *= 1.5f;
            }
        }
    }
}