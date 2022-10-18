using System;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyData enemyData;
    private Animator animator;
    private Transform groundChecker;
    private Transform pointOfView;
    private float speed;

    private const float groundDistance = 0.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyData = GetComponent<EnemyData>();
        groundChecker = transform.Find("CheckGround");
        pointOfView= transform.Find("FieldOfView");
        speed = enemyData.Speed;
        rb.velocity = new Vector2( enemyData.Speed , 0);
        
    }

    private void Update()
    {
        if (enemyData.IsAttack || enemyData.IsDie) return;
        Patrol();
        Follow();
        
    }

    private void Patrol()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(groundChecker.position, Vector2.down, groundDistance , enemyData.GroundLayer);
        RaycastHit2D hitForward = Physics2D.Raycast(groundChecker.position, Vector2.right* enemyData.RayDirection, groundDistance , enemyData.GroundLayer);

        if (hitDown && !hitForward) return;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0); 
        enemyData.RayDirection *= -1;
        enemyData.Speed *= -1;
        rb.velocity = new Vector2( enemyData.Speed , 0);
    }
    private void Follow()
    {
        if (enemyData.FieldOfView == 0) return;
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
        Gizmos.DrawLine(groundChecker.position, groundChecker.position + ( Vector3.right* enemyData.RayDirection) * groundDistance);
    }
}

