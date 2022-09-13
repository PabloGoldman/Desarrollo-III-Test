using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FoxState : CharacterState, IDamageable
{
    [HideInInspector] public PlayerAnimatorController animator;

    [SerializeField] PlayerData foxData;

    public bool isRolling { get; private set; }

    private float rollDuration = 8.0f / 14.0f;
    private float rollCurrentTime;

    private void Awake()
    {
        OnAwake();
    }

    void Star()
    {
        foxData.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            rb.velocity = new Vector2(inputX * foxData.speed, rb.velocity.y);

            CheckIsGrounded();
            HandleInputAndMovement();
            HandleTimers();
            Jump();
            WallSlide();

            if (!isRolling)
                rb.velocity = new Vector2(inputX * foxData.speed, rb.velocity.y);
        }
    }

    public override void SwitchState()
    {
        //throw new System.NotImplementedException();
    }

    void Jump()
    {
        animator.Jump(isGrounded);
        isGrounded = false;
        animator.OnGround(isGrounded);
        rb.velocity = new Vector2(rb.velocity.x, foxData.jumpForce);
        groundSensor.Disable(0.2f);
    }

    void Run()
    {
        // Reset timer
        delayToIdle = 0.05f;
        animator.Run();
    }

    void Idle()
    {
        // Prevents flickering transitions to idle
        delayToIdle -= Time.deltaTime;
        if (delayToIdle < 0)
            animator.Idle();
    }

    void Roll()
    {
        animator.Roll();
        rb.velocity = new Vector2(facingDirection * foxData.rollForce, rb.velocity.y);
    }

    void CheckIsGrounded()
    {
        //Check if character just landed on the ground
        if (!isGrounded && groundSensor.IsColliding())
        {
            isGrounded = true;
            animator.OnGround(isGrounded);
        }

        //Check if character just started falling
        if (isGrounded && !groundSensor.IsColliding())
        {
            isGrounded = false;
            animator.OnGround(isGrounded);
        }
    }

    void WallSlide()
    {
        //Wall Slide
        isWallSliding = (wallSensorR1.IsColliding() && wallSensorR2.IsColliding()) || (wallSensorL1.IsColliding() && wallSensorL2.IsColliding());
        animator.WallSlide(isWallSliding);
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            animator.Hurt();

            foxData.currentHealth -= damage;
            if (foxData.currentHealth <= 0) Die();
            Debug.Log("entro" + foxData.currentHealth);
        }
    }

    void Die()
    {
        rb.velocity = Vector2.zero;
        animator.Death();
        isDead = true;

        Invoke(nameof(Respawn), timeToRespawn);
    }

    void HandleTimers()
    {
        // Increase timer that checks roll duration
        if (isRolling)
            rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (rollCurrentTime > rollDuration)
            isRolling = false;
    }

    public override void Respawn()
    {
        transform.position = Vector3.zero;
        isDead = false;
        animator.Idle();
        foxData.Start();
        ResetVariables();
    }
}
