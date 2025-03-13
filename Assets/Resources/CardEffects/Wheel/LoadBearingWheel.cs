using UnityEngine;

public class LoadBearingWheel : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats()
    {
        float addedAcceleration = 1f;
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if ((card.cardSchema.cardQuantityModified == CardSchema.CardQuantityModified.SPEED
            || card.cardSchema.cardQuantityModified == CardSchema.CardQuantityModified.BOTH)
            && card.cardSchema.cardType == CardSchema.CardType.WHEEL) {
                addedAcceleration *= 1.5f;
            }
        }
        player.acceleration *= addedAcceleration;

        RaceManager.Instance.CreateBonusText(addedAcceleration, 2, RaceManager.Instance.accelerationText.gameObject, Card);
    }
}