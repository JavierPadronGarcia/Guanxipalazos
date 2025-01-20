using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float generationTime = 2f;
    public float velocity = 5f;
    private EnemyMovement.EnemyType enemyType;
    private Transform player; // Referencia al jugador

    private Enemy enemy; // Referencia al script Enemy

    void Start()
    {
        // Obtenemos el componente Enemy para reutilizar sus datos
        enemy = GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("El script Enemy no est� asignado al enemigo.");
            return;
        }

        player = enemy.target;

        // Obtenemos el tipo de enemigo
        enemyType = GetComponent<EnemyMovement>().enemyType;

        if (enemyType == EnemyMovement.EnemyType.Ranged)
        {
            InvokeRepeating("Shoot", 0f, generationTime);
        }
    }

    private void Shoot()
    {
        if (player == null || enemy == null) return;

        // Instanciamos la bala y le damos direccion hacia el jugador
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Aplicamos velocidad a la bala
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * velocity;
        }
    }
}
