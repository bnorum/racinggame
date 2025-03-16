using System.Collections;
using UnityEditor;
using UnityEngine;

public class HulaGirl : CardEffect
{

    Coroutine shakeCoroutine;
    public override void ApplyCardEffectWhenRaceBegins()
    {
        shakeCoroutine = StartCoroutine(ShakeDuringRace());
    }

    public IEnumerator ShakeDuringRace() {
        float shakeTime = 1f;
        while (player.carState == Car.CarState.RACING) {
            if (shakeTime > 0) {
                shakeTime -= Time.deltaTime;
            } else {
                player.maxSpeed += 20f;
                RaceManager.Instance.CreateBonusText(20f, 1, RaceManager.Instance.speedText.gameObject, Card);
                shakeTime = 1f;
            }
            yield return null;
        }

    }

    public override void ApplyCardEffectAtEndOfRace()
    {
        StopCoroutine(shakeCoroutine);
    }


}
