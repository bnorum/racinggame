using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardInfoPanel : MonoBehaviour
{
    public static CardInfoPanel Instance;
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDescription;
    public TextMeshProUGUI cardType;
    public TextMeshProUGUI cardBrand;
    public Image rarityImage;
    public TextMeshProUGUI cardRarity;

    public Card selectedCard;

    public bool isActive = false;

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

    void Update()
    {
        if (!isActive || selectedCard == null) {
            transform.position = new Vector3(-1000, -1000, -1000);
        }
    }

    // Update is called once per frame
    public void UpdateCardInfo(Card card)
    {
        if (card != null) {
            selectedCard = card;
            cardName.text = card.cardSchema.cardName;
            cardDescription.text = card.cardSchema.cardDescription;
            cardType.text = card.cardSchema.cardType.ToString();
            cardBrand.text = card.cardSchema.cardBrand.ToString();
            rarityImage.color = rarityColors[card.cardSchema.cardRarity];
            cardRarity.text = card.cardSchema.cardRarity.ToString();
        }
        else {
            cardName.text = "";
            cardDescription.text = "";
            cardType.text = "";
        }

        StartCoroutine(FixLayout());
    }

    public System.Collections.IEnumerator FixLayout() {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
