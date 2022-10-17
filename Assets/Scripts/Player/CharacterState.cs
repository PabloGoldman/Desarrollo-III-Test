using System.Collections;
using UnityEngine;

public abstract class CharacterState : MonoBehaviour
{
    [HideInInspector] public PlayerAnimatorController animator;

    Vector3 firstSpawnPosition;

    protected PlayerManager playerManager;

    [HideInInspector] public Sensor_HeroKnight groundSensor;

    public bool isGrounded;

    [HideInInspector] public float delayToIdle;

    protected float timeToRespawn = 2.0f;

    protected bool isWallSliding = false;

    public bool isDead { get ; protected set; }

    protected Rigidbody2D rb;

    public int facingDirection = 1;

    protected float inputX;

    public virtual void Awake()
    {
        if (!rb)
        {
            firstSpawnPosition = transform.position;

            rb = GetComponent<Rigidbody2D>();

            playerManager = GetComponentInParent<PlayerManager>();

            animator = GetComponent<PlayerAnimatorController>();

            groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        }
    }

    protected void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Die();
        }
    }

    public virtual void Idle()
    {
        // Prevents flickering transitions to idle
        delayToIdle -= Time.deltaTime;
        if (delayToIdle < 0)
            animator.Idle();
    }

    public virtual void SwitchState()
    {
        animator.OnGround(isGrounded);
    }

    public virtual void Die()
    {
        rb.velocity = Vector2.zero;
        animator.Death();
        isDead = true;

        Invoke(nameof(Respawn), timeToRespawn);
    }

    public virtual void Respawn()
    {
        isGrounded = true;
        isDead = false;
        animator.Idle();
            
        if (playerManager.currentCheckPoint)
        {
            transform.position = playerManager.currentCheckPoint.transform.position;
        }
        else
        {
            transform.position = firstSpawnPosition;
        }

        ResetVariables();
    }

    public virtual void Jump()
    {
        animator.Jump(isGrounded);
        isGrounded = false;
        animator.OnGround(isGrounded);
        groundSensor.Disable(0.2f);
    }

    public abstract void Run();

    protected void HandleInputAndMovement()
    {
        // -- Handle input and movement --
        inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            facingDirection = 1;
        }

        else if (inputX < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            facingDirection = -1;
        }
    }

    protected void ResetVariables()
    {
        playerManager.currentHealth = playerManager.playerData.maxHealth;
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            playerManager.currentCheckPoint = collision.GetComponent<Checkpoint>();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            StartCoroutine(DisableColliderCoroutine(collision.gameObject.GetComponent<BoxCollider2D>()));
        }
    }

    protected IEnumerator DisableColliderCoroutine(Collider2D collider)
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, collider.gameObject.layer);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(gameObject.layer, collider.gameObject.layer, false);
    }
}
