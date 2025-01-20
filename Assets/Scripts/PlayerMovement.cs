using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidadMovimiento = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer playerRenderer;
    private Animator anim;

    void Start()
    {
        //Guardamos el rigidbody
        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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

        if (rb.linearVelocityX != 0 || rb.linearVelocityY != 0) SetAnimation("runing");
        else SetAnimation("idle");
    }

    void SetAnimation(string name)
    {
        // Obtenemos todos los parametros del Animator
        AnimatorControllerParameter[] parametros = GetComponent<Animator>().parameters;
        // Recorremos todos los parametros y los ponemos a false
        foreach (var item in parametros) GetComponent<Animator>().SetBool(item.name,false);
        // Activamos el pasado por parametro
        GetComponent<Animator>().SetBool(name, true);
    }
}
