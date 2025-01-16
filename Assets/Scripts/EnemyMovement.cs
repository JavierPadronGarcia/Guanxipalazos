using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float velocity = 3f;
    public Transform player;
    public float targetDistance = 5f;
    public float rangeDistance = 10f;

    public enum EnemyType
    {
        Melee,
        Ranged
    }

    public EnemyType enemyType;

    private void Start()
    {
        //Obtenemos el tipo de enemigo de forma aleatoria y obtenemos la posición del jugador
        enemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            //Determinamos el tipo de enemigo y realizamos su acción correspondiente
            switch (enemyType)
            {
                case EnemyType.Melee:
                    MoveToPlayer();
                    break;
                case EnemyType.Ranged:
                    //Si es de tipo Ranged mantendremos una distancia entre él y el jugador
                    float distance = Vector3.Distance(transform.position, player.position);
                    //Si te alejas demasiado del enemigo se acercará lo suficiente al jugador para disparar
                    if (distance > rangeDistance)
                    {
                        MoveToPlayer();
                    }
                    //Si el jugador se acerca demasiado al enemigo este se alejará de el
                    else if (distance < targetDistance)
                    {
                        MoveAway();
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //Se encarga de hacer que el enemigo se mueva hacia el jugador
    private void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, velocity * Time.deltaTime);
    }

    //Se encarga de hacer que el enemigo se aleje del jugador
    private void MoveAway()
    {
        Vector3 directionAway = (transform.position - player.position).normalized;
        transform.position += directionAway * velocity * Time.deltaTime;
    }

}
