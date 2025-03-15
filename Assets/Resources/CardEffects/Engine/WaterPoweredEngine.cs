using UnityEngine;

public class WaterPoweredEngine : CardEffect
{
    


    public override void ApplyCardEffectAtStartOfRaceAfterCalculatingStats() {
        if (EquipPanelManager.Instance.Cards.Count > 1) {
            player.maxSpeed *= 1.25f;
            RaceManager.Instance.CreateBonusText(1.25f, 2, RaceManager.Instance.speedText.gameObject, Card);
        }
    }
}
