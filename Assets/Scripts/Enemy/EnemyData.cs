using System;
using UnityEngine;

public class EnemyData: MonoBehaviour, Ikillable
{
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private int rayDirection;
    private bool isAttack;

    private void Awake()
    {
        currentHealth = maxHealth;
        rayDirection = 1;
        isAttack = false;
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

    public bool IsAttack
    {
        get => isAttack;
        set => isAttack= value;
    }

    public void Die()
    {
        Debug.Log("hacer algo ");
    }
}
