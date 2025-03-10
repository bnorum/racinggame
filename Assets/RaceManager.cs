using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        SelectCameraAngle(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (raceActive) {
            raceTimer += Time.deltaTime;
            playerStatsText.text = "Time: " + raceTimer.ToString("F2");
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

    public void StartRace() {
        CalibrateRaceTrack();
        SelectCameraAngle(1);
        player.CalculatePlayerStats();
        foreach(Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardEffect != null) {
                card.cardEffect.ApplyCardEffectAtStartOfRace();
            }

        }
        //playerStatsText.text = player.acceleration.ToString() + "^" + player.driverPower.ToString() + " " + player.maxSpeed.ToString() + "^" + player.driverPower.ToString();


        player.carState = Car.CarState.RACING;
        raceActive = true;


        ShopCanvas.enabled = false;
        RaceCanvas.enabled = true;
    }

    public void EndRace(bool playerWon) {
        Debug.Log("RACE OVER! TIME: " + raceTimer.ToString("F2"));
        raceActive = false;
        SelectCameraAngle(4);
        player.transform.position = playerStart.position;
        player.carState = Car.CarState.GARAGE;
        player.distanceTraveled = 0f;

        foreach(Card card in EquipPanelManager.Instance.Cards) {
            if (card.cardEffect != null) {
                card.cardEffect.ApplyCardEffectAtEndOfRace();
            }

        }

        ShopCanvas.enabled = true;
        RaceCanvas.enabled = false;
        if (playerWon) {
            PersistentData.playerMoney += 5;
            GameObject.Find("ShopPanel").GetComponent<ShopPanelManager>().FillShop();
        } else {
            Debug.Log("Game Over...");
        }

        player.transform.position = playerStart.position;
    }

    public void SelectCameraAngle(int anglenum) {

        cam.gameObject.transform.position = cameraAngles[anglenum].position;
        cam.gameObject.transform.rotation = cameraAngles[anglenum].rotation;
    }

    List<int> raceDistances = new List<int> {50, 100, 200, 350, 550, 800, 1100, 1450, 1850};
    public void CalibrateRaceTrack() {
        if (PersistentData.round <= 9) {
            raceDistance = raceDistances[PersistentData.round-1];
        } else {
        raceDistance = PersistentData.round * 750f;
        }
        road.transform.localScale = new Vector3(road.transform.localScale.x, road.transform.localScale.y, raceDistance + 20);
        finishLine.transform.position = road.transform.position + new Vector3(0, 0, raceDistance);
        cameraAngles[2].position = cameraAngles[0].position + new Vector3(0, 0, raceDistance/2);
        cameraAngles[3].position = cameraAngles[1].position + new Vector3(0, 15, raceDistance + 25f);

    }



}
