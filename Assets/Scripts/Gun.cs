using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject muzzle;
    [SerializeField] Transform muzzlePosition;
    [SerializeField] GameObject projectile;

    [SerializeField] float fireDistance = 10;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] int gunDamage = 10;
    [SerializeField] GameObject childSprite;

    public Transform player;
    public Vector2 offset;

    private float timeSinceLastShot = 0f;
    Transform closestEnemy;
    Animator anim;
    private GunMeleeRangeController meleeRangeController;
    private bool ranged = false;

    private void Start()
    {
        anim = childSprite.GetComponent<Animator>();
        timeSinceLastShot = fireRate;

        if (muzzle)
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
        transform.position = (Vector2)player.position + offset;

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
            transform.position = (Vector2)player.position + offset;
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
        anim.SetTrigger("Shoot");
        if (ranged)
        {
            var muzzleGo = Instantiate(muzzle, muzzlePosition.position, transform.rotation);
            muzzleGo.transform.SetParent(transform);
            Destroy(muzzleGo, 0.5f);

            var projectileGo = Instantiate(projectile, muzzlePosition.position, transform.rotation);
            Destroy(projectileGo, 3);
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
