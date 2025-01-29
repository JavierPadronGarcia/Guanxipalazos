using UnityEngine;

public class EndRoundSceneCanvasController : MonoBehaviour
{
    private WaveManager waveManager;
    private GameObject[] players;

    private void Awake()
    {
        waveManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveManager>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void ContinueNextRound()
    {
        waveManager.ContinueGame();
    }

    public void HealPlayer(int healQuantity, int money)
    {
        if (waveManager.GetPlayer1Turn())
        {
            players[0].GetComponent<PlayerHealth>().RestoreHealth(healQuantity);
        }
        else
        {
            players[1].GetComponent<PlayerHealth>().RestoreHealth(healQuantity);
        }
        GameManager.Coins -= money;
    }

    public void BuyGun(string gunName, int money)
    {
        if (waveManager.GetPlayer1Turn())
        {
            players[0].GetComponent<PlayerGunItemsController>().ActivateGun(gunName);
        }
        else
        {
            players[1].GetComponent<PlayerGunItemsController>().ActivateGun(gunName);
        }
        GameManager.Coins -= money;
    }

    public void ImproveGun(ArmaMejoras gunImproves, string gunName, int money)
    {
        if (waveManager.GetPlayer1Turn())
        {
            players[0].GetComponent<PlayerGunItemsController>().UpgradeGun(gunName, gunImproves);
        }
        else
        {
            players[1].GetComponent<PlayerGunItemsController>().UpgradeGun(gunName, gunImproves);
        }
        GameManager.Coins -= money;
    }
}
