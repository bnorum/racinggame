using System.Collections.Generic;
using UnityEngine;

public class EquipPanelManager : MonoBehaviour
{

    public static EquipPanelManager Instance;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }


    public Slot EngineSlot;
    public Slot TransmissionSlot;
    public Slot MirrorSlot;
    public Slot DashSlot;
    public List<Slot> WheelSlot;


    public void AddCardToPanel(GameObject Card, Slot slot) {
        if (!slot.isOccupied &&
        (Card.GetComponent<Card>().cardSchema.cardType == slot.cardType ||
        Card.GetComponent<Card>().cardSchema.cardType == CardSchema.CardType.ANY)) {
            slot.item = Card;
            Card.transform.SetParent(slot.transform, true); // Set the parent of the card to the slot
            slot.isOccupied = true;
            Debug.Log("Card added to slot: " + slot.name);
        }

    }


    public void RemoveCardFromPanel(GameObject Card, Slot slot) {
        if (slot.item == Card) {
            slot.item = null;
            slot.isOccupied = false;
        }
    }


    public List<CardSchema> GetCards() {
        List<CardSchema> cards = new List<CardSchema>();
        if (EngineSlot.item != null) {
            cards.Add(EngineSlot.item.GetComponent<Card>().cardSchema);
        }
        if (TransmissionSlot.item != null) {
            cards.Add(TransmissionSlot.item.GetComponent<Card>().cardSchema);
        }
        if (MirrorSlot.item != null) {
            cards.Add(MirrorSlot.item.GetComponent<Card>().cardSchema);
        }
        if (DashSlot.item != null) {
            cards.Add(DashSlot.item.GetComponent<Card>().cardSchema);
        }
        for (int i = 0; i < WheelSlot.Count; i++) {
            if (WheelSlot[i].GetComponent<Slot>().item != null) {
                cards.Add(WheelSlot[i].GetComponent<Slot>().item.GetComponent<Card>().cardSchema);
            }
        }
        return cards;
    }
}
