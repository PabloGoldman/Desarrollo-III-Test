using UnityEngine;
public enum TypeOfMovement { Patrol, Flying}
 public enum TypeOfAttack { Melee, Distance}
public class EnemyData: MonoBehaviour
{   
   
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;
    
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float fieldOfView;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private float timeToAttack;
    [SerializeField] private float damage;
    [SerializeField] private GameObject model;
    [SerializeField] private float force;

    public float Speed
    {
        get => speed;
        set => speed= value;
    }
    public GameObject Model => model;
    public float FieldOfView=> fieldOfView;
    public LayerMask GroundLayer => groundLayer;
    public float Force => force;
    public float DistanceToAttack => distanceToAttack;
    public float CurrentHealth { get; set; }
    public float TimeToAttack=> timeToAttack;
    public float AttackDamage=> damage;
    public LayerMask PlayerLayer => playerLayer;
    public bool IsAttack { get; set;}
    public bool IsDie { get; set; }
    
    public  int RayDirection { get; set; }

    public void Awake()
    {
        RayDirection = 1;
        CurrentHealth = maxHealth;
        IsAttack = false;
        IsDie = false;
    }
    
}

