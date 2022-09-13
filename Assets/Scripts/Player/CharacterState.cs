using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState : MonoBehaviour
{
    [HideInInspector] public Sensor_HeroKnight groundSensor;
    [HideInInspector] public Sensor_HeroKnight wallSensorR1;
    [HideInInspector] public Sensor_HeroKnight wallSensorR2;
    [HideInInspector] public Sensor_HeroKnight wallSensorL1;
    [HideInInspector] public Sensor_HeroKnight wallSensorL2;

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public float delayToIdle;

    protected float timeToRespawn = 2.0f;

    protected bool isWallSliding = false;

    protected bool isDead;

    protected Rigidbody2D rb;

    protected static int lifePoints;

    public int facingDirection = 1;

    protected float inputX;

    public abstract void SwitchState();

    public abstract void Respawn();

    protected void OnAwake()
    {
        rb = GetComponent<Rigidbody2D>();

        groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    }

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

    // Animation Events
    // Called in slide animation.
    //void AE_SlideDust()
    //{
    //    Vector3 spawnPosition;

    //    if (facingDirection == 1)
    //        spawnPosition = wallSensorR2.transform.position;
    //    else
    //        spawnPosition = wallSensorL2.transform.position;

    //    if (slideDust != null)
    //    {
    //        // Set correct arrow spawn position
    //        GameObject dust = Instantiate(slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
    //        // Turn arrow in correct direction
    //        dust.transform.localScale = new Vector3(facingDirection, 1, 1);
    //    }
    //}
}