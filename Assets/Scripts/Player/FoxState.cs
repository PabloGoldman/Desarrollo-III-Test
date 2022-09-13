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

    public bool isRolling { get; private set; }

    private float rollDuration = 8.0f / 14.0f;
    private float rollCurrentTime;

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
            rb.velocity = new Vector2(inputX * foxData.speed, rb.velocity.y);

            jumpDelay -= Time.deltaTime;

            if (Input.GetKeyDown("space") && jumpCount < maxJumpsAvailable && jumpDelay <= 0)
            {
                Jump();
            }

            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Roll();
            }

            else if (Mathf.Abs(rb.velocity.x) > 0.15)
            {
                Run();
            }

            CheckIsGrounded();
            HandleInputAndMovement();
            HandleTimers();
            WallSlide();

            if (!isRolling)
                rb.velocity = new Vector2(inputX * foxData.speed, rb.velocity.y);
        }
    }

    public override void SwitchState()
    {
        animator.Idle();
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
        rb.velocity = new Vector2(100, rb.velocity.y);
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
        //Wall Slide
        isWallSliding = (wallSensorR1.IsColliding() && wallSensorR2.IsColliding()) || (wallSensorL1.IsColliding() && wallSensorL2.IsColliding());

        if (isWallSliding)
        {
            jumpCount = 1;
        }

        animator.WallSlide(isWallSliding);
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            animator.Hurt();

            playerManager.TakeDamage(damage);
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
