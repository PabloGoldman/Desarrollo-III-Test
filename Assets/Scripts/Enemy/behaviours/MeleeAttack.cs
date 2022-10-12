using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private Transform attackPoint;
    private EnemyData enemyData;
    private Animator animator;
    private bool attack;
    private Transform pointOfView;
    private float speed;

    private void Awake()
    {
        attack = false;
        //animator = GetComponent<Animator>();
        enemyData = GetComponent<EnemyData>();
        pointOfView= transform.Find("FieldOfView");
        speed = enemyData.Speed;
    }

    private void Update()
    {
        if (enemyData.IsDie) return;
        Attack();
    }

    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(pointOfView.position, Vector2.right * enemyData.RayDirection, enemyData.DistanceToAttack, enemyData.PlayerLayer);

        //animator.SetBool("run",!hit);
        if (!hit) return;
        if (attack) return;
        enemyData.Speed *= 4;
        attack = true;
        Invoke(nameof(StopAttack),enemyData.TimeToAttack*3.5f);
    }
    
    private void StopAttack()
    {
        attack = false;
        enemyData.Speed = speed * enemyData.RayDirection;
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
      
    }

    private void ResetAttack()
    {
        enemyData.IsAttack = false;
    }

    private void OnDrawGizmos()
    {
       Gizmos.color = Color.blue;
       Gizmos.DrawLine(pointOfView.position, pointOfView.position + (Vector3.right * enemyData.RayDirection) * enemyData.DistanceToAttack);
    }
}
