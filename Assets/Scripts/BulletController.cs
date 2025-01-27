using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int bulletDamage = 0;
    public string spawnedBy = "";
    public bool rotatingBullet = false;
    public float rotationSpeed = 360f;

    private void Update()
    {
        if (rotatingBullet)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

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
