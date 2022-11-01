using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class HumanState : CharacterState, IDamageable
{
    [SerializeField] HumanData humanData;

    [SerializeField] Transform attackPoint;

    public UnityEvent onThirdAttack;

    private float timeSinceAttack = 0.0f;

    private int currentAttack;

    public override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        OnUpdate();
        if (!isDead)
        {
            if (timeSinceAttack > 0.4f)
            {
                HandleInputAndMovement();
            }

            CheckIsGrounded();

            rb.velocity = new Vector2(inputX * humanData.speed, rb.velocity.y);
            // Increase timer that controls attack combo
            timeSinceAttack += Time.deltaTime;

            //Attack
            if (Input.GetMouseButtonDown(0) && timeSinceAttack > 0.5f)
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

    void Attack()
    {
        AkSoundEngine.PostEvent("Play_Ataque_TK", gameObject);
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
        {
            currentAttack = 1;          
        }

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
        //onThirdAttack?.Invoke();
    }

    public override void SwitchState()
    {
        base.SwitchState();
    }

    public override void Jump()
    {
        base.Jump();
        rb.velocity = new Vector2(rb.velocity.x, humanData.jumpForce);
        AkSoundEngine.PostEvent("Play_Salto_TK", gameObject);
    }

    public override void Run()
    {
        // Reset timer
        delayToIdle = 0.05f;
        animator.Run();

        if (!isFootsteping)
        {
            StartCoroutine(TriggerFootstepCoroutine());
        }
    }

    IEnumerator TriggerFootstepCoroutine()
    {
        isFootsteping = true;
        AkSoundEngine.PostEvent("Play_FS_TK", gameObject);
        yield return new WaitForSeconds(0.3f);
        isFootsteping = false;
    }

    public override void Idle()
    {
        base.Idle();
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

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            playerManager.TakeDamage(damage);
            

            if (playerManager.currentHealth <= 0)
            {
                Die();
                AkSoundEngine.PostEvent("Play_Die_TK", gameObject);
                Debug.Log("morir" + playerManager.currentHealth);
            }
            else
            {
                AkSoundEngine.PostEvent("Play_HURT_TK", gameObject);
                animator.Hurt();
            }
        }
    }

    public override void Die()
    {
        base.Die();
    }

    public override void Respawn()
    {
        base.Respawn();
        AkSoundEngine.PostEvent("Play_Respawn", gameObject);

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
