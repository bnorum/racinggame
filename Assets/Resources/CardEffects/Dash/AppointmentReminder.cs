using UnityEditor;
using UnityEngine;

public class AppointmentReminder : CardEffect
{

    public override void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats() {
        if (PersistentData.round.ToString()[PersistentData.round.ToString().Length-1] == '3'
        || PersistentData.round.ToString()[PersistentData.round.ToString().Length-1] == '6'
        || PersistentData.round.ToString()[PersistentData.round.ToString().Length-1] == '9') {
            player.maxSpeed += 36.9f;
            player.acceleration += 36.9f;
            RaceManager.Instance.CreateBonusText(36.9f, 1, RaceManager.Instance.speedText.gameObject, Card);
            RaceManager.Instance.CreateBonusText(36.9f, 1, RaceManager.Instance.accelerationText.gameObject, Card);
        }

    }





}
