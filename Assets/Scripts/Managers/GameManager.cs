using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Coins = 0;
    public static bool isPlayer1Turn = false;
    public static bool isMultiplayer = false;
    public static int selectedPlayer;
    public static int playerCount = 0;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
