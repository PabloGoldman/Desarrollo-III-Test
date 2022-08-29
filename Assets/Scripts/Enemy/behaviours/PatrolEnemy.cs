using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyData enemyData;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        enemyData = GetComponent<EnemyData>();
    }

    private void Update()
    {
        if (enemyData.IsAttack || enemyData.IsDie) return;
        Patrol();
        rb.MovePosition(new Vector2(rb.position.x + enemyData.Speed * Time.deltaTime, rb.position.y));
    }

    private void Patrol()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemyData.GroundChecker.position, Vector2.down, enemyData.GroundDistance);

        if (hit) return;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0); 
        enemyData.RayDirection *= -1;
        enemyData.Speed *= -1;
    }

    private void OnDrawGizmos()
    {
        if (enemyData.GroundChecker == null || enemyData.RayDirection == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(enemyData.GroundChecker.transform.position, enemyData.GroundChecker.transform.position + Vector3.down * enemyData.GroundDistance);
    }
}

