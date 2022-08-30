using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private Transform checkPlayer;
    private Transform attackPoint;
    private EnemyData enemyData;
    private Animator animator;
    private float RadiusPunch;
    private float time;
    private bool attack;

    private void Start()
    {
        RadiusPunch = 0.7f;
        attack = false;
        time = 0;
        enemyData = GetComponent<EnemyData>();
        animator = GetComponent<Animator>();
        checkPlayer = enemyData.GroundChecker;
        attackPoint = transform.Find("AttackPoint");

    }
    
    private void Update()
    {
        if (enemyData.IsDie) return;
        Attack();
    }

    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, Vector2.right * enemyData.RayDirection, enemyData.DistanceToAttack, enemyData.EnemyLayer);
        
        enemyData.IsAttack = hit;
        animator.SetBool("run",!hit);
        
        if(attack) time += Time.deltaTime;
        
        if (time >= enemyData.TimeToAttack)
        {
            time = 0;
            attack = false;
        }

        if (!hit) return;
        attack = true;
        if (time == 0)
        {
            animator.SetTrigger("attack");
            CheckCollision();
        }
    }

    private void CheckCollision()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.transform.position,RadiusPunch,enemyData.EnemyLayer);

        if (!hit) return;
        var obj = hit.gameObject.GetComponent<IDamageable>();
        obj?.TakeDamage(enemyData.Damage);
    }

   
    
    
    private void OnDrawGizmos()
    {
        if (checkPlayer == null || enemyData.RayDirection == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position,RadiusPunch);
        Gizmos.DrawLine(checkPlayer.position, checkPlayer.position + (Vector3.right * enemyData.RayDirection) * enemyData.DistanceToAttack);
    }
}
