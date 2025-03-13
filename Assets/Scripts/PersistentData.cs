using UnityEngine;

public static class PersistentData
{
    public static int playerMoney;
    public static int round;
    public enum GameState { MainMenu, InGame, GameOver };
    public static GameState gameState = GameState.MainMenu;
    public enum CarType {AMERICAN, MEXICAN, BRITISH, JAPANESE, GERMAN, PREHISTORIC};
    public static CarType playerCarType;
    

}
