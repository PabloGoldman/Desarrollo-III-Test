using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyData enemyData;
    private Animator animator;
    
    private Transform groundChecker;
    private Transform pointOfView;
   
   
    private const float groundDistance = 1.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyData = GetComponent<EnemyData>();
        groundChecker = transform.Find("CheckGround");
        pointOfView= transform.Find("FieldOfView");
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
        RaycastHit2D hit = Physics2D.Raycast(groundChecker.position, Vector2.down, groundDistance , enemyData.GroundLayer);

        if (hit) return;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0); 
        enemyData.RayDirection *= -1;
        enemyData.Speed *= -1;
    }
    private void Follow()
    {
        RaycastHit2D hit = Physics2D.Raycast(pointOfView.position, (Vector2.left * enemyData.RayDirection), enemyData.FieldOfView, enemyData.PlayerLayer);

        if (!hit) return;
        enemyData.RayDirection *= -1;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        enemyData.Speed *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pointOfView.position, pointOfView.position + (Vector3.left * enemyData.RayDirection) * enemyData.FieldOfView);
        Gizmos.DrawLine(groundChecker.position, groundChecker.position + Vector3.down * groundDistance);
    }
}

