using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float generationTime = 3f;
    public float generationDistance = 15f;

    private Transform player;

    void Start()
    {
        //Se obtiene la posici�n del jugador e invocamos constantemente la funci�n que genera enemigos
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("GenerateEnemy", 0f, generationTime);
    }

    void GenerateEnemy()
    {
        //Obtenemos la posici�n de generaci�n de forma aleatoria entre el rango del mapa generado
        //y posteriormente instanciamos el prefab del enemigo
        Vector3 generationPosition = new Vector3(
            Random.Range(-17, 17),
            Random.Range(-9, 9),
            0f
        );

        Instantiate(enemyPrefab, generationPosition, Quaternion.identity);
    }
}
