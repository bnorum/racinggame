using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public string cardEffectName;

    public CardSchema cardSchema;

    public GameObject cardInfoPanel;
    public GameObject cardInfoPanelTargetLeft;
    public GameObject cardInfoPanelTargetRight;
    public Image cardImage;

    public bool isOnSlot = true;

    public Canvas currentCanvas;

    public GameObject EquipPanel;
    public GameObject ShopPanel;
    public GameObject trashCan;

    public List<GameObject> slots;
    public Slot currentSlot;

    public int price;

    public bool isEnabled = true;
    public Image isEnabledImage;

    public CardEffect cardEffect;
    // Start is called before the first frame update
    void Start()
    {
        trashCan = GameObject.Find("Trash");
        currentCanvas = GameObject.Find("ShopCanvas").GetComponent<Canvas>();
        EquipPanel = EquipPanelManager.Instance.gameObject;
        ShopPanel = ShopPanelManager.Instance.gameObject;
        cardInfoPanel = GameObject.FindGameObjectWithTag("InfoPanel");

        cardEffectName = cardSchema.cardEffectName;
        cardImage.sprite = cardSchema.cardImage;
        price = cardSchema.price;

        slots = GameObject.FindGameObjectsWithTag("Slot").ToList();
        currentSlot = transform.parent.GetComponent<Slot>();

        if (cardEffectName != "") cardEffect = gameObject.AddComponent(System.Type.GetType(cardEffectName)) as CardEffect;
        cardImage.sprite = Resources.Load<Sprite>("CardImages/"+ TransformTypeString(cardSchema.cardType.ToString())+"/"+cardSchema.name);
        isEnabled = true;

    }



    // Update is called once per frame
    void Update()
    {
        if (isOnSlot) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
        }
        if (IsPointerOverUIElement(GetComponent<RectTransform>()) && CardInfoPanel.Instance.selectedCard == GetComponent<Card>()) {
            cardInfoPanel.GetComponent<CardInfoPanel>().isActive = true;
        } else if (CardInfoPanel.Instance.selectedCard == GetComponent<Card>()) {
            cardInfoPanel.GetComponent<CardInfoPanel>().isActive = false;
        }



        if (transform.position.x < Screen.width / 2 && CardInfoPanel.Instance.selectedCard == GetComponent<Card>())
        {
            cardInfoPanel.transform.position = cardInfoPanelTargetRight.transform.position;
        }
        else if (transform.position.x > Screen.width / 2 && CardInfoPanel.Instance.selectedCard == GetComponent<Card>())
        {
            cardInfoPanel.transform.position = cardInfoPanelTargetLeft.transform.position;
        }


        isEnabledImage.gameObject.SetActive(!isEnabled);
    }

    public bool IsPointerOverUIElement(RectTransform rectTransform)
    {
        // Get the current mouse or touch position
        Vector2 screenPosition = Input.mousePosition; // For mouse
        if (Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position; // For touch
        }

        // Check if the screen position is within the RectTransform
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPosition, null);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isOnSlot = false;
        cardInfoPanel.GetComponent<CardInfoPanel>().UpdateCardInfo(this);
        Trash.Instance.heldCard = this;

        Debug.Log("Drag started!");
    }

    public void OnDrag(PointerEventData eventData)
    {

        // Update the position of the UI element based on the drag input
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            currentCanvas.transform as RectTransform, // Parent canvas
            eventData.position, // Current mouse/touch position
            currentCanvas.worldCamera, // Canvas camera (if using Screen Space - Camera)
            out position // Output position in local space
        );

        // Set the position of the UI element
        transform.position = currentCanvas.transform.TransformPoint(position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cardInfoPanel.GetComponent<CardInfoPanel>().isActive = false;
        cardInfoPanel.GetComponent<CardInfoPanel>().selectedCard = null;

        isOnSlot = true;

        if (IsPointerOverUIElement(trashCan.GetComponent<RectTransform>()) && !currentSlot.isShop && !currentSlot.isLocked) {
            if (GetComponent<CardEffect>() != null) GetComponent<CardEffect>().OnSell();
            currentSlot.GetComponent<Slot>().RemoveCardFromSlot();
            EquipPanelManager.Instance.Cards.Remove(gameObject.GetComponent<Card>());
            PersistentData.playerMoney += cardSchema.price / 2;
            Destroy(gameObject);
        }

        foreach (GameObject slot in slots) {
            if (IsPointerOverUIElement(slot.GetComponent<RectTransform>())
            && slot.GetComponent<Slot>().isOccupied == false
            && slot.GetComponent<Slot>().isLocked == false
            && currentSlot.GetComponent<Slot>().isLocked == false
            && (cardSchema.cardType == slot.GetComponent<Slot>().cardType
            || cardSchema.cardType == CardSchema.CardType.ANY
            || slot.GetComponent<Slot>().cardType == CardSchema.CardType.ANY)
            && (currentSlot.GetComponent<Slot>().isShop == false
            || (currentSlot.GetComponent<Slot>().isShop == true
            && PersistentData.playerMoney >= cardSchema.price))) {

                if (currentSlot.GetComponent<Slot>().isShop && slot.GetComponent<Slot>().isShop == false) {
                    PersistentData.playerMoney -= cardSchema.price;
                    ShopPanelManager.Instance.CardsInShop.Remove(gameObject.GetComponent<Card>());
                    if (GetComponent<CardEffect>() != null) GetComponent<CardEffect>().OnBuy();
                }
                currentSlot.GetComponent<Slot>().RemoveCardFromSlot();
                slot.GetComponent<Slot>().AddCardToSlot(gameObject.GetComponent<Card>());
                currentSlot = slot.GetComponent<Slot>();
            }
        }
        Trash.Instance.heldCard = null;
        Debug.Log("Drag ended!");
    }




    public string TransformTypeString(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        string lowerCaseString = input.ToLower();
        char lastChar = char.ToUpper(lowerCaseString[lowerCaseString.Length - 1]);
        return lowerCaseString.Substring(0, lowerCaseString.Length - 1) + lastChar;
    }

    public IEnumerator Shake() {
        Vector3 originalRotation = transform.eulerAngles;
        float elapsedTime = 0f;
        while (elapsedTime < 0.2f) {
            float zRotation = Random.Range(-5f, 5f);
            transform.eulerAngles = new Vector3(originalRotation.x, originalRotation.y, originalRotation.z + zRotation);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.eulerAngles = Vector3.zero;
    }

}
