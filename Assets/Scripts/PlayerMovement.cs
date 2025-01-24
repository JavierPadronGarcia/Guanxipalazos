using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidadMovimiento = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer playerRenderer;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoY = Input.GetAxis("Vertical");

        if (movimientoX < 0) playerRenderer.flipX = true;
        else if (movimientoX > 0) playerRenderer.flipX = false;

        Vector2 vectorVelocidad = new Vector2(movimientoX * velocidadMovimiento, movimientoY * velocidadMovimiento);
        rb.linearVelocity = vectorVelocidad;

        if (rb.linearVelocity.sqrMagnitude > 0) SetAnimation("running");
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
