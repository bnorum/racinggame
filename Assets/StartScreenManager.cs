using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartScreenManager : MonoBehaviour
{

    public GameObject characterSelectMenu;
    public GameObject mainMenu;


    public int selectedCharacterIndex = 0;
    public List<Sprite> characterSprites = new List<Sprite>();
    public List<Sprite> carSprites = new List<Sprite>();
    public List<string> characterNames = new List<string>();
    public List<string> carNames = new List<string>();
    //{AMERICAN, MEXICAN, BRITISH, JAPANESE, GERMAN, PREHISTORIC};
    public List<string> nationNames = new List<string>();
    public List<string> descriptions = new List<string>();

    public Image characterImage;
    public Image carImage;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI carNameText;
    public TextMeshProUGUI nationNameText;
    public TextMeshProUGUI descriptionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            ChangeSelectedNation(0);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OpenCharacterSelectMenu() {
        characterSelectMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenMainMenu() {
        characterSelectMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ChangeSelectedNation(int selectedNation) {
        selectedCharacterIndex = selectedNation;
        characterImage.sprite = characterSprites[selectedCharacterIndex];
        carImage.sprite = carSprites[selectedCharacterIndex];
        characterNameText.text = characterNames[selectedCharacterIndex] + " in";
        carNameText.text = "\""+ carNames[selectedCharacterIndex] + "\"";
        nationNameText.text = nationNames[selectedCharacterIndex];
        descriptionText.text = descriptions[selectedCharacterIndex];
        switch (selectedNation) {
            case 0:
                PersistentData.playerCarType = PersistentData.CarType.AMERICAN;
                break;
            case 1:
                PersistentData.playerCarType = PersistentData.CarType.MEXICAN;
                break;
            case 2:
                PersistentData.playerCarType = PersistentData.CarType.SOUTHAFRICAN;
                break;
            case 3:
                PersistentData.playerCarType = PersistentData.CarType.JAPANESE;
                break;
            case 4:
                PersistentData.playerCarType = PersistentData.CarType.GERMAN;
                break;
            case 5:
                PersistentData.playerCarType = PersistentData.CarType.PREHISTORIC;
                break;
        }
    }

    public void IncrementSelectedNation() {
        if (selectedCharacterIndex + 1 < characterSprites.Count) {
            ChangeSelectedNation(selectedCharacterIndex + 1);
        } else {
            ChangeSelectedNation(0);
        }
    }

    public void DecrementSelectedNation() {
        if (selectedCharacterIndex - 1 >= 0) {
            ChangeSelectedNation(selectedCharacterIndex - 1);
        } else {
            ChangeSelectedNation(characterSprites.Count - 1);
        }
    }
}
