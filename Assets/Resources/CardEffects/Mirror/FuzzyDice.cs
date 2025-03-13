using UnityEngine;

public class FuzzyDice : CardEffect
{

    public override void OnBuy() {
        PersistentData.chanceModifier += 0.2f;
    }

    public override void OnSell() {
        PersistentData.chanceModifier -= 0.2f;
    }


}