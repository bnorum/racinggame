using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopPanelManager : MonoBehaviour
{

    public static ShopPanelManager Instance;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public int maxSlots = 3;
    public GameObject slotPrefab;
    public List<GameObject> slots = new List<GameObject>();
    public List<Card> CardsInShop = new List<Card>();
    public CardManager cardManager;

    public TextMeshProUGUI moneyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshNumberOfSlots();
        PersistentData.playerMoney = 7;
    }

    // Update is called once per frame
    void Update()
    {
        RefreshNumberOfSlots();
        moneyText.text = "$" + PersistentData.playerMoney.ToString();
    }

    void RefreshNumberOfSlots() {
        for (int i = 0; i <= maxSlots; i++) {
            if (maxSlots > slots.Count) {
            GameObject slot = Instantiate(slotPrefab, transform);
            slot.GetComponent<Slot>().isShop = true;
            slot.GetComponent<Slot>().slotNumber = slot.transform.GetSiblingIndex();
            slots.Add(slot);
            }
            else if (maxSlots < slots.Count) {
                GameObject slot = slots[slots.Count - 1];
                slots.Remove(slot);
                Destroy(slot);
            }

        }


    }

    public List<CardSchema> GetCards() {
        List<CardSchema> cards = new List<CardSchema>();
        for (int i = 0; i < slots.Count; i++) {
            if (slots[i].GetComponent<Slot>().item != null) {
                cards.Add(slots[i].GetComponent<Slot>().item.GetComponent<Card>().cardSchema);
            }
        }
        return cards;
    }


    public void FillShop() {

        for (int i = 0; i < slots.Count; i++) {
            if (slots[i].GetComponent<Slot>().item != null) {
                CardsInShop.Remove(slots[i].GetComponent<Slot>().item.GetComponent<Card>());
                Destroy(slots[i].GetComponent<Slot>().item);
                slots[i].GetComponent<Slot>().RemoveCardFromSlot();

            }
        }
        for (int i = 0; i < maxSlots; i++) {
            GameObject card = cardManager.GenerateRandomCard(slots[i].GetComponent<Slot>());
            slots[i].GetComponent<Slot>().AddCardToSlot(card.GetComponent<Card>());
            CardsInShop.Add(card.GetComponent<Card>());
        }
    }

}
