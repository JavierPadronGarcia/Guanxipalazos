using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public string selectionMenuScene = "SelectionMenu";

    public GameObject enemyPrefab; // Prefab de los enemigos
    public Vector2 spawnArea = new Vector2(10f, 10f); // Área de spawn para enemigos
    public int initialEnemiesPerWave = 5; // Enemigos iniciales por oleada
    public float spawnInterval = 2f; // Intervalo entre spawns
    public float waveDuration = 30f; // Duración de cada oleada
    public int enemiesIncrementPerWave = 2; // Incremento de enemigos por oleada

    private bool isMenuActive = false; // Indica si el menú está cargado
    private int currentWave = 0; // Número de la oleada actual
    private int enemiesSpawned; // Contador de enemigos spawneados
    private bool waveActive; // Indica si la oleada está activa
    private PerlinSpawner perlinHealsSpawner;

    private void Awake()
    {
        perlinHealsSpawner = GetComponent<PerlinSpawner>();
    }

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !isMenuActive)
        {
            EndWave();
        }
    }

    public void EndWave()
    {
        waveActive = false;
        StopAllCoroutines();
        GameObject[] enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] remainingBullets = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] remainingHeals = GameObject.FindGameObjectsWithTag("Heal");

        foreach (var enemy in enemiesAlive) Destroy(enemy);
        foreach (var bullet in remainingBullets) Destroy(bullet);
        foreach (var heal in remainingHeals) Destroy(heal);

        Time.timeScale = 0f;
        SceneManager.LoadScene(selectionMenuScene, LoadSceneMode.Additive);
        isMenuActive = true;
    }

    public void ContinueGame()
    {
        SceneManager.UnloadSceneAsync(selectionMenuScene);
        isMenuActive = false;

        Time.timeScale = 1f;

        StartNextWave();
    }

    private void StartNextWave()
    {
        currentWave++;
        enemiesSpawned = 0;
        waveActive = true;

        Debug.Log($"Iniciando oleada {currentWave}");
        StartCoroutine(WaveTimer());
        StartCoroutine(SpawnEnemies());
        perlinHealsSpawner.StartNewWave();
    }

    private IEnumerator WaveTimer()
    {
        yield return new WaitForSeconds(waveDuration);

        if (waveActive)
        {
            Debug.Log($"Tiempo de la oleada {currentWave} agotado.");
            EndWave();
        }

    }

    private IEnumerator SpawnEnemies()
    {
        int enemiesPerWave = initialEnemiesPerWave + (currentWave - 1) * enemiesIncrementPerWave;

        while (enemiesSpawned < enemiesPerWave && waveActive)
        {
            SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        float x = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
        float y = Random.Range(-spawnArea.y / 2, spawnArea.y / 2);
        Vector2 spawnPosition = new Vector2(x, y);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
