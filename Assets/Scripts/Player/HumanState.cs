using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class HumanState : CharacterState, IDamageable
{
    [HideInInspector] public PlayerAnimatorController animator;

    [SerializeField] HumanData humanData;

    [SerializeField] Transform attackPoint;

    public UnityEvent onThirdAttack;

    private float timeSinceAttack = 0.0f;

    private int currentAttack;

    private void Awake()
    {
        animator = GetComponent<PlayerAnimatorController>();
        OnAwake();
    }

    private void Update()
    {
        if (!isDead)
        {
            HandleInputAndMovement();
            CheckIsGrounded();
            WallSlide();

            rb.velocity = new Vector2(inputX * humanData.speed, rb.velocity.y);

            // Increase timer that controls attack combo
            timeSinceAttack += Time.deltaTime;

            //Attack
            if (Input.GetMouseButtonDown(0) && timeSinceAttack > 0.25f)
            {
                Attack();
            }

            //Jump
            else if (Input.GetKeyDown("space") && (isGrounded) && (!isWallSliding))
            {
                Jump();
            }

            //Run
            else if (Mathf.Abs(rb.velocity.x) > 0.15)
            {
                Run();
            }

            //Idle
            else
            {
                Idle();
            }
        }
    }

    void WallSlide()
    {
        //Wall Slide
        isWallSliding = (wallSensorR1.IsColliding() && wallSensorR2.IsColliding()) || (wallSensorL1.IsColliding() && wallSensorL2.IsColliding());
        animator.WallSlide(isWallSliding);
    }

    public override void SwitchState()
    {
        animator.Idle();
    }

    void Attack()
    {
        currentAttack++;

        if (currentAttack == 3)
        {
            TriggerThirdAttack();
        }

        // Loop back to one after third attack
        else if (currentAttack > 3)
        {
            currentAttack = 1;
        }

        // Reset Attack combo if time since last attack is too large
        if (timeSinceAttack > 1.0f)
            currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        animator.Attack(currentAttack);

        // Reset time
        timeSinceAttack = 0.0f;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, humanData.attackRange, playerManager.playerData.enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            var obj = enemy.gameObject.GetComponent<IDamageable>();
            obj?.TakeDamage(humanData.attackDamage);
        }
    }

    public void TriggerThirdAttack()
    {
        onThirdAttack?.Invoke();
    }

    void Jump()
    {
        animator.Jump(isGrounded);
        isGrounded = false;
        animator.OnGround(isGrounded);
        rb.velocity = new Vector2(rb.velocity.x, humanData.jumpForce);
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

    void CheckIsGrounded()
    {
        Debug.Log(groundSensor.IsColliding());

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

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            playerManager.TakeDamage(damage);
            animator.Hurt();

            if (playerManager.currentHealth <= 0) Die();
            Debug.Log("morir" + playerManager.currentHealth);
        }
    }

    void Die()
    {
        rb.velocity = Vector2.zero;
        animator.Death();
        isDead = true;

        Invoke(nameof(Respawn), timeToRespawn);
    }

    public override void Respawn()
    {
        isGrounded = true;
        transform.position = Vector3.zero;
        isDead = false;
        animator.Idle();

        ResetVariables();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null || humanData == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, humanData.attackRange);
    }
}
