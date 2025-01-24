using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int bulletDamage = 0;
    public string spawnedBy = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si la bala colisiona con la pared, se destruye
        if (collision.CompareTag("Limit"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Player") && spawnedBy == "Enemy")
        {
            collision.gameObject.GetComponent<PlayerHealth>().Hit(bulletDamage);
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Enemy") && spawnedBy == "Player")
        {
            collision.gameObject.GetComponent<Enemy>().Hit(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}
