using UnityEngine;

public class CrossNecklace : CardEffect
{
    public override void ApplyCardEffectWhenRaceBegins()
    {
        foreach(Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardEffect != null && card.cardSchema.cardType == CardSchema.CardType.WHEEL) {
                card.cardEffect.ApplyCardEffectAtStartOfRaceBeforeCalculatingStats();
            }
        }

        foreach (Card card in player.cards) {
            if (!card.isEnabled) continue;
            bool isMultiplier = false;
            if (card.cardSchema.cardQuantityType == CardSchema.CardQuantityType.MULTIPLY) {
                isMultiplier = true;
            } else if (card.cardSchema.cardQuantityType == CardSchema.CardQuantityType.ADD) {
                isMultiplier = false;
            }
            if (!isMultiplier && card.cardSchema.cardQuantityValue != 0 && card.cardSchema.cardType == CardSchema.CardType.WHEEL){
                switch (card.cardSchema.cardQuantityModified) {
                    case CardSchema.CardQuantityModified.ACCELERATION:

                        player.acceleration += card.cardSchema.cardQuantityValue;
                        RaceManager.Instance.CreateBonusText(card.cardSchema.cardQuantityValue, 1, RaceManager.Instance.accelerationText.gameObject, card);

                        break;
                    case CardSchema.CardQuantityModified.DRIVERPOWER:

                        player.driverPower += card.cardSchema.cardQuantityValue;
                        RaceManager.Instance.CreateBonusText(card.cardSchema.cardQuantityValue, 1, RaceManager.Instance.driverPowerText.gameObject, card);

                        break;
                    case CardSchema.CardQuantityModified.SPEED:

                        player.maxSpeed += card.cardSchema.cardQuantityValue;
                        RaceManager.Instance.CreateBonusText(card.cardSchema.cardQuantityValue, 1, RaceManager.Instance.speedText.gameObject, card);

                        break;
                }
            }
        }

        foreach (Card card in player.cards) {
            if (!card.isEnabled) continue;
            bool isMultiplier = false;
            if (card.cardSchema.cardQuantityType == CardSchema.CardQuantityType.MULTIPLY) {
                isMultiplier = true;
            } else if (card.cardSchema.cardQuantityType == CardSchema.CardQuantityType.ADD) {
                isMultiplier = false;
            }
            if (isMultiplier && card.cardSchema.cardQuantityValue != 1 && card.cardSchema.cardType == CardSchema.CardType.WHEEL) {
                switch (card.cardSchema.cardQuantityModified) {
                    case CardSchema.CardQuantityModified.ACCELERATION:
                        if (isMultiplier) {
                            player.acceleration *= card.cardSchema.cardQuantityValue;
                            RaceManager.Instance.CreateBonusText(card.cardSchema.cardQuantityValue, 2, RaceManager.Instance.accelerationText.gameObject, card);

                        }

                        break;
                    case CardSchema.CardQuantityModified.SPEED:
                        if (isMultiplier) {
                            player.maxSpeed *= card.cardSchema.cardQuantityValue;
                            RaceManager.Instance.CreateBonusText(card.cardSchema.cardQuantityValue, 2, RaceManager.Instance.speedText.gameObject, card);
                        }

                        break;
                }

            }
        }


        foreach(Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardEffect != null && card.cardSchema.cardType == CardSchema.CardType.WHEEL) {
                card.cardEffect.ApplyCardEffectAtStartOfRaceAfterCalculatingStats();
            }
        }


    }



}