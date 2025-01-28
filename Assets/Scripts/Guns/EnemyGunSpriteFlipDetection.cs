using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public SpriteRenderer parentEnemySpriteRenderer;
    public EnemyGunController enemyGunController;

    private SpriteRenderer personalSpriteRenderer;

    private void Start()
    {
        personalSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (parentEnemySpriteRenderer != null && enemyGunController != null && enemyGunController.GetClosestPlayer() != null)
        {
            if (parentEnemySpriteRenderer.flipX == true)
            {
                personalSpriteRenderer.flipY = true;
            }
            else
            {
                personalSpriteRenderer.flipY = false;
            }
        }
    }
}
