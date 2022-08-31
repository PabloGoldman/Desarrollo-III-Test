using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public PlayerData playerData;

    PlayerAnimatorController animator;

    Collider2D col2D;

    bool isDead;

    private float timeToDestroy = 2.0f;

    [SerializeField] Transform attackPoint;

    [SerializeField] GameObject slideDust;

    private Rigidbody2D rb;
    private Sensor_HeroKnight groundSensor;
    private Sensor_HeroKnight wallSensorR1;
    private Sensor_HeroKnight wallSensorR2;
    private Sensor_HeroKnight wallSensorL1;
    private Sensor_HeroKnight wallSensorL2;

    private int facingDirection = 1;
    private float timeSinceAttack = 0.0f;
    private float rollDuration = 8.0f / 14.0f;
    private float rollCurrentTime;

    float inputX;

    public float delayToIdle { get; private set; }
    public int currentAttack { get; private set; }
    public bool isWallSliding { get; private set; }
    public bool isGrounded { get; private set; }
    public bool isRolling { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<PlayerAnimatorController>();

        groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerData.Start();
    }

    void Update()
    {
        if (!isDead)
        {
            HandleTimers();
            CheckIsGrounded();
            HandleInputAndMovement();
            WallSlide();

            //Attack
            if (Input.GetMouseButtonDown(0) && timeSinceAttack > 0.25f && !isRolling)
            {
                Attack();
            }

            // Block
            else if (Input.GetMouseButtonDown(1) && !isRolling)
            {
                Block();
            }

            else if (Input.GetMouseButtonUp(1))
            {
                StopBlocking();
            }

            // Roll
            else if (Input.GetKeyDown("left shift") && !isRolling && !isWallSliding)
            {
                Roll();
            }

            //Jump
            else if (Input.GetKeyDown("space") && (isGrounded || isWallSliding) && !isRolling)
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
    }

    void HandleTimers()
    {
        // Increase timer that controls attack combo
        timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if (isRolling)
            rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (rollCurrentTime > rollDuration)
            isRolling = false;
    }

    void CheckIsGrounded()
    {
        //Check if character just landed on the ground
        if (!isGrounded && groundSensor.IsColliding())
        {
            isGrounded = true;
            animator.OnGround();
        }

        //Check if character just started falling
        if (isGrounded && !groundSensor.IsColliding())
        {
            isGrounded = false;
            animator.OnGround();
        }
    }

    void HandleInputAndMovement()
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

        // Move
        if (!isRolling)
            rb.velocity = new Vector2(inputX * playerData.speed, rb.velocity.y);
    }

    void WallSlide()
    {
        //Wall Slide
        isWallSliding = (wallSensorR1.IsColliding() && wallSensorR2.IsColliding()) || (wallSensorL1.IsColliding() && wallSensorL2.IsColliding());
        animator.WallSlide();
    }

    void Attack()
    {
        currentAttack++;

        // Loop back to one after third attack
        if (currentAttack > 3)
            currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (timeSinceAttack > 1.0f)
            currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        animator.Attack();

        // Reset timer
        timeSinceAttack = 0.0f;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, playerData.attackRange, playerData.enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            var obj = enemy.gameObject.GetComponent<IDamageable>();
            obj?.TakeDamage(playerData.attackDamage);
        }
    }

    void Block()
    {
        animator.Block();
    }

    void StopBlocking()
    {
        animator.StopBlocking();
    }

    void Roll()
    {
        isRolling = true;
        animator.Roll();
        rb.velocity = new Vector2(facingDirection * playerData.rollForce, rb.velocity.y);
    }

    void Jump()
    {
        animator.Jump();
        isGrounded = false;
        animator.OnGround();
        rb.velocity = new Vector2(rb.velocity.x, playerData.jumpForce);
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

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            animator.Hurt();

            playerData.currentHealth -= damage;
            if (playerData.currentHealth <= 0) Die();
        }
    }

    void Die()
    {
        animator.Death();
        isDead = true;
        Destroy(gameObject, timeToDestroy);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null || playerData == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, playerData.attackRange);
    }

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (facingDirection == 1)
            spawnPosition = wallSensorR2.transform.position;
        else
            spawnPosition = wallSensorL2.transform.position;

        if (slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(facingDirection, 1, 1);
        }
    }
}
