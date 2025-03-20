using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceLengthChooser : MonoBehaviour
{

    public static RaceLengthChooser Instance;
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }


    public List<TextMeshProUGUI> race1Texts = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> race2Texts = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> race3Texts = new List<TextMeshProUGUI>();
    public Image race1Image;
    public Image race2Image;
    public Image race3Image;

    public List<string> locations;
    public List<Sprite> locationImages = new List<Sprite>();

    public List<GameObject> buildingPrefabs = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRaceLength(int length) {
        PersistentData.raceLength = length;
    }

    public void SetRaceTime(int time) {
        PersistentData.raceTime = time;
    }


    public void SetUpRaces() {
        int race1choice = Random.Range(0, locations.Count);
        race1Texts[0].text = locations[race1choice];
        race1Image.sprite = locationImages[race1choice];
        race2Texts[0].text = race1Texts[0].text;
        while (race2Texts[0].text == race1Texts[0].text) {
            int race2choice = Random.Range(0, locations.Count);
            race2Texts[0].text = locations[race2choice];
            race2Image.sprite = locationImages[race2choice];
        }
        race3Texts[0].text = race1Texts[0].text;
        while (race3Texts[0].text == race1Texts[0].text || race3Texts[0].text == race2Texts[0].text) {
            int race3choice = Random.Range(0, locations.Count);
            race3Texts[0].text = locations[race3choice];
            race3Image.sprite = locationImages[race3choice];
        }

        race1Texts[1].text = PersistentData.round * 100 + "m";
        race2Texts[1].text = PersistentData.round * 300 + "m";
        race3Texts[1].text = PersistentData.round * 600 + "m";
        race1Texts[2].text = "TIME TO BEAT: 5s";
        race2Texts[2].text = "TIME TO BEAT: 15s";
        race3Texts[2].text = "TIME TO BEAT: 30s";
        race1Texts[3].text = "REWARD: $6";
        race2Texts[3].text = "REWARD: $5";
        race3Texts[3].text = "REWARD: $3";
    }

    public void SelectRace(int racenum) {
        if (racenum == 1) {
            PersistentData.raceLength = PersistentData.round * 100;
            PersistentData.raceTime = 5;
            PersistentData.raceReward = 6;
        } else if (racenum == 2) {
            PersistentData.raceLength = PersistentData.round * 300;
            PersistentData.raceTime = 15;
            PersistentData.raceReward = 5;
        } else if (racenum == 3) {
            PersistentData.raceLength = PersistentData.round * 600;
            PersistentData.raceTime = 30;
            PersistentData.raceReward = 3;
        }

        CreateBuildingsAlongLength(PersistentData.raceLength);
        RaceManager.Instance.HeadToStart();
    }


    public void CreateBuildingsAlongLength(float length) {
        float distanceBetweenBuildings = 50f;
        float numberOfBuildings = length / distanceBetweenBuildings;
        for (int i = 0; i < numberOfBuildings; i++) {
            int buildingChoice = Random.Range(0, buildingPrefabs.Count);
            GameObject building = Instantiate(buildingPrefabs[buildingChoice], new Vector3(-25, -1.75f, i * distanceBetweenBuildings), Quaternion.identity);
        }
        for (int i = 0; i < numberOfBuildings; i++) {
            int buildingChoice = Random.Range(0, buildingPrefabs.Count);
            GameObject building = Instantiate(buildingPrefabs[buildingChoice], new Vector3(25, -1.75f, i * distanceBetweenBuildings), Quaternion.identity);
        }
    }
}
