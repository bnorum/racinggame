using UnityEditor;
using UnityEngine;

public class GazelleFigure : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats() {
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardBrand == CardSchema.CardBrand.GAZELLE
            && card.cardSchema.cardType == CardSchema.CardType.WHEEL) {
                player.driverPower += 0.15f;
                RaceManager.Instance.CreateBonusText(0.15f, 1, RaceManager.Instance.driverPowerText.gameObject, Card);
                return;
            }
        }
    }


}


