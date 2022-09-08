using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FoxState : CharacterState
{
    // Update is called once per frame
    public override void Update()
    {
        Jump();
    }

    public override void SwitchState()
    {
        //throw new System.NotImplementedException();
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
