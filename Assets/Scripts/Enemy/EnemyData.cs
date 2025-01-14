﻿using System;
using UnityEngine;
public enum TypeOfMovement { Patrol, Flying}
 public enum TypeOfAttack { Melee, Distance,Flying}
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
    private Collider2D col2D;
    private SpriteRenderer mr;
    private Rigidbody2D rb;
    private bool reSpawn;

    public bool ReSpawn
    {
        get => reSpawn;
        set => reSpawn = value;
    }
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
        reSpawn = false;
        RayDirection = 1;
        CurrentHealth = maxHealth;
        IsAttack = false;
        IsDie = false;
        col2D = GetComponent<Collider2D>();
        mr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        SpawnController.OnTimeSpawn += Spawn;

    }

    private void OnDestroy()
    {
        SpawnController.OnTimeSpawn -= Spawn;
    }

    public void Spawn()
    { Debug.Log("entro");
        if (!reSpawn) return;
        gameObject.SetActive(true);
        reSpawn = false;
        rb.velocity = new Vector2( speed , 0);
        col2D.enabled = true;
        mr.enabled = true;
        IsDie = false;
        CurrentHealth = maxHealth;
    }

   


   

}

