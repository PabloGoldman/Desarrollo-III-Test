using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState
{
    [SerializeField] public Rigidbody2D rb;

    public PlayerController playerController;

    public abstract void Update();

    public abstract void SwitchState();

    public abstract void Respawn();
}
