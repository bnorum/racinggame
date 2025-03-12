using UnityEngine;

public class AirFreshener : CardEffect
{
    public Car player;
    public override void Start() {
        base.Start();
        player = RaceManager.Instance.player;
    }
    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats() {
        int numMissing = 8-EquipPanelManager.Instance.Cards.Count;
        player.acceleration += 3 * numMissing;
        player.maxSpeed += 3 * numMissing;
    }


}