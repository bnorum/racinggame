using UnityEngine;
using System.Collections;

public class TwoGuysPedalling : CardEffect
{
    //x3 Speed. Lose 5% of total speed every second.

    Coroutine decayspeed;


    public override void ApplyCardEffectWhenRaceBegins() {
        decayspeed = StartCoroutine(DecaySpeed());
    }

    private IEnumerator DecaySpeed() {
        while (true) {
            player.maxSpeed -= player.maxSpeed * 0.05f;
            RaceManager.Instance.CreateBonusText(-player.maxSpeed * 0.05f, 1, RaceManager.Instance.speedText.gameObject, Card);
            yield return new WaitForSeconds(1f);
        }
    }

    public override void ApplyCardEffectAtEndOfRace()
    {
        if (decayspeed != null) {
            StopCoroutine(decayspeed);
        }
    }
}
