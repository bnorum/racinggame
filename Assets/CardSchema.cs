using UnityEngine;


[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/CardSchema", order = 1)]
public class CardSchema : ScriptableObject
{
    public string cardEffectName;
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
    public enum CardBrand {HORSE, GOAT, CHEETAH, GAZELLE}
    public CardBrand cardBrand;
    public enum CardRarity {COMMON, UNCOMMON, RARE, EPIC, LEGENDARY}
    public CardRarity cardRarity;

    public float cardQuantityValue;

}
