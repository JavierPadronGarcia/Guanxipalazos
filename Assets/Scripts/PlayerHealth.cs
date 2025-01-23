using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;

    public void Hit(float damage)
    {
        health -= damage;

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
}
