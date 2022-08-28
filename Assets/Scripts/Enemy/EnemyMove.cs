using UnityEngine;

public class EnemyMove : MonoBehaviour
{
   [SerializeField] private Transform checkGround;
   [SerializeField] private float distance;
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
        if (enemyData.IsAttack) return;
        Patrol();
        rb.MovePosition(new Vector2(rb.position.x +enemyData.Speed*Time.deltaTime,rb.position.y));
        
    }

    private void Patrol()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkGround.position, Vector2.down, distance);

        if (hit) return;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        enemyData.RayDirection *= -1;
        enemyData.Speed *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawLine(checkGround.transform.position,checkGround.transform.position + Vector3.down* distance );
       
    }
}
