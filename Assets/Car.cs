using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Car : MonoBehaviour
{
    public List<CardSchema> cards;
    public float currentSpeed;
    public float maxSpeed = 30f;
    public float baseMaxSpeed = 30f;
    public float acceleration = 5f;
    public float baseAcceleration = 5f;
    public float driverPower = 1f;
    public float baseDriverPower = 1f;



    public enum CarState {
        RACING,
        ATSTART,
        GARAGE
    }
    public CarState carState = CarState.ATSTART;

    public bool isPlayer = false;

    public float distanceTraveled = 0f;

    public GameObject garagePosition;
    public GameObject startLinePosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (carState == CarState.RACING) {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxSpeed) {
                currentSpeed = maxSpeed;
            }
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            distanceTraveled += currentSpeed * Time.deltaTime;
        } else if (carState == CarState.GARAGE) {
            transform.position = Vector3.MoveTowards(transform.position, garagePosition.transform.position, currentSpeed * Time.deltaTime);
        } else if (carState == CarState.ATSTART) {
            transform.position = Vector3.MoveTowards(transform.position, startLinePosition.transform.position, currentSpeed * Time.deltaTime);
        }
    }

    public void CalculatePlayerStats() {
        distanceTraveled = 0f;
        currentSpeed = 0f;
        driverPower = 1f;
        float flatSpeed = 0f;
        float flatAcceleration = 0f;
        float flatDriverPower = 0f;
        float multSpeed = 1f;
        float multAcceleration = 1f;
        float multDriverPower = 1f;
        if (isPlayer) GetCards();
        foreach (CardSchema card in cards) {
            bool isMultiplier = false;
            if (card.cardQuantityType == CardSchema.CardQuantityType.MULTIPLY) {
                isMultiplier = true;
            } else if (card.cardQuantityType == CardSchema.CardQuantityType.ADD) {
                isMultiplier = false;
            }
            switch (card.cardQuantityModified) {
                case CardSchema.CardQuantityModified.ACCELERATION:
                    if (isMultiplier) {
                        multAcceleration *= card.cardQuantityValue;
                    } else {
                        flatAcceleration += card.cardQuantityValue;
                    }
                    break;
                case CardSchema.CardQuantityModified.DRIVERPOWER:
                    if (isMultiplier && card.cardQuantityValue > 1) {
                        multDriverPower *= card.cardQuantityValue;
                    } else {
                        flatDriverPower += card.cardQuantityValue;
                    }
                    break;
                case CardSchema.CardQuantityModified.SPEED:
                    if (isMultiplier) {
                        multSpeed *= card.cardQuantityValue;
                    } else {
                        flatSpeed += card.cardQuantityValue;
                    }
                    break;
            }
        }

        acceleration = baseAcceleration + flatAcceleration;
        maxSpeed = baseMaxSpeed + flatSpeed;
        driverPower = baseDriverPower + flatDriverPower;

        acceleration *= multAcceleration;
        maxSpeed *= multSpeed;
        driverPower *= multDriverPower;
        acceleration = Mathf.Pow(acceleration, driverPower);
        maxSpeed = Mathf.Pow(maxSpeed, driverPower);
    }

    public void CalculateEnemyStats() {
        distanceTraveled = 0f;
        currentSpeed = 0f;
        float driverPower = 1f;

        for (int i = 0; i < PersistentData.round; i++) {
            int choice = Random.Range(0, 5);
            switch (choice) {
                case 0:
                    acceleration += Random.Range(2f, 4f);
                    break;
                case 1:
                    acceleration *= Random.Range(1.1f, 1.3f);
                    break;
                case 2:
                    maxSpeed += Random.Range(2f, 5f);
                    break;
                case 3:
                    maxSpeed *= Random.Range(1.1f, 1.3f);
                    break;
                case 4:
                    driverPower += Random.Range(0.1f, 0.3f);
                    break;
            }
        }
        acceleration = Mathf.Pow(acceleration, driverPower);
        maxSpeed = Mathf.Pow(maxSpeed, driverPower);
    }

    public void GetCards() {
        cards = EquipPanelManager.Instance.GetCards();
    }

}
