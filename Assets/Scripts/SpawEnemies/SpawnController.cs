using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
   [SerializeField] private List<EnemyData> enemies;
   

   private void OnEnable()
   {
      SpawnPoint.OnSpawn += Spawn;
   }

   private void OnDisable()
   {
      SpawnPoint.OnSpawn -= Spawn;
   }
   
   private void Spawn()
   {
      foreach (var e in enemies)
      {
         if (e.IsDie) e.ReSpawn();
      }
   }

}
