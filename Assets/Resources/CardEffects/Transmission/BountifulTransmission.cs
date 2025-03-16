using UnityEngine;

public class BountifulTransmission : CardEffect
{
    float currentBonus = 20f;

    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats() {
        if (Random.Range(0, 100) < 50) {
            player.acceleration += currentBonus;
            RaceManager.Instance.CreateBonusText(currentBonus, 1, RaceManager.Instance.accelerationText.gameObject, Card);
            currentBonus = 20f;
            Card.cardSchema.cardDescription = "50% chance to give +" + currentBonus +" Acceleration. 50% chance to double it and give it to the next race.";
        } else {
            currentBonus *= 2;
            Card.cardSchema.cardDescription = "50% chance to give +" + currentBonus +" Acceleration. 50% chance to double it and give it to the next race.";
        }
    }


}
