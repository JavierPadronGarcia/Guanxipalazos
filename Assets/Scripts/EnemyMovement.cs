using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float velocity = 3f;
    public float targetDistance = 5f;
    public float rangeDistance = 10f;

    public enum EnemyType
    {
        Melee,
        Ranged
    }

    public EnemyType enemyType;

    private Transform player;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("El script Enemy no est� asignado al enemigo.");
            return;
        }

        player = enemy.target;

        if (enemyType == 0)
        {
            enemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        }
    }

    private void Update()
    {
        if (player == null) return;

        switch (enemyType)
        {
            case EnemyType.Melee:
                MoveToPlayer();
                break;
            case EnemyType.Ranged:
                HandleRangedBehavior();
                break;
            default:
                break;
        }
    }

    private void HandleRangedBehavior()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > rangeDistance)
        {
            MoveToPlayer();
        }
        else if (distance < targetDistance)
        {
            MoveAway();
        }
    }

    private void MoveToPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * velocity * Time.deltaTime;
    }

    private void MoveAway()
    {
        Vector3 directionAway = (transform.position - player.position).normalized;
        transform.position += directionAway * velocity * Time.deltaTime;
    }
}
