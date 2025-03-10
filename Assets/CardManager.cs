using System.Collections.Generic;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GenerateRandomCard() {
        GameObject card = Instantiate(cardPrefab, transform);
        card.GetComponent<Card>().cardSchema = cardSchemas[Random.Range(0, cardSchemas.Count)];
        return card;
    }
}
