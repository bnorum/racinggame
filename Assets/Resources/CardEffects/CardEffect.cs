using UnityEngine;

public class CardEffect : MonoBehaviour
{
    public Car player;
    public Card Card;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        Card = GetComponent<Card>();
        player = RaceManager.Instance.player;
        UpdateCardDescription();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        UpdateCardDescription();
    }

    public virtual void ApplyCardEffectAtEndOfRace() {

    }

    public virtual void ApplyCardEffectHalfwayThroughRace() {

    }

    public virtual void ApplyCardEffectAtStartOfRaceBeforeCalculatingStats() {

    }

    public virtual void ApplyCardEffectAtStartOfRaceAfterCalculatingStats() {

    }

    public virtual void OnSell() {

    }

    public virtual void OnBuy() {
    }

    public virtual void ConstantlyActive() {

    }

    public virtual void ApplyCardEffectWhenRaceBegins() {

    }

    public virtual void UpdateCardDescription() {

    }
}
