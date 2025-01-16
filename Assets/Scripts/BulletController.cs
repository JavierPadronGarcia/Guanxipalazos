using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si la bala colisiona con la pared, se destruye
        if (collision.CompareTag("Limit"))
        {
            Destroy(this.gameObject);
        }
    }
}
