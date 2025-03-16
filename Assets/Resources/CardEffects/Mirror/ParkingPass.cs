using UnityEngine;

public class ParkingPass : CardEffect
{

    public override void ApplyCardEffectWhenRaceBegins() {
        PersistentData.playerMoney -= 5;

    }


}