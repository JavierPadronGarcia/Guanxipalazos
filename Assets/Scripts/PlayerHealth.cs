using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;

    private Image PF;
    public Sprite Face1, Face2, Face3;

    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D =GetComponent<BoxCollider2D>();
        maxHealth = health;
        PF = GameObject.FindGameObjectWithTag("Face").GetComponent<Image>();
    }

    private void Update()
    {
        CheckHealthAndChangeImage();
    }


    private void CheckHealthAndChangeImage()
    {
        if (health > 70)
        {
            PF.sprite = Face1;
        }
        else if (health < 70 && health > 30)
        {
            PF.sprite = Face2;
        }
        else if (health < 30)
        {
            PF.sprite = Face3;
        }

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
        if (boxCollider2D.IsTouching(collision) && collision.CompareTag("Heal"))
        {
            HealItemController healItemControler = collision.GetComponent<HealItemController>();
            Debug.Log(healItemControler);
            RestoreHealth(healItemControler.heal);

            Debug.Log("Vida restaurada: " + health);

            Destroy(collision.gameObject);
        }
    }
}
