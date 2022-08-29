using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float groundDistance;
    [SerializeField] private float fieldOfView;
    [SerializeField] private float distanceToAttack;

    private Transform groundChecker;
    private float currentHealth;
    private int rayDirection;
    private bool isAttacking;

    private void Awake()
    { 
        currentHealth = maxHealth;
        rayDirection = 1;
        isAttacking = false;
        groundChecker = transform.Find("CheckGround");
    }

    public Transform GroundChecker => groundChecker;
    public float DistanceToAttack => distanceToAttack;
    public float GroundDistance => groundDistance;
    public float FieldOfView=> fieldOfView;
    public LayerMask Layer => layer;

    public float Speed
    {
        get => speed;
        set => speed= value;
    }
    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    public int RayDirection
    {
        get => rayDirection;
        set => rayDirection= value;
    }

    public bool IsAttacking
    {
        get => isAttacking;
        set => isAttacking= value;
    }
    
   
}
