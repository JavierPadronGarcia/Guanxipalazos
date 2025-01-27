
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Coins = 0;

    public static bool[] player1ShowedGuns = new bool[4];
    public static bool[] player2ShowedGuns = new bool[4];

    private void Awake()
    {
        player1ShowedGuns[0] = true;
        player2ShowedGuns[0] = true;

        for (int i = 1; i < 4; i++)
        {
            player1ShowedGuns[i] = false;
            player2ShowedGuns[i] = false;
        }

        DontDestroyOnLoad(gameObject);
    }
}
