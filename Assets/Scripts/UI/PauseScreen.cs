using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScreen : MonoBehaviour
{
    public GameObject FirstSelectedButton;

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            player.GetComponent<PlayerMovement>().StopRunning();
        }
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
        GameManager.ResetGame();
        SCManager.instance.LoadScene("MainMenu");
    }
}
