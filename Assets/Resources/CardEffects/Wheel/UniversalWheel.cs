using System.Collections.Generic;
using UnityEngine;

public class UniversalWheel : CardEffect
{


    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats()
    {
        List<CardSchema.CardBrand> listOfBrands = new List<CardSchema.CardBrand>();
        int wheelCount = 0;
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardType == CardSchema.CardType.WHEEL) {
                wheelCount++;

                if (!listOfBrands.Contains(card.cardSchema.cardBrand))
                    listOfBrands.Add(card.cardSchema.cardBrand);
                else {
                    return;
                }
            }
        }

        player.maxSpeed += wheelCount * 15;
        RaceManager.Instance.CreateBonusText(wheelCount * 15, 1, RaceManager.Instance.speedText.gameObject, Card);
        player.acceleration += wheelCount * 15;
        RaceManager.Instance.CreateBonusText(wheelCount * 15, 1, RaceManager.Instance.accelerationText.gameObject, Card);


    }
}

