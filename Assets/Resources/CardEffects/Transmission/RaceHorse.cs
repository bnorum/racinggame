using UnityEngine;

public class RaceHorse : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats() {
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardBrand != CardSchema.CardBrand.HORSE) {
                return;
            }
        }
        player.acceleration *= 2f;
        RaceManager.Instance.CreateBonusText(2, 2, RaceManager.Instance.accelerationText.gameObject);
    }


}
