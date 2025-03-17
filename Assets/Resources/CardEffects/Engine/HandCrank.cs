using System.Collections;
using UnityEngine;

public class HandCrank : CardEffect
{
    float maxSpeed = 0f;

    Coroutine decayspeed;

    public override void ApplyCardEffectWhenRaceBegins() {
        maxSpeed = player.maxSpeed;
        player.maxSpeed = 0f;
        decayspeed = StartCoroutine(ReduceSpeed());
    }

    public override void ApplyCardEffectAtEndOfRace()
    {
        if (player.carState == Car.CarState.RACING) {
            player.maxSpeed = maxSpeed;
        }
        if (decayspeed != null) {
            StopCoroutine(decayspeed);
        }
    }

    public  void WhenTapped() {
        player.maxSpeed += maxSpeed / 10f;
        if (player.maxSpeed >= maxSpeed) {
            player.maxSpeed = maxSpeed;
        } else {
            RaceManager.Instance.CreateBonusText(maxSpeed / 10f, 1, RaceManager.Instance.speedText.gameObject, Card);
        }
    }

    public IEnumerator ReduceSpeed() {
        while (true) {
            if (player.maxSpeed > 0) player.maxSpeed -= maxSpeed / 5f;
            RaceManager.Instance.CreateBonusText(-maxSpeed / 5f, 1, RaceManager.Instance.speedText.gameObject, Card);
            yield return new WaitForSeconds(1f);
        }
    }

    public override void Update() {
        if ((Input.GetKeyDown(KeyCode.Mouse0) ||
        (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) &&
        Card.IsPointerOverUIElement(gameObject.GetComponent<RectTransform>()) &&
        player.carState == Car.CarState.RACING)
        {
            WhenTapped();
        }
    }
}
