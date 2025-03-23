using UnityEngine;

public class Sunglasses : CardEffect
{
    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats()
    {
        int numOfGoats = 0;
        foreach (Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardSchema.cardBrand == CardSchema.CardBrand.GOAT) {
                numOfGoats++;
            }
        }

        player.driverPower += numOfGoats*0.05f;
        RaceManager.Instance.CreateBonusText(numOfGoats*0.05f, 1, RaceManager.Instance.driverPowerText.gameObject, Card);



    }



}