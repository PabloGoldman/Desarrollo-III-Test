using UnityEngine;

public class BurstAttack : MonoBehaviour
{
   private EnemyData enemyData;

   private void Start()
   {
      enemyData = GetComponent<EnemyData>();
   }

   private void OnCollisionEnter2D(Collision2D c)
   {
      var obj = c.gameObject.GetComponent<IDamageable>();
      
      if (obj == null) return;
      obj.TakeDamage(enemyData.Damage);
      gameObject.GetComponent<IDamageable>().TakeDamage(enemyData.CurrentHealth);
      
   }
   
  
   
   
}
