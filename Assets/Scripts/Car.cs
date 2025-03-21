using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    public List<Card> cards;
    public float currentSpeed;
    public float maxSpeed = 30f;
    public float baseMaxSpeed = 30f;
    public float acceleration = 5f;
    public float baseAcceleration = 5f;
    public float driverPower = 1f;
    public float baseDriverPower = 1f;


    public GameObject CarModel;



    public enum CarState {
        RACING,
        ATSTART,
        GARAGE
    }
    public CarState carState = CarState.ATSTART;

    public bool isPlayer = false;
    public bool isGerman = false;

    public float distanceTraveled = 0f;

    public GameObject garagePosition;
    public GameObject startLinePosition;
    public bool statsCalculated = true;

    public float revInPlaceTimer = 0f;


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
            if (revInPlaceTimer < 0) {
                transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
                distanceTraveled += currentSpeed * Time.deltaTime;

            }
            revInPlaceTimer -= Time.deltaTime;
        } else if (carState == CarState.GARAGE) {
            transform.position = Vector3.MoveTowards(transform.position, garagePosition.transform.position, currentSpeed * Time.deltaTime);
        } else if (carState == CarState.ATSTART) {
            transform.position = Vector3.MoveTowards(transform.position, startLinePosition.transform.position, currentSpeed * Time.deltaTime);
        }
    }

    public void ResetCar() {
        acceleration = baseAcceleration;
        maxSpeed = baseMaxSpeed;
        driverPower = baseDriverPower;
    }

    public System.Collections.IEnumerator CalculatePlayerStats() {




        statsCalculated = false;

        distanceTraveled = 0f;
        currentSpeed = 0f;
        if (isPlayer) GetCards();
        foreach (Card card in cards) {
            if (!card.isEnabled) continue;
            bool isMultiplier = false;
            if (card.cardSchema.cardQuantityType == CardSchema.CardQuantityType.MULTIPLY) {
                isMultiplier = true;
            } else if (card.cardSchema.cardQuantityType == CardSchema.CardQuantityType.ADD) {
                isMultiplier = false;
            }
            if (!isMultiplier && card.cardSchema.cardQuantityValue != 0){
                switch (card.cardSchema.cardQuantityModified) {
                    case CardSchema.CardQuantityModified.ACCELERATION:

                        acceleration += card.cardSchema.cardQuantityValue;
                        RaceManager.Instance.CreateBonusText(card.cardSchema.cardQuantityValue, 1, RaceManager.Instance.accelerationText.gameObject, card);

                        yield return new WaitForSeconds(PersistentData.calculationDelay);
                        break;
                    case CardSchema.CardQuantityModified.DRIVERPOWER:

                        driverPower += card.cardSchema.cardQuantityValue;
                        RaceManager.Instance.CreateBonusText(card.cardSchema.cardQuantityValue, 1, RaceManager.Instance.driverPowerText.gameObject, card);

                        yield return new WaitForSeconds(PersistentData.calculationDelay);
                        break;
                    case CardSchema.CardQuantityModified.SPEED:

                        maxSpeed += card.cardSchema.cardQuantityValue;
                        RaceManager.Instance.CreateBonusText(card.cardSchema.cardQuantityValue, 1, RaceManager.Instance.speedText.gameObject, card);

                        yield return new WaitForSeconds(PersistentData.calculationDelay);
                        break;
                }
            }
        }

        foreach (Card card in cards) {
            if (!card.isEnabled) continue;
            bool isMultiplier = false;
            if (card.cardSchema.cardQuantityType == CardSchema.CardQuantityType.MULTIPLY) {
                isMultiplier = true;
            } else if (card.cardSchema.cardQuantityType == CardSchema.CardQuantityType.ADD) {
                isMultiplier = false;
            }
            if (isMultiplier && card.cardSchema.cardQuantityValue != 1) {
                switch (card.cardSchema.cardQuantityModified) {
                    case CardSchema.CardQuantityModified.ACCELERATION:
                        if (isMultiplier) {
                            acceleration *= card.cardSchema.cardQuantityValue;
                            RaceManager.Instance.CreateBonusText(card.cardSchema.cardQuantityValue, 2, RaceManager.Instance.accelerationText.gameObject, card);

                        }
                        yield return new WaitForSeconds(PersistentData.calculationDelay);
                        break;
                    case CardSchema.CardQuantityModified.SPEED:
                        if (isMultiplier) {
                            maxSpeed *= card.cardSchema.cardQuantityValue;
                            RaceManager.Instance.CreateBonusText(card.cardSchema.cardQuantityValue, 2, RaceManager.Instance.speedText.gameObject, card);
                        }
                        yield return new WaitForSeconds(PersistentData.calculationDelay);
                        break;
                }

            }
        }


        foreach (Card card in cards) {
            if (!card.isEnabled) continue;
            bool isMultiplier = false;
            if (card.cardSchema.cardQuantityType == CardSchema.CardQuantityType.MULTIPLY) {
                isMultiplier = true;
            } else if (card.cardSchema.cardQuantityType == CardSchema.CardQuantityType.ADD) {
                isMultiplier = false;
            }
            switch (card.cardSchema.cardQuantityModified) {
                case CardSchema.CardQuantityModified.DRIVERPOWER:
                    if (isMultiplier && card.cardSchema.cardQuantityValue > 1) {
                        driverPower *= card.cardSchema.cardQuantityValue;
                        RaceManager.Instance.CreateBonusText(card.cardSchema.cardQuantityValue, 2, RaceManager.Instance.driverPowerText.gameObject, card);
                    }
                    yield return new WaitForSeconds(PersistentData.calculationDelay);
                    break;

            }
        }






        statsCalculated = true;
    }

    public void ApplyDriverPower() {
        acceleration = Mathf.Pow(acceleration, driverPower);
        maxSpeed = Mathf.Pow(maxSpeed, driverPower);
    }



    public void GetCards() {
        cards = EquipPanelManager.Instance.GetCards();
    }

    public System.Collections.IEnumerator ShakeCar() {
        Vector3 originalPosition = transform.position;
        float shakeMagnitude = 0.1f;
        float elapsed = 0.0f;

        while (true) {
            float x = Random.Range(-0.1f, 0.1f) * shakeMagnitude;
            CarModel.transform.position += new Vector3(x, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }


    }

}
