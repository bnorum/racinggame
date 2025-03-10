using UnityEngine;


[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CardSchema", order = 1)]
public class CardSchema : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    public Sprite cardImage;

    public int price;

    public enum CardType {ANY, ENGINE, TRANSMISSION, MIRROR, DASH, WHEEL}
    public CardType cardType;

    public enum CardQuantityModified {SPEED, DRIVERPOWER, ACCELERATION}
    public CardQuantityModified cardQuantityModified;
    public enum CardQuantityType {ADD, MULTIPLY}
    public CardQuantityType cardQuantityType;

    public float cardQuantityValue;
}
