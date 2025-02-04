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
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] GameObject coinPrefab;
    public List<EnemyTypeGunsDictionary> enemyGunTypesEntries = new List<EnemyTypeGunsDictionary>();

    private Dictionary<EnemyMovement.EnemyType, GameObject> enemyGunTypesDictionary;
    private int currentHealth;

    public Transform target;
    public ParticleSystem hitParticles;
    private GameObject[] players;
    public bool isBoss;

    public delegate void EnemyDeathEvent(Enemy enemy);
    public event EnemyDeathEvent OnDeath;

    private Animator anim;
    private EnemyMovement enemyMovementScript;

    private void Awake()
    {
        // Inicializar el diccionario
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

        // Inicializar el script EnemyMovement
        enemyMovementScript = GetComponent<EnemyMovement>();
        enemyMovementScript.enemyType = (EnemyMovement.EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyMovement.EnemyType)).Length);
        if (enemyMovementScript == null)
        {
            Debug.LogError("No se encontró el componente EnemyMovement en el GameObject.");
            return;
        }

        // Activar el arma correspondiente
        ActivateWeapon();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        players = GameObject.FindGameObjectsWithTag("Player");
        FindClosestPlayer();
    }

    private void Update()
    {
        FindClosestPlayer();

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();



            if (isBoss)
            {
                bool playerToTheRight = target.position.x > transform.position.x;
                spriteRenderer.flipX = playerToTheRight;
            }
            else
            {
                bool playerToTheRight = target.position.x > transform.position.x;
                spriteRenderer.flipX = !playerToTheRight;
            }
        }
    }

    private void FindClosestPlayer()
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
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

    private void ActivateWeapon()
    {
        if (enemyGunTypesDictionary.TryGetValue(enemyMovementScript.enemyType, out GameObject weapon))
        {
            weapon.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"No se encontró un arma para el tipo de enemigo: {enemyMovementScript.enemyType}");
        }
    }
}
