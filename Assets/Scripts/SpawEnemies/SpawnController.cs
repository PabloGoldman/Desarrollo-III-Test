using System;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private float timeToSpawn;
    public static event Action OnTimeSpawn;
    
    
    private void Start()
    {
       InvokeRepeating("Spawn",timeToSpawn,timeToSpawn);
    }

    private void Spawn()
    {
        OnTimeSpawn?.Invoke();
    }
    
    
}
