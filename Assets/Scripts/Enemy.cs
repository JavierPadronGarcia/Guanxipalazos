using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    private int currentHealth;

    Animator anim;
    public Transform target;
    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            var playerToTheRight = target.position.x > transform.position.x;
            transform.localScale = new Vector2(playerToTheRight ? 1 : -1, 1);
        }
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}