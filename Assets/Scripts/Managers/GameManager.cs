
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Coins = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
