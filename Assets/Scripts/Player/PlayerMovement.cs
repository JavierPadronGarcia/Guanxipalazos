using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;

    private Rigidbody2D rb2D;
    private SpriteRenderer playerRenderer;
    private Animator anim;

    private Vector2 movementInput = Vector2.zero;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 move = movementInput * playerSpeed * Time.fixedDeltaTime;
        rb2D.MovePosition(rb2D.position + move);

        if (move.x < 0) playerRenderer.flipX = true;
        else if (move.x > 0) playerRenderer.flipX = false;

        if (move != Vector2.zero) SetAnimation("running");
        else SetAnimation("idle");
    }

    private void SetAnimation(string name)
    {
        foreach (var param in anim.parameters)
        {
            anim.SetBool(param.name, false);
        }
        anim.SetBool(name, true);
    }
}
