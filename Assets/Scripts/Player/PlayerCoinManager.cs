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
            GameManager.Coins++;
            Debug.Log("Moneda recogida, total: " + GameManager.Coins);
            AudioManager.instance.PlaySFX("GrabItem");
            Destroy(collision.gameObject);
        }
    }
}
