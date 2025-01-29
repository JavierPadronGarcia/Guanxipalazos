using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{
    public void SeleccionarJugador(int personajeID)
    {
        PlayerPrefs.SetInt("PersonajeSeleccionado", personajeID);
        PlayerPrefs.Save();

        // Cargar la escena del nivel
        SceneManager.LoadScene("Level1");
    }
}
