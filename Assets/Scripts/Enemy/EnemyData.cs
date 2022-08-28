using System;
using UnityEngine;

public class EnemyData: MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private int rayDirection;

    private void Awake()
    {
        currentHealth = maxHealth;
        rayDirection = 1;
    }
    
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    
    public int CurrenHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    public int RayDirection
    {
        get => rayDirection;
        set => rayDirection= value;
    }
}
