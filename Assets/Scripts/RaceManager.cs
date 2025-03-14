using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;
using System.Collections;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }


    public Car player;
    public Transform playerStart;
    public GameObject road;
    public GameObject finishLine;

    public Canvas canvas;

    public Camera cam;
    public List<Transform> cameraAngles = new List<Transform>();

    public float raceDistance = 100f;

    public GameObject ShopPanel;
    public GameObject RaceStatsPanel;
    public GameObject PostRacePanel;
    public GameObject HeadToStartButton;
    public GameObject StartRaceButton;
    public GameObject TrashCan;
    public GameObject MoneyText;
    public GameObject GameOverPanel;


    public TextMeshProUGUI timeText;

    public float raceTimer = 0f;
    public bool raceActive = false;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI accelerationText;
    public TextMeshProUGUI driverPowerText;
    public Vector3 driverPowerOGPosition;

    public GameObject BonusTextPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        SelectCameraAngle(4);
        ReturnToGarage();
        player.transform.position = player.garagePosition.transform.position;
        driverPowerOGPosition = driverPowerText.transform.parent.position;


    }

    // Update is called once per frame
    void Update()
    {
        if (raceActive) {
            raceTimer += Time.deltaTime;
        }
        timeText.text = raceTimer.ToString("F2");
        speedText.text = player.maxSpeed.ToString("F2");
        accelerationText.text = player.acceleration.ToString("F2");
        driverPowerText.text = player.driverPower.ToString("F2");

        if ((player.distanceTraveled >= raceDistance) && raceTimer <= 5 && raceActive) {
            EndRace(true);
        } else if ((player.distanceTraveled >= raceDistance) && raceTimer > 5 && raceActive) {
            EndRace(false);
        }

        if (player.distanceTraveled > raceDistance/2 - 20) {
            SelectCameraAngle(2);
        }

        if (player.distanceTraveled > raceDistance*0.75f) {
            SelectCameraAngle(3);
        }

    }

    public void SelectCameraAngle(int anglenum) {

        cam.gameObject.transform.position = cameraAngles[anglenum].position;
        cam.gameObject.transform.rotation = cameraAngles[anglenum].rotation;
    }

    List<int> raceDistances = new List<int> {50, 100, 150, 250, 300, 350, 450, 500, 550};
    public void CalibrateRaceTrack() {
        if (PersistentData.round <= 9) {
            raceDistance = raceDistances[PersistentData.round-1];
        } else {
        raceDistance = PersistentData.round * 100f;
        }
        road.transform.localScale = new Vector3(road.transform.localScale.x, road.transform.localScale.y, raceDistance + 20);
        finishLine.transform.position = road.transform.position + new Vector3(0, 0, raceDistance);
        cameraAngles[2].position = cameraAngles[0].position + new Vector3(0, 0, raceDistance/2);
        cameraAngles[3].position = cameraAngles[1].position + new Vector3(0, 15, raceDistance + 25f);

    }

    public System.Collections.IEnumerator HeadToStartCoroutine() {
        player.ResetCar();
        HeadToStartButton.SetActive(false);
        ShopPanel.SetActive(false);
        PostRacePanel.SetActive(false);
        TrashCan.SetActive(false);
        MoneyText.SetActive(false);
        timeText.gameObject.SetActive(true);
        RaceStatsPanel.SetActive(true);
        driverPowerText.transform.parent.gameObject.SetActive(true);
        driverPowerText.transform.parent.position = driverPowerOGPosition;
        accelerationText.transform.parent.GetComponent<Image>().color = Color.green;
        speedText.transform.parent.GetComponent<Image>().color = Color.red;
        AudioManager.Instance.ResetScoreCalcRev();
        yield return new WaitForSeconds(0.5f);


        player.transform.position = playerStart.position;
        player.carState = Car.CarState.ATSTART;
        player.distanceTraveled = 0f;
        SelectCameraAngle(0);

        //One day, these will have animations attached. Stay tuned.
        foreach(Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardEffect != null) {
                card.cardEffect.ApplyCardEffectAtStartOfRaceBeforeCalculatingStats();
            }
            yield return new WaitForSeconds(PersistentData.calculationDelay);
        }
        StartCoroutine(player.CalculatePlayerStats());
        while (!player.statsCalculated) {
            yield return null;
        }
        player.statsCalculated = false;

        Transform driverPowerParent = driverPowerText.transform.parent;
        Transform speedTextParent = speedText.transform.parent;
        Vector3 targetPosition = new Vector3(driverPowerParent.position.x, speedTextParent.position.y, driverPowerParent.position.z);

        while (Vector3.Distance(driverPowerParent.position, targetPosition) > 0.01f) {
            driverPowerText.transform.parent.position = Vector3.MoveTowards(driverPowerText.transform.parent.position, targetPosition, Time.deltaTime * 2000f);
            yield return null;
        }

        driverPowerText.transform.parent.gameObject.SetActive(false);

        foreach(Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardEffect != null) {
                card.cardEffect.ApplyCardEffectAtStartOfRaceAfterCalculatingStats();
            }
            yield return new WaitForSeconds(PersistentData.calculationDelay);
        }







        StartRaceButton.SetActive(true);


    }

    public void HeadToStart() {
        StartCoroutine(HeadToStartCoroutine());
    }

    public void StartRace() {

        StartRaceButton.SetActive(false);

        CalibrateRaceTrack();
        SelectCameraAngle(1);


        player.carState = Car.CarState.RACING;
        raceActive = true;
        raceTimer = 0f;
    }

    public void EndRace(bool playerWon) {
        Debug.Log("RACE OVER! TIME: " + raceTimer.ToString("F2"));
        raceActive = false;
        foreach(Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardEffect != null) {
                card.cardEffect.ApplyCardEffectAtEndOfRace();
            }

        }
        if (playerWon) {
            StartCoroutine(CreateEndOfRoundScreen());
        } else {
            GameOver();
        }
    }

    public System.Collections.IEnumerator CreateEndOfRoundScreen() {
        PostRacePanel.SetActive(true);
        RaceStatsPanel.SetActive(false);
        ShopPanel.SetActive(false);
        StartRaceButton.SetActive(false);
        HeadToStartButton.SetActive(false);


        PersistentData.round++;

        for (int i = 0; i < 5; i++) {
            PersistentData.playerMoney += 1;
            yield return new WaitForSeconds(0.1f);
        }
        PersistentData.playerMoney += Mathf.Clamp(PersistentData.playerMoney, 0, 25)  / 5;
        //show money made, etc. etc.

    }

    public void ReturnToGarage() {

        RaceStatsPanel.SetActive(false);
        ShopPanel.SetActive(true);
        PostRacePanel.SetActive(false);
        StartRaceButton.SetActive(false);
        HeadToStartButton.SetActive(true);
        timeText.gameObject.SetActive(false);
        TrashCan.SetActive(true);
        MoneyText.SetActive(true);

        player.transform.position = playerStart.position;
        GameObject.Find("ShopPanel").GetComponent<ShopPanelManager>().FillShop();
        player.carState = Car.CarState.GARAGE;
        player.distanceTraveled = 0f;
        SelectCameraAngle(4);
    }

    public void GameOver() {
        GameOverPanel.SetActive(true);
    }


    public void CreateBonusText(float quantity, int type, GameObject location, Card relatedCard = null) {
        //1 = add, 2 = mult
        GameObject bonusText = Instantiate(BonusTextPrefab, Vector3.zero, Quaternion.identity, location.transform);
        AudioManager.Instance.PlayScoreCalcRev();
        bonusText.transform.localPosition = new Vector3(0, 0, 0) + new Vector3(0, 20, 0);
        StartCoroutine(UIPulse(location.transform.parent.gameObject));
        if (relatedCard != null) {
            StartCoroutine(relatedCard.Shake());
        }
        //bonusText.transform.SetParent(canvas.transform, true);
        bonusText.transform.SetAsLastSibling();
        switch (type) {
            case 1:
                bonusText.GetComponent<TextMeshProUGUI>().text = "+" + quantity.ToString("F2");
                break;
            case 2:
                bonusText.GetComponent<TextMeshProUGUI>().text = "x" + quantity.ToString("F2");
                break;
        }


    }

    public IEnumerator UIPulse(GameObject uiElement) {
        uiElement.transform.localScale = uiElement.transform.localScale + new Vector3(0.2f, 0.2f, 0.2f);
        while (uiElement.transform.localScale.x > 1f) {
            uiElement.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            yield return null;
        }
    }




}
