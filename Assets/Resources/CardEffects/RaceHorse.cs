using UnityEngine;

public class RaceHorse : CardEffect
{
    public Car player;
    public override void Start() {
        base.Start();
        player = RaceManager.Instance.player;
    }
    public override void ApplyCardEffectAtStartOfRace() {
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardBrand == CardSchema.CardBrand.HORSE) {
                player.acceleration *= 2f;
            }
        }
    }


}
