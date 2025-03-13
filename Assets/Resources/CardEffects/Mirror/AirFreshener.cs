using UnityEngine;

public class AirFreshener : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats() {
        int numMissing = 8-EquipPanelManager.Instance.Cards.Count;
        player.acceleration += 3 * numMissing;
        RaceManager.Instance.CreateBonusText(3*numMissing, 1, RaceManager.Instance.accelerationText.gameObject);
        player.maxSpeed += 3 * numMissing;
        RaceManager.Instance.CreateBonusText(3*numMissing, 1, RaceManager.Instance.speedText.gameObject);

    }


}