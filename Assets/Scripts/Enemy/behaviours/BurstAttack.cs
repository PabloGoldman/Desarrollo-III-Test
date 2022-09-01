using System;
using System.Collections;
using UnityEngine;

public class BurstAttack : MonoBehaviour
{
   private EnemyData enemyData;
   private Transform checkPlayer;
   private Animator animator;
   private float blastRadius;
   private bool isActive;
   
   
   private void Awake()
   {
      isActive = false;
      blastRadius = 1.15f;
      enemyData = GetComponent<EnemyData>();
      animator = GetComponent<Animator>();
      checkPlayer= transform.Find("CheckGround");
   }

   private void Update()
   {
      Burst();
   }

   private void Burst()
   {
      RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, Vector2.right * enemyData.RayDirection, enemyData.DistanceToAttack, enemyData.PlayerLayer);

      if (!hit || isActive) return;
      isActive = true;
      enemyData.IsAttack = true;
      StartCoroutine(ActiveBurst());
   }

    private void ActiveBurstImmeadiate()
    {
        BurstCollision();
        animator.SetTrigger("Destroy");
        Destroy(gameObject, 0.4f);
    }

   private IEnumerator ActiveBurst()
   {
      yield return new WaitForSeconds(enemyData.TimeToAttack);
      BurstCollision();
      animator.SetTrigger("Destroy");
      Destroy(gameObject,0.4f);
   }

   private void BurstCollision()
   {
      Collider2D hitPlayer = Physics2D.OverlapCircle(checkPlayer.position, blastRadius, enemyData.PlayerLayer);
      
      if (!hitPlayer) return;
      var obj = hitPlayer.gameObject.GetComponent<IDamageable>();
      obj?.TakeDamage(enemyData.AttackDamage);
   }

   private void OnCollisionEnter2D(Collision2D c)
   {
      var obj = c.gameObject.GetComponent<IDamageable>();
      
      if (obj == null) return;
      obj.TakeDamage(enemyData.AttackDamage);
      ActiveBurstImmeadiate();
   }

   

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawLine(checkPlayer.position, checkPlayer.position + (Vector3.right * enemyData.RayDirection)* enemyData.DistanceToAttack);
      Gizmos.DrawWireSphere(checkPlayer.position,blastRadius);
   }
   
  
   
   
}
