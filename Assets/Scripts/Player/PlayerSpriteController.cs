using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    [Header("Ayose")]
    [SerializeField] Sprite AyoseSprite;
    [SerializeField] RuntimeAnimatorController AyoseAnimatorController;

    [Header("Guanchita")]
    [SerializeField] Sprite GuanchitaSprite;
    [SerializeField] RuntimeAnimatorController GuanchitaAnimatorController;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (GameManager.playerCount == 1)
        {
            spriteRenderer.sprite = AyoseSprite;
            animator.runtimeAnimatorController = AyoseAnimatorController;
        }
        else if (GameManager.playerCount == 2)
        {
            spriteRenderer.sprite = GuanchitaSprite;
            animator.runtimeAnimatorController = GuanchitaAnimatorController;
        }
    }
}
