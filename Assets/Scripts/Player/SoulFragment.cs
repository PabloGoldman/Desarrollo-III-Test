using System;
using System.Collections.Generic;
using UnityEngine;

public class SoulFragment : MonoBehaviour
{
   [SerializeField] private Collider2D tanuk;
   [SerializeField] private Collider2D fox;
   private ParticleSystem ps;
   private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
   public static event Action OnHit;

   private void OnEnable()
   {
      ps = GetComponent<ParticleSystem>();
      ps.trigger.AddCollider(tanuk);
      ps.trigger.AddCollider(fox);
   }

   private void OnParticleTrigger()
   {
      int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
      for (int i = 0; i < numEnter; i++)
      {
         ParticleSystem.Particle p = enter[i];
         OnHit?.Invoke();
         p.remainingLifetime = 0;
         enter[i] = p;
      }
      
      ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
   }
}
