using UnityEngine;

//Podemos implementar una interfaz de IMovable, hacer diferentes scripts de movimiento (Ejemplo "Flying Enemy"), que implemente la interfaz, y en el enemy behaviour
//le asignamos la interfaz (new Flying Enemy()) etc etc

//Otra que se me ocurre es armar los scripts de movimiento por separado, y asignarselo con un AddComponent, dependiendo del enemigo que elijamos <-- Prefiero esta yo

public class PatrolEnemy : MonoBehaviour
{
    private float distance = 1.5f;

    private Rigidbody2D rb;

    private EnemyData enemyData;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        enemyData = GetComponent<EnemyData>();
    }

    private void Update()
    {
        if (enemyData.IsAttacking) return;
        Patrol();
        rb.MovePosition(new Vector2(rb.position.x + enemyData.Speed * Time.deltaTime, rb.position.y));
    }

    private void Patrol()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemyData.groundChecker.position, Vector2.down, distance);

        if (hit) return;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0); //Si esto es cambiar de direccion, podemos multiplicarlo por -1
        enemyData.RayDirection *= -1;
        enemyData.Speed *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(enemyData.groundChecker.transform.position, enemyData.groundChecker.transform.position + Vector3.down * distance);
    }
}

