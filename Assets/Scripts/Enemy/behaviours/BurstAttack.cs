using System;
using UnityEngine;

public class BurstAttack : MonoBehaviour
{
   private EnemyData enemyData;

   private void Awake()
   {
      enemyData = GetComponent<EnemyData>();
   }

   private void OnCollisionEnter2D(Collision2D c)
   {
      var obj = c.gameObject.GetComponent<IDamageable>();
      
      if (obj == null) return;
      obj.TakeDamage(enemyData.AttackDamage);
      gameObject.GetComponent<IDamageable>().TakeDamage(enemyData.CurrentHealth);
      
   }
   
  
   
   
}
