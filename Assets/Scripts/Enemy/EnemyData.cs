using UnityEngine;

 public enum TypeOfMovement { Patrol, Flying}
 public enum TypeOfAttack { Melee, Distance, Burst}

[CreateAssetMenu(fileName = "new Enemy",menuName = "ScriptableObject/Enemy")]
public class EnemyData : ScriptableObject
{   
    [SerializeField] private TypeOfMovement typeOfMovement;
    [SerializeField] private TypeOfAttack  typeOfAttack;
    [SerializeField] private float speed;
    [SerializeField] private float maxHealth;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance;
    [SerializeField] private float fieldOfView;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private float timeToAttack;
    [SerializeField] private float damage;

    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public int RayDirection { get; set;}
    public bool IsAttack { get; set;}
    public Transform GroundChecker { get; set; }
    public Transform PointOfView { get; set; }
    public bool IsDie { get; set; }
    public float Damage=> damage;
    public float TimeToAttack=> timeToAttack;
    public float DistanceToAttack => distanceToAttack;
    public float GroundDistance => groundDistance;
    public float FieldOfView=> fieldOfView;
    public LayerMask PlayerLayer => playerLayer;
    public LayerMask GroundLayer => groundLayer;
    
    public TypeOfMovement TypeOfMovement => typeOfMovement;
    public TypeOfAttack TypeOfAttack => typeOfAttack;
    
    public float Speed
    {
        get => speed;
        set => speed= value;
    }

    public void Init()
    {
        CurrentHealth = MaxHealth;
        RayDirection = 1;
        IsAttack = false;
    }
    
}
