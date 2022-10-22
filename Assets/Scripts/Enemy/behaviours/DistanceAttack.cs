using UnityEngine;
public class DistanceAttack : MonoBehaviour
{
   private EnemyData enemyData;
   private Transform checkPlayer;
   private Animator animator;
   private Transform spawnPoint;
   private float time;
   private bool attack;

   private void Awake()
   {
      attack = false;
      time = 0;
      enemyData = GetComponent<EnemyData>();
      animator = GetComponent<Animator>();
      checkPlayer= transform.Find("CheckGround");
      spawnPoint= transform.Find("SpawnPoint");
   }
   
   private void Update()
   {
      if (enemyData.IsDie) return;
      Shoot();
   }
   private void Shoot()
   {
      RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, Vector2.right * enemyData.RayDirection, enemyData.DistanceToAttack, enemyData.PlayerLayer);

      enemyData.IsAttack = hit;

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
          //animator.SetTrigger("Shoot");
         
         GameObject newBullet;
         newBullet = Instantiate(enemyData.Model, spawnPoint.position, enemyData.Model.transform.rotation,spawnPoint);
         newBullet.GetComponent<Rigidbody2D>().AddForce(spawnPoint.right*enemyData.Force, ForceMode2D.Impulse);
         Destroy(newBullet,2.0f);
      }
     
      
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.green;
      Gizmos.DrawLine(checkPlayer.position, checkPlayer.position + (Vector3.right * enemyData.RayDirection)* enemyData.DistanceToAttack);
   }

}
