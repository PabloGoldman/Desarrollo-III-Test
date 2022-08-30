using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance;
    [SerializeField] private float fieldOfView;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private float timeToAttack;
    [SerializeField] private float damage;

    public float CurrentHealth { get; set; }
    public int RayDirection { get; set; }
    public bool IsAttack { get; set; }
    public Transform GroundChecker { get; private set; }
    public bool IsDie { get; set; }
    
    private void Awake()
    { 
        CurrentHealth = maxHealth;
        RayDirection = 1;
        IsAttack = false;
        GroundChecker = transform.Find("CheckGround");
    }

    public float Damage=> damage;
    public float TimeToAttack=> timeToAttack;
    public float DistanceToAttack => distanceToAttack;
    public float GroundDistance => groundDistance;
    public float FieldOfView=> fieldOfView;
    public LayerMask EnemyLayer => enemyLayer;
    public LayerMask GroundLayer => groundLayer;
    public float Speed
    {
        get => speed;
        set => speed= value;
    }
    
}
