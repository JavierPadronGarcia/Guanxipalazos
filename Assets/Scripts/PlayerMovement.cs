using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidadMovimiento = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer playerRenderer;

    void Start()
    {
        //Guardamos el rigidbody
        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Obtenemos el input de los ejes vertical y horizontal
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoY = Input.GetAxis("Vertical");

        if (movimientoX < 0) playerRenderer.flipX = true;
        else if (movimientoX > 0) playerRenderer.flipX = false;


        //Obtenemos el vector velocidad y le asignamos esa velocidad al rigidbody
        Vector2 vectorVelocidad = new Vector2(movimientoX * velocidadMovimiento, movimientoY * velocidadMovimiento);
        rb.linearVelocity = vectorVelocidad;
    }
}
