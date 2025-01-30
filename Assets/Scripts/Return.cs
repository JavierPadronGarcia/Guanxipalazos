using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    public void backToLibrary()
    {
        SceneManager.LoadScene("Library");
    }
    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
