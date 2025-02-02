using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform muzzlePosition;
    [SerializeField] GameObject projectile;

    public string gunName;
    public float fireDistance = 10;
    public float fireRate = 0.5f;
    public int gunDamage = 10;

    [SerializeField] Animator childAnimator;

    public Transform player;

    private float timeSinceLastShot = 0f;
    private Transform closestEnemy;
    private GunMeleeRangeController meleeRangeController;
    private bool ranged = false;

    private void Start()
    {
        timeSinceLastShot = fireRate;

        if (muzzlePosition)
        {
            ranged = true;
        }
        else
        {
            meleeRangeController = transform.parent.GetComponent<GunMeleeRangeController>();
        }
    }

    private void Update()
    {
        FindClosestEnemy();
        AimAtEnemy();
        Shooting();
    }

    void FindClosestEnemy()
    {
        closestEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= fireDistance)
            {
                closestEnemy = enemy.transform;
            }
        }
    }

    void AimAtEnemy()
    {
        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Shooting()
    {
        if (closestEnemy == null) return;
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= fireRate)
        {
            Shoot();
            timeSinceLastShot = 0;
        }
    }

    void Shoot()
    {
        childAnimator.SetTrigger("Shoot");
        if (ranged)
        {
            Vector2 direction = (closestEnemy.position - muzzlePosition.position).normalized;
            var projectileGo = Instantiate(projectile, muzzlePosition.position, transform.rotation);
            BulletController bulletController = projectileGo.GetComponent<BulletController>();
            bulletController.bulletDamage = gunDamage;
            bulletController.spawnedBy = "Player";

            Rigidbody2D rb = projectileGo.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * 10f;
            }
        }
        else
        {
            List<Enemy> enemiesInRange = meleeRangeController.getEnemiesInRange();

            foreach (var enemy in enemiesInRange)
            {
                if (enemy)
                {
                    enemy.GetComponent<Enemy>().Hit(gunDamage);
                }
            }
        }
    }
}
