using UnityEngine;
using UnityEngine.EventSystems;

public class DeathScreen : MonoBehaviour
{
    public GameObject mainMenuButton;

    void Start()
    {
        Time.timeScale = 0f;
        GameManager.gamePaused = true;
        EventSystem.current.SetSelectedGameObject(mainMenuButton);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.ResetGame();
        SCManager.instance.LoadScene("MainMenu");
    }
}
