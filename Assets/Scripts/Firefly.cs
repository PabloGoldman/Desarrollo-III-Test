using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Firefly : MonoBehaviour
{
    [SerializeField] private Transform tanuk;
    [SerializeField] private Transform fox;
    [SerializeField] private List<Statue> targets;
    [SerializeField] private float TimeToSpawn;

    private NavMeshAgent agent;
    private SpriteRenderer sr;

    private void Awake()
    {
        Statue.SendID += DiscardStatue;
        agent = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();
        if(tanuk.gameObject.activeInHierarchy)  gameObject.transform.position = tanuk.position;
        else if (fox.gameObject.activeInHierarchy) gameObject.transform.position = fox.position;
        
    }

    private void OnDestroy()
    {
        Statue.SendID -= DiscardStatue;
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Invoke( nameof(SetActive),TimeToSpawn);
        
    }

    private void SetActive()
    {
        sr.enabled = true;
        agent.isStopped = false;
        agent.SetDestination(targets[0].transform.position);
    }

    private void SetInactive()
    {
        sr.enabled = false;
        if(tanuk.gameObject.activeInHierarchy)  gameObject.transform.position = tanuk.position;
        else if (fox.gameObject.activeInHierarchy) gameObject.transform.position = fox.position;
        agent.isStopped = true;
    }

    private void DiscardStatue(int id)
    {
        Statue targetStatue =targets.Find(target => target.ID == id);
         if (targetStatue != null) targets.Remove(targetStatue);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.CompareTag("Checkpoint")) return;
        SetInactive();
        Invoke( nameof(SetActive),TimeToSpawn);

    }
}
