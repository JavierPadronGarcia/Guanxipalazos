using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public ParticleSystem runParticles;
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
        if (!runParticles.isPlaying) runParticles.Play();

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += Time.deltaTime * velocity * direction;
    }

    private void MoveAway()
    {
        if (!runParticles.isPlaying) runParticles.Play();
        Vector3 directionAway = (transform.position - player.position).normalized;
        transform.position += Time.deltaTime * (velocity/2) * directionAway;
    }

    private void SetIdleAnimation()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Idle", true);
        if (runParticles.isPlaying) runParticles.Stop();
    }

    private void SetRunAnimation()
    {
        animator.SetBool("Run", true);
        animator.SetBool("Idle", false);
    }
}