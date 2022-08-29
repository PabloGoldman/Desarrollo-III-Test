using UnityEngine;

public class MeleeAttack : MonoBehaviour, IDamageable
{
    private Transform checkPlayer;
    private EnemyData enemyData;
    private Animator animator;
    Collider2D col2D;
    
    private void Start()
    {
        col2D = GetComponent<Collider2D>();
        enemyData = GetComponent<EnemyData>();
        animator = GetComponent<Animator>();
        checkPlayer = enemyData.GroundChecker;
    }
    
    private void Update()
    {
        Follow();
        Attack();
    }

    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, Vector2.right * enemyData.RayDirection, enemyData.DistanceToAttack, enemyData.Layer);

        if (!hit) return;
        enemyData.IsAttacking = true;
        animator.SetTrigger("attack");
    }

    private void Follow()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, (Vector2.left * enemyData.RayDirection), enemyData.FieldOfView, enemyData.Layer);

        if (!hit) return;
        enemyData.RayDirection *= -1;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        enemyData.Speed *= -1;
    }
    public void TakeDamage(float damage)
    {
        enemyData.CurrentHealth -= damage;

        //Animacion de que recibe daño

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
        if (checkPlayer == null || enemyData.RayDirection == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(checkPlayer.transform.position, checkPlayer.transform.position + (Vector3.left * enemyData.RayDirection) * enemyData.FieldOfView);
        Gizmos.DrawLine(checkPlayer.transform.position, checkPlayer.transform.position + (Vector3.right * enemyData.RayDirection) * enemyData.DistanceToAttack);
    }
}
