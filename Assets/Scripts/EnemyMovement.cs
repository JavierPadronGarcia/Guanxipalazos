using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float velocity = 3f;
    public float targetDistance = 5f;
    public float rangeDistance = 10f;

    public enum EnemyType { Melee, Ranged }
    public EnemyType enemyType;
    private Transform player;
    private Enemy enemy;
    private Animator animator;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("El script Enemy no está asignado al enemigo.");
            return;
        }

        player = enemy.target;
        animator = GetComponent<Animator>();

        if (enemyType == EnemyType.Melee)
        {
            SetRunAnimation();
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
        }
    }

    private void HandleRangedBehavior()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > rangeDistance)
        {
            SetRunAnimation();
            MoveToPlayer();
        }
        else if (distance < targetDistance)
        {
            SetRunAnimation();
            MoveAway();
        }
        else
        {
            SetIdleAnimation();
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

    private void SetIdleAnimation()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Idle", true);
    }

    private void SetRunAnimation()
    {
        animator.SetBool("Run", true);
        animator.SetBool("Idle", false);
    }
}