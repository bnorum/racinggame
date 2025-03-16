using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    public List<string> locations = new List<string> {"Boston", "Shibuya", "Hyderabad", "Mexico City", "London", "Cairo", "Berlin"};
    public void SetUpRaces() {
        race1Texts[0].text = locations[Random.Range(0, locations.Count)];
        race2Texts[0].text = race1Texts[0].text;
        while (race2Texts[0].text == race1Texts[0].text) {
            race2Texts[0].text = locations[Random.Range(0, locations.Count)];
        }
        race3Texts[0].text = race1Texts[0].text;
        while (race3Texts[0].text == race1Texts[0].text && race3Texts[0].text == race2Texts[0].text) {
            race3Texts[0].text = locations[Random.Range(0, locations.Count)];
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

        RaceManager.Instance.HeadToStart();
    }
}
