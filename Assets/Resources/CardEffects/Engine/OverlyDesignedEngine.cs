using UnityEngine;

public class OverlyDesignedEngine : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats()
    {
        if (EquipPanelManager.Instance.TransmissionSlot.isOccupied) {
            player.acceleration += 5f;
            RaceManager.Instance.CreateBonusText(5, 1, RaceManager.Instance.accelerationText.gameObject, Card);

        }
        if (EquipPanelManager.Instance.DashSlot.isOccupied) {
            player.driverPower += 0.1f;
            RaceManager.Instance.CreateBonusText(0.1f, 1, RaceManager.Instance.driverPowerText.gameObject, Card);
        }
        if (EquipPanelManager.Instance.MirrorSlot.isOccupied) {
            player.driverPower += 0.1f;
            RaceManager.Instance.CreateBonusText(0.1f, 1, RaceManager.Instance.driverPowerText.gameObject, Card);
        }
        for (int i = 0; i < EquipPanelManager.Instance.WheelSlot.Count; i++) {
            if (EquipPanelManager.Instance.WheelSlot[i].isOccupied) {
                player.maxSpeed += 1f;
                RaceManager.Instance.CreateBonusText(1, 1, RaceManager.Instance.speedText.gameObject, Card);
                player.acceleration += 1f;
                RaceManager.Instance.CreateBonusText(1, 1, RaceManager.Instance.accelerationText.gameObject, Card);

            }
        }
    }
}

//+5 Speed. For each owned, gain the corresponding stat:
// Transmission, +5 Acceleration, Dash, +0.1 DP, Mirror, +0.1 DP, Wheel +1 Speed and +1 Acceleration
