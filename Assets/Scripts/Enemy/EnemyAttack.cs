using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private float distance;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private LayerMask layer;
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
       Follow();
       Attack();
    }

    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, Vector2.right*enemyData.RayDirection, distanceToAttack,layer);

        if (!hit) return;
        enemyData.IsAttack = true;
        animator.SetTrigger("attack");
    }

    private void Follow()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, (Vector2.left*enemyData.RayDirection), distance,layer);

        if (!hit) return;
        enemyData.RayDirection *= -1;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        enemyData.Speed *= -1;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.green;
        Gizmos.DrawLine(checkPlayer.transform.position,checkPlayer.transform.position + (Vector3.left*enemyData.RayDirection)* distance);
        Gizmos.DrawLine(checkPlayer.transform.position,checkPlayer.transform.position + (Vector3.right*enemyData.RayDirection)* distanceToAttack);
    }
}
