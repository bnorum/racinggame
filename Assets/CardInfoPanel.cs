using UnityEngine;
using TMPro;

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

    public Card selectedCard;

    public bool isActive = false;
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
        }
        else {
            cardName.text = "";
            cardDescription.text = "";
            cardType.text = "";
        }
    }
}
