using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScreen : MonoBehaviour
{
    public GameObject FirstSelectedButton;

    private void Start()
    {
        Time.timeScale = 0f;
        GameManager.gamePaused = true;
        EventSystem.current.SetSelectedGameObject(FirstSelectedButton);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        GameManager.gamePaused = false;
        SCManager.instance.UnloadScene("PauseMenu");
    }

    public void ExitMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.gamePaused = false;
        GameManager.gameStarted = false;
        GameManager.playerCount = 0;
        GameManager.isMultiplayer = false;
        GameManager.isPlayer1Turn = false;
        GameManager.selectedPlayer = 0;
        GameManager.Coins = 0;
        SCManager.instance.LoadScene("MainMenu");
    }
}
