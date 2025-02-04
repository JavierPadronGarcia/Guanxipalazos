using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public string selectionMenuScene = "SelectionMenu";
    public TextMeshProUGUI waveTimerText;
    public TextMeshProUGUI roundCountText;

    public GameObject enemyPrefab;
    public GameObject bossPrefab; // Enemigo especial que aparece cada 5 rondas
    public Vector2 spawnArea = new Vector2(10f, 10f);
    public int initialEnemiesPerWave = 5;
    public float spawnInterval = 2f;
    public float waveDuration = 30f;
    public int enemiesIncrementPerWave = 2;
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

    public void EndWave()
    {
        AudioManager.instance.StopSFX();
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

        SCManager.instance.LoadSceneAdd(selectionMenuScene);
    }

    public void ContinueGame()
    {
        SCManager.instance.UnloadScene(selectionMenuScene);
        EventSystem.current.SetSelectedGameObject(null);

        TogglePlayerMovement(true);

        StartNextWave();
    }

    private void StartNextWave()
    {
        currentWave++;
        roundCountText.text = "Oleada " + currentWave.ToString();
        enemiesSpawned = 0;
        if (GameManager.isMultiplayer) GameManager.isPlayer1Turn = !GameManager.isPlayer1Turn;
        else GameManager.isPlayer1Turn = true;

        waveActive = true;

        Debug.Log($"Iniciando oleada {currentWave}");
        StartCoroutine(WaveTimer());
        StartCoroutine(SpawnEnemies());
        if (currentWave % 5 == 0) 
        {
            SpawnBoss();
        }
        perlinHealsSpawner.StartNewWave();
    }

    private IEnumerator WaveTimer()
    {
        float remainingTime = waveDuration;
        int enemiesPerWave = initialEnemiesPerWave + (currentWave - 1) * enemiesIncrementPerWave;

        while (remainingTime > 0 && waveActive)
        {
            if (waveTimerText != null)
            {
                waveTimerText.text = $"Tiempo: {Mathf.CeilToInt(remainingTime)}s";
            }

            yield return new WaitForSeconds(1f);
            remainingTime--;

            if (enemiesSpawned >= enemiesPerWave && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                remainingTime = Mathf.Min(remainingTime, 5f);
            }
        }

        if (waveTimerText != null)
        {
            waveTimerText.text = "¡Oleada terminada!";
        }

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

    private void SpawnBoss()
    {
        float x = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
        float y = Random.Range(-spawnArea.y / 2, spawnArea.y / 2);
        Vector2 spawnPosition = new Vector2(x, y);

        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("¡Boss ha aparecido en la oleada " + currentWave + "!");
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
