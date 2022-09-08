using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanState : CharacterState
{
    private float timeSinceAttack = 0.0f;

    private int currentAttack;

    // Update is called once per frame
    public override void Update()
    {
        // Increase timer that controls attack combo
        timeSinceAttack += Time.deltaTime;

        //Attack
        if (Input.GetMouseButtonDown(0) && timeSinceAttack > 0.25f)
        {
            Attack();
        }

        //Jump
        else if (Input.GetKeyDown("space") && (playerController.isGrounded) && (!playerController.isWallSliding))
        {
            Jump();
        }

        //Run
        else if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            Run();
        }

        //Idle
        else
        {
            Idle();
        }
    }

    public override void SwitchState()
    {
        //throw new System.NotImplementedException();
    }

    void Attack()
    {
        currentAttack++;

        if (currentAttack == 3)
        {
            playerController.TriggerThirdAttack();
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
       playerController.animator.Attack(currentAttack);

        // Reset time
        timeSinceAttack = 0.0f;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(playerController.attackPoint.position, playerController.playerData.attackRange, playerController.playerData.enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            var obj = enemy.gameObject.GetComponent<IDamageable>();
            obj?.TakeDamage(playerController.playerData.attackDamage);
        }
    }

    
    void Jump()
    {
        playerController.animator.Jump();
        playerController.isGrounded = false;
        playerController.animator.OnGround();
        rb.velocity = new Vector2(rb.velocity.x, playerController.playerData.jumpForce);
        playerController.groundSensor.Disable(0.2f);
    }

    void Run()
    {
        // Reset timer
        playerController.delayToIdle = 0.05f;
        playerController.animator.Run();
    }

    void Idle()
    {
        // Prevents flickering transitions to idle
        playerController.delayToIdle -= Time.deltaTime;
        if (playerController.delayToIdle < 0)
            playerController.animator.Idle();
    }

    void Die()
    {
        playerController.Die();
    }

    public override void Respawn()
    {
        throw new NotImplementedException();
    }
}
