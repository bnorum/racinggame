using UnityEngine;

public class WheelOfFortune : CardEffect
{


    public override void ApplyCardEffectWhenRaceBegins()
    {
        if (.3f+PersistentData.chanceModifier < Random.value) {
            PersistentData.playerMoney += 3;
        }
    }

    public override void ApplyCardEffectHalfwayThroughRace()
    {
        if (.3f+PersistentData.chanceModifier < Random.value) {
            PersistentData.playerMoney += 3;
        }
    }

    public override void ApplyCardEffectAtEndOfRace()
    {
        if (.3f+PersistentData.chanceModifier < Random.value) {
            PersistentData.playerMoney += 3;
        }
    }

    public override void UpdateCardDescription() {
        Card.cardSchema.cardDescription = (0.3f+PersistentData.chanceModifier)*100 + "% chance to get $3 at start of race, halfway through race, and end of race.";
    }



}