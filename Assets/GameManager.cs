using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public RaceManager raceManager;
    public ShopPanelManager shopPanelManager;
    public EquipPanelManager equipPanelManager;
    public CardManager cardManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        raceManager = RaceManager.Instance;
        shopPanelManager = ShopPanelManager.Instance;
        equipPanelManager = EquipPanelManager.Instance;
        cardManager = CardManager.Instance;

        PersistentData.round = 1;
        PersistentData.playerMoney = 7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
