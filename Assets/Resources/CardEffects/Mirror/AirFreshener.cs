using UnityEngine;

public class AirFreshener : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats() {
        int numMissing = 8-EquipPanelManager.Instance.Cards.Count;
        player.acceleration += 3 * numMissing;
        player.maxSpeed += 3 * numMissing;
    }


}