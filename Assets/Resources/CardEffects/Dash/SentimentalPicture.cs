using UnityEditor;
using UnityEngine;

public class SentimentalPicture : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats() {
        float addedDriverPower = 0.1f;
        float chance = 0.5f;
        while (Random.Range(0f, 1f) < (chance + PersistentData.chanceModifier)) {
            addedDriverPower += 0.1f;
        }

        player.driverPower +=addedDriverPower;
        RaceManager.Instance.CreateBonusText(addedDriverPower, 1, RaceManager.Instance.driverPowerText.gameObject, Card);

    }


    public override void UpdateCardDescription() {
        Card.cardSchema.cardDescription = "Driver Power +0.1. " + (0.5f+PersistentData.chanceModifier)*100 + "% to add +0.1 driver power, repeating.";
    }


}
