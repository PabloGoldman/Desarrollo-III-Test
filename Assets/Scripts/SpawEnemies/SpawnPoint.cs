using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static event Action OnSpawn;
    
    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.CompareTag("Player"))
            OnSpawn?.Invoke();
    }
}
