using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public string selectionMenuScene = "SelectionMenu";

    public GameObject enemyPrefab;
    public Vector2 spawnArea = new Vector2(10f, 10f);
    public int initialEnemiesPerWave = 5;
    public float spawnInterval = 2f;
    public float waveDuration = 30f;
    public int enemiesIncrementPerWave = 2;

    private bool isMenuActive = false;
    private int currentWave = 0;
    private int enemiesSpawned;
    private bool waveActive;
    private PerlinSpawner perlinHealsSpawner;

    private void Awake()
    {
        perlinHealsSpawner = GetComponent<PerlinSpawner>();
    }

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1) GameManager.isMultiplayer = true;

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerGunItemsController>().ActivateGun("magado");
        }

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
        perlinHealsSpawner.StopCoroutines();

        GameObject[] enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] remainingBullets = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] remainingHeals = GameObject.FindGameObjectsWithTag("Heal");

        foreach (var enemy in enemiesAlive) Destroy(enemy);
        foreach (var bullet in remainingBullets) Destroy(bullet);
        foreach (var heal in remainingHeals) Destroy(heal);

        TogglePlayerMovement(false);

        SceneManager.LoadScene(selectionMenuScene, LoadSceneMode.Additive);
        isMenuActive = true;
    }

    public void ContinueGame()
    {
        SceneManager.UnloadSceneAsync(selectionMenuScene);
        isMenuActive = false;

        TogglePlayerMovement(true);

        StartNextWave();
    }

    private void StartNextWave()
    {
        currentWave++;
        enemiesSpawned = 0;
        if (GameManager.isMultiplayer) GameManager.isPlayer1Turn = !GameManager.isPlayer1Turn;
        else GameManager.isPlayer1Turn = true;

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

    private void TogglePlayerMovement(bool enable)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {

            if (!enable)
            {
                var rigidbody2D = player.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null)
                {
                    rigidbody2D.linearVelocity = Vector2.zero;
                    rigidbody2D.angularVelocity = 0f;
                }
            }

            var movementScript = player.GetComponent<PlayerMovement>();
            if (movementScript != null)
            {
                movementScript.enabled = enable;
            }
        }
    }
}
