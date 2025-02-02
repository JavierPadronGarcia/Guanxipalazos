using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;

    public Image PF;

    public bool healthActive = false;

    [Header("Caras Ayose")]
    public Sprite AFace1;
    public Sprite AFace2;
    public Sprite AFace3;

    [Header("Caras Guanchita")]
    public Sprite GFace1;
    public Sprite GFace2;
    public Sprite GFace3;

    private BoxCollider2D boxCollider2D;

    private int playerNumber;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        maxHealth = health;
        playerNumber = GameManager.playerCount;
    }

    private void Update()
    {
        if (healthActive == true) CheckHealthAndChangeImage();
    }


    private void CheckHealthAndChangeImage()
    {
        if (health > 70)
        {
            PF.sprite = playerNumber == 1 ? AFace1 : GFace1;
        }
        else if (health < 70 && health > 30)
        {
            PF.sprite = playerNumber == 1 ? AFace2 : GFace2;
        }
        else if (health < 30)
        {
            PF.sprite = playerNumber == 1 ? AFace3 : GFace3;
        }

    }
    public void Hit(float damage)
    {
        health -= damage;
        Debug.Log("Daño recibido: " + health);

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void goBack()
    {
        SCManager.instance.LoadScene("MainMenu");
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
            AudioManager.instance.PlaySFX("Drink");
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
