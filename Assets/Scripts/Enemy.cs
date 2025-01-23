using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyTypeGunsDictionary
{
    public EnemyMovement.EnemyType key;
    public GameObject value;
}

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] SpriteRenderer spriteRenderer;
    public List<EnemyTypeGunsDictionary> enemyGunTypesEntries = new List<EnemyTypeGunsDictionary>();

    private Dictionary<EnemyMovement.EnemyType, GameObject> enemyGunTypesDictionary;

    private int currentHealth;

    Animator anim;
    public Transform target;
    GameObject[] players;

    public delegate void EnemyDeathEvent(Enemy enemy);
    public event EnemyDeathEvent OnDeath;

    private void Awake()
    {
        enemyGunTypesDictionary = new Dictionary<EnemyMovement.EnemyType, GameObject>();
        foreach (var entry in enemyGunTypesEntries)
        {
            if (!enemyGunTypesDictionary.ContainsKey(entry.key))
            {
                enemyGunTypesDictionary.Add(entry.key, entry.value);
            }
            else
            {
                Debug.LogWarning($"Clave duplicada encontrada: {entry.key}");
            }
        }
    }

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
            spriteRenderer.flipX = !playerToTheRight;
        }
    }

    void FindClosestPlayer()
    {
        Transform closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            if (player)
            {
                float distance = Vector2.Distance(transform.position, player.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = player.transform;
                }
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
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
