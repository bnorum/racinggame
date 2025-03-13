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

    public List<Card> Cards;
    public Slot EngineSlot;
    public Slot TransmissionSlot;
    public Slot MirrorSlot;
    public Slot DashSlot;
    public List<Slot> WheelSlot;








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
