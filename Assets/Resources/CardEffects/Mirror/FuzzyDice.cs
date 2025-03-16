using UnityEngine;

public class FuzzyDice : CardEffect
{

    public override void OnBuy() {
        PersistentData.chanceModifier += 0.2f;
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            card.GetComponent<CardEffect>().UpdateCardDescription();
        }
    }

    public override void OnSell() {
        PersistentData.chanceModifier -= 0.2f;
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            card.GetComponent<CardEffect>().UpdateCardDescription();
        }
    }

}