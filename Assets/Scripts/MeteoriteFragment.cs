using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteFragment : MonoBehaviour
{
    private Collider2D character;
    private ParticleSystem ps;
    private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    public static event Action<float> OnMeteoriteFragmentHit;

    [SerializeField] float meteoriteDamage = 10;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        var t = GameObject.FindWithTag("CheckTanuk");
        character = t.GetComponent<Collider2D>();
        ps.trigger.AddCollider(character); //Añade el objeto a colisionar con la particula

    }

    private void OnParticleTrigger()
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        for (int i = 0; i < numEnter; i++)
        {
            AkSoundEngine.PostEvent("Play_SoulPlops", gameObject);
            ParticleSystem.Particle p = enter[i];
            OnMeteoriteFragmentHit?.Invoke(meteoriteDamage); //Colisiona la particula con el player
            p.remainingLifetime = 0;
            enter[i] = p;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }
}
