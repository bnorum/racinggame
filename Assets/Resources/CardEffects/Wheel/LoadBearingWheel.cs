using UnityEngine;

public class LoadBearingWheel : CardEffect
{
    public Car player;
    public override void Start() {
        base.Start();
        player = RaceManager.Instance.player;
    }
    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats()
    {
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardQuantityModified == CardSchema.CardQuantityModified.SPEED
            && card.cardSchema.cardType == CardSchema.CardType.WHEEL) {
                player.acceleration *= 1.5f;
            }
        }
    }
}