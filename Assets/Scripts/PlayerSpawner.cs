using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject jugador1Prefab;
    public GameObject jugador2Prefab;
    public Transform spawnPoint; // Punto donde aparecerá el personaje

    void Awake()
    {
        int personajeSeleccionado = PlayerPrefs.GetInt("PersonajeSeleccionado", 1);

        // Elegir el prefab correcto
        GameObject prefabSeleccionado = (personajeSeleccionado == 1) ? jugador1Prefab : jugador2Prefab;
        HPBar HP1 = GameObject.FindGameObjectWithTag("HPBar1").GetComponent<HPBar>();
        //HPBar HP2 = GameObject.FindGameObjectWithTag("HPBar2").GetComponent<HPBar>();

        // Instanciar el personaje en la escena 2D
        GameObject playerSelected =  Instantiate(prefabSeleccionado, spawnPoint.position, Quaternion.identity);

        PlayerHealth playerHealth = playerSelected.GetComponent<PlayerHealth>();

        HP1.healthScript = playerHealth;
    }
}
