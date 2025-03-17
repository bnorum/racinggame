using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

    }


    public GameObject cardPrefab;
    public List<CardSchema> cardSchemas = new List<CardSchema>();
    public List<CardSchema> commonCardSchemas = new List<CardSchema>();
    public List<CardSchema> uncommonCardSchemas = new List<CardSchema>();
    public List<CardSchema> rareCardSchemas = new List<CardSchema>();
    public List<CardSchema> epicCardSchemas = new List<CardSchema>();
    public List<CardSchema> legendaryCardSchemas = new List<CardSchema>();
    public Canvas shopCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardSchemas.Clear();
        cardSchemas.AddRange(Resources.LoadAll<CardSchema>(""));
        foreach (CardSchema cardSchema in cardSchemas) {
            switch (cardSchema.cardRarity) {
                case CardSchema.CardRarity.COMMON:
                    commonCardSchemas.Add(cardSchema);
                    break;
                case CardSchema.CardRarity.UNCOMMON:
                    uncommonCardSchemas.Add(cardSchema);
                    break;
                case CardSchema.CardRarity.RARE:
                    rareCardSchemas.Add(cardSchema);
                    break;
                case CardSchema.CardRarity.EPIC:
                    epicCardSchemas.Add(cardSchema);
                    break;
                case CardSchema.CardRarity.LEGENDARY:
                    legendaryCardSchemas.Add(cardSchema);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GenerateRandomCard(Slot slot) {
       // Debug.Log(cardSchemas.Count);
        CardSchema cardSchema = null;
        int chance = Random.Range(0, 100);
        if (chance is >= 0 and < 40) {
            cardSchema = commonCardSchemas[Random.Range(0, commonCardSchemas.Count)];
        }
        else if (chance is >= 40 and < 60) {
            cardSchema = uncommonCardSchemas[Random.Range(0, uncommonCardSchemas.Count)];
        }
        else if (chance is >= 60 and < 80) {
            cardSchema = rareCardSchemas[Random.Range(0, rareCardSchemas.Count)];
        }
        else if (chance is >= 80 and < 92) {
            cardSchema = epicCardSchemas[Random.Range(0, epicCardSchemas.Count)];
        }
        else if (chance is >= 92 and < 100) {
            cardSchema = legendaryCardSchemas[Random.Range(0, legendaryCardSchemas.Count)];
        }

        bool isDupe = false;
        foreach (Card shopCard in ShopPanelManager.Instance.CardsInShop) {
            if (shopCard.cardSchema == cardSchema) {
                isDupe = true;
                break;
            }
        }
        foreach (Card equipCard in EquipPanelManager.Instance.Cards) {
            if (equipCard.cardSchema == cardSchema) {
                isDupe = true;
                break;
            }
        }

        if ((isDupe || cardSchema.name == "HandCrank") && cardSchema.name != "DevilsMachineWheel") {
            return GenerateRandomCard(slot);
        } else {
            GameObject card = Instantiate(cardPrefab, slot.gameObject.transform);
            card.GetComponent<Card>().cardSchema = cardSchema;
            return card;
        }
    }

    public GameObject GenerateNamedCard(string name, Slot slot) {
        CardSchema cardSchema = null;
        foreach (CardSchema schema in cardSchemas) {
            if (schema.name == name) {
                cardSchema = schema;
                break;
            }
        }

        bool isDupe = false;
        foreach (Card shopCard in ShopPanelManager.Instance.CardsInShop) {
            if (shopCard.cardSchema == cardSchema) {
                isDupe = true;
                break;
            }
        }
        foreach (Card equipCard in EquipPanelManager.Instance.Cards) {
            if (equipCard.cardSchema == cardSchema) {
                isDupe = true;
                break;
            }
        }

        if (isDupe) {
            return GenerateNamedCard(name, slot);
        } else {
            GameObject card = Instantiate(cardPrefab, slot.gameObject.transform);
            card.GetComponent<Card>().cardSchema = cardSchema;
            slot.AddCardToSlot(card.GetComponent<Card>());
            return card;
        }
    } 
}
