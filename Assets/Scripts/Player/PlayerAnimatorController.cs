using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AirSpeed();
    }

    public void AirSpeed()
    {
        //Set AirSpeed in animator
        animator.SetFloat("AirSpeedY", rb.velocity.y);
    }

    public void WallSlide()
    {
        animator.SetBool("WallSlide", playerController.isWallSliding);
    }

    public void OnGround()
    {
        animator.SetBool("Grounded", playerController.isGrounded);
    }

    public void Attack()
    {
        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        animator.SetTrigger("Attack" + playerController.currentAttack);
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
        animator.SetBool("Grounded", playerController.isGrounded);
    }

    public void Death()
    {
        animator.SetBool("noBlood", playerController.playerData.noBlood);
        animator.SetTrigger("Death");
    }

    public void WallSliding()
    {
        animator.SetBool("WallSlide", playerController.isWallSliding);
    }

    public void Run()
    {
        animator.SetInteger("AnimState", 1);
    }

    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void Block()
    {
        animator.SetTrigger("Block");
        animator.SetBool("IdleBlock", true);
    }

    public void StopBlocking()
    {
        animator.SetBool("IdleBlock", false);
    }

    public void Roll()
    {
        animator.SetTrigger("Roll");
    }

    public void Idle()
    {
        animator.SetInteger("AnimState", 0);
    }
}
