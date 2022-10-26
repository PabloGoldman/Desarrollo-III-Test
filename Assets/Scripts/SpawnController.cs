using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
   [SerializeField] private List<EnemyData> enemies;
   [SerializeField] private float timeToSpawn;

   private void Start()
   {
      InvokeRepeating("Spawn",0,timeToSpawn);
   }

   private void Spawn()
   {
      foreach (var e in enemies)
      {
         if (e.IsDie)
         {  e.gameObject.SetActive(true);
            e.ReSpawn();
         }
         
      }
   }

}
