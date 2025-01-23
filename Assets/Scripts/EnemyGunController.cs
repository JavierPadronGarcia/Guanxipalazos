using System.Collections;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    [SerializeField] GameObject muzzle;
    [SerializeField] Transform muzzlePosition;
    [SerializeField] GameObject projectile;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float fireDistance = 10f;
    [SerializeField] int gunDamage = 10;
    [SerializeField] Vector2 offset;
    [SerializeField] Animator anim;
    [SerializeField] float raycastLength = 1.5f;
    [SerializeField] bool isMelee = false;
    [SerializeField] Transform meleeRaycastOrigin;

    [SerializeField] float timeSinceLastShot = 0f;
    [SerializeField] Transform parentPosition;
    [SerializeField] Transform closestPlayer;

    private void Start()
    {
        timeSinceLastShot = fireRate;
        parentPosition = transform.parent;
    }

    private void Update()
    {
        transform.position = (Vector2)parentPosition.position + offset;
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
        anim.SetTrigger("Shoot");

        //ranged
        if (!isMelee && projectile != null && muzzlePosition != null)
        {
            var muzzleGo = Instantiate(muzzle, muzzlePosition.position, transform.rotation);
            muzzleGo.transform.SetParent(transform);
            Destroy(muzzleGo, 0.5f);

            var projectileGo = Instantiate(projectile, muzzlePosition.position, transform.rotation);
            Destroy(projectileGo, 3);

            Vector2 direction = (closestPlayer.position - transform.position).normalized;
            GameObject bullet = Instantiate(projectile, muzzlePosition.position, Quaternion.identity);

            // Aplicamos velocidad a la bala
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * 10f;
            }
            return;
        }

        //melee
        LayerMask playerLayerMask = LayerMask.GetMask("PlayerLayer");
        RaycastHit2D hit = Physics2D.Raycast(meleeRaycastOrigin.position, transform.right, raycastLength, playerLayerMask);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("ColisionConJugador");
            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Hit(gunDamage);
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
}
