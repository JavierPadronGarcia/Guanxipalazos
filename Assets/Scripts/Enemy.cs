using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    private int currentHealth;

    Animator anim;
    public Transform target;
    GameObject[] players;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        players = GameObject.FindGameObjectsWithTag("Player");
        FindClosestPlayer();
    }

    void Update()
    {
        FindClosestPlayer();

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            bool playerToTheRight = target.position.x > transform.position.x;
            transform.localScale = new Vector2(playerToTheRight ? 1 : -1, 1);
        }
    }

    void FindClosestPlayer()
    {
        Transform closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.transform;
            }
        }

        target = closestPlayer;
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
