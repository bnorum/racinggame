using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Car : MonoBehaviour
{
    public List<CardSchema> cards;
    public float currentSpeed;
    public float maxSpeed = 30f;
    public float acceleration = 2f;
    public float driverPower = 1f;

    public bool isRacing = false;
    public bool isPlayer = false;

    public float distanceTraveled = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isRacing) {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > maxSpeed) {
                currentSpeed = maxSpeed;
            }
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            distanceTraveled += currentSpeed * Time.deltaTime;
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

        acceleration = 5 + flatAcceleration;
        maxSpeed = 30 + flatSpeed;
        driverPower = 1 + flatDriverPower;

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
