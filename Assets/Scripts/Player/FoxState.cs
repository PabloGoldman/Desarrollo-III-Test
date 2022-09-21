using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FoxState : CharacterState, IDamageable
{
    [HideInInspector] public PlayerAnimatorController animator;

    [SerializeField] FoxData foxData;

    bool enteredWallSlide;

    int jumpCount = 0;

    int maxJumpsAvailable = 2;

    [SerializeField] float timeBetweenJumps = 0.3f;
    float jumpDelay;

    [SerializeField] float timeBetweenRolls = 1f;

    float rollTimer = 1; //Tiempo para que te deje rollear de nuevo

    public bool isRolling { get; private set; }

    private float rollDuration = 8.0f / 14.0f;
    private float rollCurrentTime; //Tiempo en el que estas en el roll
    

    private void Awake()
    {
        animator = GetComponent<PlayerAnimatorController>();
        OnAwake();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            jumpDelay -= Time.deltaTime;

            if (Input.GetKeyDown("space") && jumpCount < maxJumpsAvailable && jumpDelay <= 0)
            {
                Jump();
            }

            else if (Input.GetMouseButtonDown(0) && rollTimer >= timeBetweenRolls)
            {
                Roll();
            }

            else if (Mathf.Abs(rb.velocity.x) > 0.15)
            {
                Run();
            }

            else
            {
                Idle();
            }

            CheckIsGrounded();
            HandleInputAndMovement();
            HandleTimers();
            WallSlide();

            if (!isRolling)
            {
                rb.velocity = new Vector2(inputX * foxData.speed, rb.velocity.y);
                rb.gravityScale = 1;
            }
            else
            {
                rb.gravityScale = 0;
            }

        }
    }

    public override void SwitchState()
    {
        animator.OnGround(isGrounded);
    }

    void Jump()
    {
        animator.Jump(isGrounded);
        isGrounded = false;
        animator.OnGround(isGrounded);

        rb.velocity = new Vector2(rb.velocity.x, foxData.jumpForce);

        groundSensor.Disable(0.2f);

        jumpDelay = timeBetweenJumps;

        jumpCount++;
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

        isRolling = true;

        rb.velocity = new Vector2(facingDirection * foxData.rollForce, 0.0f);
       

        rollTimer = 0;

        rollCurrentTime = 0;
    }

    void CheckIsGrounded()
    {
        //Check if character just landed on the ground
        if (!isGrounded && groundSensor.IsColliding())
        {
            isGrounded = true;

            jumpCount = 0;
            jumpDelay = 0;

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
        if (isWallSliding)
        {
            jumpCount = 1;
        }

        if (facingDirection > 0 && Input.GetKey(KeyCode.D))
        {
            //Wall Slide
            isWallSliding = (wallSensorR1.IsColliding() && wallSensorR2.IsColliding()) || (wallSensorL1.IsColliding() && wallSensorL2.IsColliding());
            animator.WallSlide(isWallSliding);
        }
        else if (facingDirection < 0 && Input.GetKey(KeyCode.A))
        {
            //Wall Slide
            isWallSliding = (wallSensorR1.IsColliding() && wallSensorR2.IsColliding()) || (wallSensorL1.IsColliding() && wallSensorL2.IsColliding());
            animator.WallSlide(isWallSliding);
        }
        else
        {
            isWallSliding = false;
            animator.WallSlide(isWallSliding);
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            animator.Hurt();

            playerManager.TakeDamage(damage * foxData.damageTakenMultiplier);
            if (playerManager.currentHealth <= 0) Die();
            Debug.Log("entro" + playerManager.currentHealth);
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

        if (!isRolling)
            rollTimer += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (rollCurrentTime > rollDuration)
            isRolling = false;
    }

    public override void Respawn()
    {
        transform.position = Vector3.zero;
        isDead = false;
        animator.Idle();
        ResetVariables();
    }
}
