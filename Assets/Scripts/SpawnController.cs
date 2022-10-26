using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
   [SerializeField] private List<EnemyData> enemies;


   private void OnTriggerEnter(Collider o)
   {
      if (o.gameObject.CompareTag("Player"))
         Spawn();
   }

   private void Spawn()
   {
      foreach (var e in enemies)
      {
         if (e.IsDie) e.gameObject.SetActive(true);
      }
   }

}
