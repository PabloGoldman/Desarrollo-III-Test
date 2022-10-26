using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
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

    public void WallSlide(bool wallSliding)
    {
        animator.SetBool("WallSlide", wallSliding);
    }

    public void OnGround(bool isGrounded)
    {
        animator.SetBool("Grounded", isGrounded);
    }

    public void Attack(int currentAttack)
    {
        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        animator.SetTrigger("Attack" + currentAttack);
    }

    public void Jump(bool isGrounded)
    {
        animator.SetTrigger("Jump");
        animator.SetBool("Grounded", isGrounded);
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }

    public void WallSliding(bool wallSliding)
    {
        animator.SetBool("WallSlide", wallSliding);
    }

    public void Run()
    {
        animator.SetInteger("AnimState", 1);

        //AkSoundEngine.PostEvent("Play_FS_Fox", gameObject);
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
