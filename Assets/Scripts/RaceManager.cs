using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UIElements;

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

    public GameObject ShopPanel;
    public GameObject RaceStatsPanel;
    public GameObject PostRacePanel;
    public GameObject HeadToStartButton;
    public GameObject StartRaceButton;
    public GameObject TrashCan;
    public GameObject MoneyText;
    public GameObject GameOverPanel;
    public GameObject LengthChooser;
    public GameObject RerollButton;


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
        PersistentData.round = 1;
        PersistentData.raceLength = 999999;

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

        if ((player.distanceTraveled >= PersistentData.raceLength) && raceTimer <= PersistentData.raceTime && raceActive) {
            EndRace(true);
        } else if ((player.distanceTraveled >= PersistentData.raceLength) && raceTimer > PersistentData.raceTime && raceActive) {
            EndRace(false);
        }

        if (player.distanceTraveled > PersistentData.raceLength/2 - 20) {
            SelectCameraAngle(2);
        }

        if (player.distanceTraveled > PersistentData.raceLength*0.75f) {
            SelectCameraAngle(3);
        }

    }

    public void SelectCameraAngle(int anglenum) {

        cam.gameObject.transform.position = cameraAngles[anglenum].position;
        cam.gameObject.transform.rotation = cameraAngles[anglenum].rotation;
    }

    public void CalibrateRaceTrack() {

        road.transform.localScale = new Vector3(road.transform.localScale.x, road.transform.localScale.y, PersistentData.raceLength + 20);
        finishLine.transform.position = road.transform.position + new Vector3(0, 0, PersistentData.raceLength);
        cameraAngles[2].position = cameraAngles[0].position + new Vector3(0, 0, PersistentData.raceLength/2);
        cameraAngles[3].position = cameraAngles[1].position + new Vector3(0, 15, PersistentData.raceLength + 25f);

    }

    public void OpenLengthChooser() {
        LengthChooser.SetActive(true);
        LengthChooser.GetComponent<RaceLengthChooser>().SetUpRaces();
    }


    public System.Collections.IEnumerator HeadToStartCoroutine() {
        yield return new WaitForSeconds(0.5f);
        LengthChooser.SetActive(false);
        player.ResetCar();
        HeadToStartButton.SetActive(false);
        ShopPanel.SetActive(false);
        PostRacePanel.SetActive(false);
        TrashCan.SetActive(false);
        MoneyText.SetActive(false);
        timeText.gameObject.SetActive(true);
        RaceStatsPanel.SetActive(true);
        RerollButton.SetActive(false);
        driverPowerText.transform.parent.gameObject.SetActive(true);
        driverPowerText.transform.parent.position = driverPowerOGPosition;
        accelerationText.transform.parent.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        speedText.transform.parent.GetComponent<UnityEngine.UI.Image>().color = Color.red;
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



        foreach(Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardEffect != null) {
                card.cardEffect.ApplyCardEffectAtStartOfRaceAfterCalculatingStats();
            }
            yield return new WaitForSeconds(PersistentData.calculationDelay);
        }

        Transform driverPowerParent = driverPowerText.transform.parent;
        Transform speedTextParent = speedText.transform.parent;
        Vector3 targetPosition = new Vector3(driverPowerParent.position.x, speedTextParent.position.y, driverPowerParent.position.z);

        while (Vector3.Distance(driverPowerParent.position, targetPosition) > 0.01f) {
            driverPowerText.transform.parent.position = Vector3.MoveTowards(driverPowerText.transform.parent.position, targetPosition, Time.deltaTime * 2000f);
            yield return null;
        }



        if (player.driverPower != 1) {
            RaceManager.Instance.CreateBonusText(Mathf.Pow(player.maxSpeed, player.driverPower) - player.maxSpeed, 1, speedText.gameObject);
            RaceManager.Instance.CreateBonusText(Mathf.Pow(player.acceleration, player.driverPower) - player.acceleration, 1, accelerationText.gameObject);
            player.ApplyDriverPower();
        }

        driverPowerText.transform.parent.gameObject.SetActive(false);

        if (PersistentData.playerCarType == PersistentData.CarType.GERMAN) {
            float avg = (player.maxSpeed + player.acceleration) / 2f;
            player.maxSpeed = avg;
            player.acceleration = avg;
            CreateBonusText(0, 1, accelerationText.gameObject);
            CreateBonusText(0, 1, speedText.gameObject);
            accelerationText.transform.parent.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
            speedText.transform.parent.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
        }
        yield return new WaitForSeconds(PersistentData.calculationDelay);







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
        foreach(Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardEffect != null) {
                card.cardEffect.ApplyCardEffectWhenRaceBegins();
            }
        }
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

        for (int i = 0; i < PersistentData.raceReward; i++) {
            PersistentData.playerMoney += 1;
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < Mathf.Clamp(PersistentData.playerMoney, 0, 25)  / 5; i++) {
            PersistentData.playerMoney += 1;
            yield return new WaitForSeconds(0.1f);
        }
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
        RerollButton.SetActive(true);

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
        if (type == 1 && quantity == 0) return;
        if (type == 2 && quantity == 1) return;
        StartCoroutine(UIPulse(location.transform.parent.gameObject));
        if (relatedCard != null) {
            StartCoroutine(relatedCard.Shake());
        }
        if (type == 1 && quantity < 0 || type == 2 && quantity < 1) {
            AudioManager.Instance.PlayScoreCalcRevNegative();
        } else {
            AudioManager.Instance.PlayScoreCalcRev();
        }
        GameObject bonusText = Instantiate(BonusTextPrefab, Vector3.zero, Quaternion.identity, location.transform);
        bonusText.transform.localPosition = new Vector3(0, 0, 0) + new Vector3(0, 20, 0);

        //bonusText.transform.SetParent(canvas.transform, true);
        bonusText.transform.SetAsLastSibling();
        switch (type) {
            case 1:
                if (quantity > 0) bonusText.GetComponent<TextMeshProUGUI>().text = "+" + quantity.ToString("F2");
                if (quantity < 0) bonusText.GetComponent<TextMeshProUGUI>().text = quantity.ToString("F2");
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
