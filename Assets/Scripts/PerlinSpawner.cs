using System.Collections;
using UnityEngine;

public class PerlinSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Vector2 areaSize;
    public float density = 0.1f;
    public int spawnCount;
    public int totalHealsPerWave = 10;
    public float spawnInterval = 5f;

    private float seedX;
    private float seedY;
    private int spawnedCount;

    private void Start()
    {
        seedX = Random.Range(0f, 100f);
        seedY = Random.Range(0f, 100f);
        StartNewWave();
    }

    public void StartNewWave()
    {
        spawnedCount = 0;
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (spawnedCount < totalHealsPerWave)
        {
            float x = Mathf.PerlinNoise(seedX + spawnedCount * density, seedY) * areaSize.x - areaSize.x / 2;
            float y = Mathf.PerlinNoise(seedY + spawnedCount * density, seedX) * areaSize.y - areaSize.y / 2;

            Vector2 spawnPosition = new Vector2(x, y);

            Instantiate(prefab, spawnPosition, Quaternion.identity);

            spawnedCount++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
