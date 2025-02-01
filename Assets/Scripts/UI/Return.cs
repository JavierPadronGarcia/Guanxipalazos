using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Return : MonoBehaviour
{
    public GameObject popupPanel; 
    public Text popupText; 
    public void backToLibrary()
    {
        SceneManager.LoadScene("Library");
    }
    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LaunchVuforia()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length > 1)
        {
            Debug.Log("Webcam detected: " + devices[0].name);
            SceneManager.LoadScene("Vuforia"); // Load Vuforia scene
        }
        else
        {
            Debug.Log("No webcam detected.");
            ShowPopup();
        }
    }

    public void ShowPopup()
    {
        popupPanel.SetActive(true);
    }
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}
