using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardListEntry : MonoBehaviour
{
    public CardSchema card;

    public TextMeshProUGUI CardTypeText;
    public Image RarityImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI BrandText;
    public TextMeshProUGUI CostText;


    public Dictionary<CardSchema.CardRarity, Color> rarityColors = new Dictionary<CardSchema.CardRarity, Color>
    {
        { CardSchema.CardRarity.COMMON, Color.white },
        { CardSchema.CardRarity.UNCOMMON, Color.green },
        { CardSchema.CardRarity.RARE, Color.blue },
        { CardSchema.CardRarity.EPIC, new Color(0.5f, 0f, 0.5f) },
        { CardSchema.CardRarity.LEGENDARY, new Color(1f, 0.64f, 0f) }
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CardTypeText.text = card.cardType.ToString()[0].ToString();

        RarityImage.color = rarityColors[card.cardRarity];
        NameText.text = card.cardName;
        BrandText.text = card.cardBrand.ToString();
        CostText.text = "$" + card.price;

    }
}
