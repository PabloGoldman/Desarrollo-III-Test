using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class FoxState : CharacterState, IDamageable
{
    [SerializeField] FoxData foxData;

    int jumpCount = 0;

    float initialGravityScale;

    int maxJumpsAvailable = 1;

    [SerializeField] float timeBetweenJumps = 0.3f;
    float jumpDelay;

    [SerializeField] float timeBetweenRolls = 1f;

    float rollTimer = 1; //Tiempo para que te deje rollear de nuevo

    public bool isRolling { get; private set; }

    private float rollDuration = 8.0f / 14.0f;
    private float rollCurrentTime; //Tiempo en el que estas en el roll

    public override void Awake()
    {
        base.Awake();

        initialGravityScale = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
        if (!isDead)
        {
            HandleInputAndMovement();

            jumpDelay -= Time.deltaTime;

            if (Input.GetKeyDown("space") && jumpCount < maxJumpsAvailable && jumpDelay <= 0)
            {
                Jump();
            }

            else if (Input.GetKeyDown(KeyCode.LeftShift) && rollTimer >= timeBetweenRolls)
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
            HandleTimers();

            if (!isRolling)
            {
                rb.velocity = new Vector2(inputX * foxData.speed, rb.velocity.y);
                rb.gravityScale = initialGravityScale;
            }
            else
            {
                rb.gravityScale = 0;
            }
        }
    }

    public override void SwitchState()
    {
        base.SwitchState();
    }

    public override void Jump()
    {
        base.Jump();

        rb.velocity = new Vector2(rb.velocity.x, foxData.jumpForce);

        jumpDelay = timeBetweenJumps;

        jumpCount++;
        AkSoundEngine.PostEvent("Play_1erSalto_FOX", gameObject);
    }

    public override void Run()
    {
        // Reset timer
        delayToIdle = 0.05f;
        animator.Run();

        if (!isFootsteping && isGrounded)
        {
            StartCoroutine(TriggerFootstepCoroutine());
        }
    }

    IEnumerator TriggerFootstepCoroutine()
    {
        isFootsteping = true;
        AkSoundEngine.PostEvent("Play_FS_Fox", gameObject);
        yield return new WaitForSeconds(0.3f);
        isFootsteping = false;
    }

    public override void Idle()
    {
        base.Idle();
    }

    void Roll()
    {
        animator.Roll();

        isRolling = true;

        rb.velocity = new Vector2(facingDirection * foxData.rollForce, 0.0f);

        rollTimer = 0;

        rollCurrentTime = 0;

        AkSoundEngine.PostEvent("Play_Dash_FOX", gameObject);
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
    
    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            playerManager.TakeDamage(damage * foxData.damageTakenMultiplier);

            if (playerManager.currentHealth <= 0)
            {
                Die();
                Debug.Log("entro" + playerManager.currentHealth);
            }
            else
            {
                animator.Hurt();
                AkSoundEngine.PostEvent("Play_HURT_Fox", gameObject);


            }
        }
    }

    public override void Die()
    {
        base.Die();
        AkSoundEngine.PostEvent("Play_Muerte_FOX", gameObject);
        
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
        base.Respawn();
        AkSoundEngine.PostEvent("Play_Respawn", gameObject);

    }
}
