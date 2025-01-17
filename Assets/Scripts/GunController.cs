using UnityEngine;

public class GunController : MonoBehaviour
{
    public enum gunType
    {
        alabarda,
    }

    public gunType type;

    public Sprite gunSprite;

    public float damage, fireRate;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = gunSprite;
    }
}
