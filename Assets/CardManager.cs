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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardSchemas.Clear();
        cardSchemas.AddRange(Resources.LoadAll<CardSchema>(""));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GenerateRandomCard() {
       // Debug.Log(cardSchemas.Count);
        CardSchema cardSchema = cardSchemas[Random.Range(0, cardSchemas.Count)];
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
            return GenerateRandomCard();
        } else {
            GameObject card = Instantiate(cardPrefab, transform);
            card.GetComponent<Card>().cardSchema = cardSchema;
            return card;
        }
    }
}
