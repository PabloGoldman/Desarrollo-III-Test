using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IDamageable
{
    public PlayerData playerData;

    [HideInInspector] public PlayerAnimatorController animator;

    public UnityEvent onThirdAttack;

    Vector3 spawnPosition;

    bool isDead;

    private float timeToRespawn = 2.0f;

    public Transform attackPoint;

    [SerializeField] GameObject slideDust;

    CharacterState characterState;

    [SerializeField] FoxState foxState;
    [SerializeField] HumanState humanState;

    private Rigidbody2D rb;

    [HideInInspector] public Sensor_HeroKnight groundSensor;
    [HideInInspector] public Sensor_HeroKnight wallSensorR1;
    [HideInInspector] public Sensor_HeroKnight wallSensorR2;
    [HideInInspector] public Sensor_HeroKnight wallSensorL1;
    [HideInInspector] public Sensor_HeroKnight wallSensorL2;

    private int facingDirection = 1;
    private float rollDuration = 8.0f / 14.0f;
    private float rollCurrentTime;

    float inputX;

    [HideInInspector] public bool isGrounded { get; set; }
    [HideInInspector] public float delayToIdle { get; set; }

    public bool isWallSliding { get; private set; }
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

        humanState.rb = this.rb;
        foxState.rb = this.rb;

        humanState.playerController = this;
        foxState.playerController = this;

        characterState = humanState;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerData.Start();

        spawnPosition = transform.position;
    }

    void Update()
    {
        if (!isDead)
        {
            characterState.Update();

            HandleTimers();
            CheckIsGrounded();
            HandleInputAndMovement();
            WallSlide();

            if (Input.GetKeyDown("left shift") && !isRolling && !isWallSliding)
            {
                Roll();
            }

            //HACKS
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                Die();
            }
        }
    }

    public void TriggerThirdAttack()
    {
        onThirdAttack?.Invoke();
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

    void Roll()
    {
        isRolling = true;
        animator.Roll();
        rb.velocity = new Vector2(facingDirection * playerData.rollForce, rb.velocity.y);
    }

    //void Jump()
    //{
    //    animator.Jump();
    //    isGrounded = false;
    //    animator.OnGround();
    //    rb.velocity = new Vector2(rb.velocity.x, playerData.jumpForce);
    //    groundSensor.Disable(0.2f);
    //}

    //void Run()
    //{
    //    // Reset timer
    //    delayToIdle = 0.05f;
    //    animator.Run();
    //}

    //void Idle()
    //{
    //    // Prevents flickering transitions to idle
    //    delayToIdle -= Time.deltaTime;
    //    if (delayToIdle < 0)
    //        animator.Idle();
    //}

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            animator.Hurt();

            playerData.currentHealth -= damage;
            if (playerData.currentHealth <= 0) Die();
            Debug.Log("entro" + playerData.currentHealth);
        }
    }

    public void Die()
    {
        animator.Death();
        isDead = true;

        Invoke(nameof(Respawn), timeToRespawn);
        //Destroy(gameObject, timeToDestroy);
    }

    void Respawn()
    {
        transform.position = spawnPosition;
        isDead = false;
        animator.Idle();
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
