using UnityEngine;

public class PlayerCoinManager : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boxCollider2D.IsTouching(collision) && collision.CompareTag("Coin"))
        {
            int coinCount = Random.Range(1, 4);
            GameManager.Coins += coinCount;
            Debug.Log("Moneda recogida, total: " + GameManager.Coins);
            AudioManager.instance.PlaySFX("GrabItem");
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }
}
