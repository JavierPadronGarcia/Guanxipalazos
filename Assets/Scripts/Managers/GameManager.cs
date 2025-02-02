using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Coins = 0;
    public static bool isPlayer1Turn = false;
    public static bool isMultiplayer = false;
    public static int selectedPlayer;
    public static int playerCount = 0;
    public static bool gamePaused = false;
    public static bool gameStarted = false;

    public static void ResetGame()
    {
        Coins = 0;
        isPlayer1Turn = false;
        isMultiplayer = false;
        selectedPlayer = 0;
        playerCount = 0;
        gamePaused = false;
        gameStarted = false;
    }
}
