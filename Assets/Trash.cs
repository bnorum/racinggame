using TMPro;
using UnityEngine;

public class Trash : MonoBehaviour
{

    public static Trash Instance;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public TextMeshProUGUI trashText;
    public Card heldCard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (heldCard != null && !ShopPanelManager.Instance.CardsInShop.Contains(heldCard)) {
            trashText.text = "Sell for $" + heldCard.cardSchema.price / 2;
        } else {
            trashText.text = "Trash";
        }

    }
}
