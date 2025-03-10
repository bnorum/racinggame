using TMPro;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int slotNumber; // The number of the slot
    public bool isOccupied = false;
    public bool isShop = false;
    public GameObject item; // The item currently in the slot
    public TextMeshProUGUI slotType;

    public CardSchema.CardType cardType; // The type of card that can be placed in this slot

    public void Start() {
        if (isShop) {
            slotType.text = "$" + item.GetComponent<Card>().cardSchema.price.ToString();
        } else {
            slotType.text = cardType.ToString();
        }
    }

    public void AddCardToSlot(Card card) {
        if (!isOccupied) {
            isOccupied = true;
            item = card.gameObject;
            card.isOnSlot = true;
            if (isShop) {
                slotType.text = "$" + item.GetComponent<Card>().cardSchema.price.ToString();
            } else {
                slotType.text = cardType.ToString();
                EquipPanelManager.Instance.Cards.Add(card);

            }
            item.transform.SetParent(transform, true); // Set the parent of the card to the slot
            Debug.Log("Card added to slot: " + name);
        }
    }

    public void RemoveCardFromSlot() {
        if (isOccupied) {
            isOccupied = false;
            item.transform.SetParent(null, true); // Set the parent of the card to null
            if (!isShop) {
                EquipPanelManager.Instance.Cards.Remove(item.GetComponent<Card>());
            }
            item = null;

            Debug.Log("Card removed from slot: " + name);
        }
    }
}
