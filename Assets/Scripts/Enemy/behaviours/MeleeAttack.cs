using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private Transform checkPlayer;
    private EnemyData enemyData;
    private Animator animator;
    private float time;
    private bool attack;
    
    private void Start()
    {
        attack = false;
        time = 0;
        enemyData = GetComponent<EnemyData>();
        animator = GetComponent<Animator>();
        checkPlayer = enemyData.GroundChecker;
    }
    
    private void Update()
    {
        if (enemyData.IsDie) return;
        Follow();
        Attack();
    }

    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, Vector2.right * enemyData.RayDirection, enemyData.DistanceToAttack, enemyData.Layer);
       
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
        if(time == 0 )  animator.SetTrigger("attack");
    }

    private void Follow()
    {
       RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, (Vector2.left * enemyData.RayDirection), enemyData.FieldOfView, enemyData.Layer);

        if (!hit) return;
        enemyData.RayDirection *= -1;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        enemyData.Speed *= -1;
    }
    
    
    private void OnDrawGizmos()
    {
        if (checkPlayer == null || enemyData.RayDirection == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(checkPlayer.transform.position, checkPlayer.transform.position + (Vector3.left * enemyData.RayDirection) * enemyData.FieldOfView);
        Gizmos.DrawLine(checkPlayer.transform.position, checkPlayer.transform.position + (Vector3.right * enemyData.RayDirection) * enemyData.DistanceToAttack);
    }
}
