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
    public Car opponent;
    public Transform opponentStart;
    public GameObject road;
    public GameObject finishLine;

    public Camera cam;
    public List<Transform> cameraAngles = new List<Transform>();

    public float raceDistance = 100f;

    public Canvas ShopCanvas;
    public Canvas RaceCanvas;

    public TextMeshProUGUI playerStatsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        SelectCameraAngle(0);
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.distanceTraveled >= raceDistance || opponent.distanceTraveled >= raceDistance) && (player.isRacing || opponent.isRacing)) {
            EndRace(player.distanceTraveled >= raceDistance);
        }

        if (player.distanceTraveled > raceDistance/2 - 20 || opponent.distanceTraveled > raceDistance/2 - 20) {
            SelectCameraAngle(2);
        }

        if (player.distanceTraveled > raceDistance*0.75f || opponent.distanceTraveled > raceDistance*0.75f) {
            SelectCameraAngle(3);
        }

    }

    public void StartRace() {
        CalibrateRaceTrack();
        SelectCameraAngle(1);
        player.CalculatePlayerStats();
        playerStatsText.text = player.acceleration.ToString() + "^" + player.driverPower.ToString() + " " + player.maxSpeed.ToString() + "^" + player.driverPower.ToString();
        opponent.CalculateEnemyStats();

        player.isRacing = true;
        opponent.isRacing = true;

        ShopCanvas.enabled = false;
        RaceCanvas.enabled = true;
    }

    public void EndRace(bool playerWon) {
        SelectCameraAngle(0);
        player.isRacing = false;
        opponent.isRacing = false;

        ShopCanvas.enabled = true;
        RaceCanvas.enabled = false;
        if (playerWon) {
            PersistentData.playerMoney += 5;
            GameObject.Find("ShopPanel").GetComponent<ShopPanelManager>().FillShop();
        } else {
            Debug.Log("Game Over...");
        }

        player.transform.position = playerStart.position;
        opponent.transform.position = opponentStart.position;
    }

    public void SelectCameraAngle(int anglenum) {

        cam.gameObject.transform.position = cameraAngles[anglenum].position;
        cam.gameObject.transform.rotation = cameraAngles[anglenum].rotation;
    }

    public void CalibrateRaceTrack() {
        raceDistance = PersistentData.round * 100f;
        road.transform.localScale = new Vector3(road.transform.localScale.x, road.transform.localScale.y, raceDistance + 20);
        finishLine.transform.position = road.transform.position + new Vector3(0, 0, raceDistance);
        cameraAngles[2].position = cameraAngles[0].position + new Vector3(0, 0, raceDistance/2);
        cameraAngles[3].position = cameraAngles[1].position + new Vector3(0, 15, raceDistance + 25f);

    }



}
