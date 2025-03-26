using UnityEditor;
using UnityEngine;

public class PhoneStand : CardEffect
{

    public override void ApplyCardEffectHalfwayThroughRace()
    {
        if (Random.value > (0.1f + PersistentData.chanceModifier)) {
            player.maxSpeed *= 0.5f;
            RaceManager.Instance.CreateBonusText(-player.maxSpeed, 1, RaceManager.Instance.speedText.gameObject, Card);

        }
    }


    public override void UpdateCardDescription() {
        Card.cardSchema.cardDescription = "Driver Power +0.3. " + (0.1f + PersistentData.chanceModifier)*100 + "% chance to lose half your speed halfway through the race.";
    }


}
//Driver Power +0.3. 10% chance to lose half your speed halfway through the race.
