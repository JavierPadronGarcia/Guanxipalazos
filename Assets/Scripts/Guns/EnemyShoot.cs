using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float generationTime = 2f;
    public float velocity = 5f;
    private EnemyMovement.EnemyType enemyType;
    private Transform player;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("El script Enemy no está asignado al enemigo.");
            return;
        }

        player = enemy.target;
        enemyType = GetComponent<EnemyMovement>().enemyType;

        if (enemyType == EnemyMovement.EnemyType.Ranged)
        {
            InvokeRepeating(nameof(Shoot), 0f, generationTime);
        }
    }

    private void Shoot()
    {
        if (player == null || enemy == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * velocity;
        }
    }
}