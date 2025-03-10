using UnityEngine;

public class CardEffect : MonoBehaviour
{
    public Card Card;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        Card = GetComponent<Card>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void ApplyCardEffectAtEndOfRace() {

    }

    public virtual void ApplyCardEffectAtStartOfRace() {

    }
}
