using UnityEngine;
using static UnityEngine.CompositeCollider2D;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject player;
    public Transform playerPosition;
    public float generationTime;
    public float velocity;
    private EnemyMovement.EnemyType enemyType;

    void Start()
    {
        //obtenemos el gameObject del jugador además del tipo de enemigo
        player = GameObject.FindGameObjectWithTag("Player");
        enemyType = GetComponent<EnemyMovement>().enemyType;

        //Si es de tipo Ranged hacemos que dispare constantemente
        if (enemyType == EnemyMovement.EnemyType.Ranged)
        {
            InvokeRepeating("Shoot", 0f, generationTime);
        }
    }

    private void Shoot()
    {
        //Obtenemos la dirección entre el jugador y el enemigo e instanciamos la bala
        Vector2 direction = (player.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        //La bala instanciada se impulsa hacia el jugador
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * velocity;
        }
    }
}
