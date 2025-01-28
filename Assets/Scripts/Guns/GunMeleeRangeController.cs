using UnityEngine;
using System.Collections.Generic;


public class GunMeleeRangeController : MonoBehaviour
{
    List<Enemy> enemiesInRange = new List<Enemy>();
    CircleCollider2D circleCollider2D;


    private void Start()
    {
        enemiesInRange = new List<Enemy>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public float getColliderSize()
    {
        return circleCollider2D.radius;
    }

    public List<Enemy> getEnemiesInRange()
    {
        return enemiesInRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && !enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
                enemy.OnDeath += RemoveEnemyFromList;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && enemiesInRange.Contains(enemy))
            {
                RemoveEnemyFromList(enemy);
            }
        }
    }

    private void RemoveEnemyFromList(Enemy enemy)
    {
        if (enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
            enemy.OnDeath -= RemoveEnemyFromList;
        }
    }

}
