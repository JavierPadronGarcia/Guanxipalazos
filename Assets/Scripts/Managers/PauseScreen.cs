using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public string sceneToLoad = "PauseMenu"; // Name of the pause scene
    private bool gameStarted = false, isSceneLoaded = false;

    void Update()
    {
        if (!gameStarted && GameManager.playerCount > 0 && (Keyboard.current.escapeKey.wasPressedThisFrame ||
            Gamepad.all.Count > 0 && Gamepad.all[0].startButton.wasPressedThisFrame))
        {
            if (isSceneLoaded)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        SCManager.instance.LoadSceneAdd(sceneToLoad);
        isSceneLoaded = true; // Mark scene as loaded
        Debug.Log("Pause Screen Loaded");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        StartCoroutine(UnloadPauseScene()); // Call async unload
        Debug.Log("Pause Screen Unloaded");
    }

    private System.Collections.IEnumerator UnloadPauseScene()
    {
        yield return SceneManager.UnloadSceneAsync(sceneToLoad);
        isSceneLoaded = false; // Mark scene as unloaded
    }
}
