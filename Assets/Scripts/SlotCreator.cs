using UnityEngine;
using UnityEngine.UI;

public class SlotCreator : MonoBehaviour
{
    public static SlotCreator Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public GameObject slotPrefab;
    public GameObject topSlotHolder;
    public Car player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (PersistentData.playerCarType) {
            case PersistentData.CarType.AMERICAN:
                CreateRegularSlots();
                ShopPanelManager.Instance.maxSlots += 1;
                break;
            case PersistentData.CarType.JAPANESE:
                CreateRegularSlots();
                GiveExtraEngine();
                break;
            case PersistentData.CarType.GERMAN:
                CreateRegularSlots();
                player.isGerman = true;
                break;
            case PersistentData.CarType.MEXICAN:
                CreateRegularSlots();
                break;
            case PersistentData.CarType.SOUTHAFRICAN:
                CreateRegularSlots();
                break;
            case PersistentData.CarType.PREHISTORIC:
                CreateRegularSlots();
                GiveHandCrank();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateRegularSlots() {

        GameObject transSlot = Instantiate(slotPrefab, topSlotHolder.transform);
        transSlot.GetComponent<Slot>().cardType = CardSchema.CardType.TRANSMISSION;
        EquipPanelManager.Instance.TransmissionSlot = transSlot.GetComponent<Slot>();
        GameObject engineSlot = Instantiate(slotPrefab, topSlotHolder.transform);
        engineSlot.GetComponent<Slot>().cardType = CardSchema.CardType.ENGINE;
        EquipPanelManager.Instance.EngineSlot = engineSlot.GetComponent<Slot>();
        GameObject dashSlot = Instantiate(slotPrefab, topSlotHolder.transform);
        dashSlot.GetComponent<Slot>().cardType = CardSchema.CardType.DASH;
        EquipPanelManager.Instance.DashSlot = dashSlot.GetComponent<Slot>();
        GameObject mirrorSlot = Instantiate(slotPrefab, topSlotHolder.transform);
        mirrorSlot.GetComponent<Slot>().cardType = CardSchema.CardType.MIRROR;
        EquipPanelManager.Instance.MirrorSlot = mirrorSlot.GetComponent<Slot>();

    }

    void GiveExtraEngine() {
        GameObject extraEngineSlot = Instantiate(slotPrefab, topSlotHolder.transform);

        extraEngineSlot.GetComponent<Slot>().cardType = CardSchema.CardType.ENGINE;
        extraEngineSlot.transform.SetSiblingIndex(2);
        topSlotHolder.GetComponent<HorizontalLayoutGroup>().spacing = -30f;
        topSlotHolder.GetComponent<HorizontalLayoutGroup>().padding.left = -20;
    }

    void GiveHandCrank() {
        Slot EngineSlot = EquipPanelManager.Instance.EngineSlot;
        CardManager.Instance.GenerateNamedCard("HandCrank", EngineSlot);
        EngineSlot.isLocked = true;
    }
}
