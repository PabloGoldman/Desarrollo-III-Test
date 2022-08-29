using System;
using UnityEngine;

enum TypeOfMovement { Patrol, Flying}
    
public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private float distance;
    [SerializeField] private float distanceToAttack;

    [SerializeField] private LayerMask layer;

    [SerializeField] private TypeOfMovement typeOfMovement;

    private Rigidbody2D rb;

    private EnemyData enemyData;

    private Animator animator;


    Collider2D col2D;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        col2D = GetComponent<Collider2D>();

        switch (typeOfMovement)
        {
            case TypeOfMovement.Patrol:
                //Aca iria el script de PatrolEnemy
                gameObject.AddComponent<PatrolEnemy>();
                break;
            case TypeOfMovement.Flying:
                //Aca iria el script de FlyingEnemy
                break;
            default:
                break;
        }
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
        RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, Vector2.right * enemyData.RayDirection, distanceToAttack, layer);

        if (!hit) return;
        enemyData.IsAttacking = true;
        animator.SetTrigger("attack");
    }

    private void Follow()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, (Vector2.left * enemyData.RayDirection), distance, layer);

        if (!hit) return;
        enemyData.RayDirection *= -1;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        enemyData.Speed *= -1;
    }

    public void TakeDamage(float damage)
    {
        enemyData.CurrentHealth -= damage;

        //Animacion de que recibe da�o

        if (enemyData.CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Animacion de morir 
        gameObject.SetActive(false);

        col2D.enabled = false;
        this.enabled = false;
    }

    private void OnDrawGizmos()
    {
        if (checkPlayer == null)
        {
            return;
        }

        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(checkPlayer.transform.position, checkPlayer.transform.position + (Vector3.left * enemyData.RayDirection) * distance);
        //Gizmos.DrawLine(checkPlayer.transform.position, checkPlayer.transform.position + (Vector3.right * enemyData.RayDirection) * distanceToAttack);
    }
}
