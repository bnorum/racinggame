using UnityEngine;

public static class PersistentData
{
    public static int playerMoney;
    public static int round;
    public static int raceLength;
    public static int raceTime;
    public static int raceReward;
    public enum GameState { MainMenu, InGame, GameOver };
    public static GameState gameState = GameState.MainMenu;
    public enum CarType {AMERICAN, MEXICAN, SOUTHAFRICAN, JAPANESE, GERMAN, PREHISTORIC};
    public static CarType playerCarType;

    public static float calculationDelay = 0.2f;
    public static float chanceModifier = 0f;

}
