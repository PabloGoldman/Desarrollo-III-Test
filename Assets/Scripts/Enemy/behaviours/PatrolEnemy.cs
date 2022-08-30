using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyData enemyData;
    private Animator animator;
    private Transform fieldOfView;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fieldOfView = transform.Find("FieldOfView");
    }

    private void Start()
    {
        enemyData = GetComponent<EnemyData>();
    }

    private void Update()
    {
        if (enemyData.IsAttack || enemyData.IsDie) return;
        Patrol();
        Follow();
        rb.MovePosition(new Vector2(rb.position.x + enemyData.Speed * Time.deltaTime, rb.position.y));
    }

    private void Patrol()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemyData.GroundChecker.position, Vector2.down, enemyData.GroundDistance, enemyData.GroundLayer);

        if (hit) return;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0); 
        enemyData.RayDirection *= -1;
        enemyData.Speed *= -1;
    }
    private void Follow()
    {
        RaycastHit2D hit = Physics2D.Raycast(fieldOfView.position, (Vector2.left * enemyData.RayDirection), enemyData.FieldOfView, enemyData.EnemyLayer);

        if (!hit) return;
        enemyData.RayDirection *= -1;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        enemyData.Speed *= -1;
    }

    private void OnDrawGizmos()
    {
        if (enemyData.GroundChecker == null || enemyData.RayDirection == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(fieldOfView.position, fieldOfView.position + (Vector3.left * enemyData.RayDirection) * enemyData.FieldOfView);
        Gizmos.DrawLine(enemyData.GroundChecker.position, enemyData.GroundChecker.position + Vector3.down * enemyData.GroundDistance);
    }
}

