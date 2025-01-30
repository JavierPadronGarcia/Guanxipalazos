using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] GameObject player1Text;
    [SerializeField] GameObject player2Text;
    [SerializeField] GameObject player1Healthbar;
    [SerializeField] GameObject player2Healthbar;
    [SerializeField] GameObject RoundManager;
    [SerializeField] GameObject CoinGroup;

    private bool gameStarted = false;

    private void Update()
    {
        if (!gameStarted && GameManager.playerCount > 0 && (Keyboard.current.enterKey.wasPressedThisFrame || Gamepad.all.Count > 0 && Gamepad.all[0].startButton.wasPressedThisFrame))
        {
            StartGame();
        }
    }

    public void PlayerSpawned()
    {
        GameManager.playerCount++;

        if (GameManager.playerCount == 1)
        {
            player1Text.SetActive(false);
            PlayerHealth player1H = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerHealth>();
            player1Healthbar.SetActive(true);
            player1Healthbar.transform.GetComponentInChildren<HPBar>().healthScript = player1H;
            player1H.PF = FindChildWithTag(player1Healthbar.transform, "Face").GetComponent<Image>();
            player1H.healthActive = true;
        }
        else if (GameManager.playerCount == 2)
        {
            player2Text.SetActive(false);
            PlayerHealth player2H = GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<PlayerHealth>();
            player2Healthbar.transform.GetComponentInChildren<HPBar>().healthScript = player2H;
            player2Healthbar.SetActive(true);
            player2H.PF = FindChildWithTag(player2Healthbar.transform, "Face").GetComponent<Image>();
            player2H.healthActive = true;
        }

    }
    GameObject FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null;
    }

    private void StartGame()
    {
        gameStarted = true;
        RoundManager.SetActive(true);
        CoinGroup.SetActive(true);
    }

}