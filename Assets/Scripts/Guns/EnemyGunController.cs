using System.Collections;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    [Header("Opciones generales")]
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float fireDistance = 10f;
    [SerializeField] int gunDamage = 10;
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem shootParticles;

    [Header("Arma a distancia")]
    [SerializeField] Transform muzzlePosition;
    [SerializeField] GameObject projectile;

    [Header("Arma melee")]
    [SerializeField] bool isMelee = false;
    [SerializeField] Transform meleeRaycastOrigin;
    [SerializeField] float raycastLength = 1.5f;

    [Header("Arma Boss")]
    [SerializeField] bool isBoss = false;
    
    

    private float timeSinceLastShot = 0f;
    private Transform parentPosition;
    private Transform closestPlayer;

    private void Start()
    {
        timeSinceLastShot = fireRate;
        parentPosition = transform.parent;
    }

    private void Update()
    {
        transform.position = (Vector2)parentPosition.position;
        FindClosestPlayer();
        AimAtPlayer();
        Shooting();
    }

    private void FindClosestPlayer()
    {
        closestPlayer = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float shortestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= fireDistance && distance < shortestDistance)
            {
                closestPlayer = player.transform;
                shortestDistance = distance;
            }
        }
    }

    private void AimAtPlayer()
    {
        if (closestPlayer != null)
        {
            Vector3 direction = closestPlayer.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void Shooting()
    {
        if (closestPlayer == null) return;

        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= fireRate)
        {
            Shoot();
            timeSinceLastShot = 0;
        }
    }

    private void Shoot()
    {

        if (anim != null) 
        {
            anim.SetTrigger("Shoot");
        }

        if (!isMelee && projectile != null && muzzlePosition != null)
        {
            AudioManager.instance.PlaySFX("Shoot");
            if (!shootParticles.isPlaying) shootParticles.Play();

            Vector2 direction = (closestPlayer.position - muzzlePosition.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            var projectileGo = Instantiate(projectile, muzzlePosition.position, rotation);

            BulletController bulletController = projectileGo.GetComponent<BulletController>();
            bulletController.bulletDamage = gunDamage;
            bulletController.spawnedBy = "Enemy";

            Rigidbody2D rb = projectileGo.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * 10f;
            }

            Destroy(projectileGo, 3);
        }

        if (!isBoss && isMelee)
        {
            AudioManager.instance.PlaySFX("EnemyStab");
            LayerMask playerLayerMask = LayerMask.GetMask("PlayerLayer");
            RaycastHit2D hit = Physics2D.Raycast(meleeRaycastOrigin.position, transform.right, raycastLength, playerLayerMask);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Hit(gunDamage);
                }
            }
        }
        if (isBoss && isMelee)
        {
            
            LayerMask playerLayerMask = LayerMask.GetMask("PlayerLayer");
            RaycastHit2D hit = Physics2D.Raycast(meleeRaycastOrigin.position, transform.right, raycastLength, playerLayerMask);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Hit(gunDamage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Transform raycastOrigin = isMelee ? meleeRaycastOrigin : muzzlePosition;
        if (raycastOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + transform.right * raycastLength);
        }
    }

    public Transform GetClosestPlayer()
    {
        return closestPlayer;
    }
}
