using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] GameObject player1Text;
    [SerializeField] GameObject player2Text;
    [SerializeField] GameObject playerTipText;
    [SerializeField] GameObject player1Healthbar;
    [SerializeField] GameObject player2Healthbar;

    [Header("Others")]
    [SerializeField] GameObject RoundManager;
    [SerializeField] GameObject CoinGroup;
    [SerializeField] GameObject RoundHelpers;

    private void Update()
    {
        bool enterAndStartButtons = GameManager.playerCount > 0 && (Keyboard.current.enterKey.wasPressedThisFrame || Gamepad.all.Count > 0 && Gamepad.all[0].startButton.wasPressedThisFrame);
        bool escapeAndStartButtons = GameManager.playerCount > 0 && (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.all.Count > 0 && Gamepad.all[0].startButton.wasPressedThisFrame);

        if (!GameManager.gameStarted && enterAndStartButtons)
        {
            StartGame();
        }
        else if (GameManager.gameStarted && !GameManager.gamePaused && escapeAndStartButtons)
        {
            PauseGame();
        }
    }

    public void PlayerSpawned()
    {
        GameManager.playerCount++;

        if (GameManager.playerCount == 1)
        {
            playerTipText.SetActive(true);
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

    public void PlayerDespawned()
    {
        GameManager.playerCount--;
        if (GameManager.gameStarted && GameManager.playerCount <= 0)
        {
            SCManager.instance.LoadSceneAdd("DeathScreen");
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
        playerTipText.SetActive(false);
        GameManager.gameStarted = true;
        RoundManager.SetActive(true);
        CoinGroup.SetActive(true);
        RoundHelpers.SetActive(true);

        PlayerInputManager.instance.DisableJoining();
    }

    public void PauseGame()
    {
        SCManager.instance.LoadSceneAdd("PauseMenu");
    }
}