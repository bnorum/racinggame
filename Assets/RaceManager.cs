using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.UI;

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

    public Camera cam;
    public List<Transform> cameraAngles = new List<Transform>();

    public float raceDistance = 100f;

    public Canvas ShopCanvas;
    public Canvas RaceCanvas;

    public TextMeshProUGUI playerStatsText;

    public float raceTimer = 0f;
    public bool raceActive = false;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI accelerationText;
    public TextMeshProUGUI driverPowerText;

    public Button nextRoundButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        SelectCameraAngle(4);
        player.transform.position = player.garagePosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (raceActive) {
            raceTimer += Time.deltaTime;
            playerStatsText.text = "Time: " + raceTimer.ToString("F2");
            speedText.text = "Speed: " + player.maxSpeed.ToString("F2");
            accelerationText.text = "Acceleration: " + player.acceleration.ToString("F2");
            driverPowerText.text = "Driver Power: " + player.driverPower.ToString("F2");
        }
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

    public void HeadToStart() {
        player.transform.position = playerStart.position;
        player.carState = Car.CarState.ATSTART;
        player.distanceTraveled = 0f;
        SelectCameraAngle(0);

        player.CalculatePlayerStats();

        StartCoroutine(TransitionCanvas(ShopCanvas, false));
        StartCoroutine(TransitionCanvas(RaceCanvas, true));
        nextRoundButton.gameObject.SetActive(false);
    }

    public void StartRace() {
        CalibrateRaceTrack();
        SelectCameraAngle(1);
        foreach(Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardEffect != null) {
                card.cardEffect.ApplyCardEffectAtStartOfRace();
            }

        }
        //playerStatsText.text = player.acceleration.ToString() + "^" + player.driverPower.ToString() + " " + player.maxSpeed.ToString() + "^" + player.driverPower.ToString();


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
            nextRoundButton.gameObject.SetActive(true);
            CreateEndOfRoundScreen();
        } else {
            nextRoundButton.gameObject.SetActive(false);
            GameOver();
        }


    }

    public void ReturnToGarage() {
        StartCoroutine(TransitionCanvas(ShopCanvas, true));
        StartCoroutine(TransitionCanvas(RaceCanvas, false));

        player.transform.position = playerStart.position;
        player.carState = Car.CarState.GARAGE;
        player.distanceTraveled = 0f;
        SelectCameraAngle(4);
        PersistentData.playerMoney += 5;
        PersistentData.playerMoney += PersistentData.playerMoney / 5;
        GameObject.Find("ShopPanel").GetComponent<ShopPanelManager>().FillShop();
        PersistentData.round++;

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

    public void CreateEndOfRoundScreen() {

    }

    public void GameOver() {

    }

    System.Collections.IEnumerator TransitionCanvas(Canvas canvas, bool fadeIn) {
        if (fadeIn) {
            canvas.enabled = true;
            canvas.GetComponent<CanvasGroup>().alpha = 0;

            for (float i = 0; i <= 1; i += Time.deltaTime) {
                canvas.GetComponent<CanvasGroup>().alpha = i;
                yield return null;
            }
        }
        else {
            for (float i = 1; i >= 0; i -= Time.deltaTime) {
                canvas.GetComponent<CanvasGroup>().alpha = i;
                yield return null;
            }
            canvas.enabled = false;
        }

    }



}
