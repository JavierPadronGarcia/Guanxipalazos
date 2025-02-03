using UnityEngine;

public class CanvasController : MonoBehaviour
{
    
    public void StartGame()
    {
        SCManager.instance.LoadScene("MainGame");
    }

    public void MainMenu()
    {
        SCManager.instance.LoadScene("MainMenu");
    }

    public void NavigateLibrary()
    {
        SCManager.instance.LoadScene("Library");
    }

    public void NavigateCredits()
    {
        SCManager.instance.LoadScene("Credits");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
