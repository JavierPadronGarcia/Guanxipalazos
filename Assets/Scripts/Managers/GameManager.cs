using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int Coins = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CollectCoin()
    {
        Coins++;
    }

    public void BuyItem(int quantity)
    {
        if (Coins! < quantity)
        {
            Coins -= quantity;
        }
    }
}
