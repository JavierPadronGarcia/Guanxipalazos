using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;

    private void Start()
    {
        maxHealth = health;
    }

    public void Hit(float damage)
    {
        health -= damage;
        Debug.Log("Daño recibido: " + health);

        if (health <= 0)
        {
            PlayerDead();
        }
    }

    public void PlayerDead()
    {
        Debug.Log("Jugador Muerto");
        Destroy(gameObject);
    }

    public void RestoreHealth(float heal = 0)
    {
        Debug.Log("Restaurando vida");
        if (heal == 0 || (health + heal) > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += heal;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heal"))
        {
            HealItemController healItemControler = collision.GetComponent<HealItemController>();
            Debug.Log(healItemControler);
            RestoreHealth(healItemControler.heal);

            Debug.Log("Vida restaurada: " + health);

            Destroy(collision.gameObject);
        }
    }
}
