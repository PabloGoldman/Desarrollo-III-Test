using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

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
        if(tanuk.gameObject.activeInHierarchy)  gameObject.transform.localPosition = tanuk.localPosition;
        else if (fox.gameObject.activeInHierarchy) gameObject.transform.localPosition = fox.localPosition;
        sr.enabled = true;
        agent.enabled = true;
        agent.SetDestination( SelectStatue().transform.position);
       
    }

    private Statue SelectStatue()
    {
        return targets[Random.Range(0,targets.Count)];
    }
    private void SetInactive()
    {
        if(tanuk.gameObject.activeInHierarchy)  gameObject.transform.localPosition = tanuk.localPosition;
        else if (fox.gameObject.activeInHierarchy) gameObject.transform.localPosition = fox.localPosition;
        agent.enabled = false;
        sr.enabled = false;
    }


    private void DiscardStatue(int id)
    {
        Statue targetStatue =targets.Find(target => target.ID == id);
         if (targetStatue != null) targets.Remove(targetStatue);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.CompareTag("Checkpoint")) return;
        Debug.Log("entro");
        SetInactive();
        Invoke( nameof(SetActive),TimeToSpawn);
    }
}
