using UnityEngine;

public class BaseballCard : CardEffect
{

    public override void OnSell() {
        player.baseDriverPower += 0.1f;
    }


}
