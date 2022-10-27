using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
   [SerializeField] private List<EnemyData> enemies;

   private void OnEnable()
   {
      SpawnPoint.OnSpawn += Spawn;
      PlayerManager.OnHit += SpawnWhitPlayer;
   }

   private void OnDisable()
   {
      SpawnPoint.OnSpawn -= Spawn;
      PlayerManager.OnHit += SpawnWhitPlayer;
   }
   
   private void Spawn()
   {
      foreach (var e in enemies)
      {
         if (e.ReSpawn) e.Spawn();
      }
   }

   private void SpawnWhitPlayer(float life)
   {
      if (life <= 0)
      {
         Debug.Log("spawn in player ");
         Spawn();
      }
   }

}
