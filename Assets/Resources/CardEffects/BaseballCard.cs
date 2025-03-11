using UnityEngine;

public class BaseballCard : CardEffect
{
    public Car player;
    public override void Start() {
        base.Start();
        player = RaceManager.Instance.player;
    }
    public override void OnSell() {
        player.baseDriverPower += 0.2f;
    }


}
