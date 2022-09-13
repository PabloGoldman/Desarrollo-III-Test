﻿using UnityEngine;
using System.Collections;

public class Sensor_HeroKnight : MonoBehaviour
{
    private int colCount = 0;

    private float disableTimer;

    private void OnEnable()
    {
        colCount = 0;
    }

    public bool IsColliding() //Chequea si hubo alguna colision, si hubo, setea el estado en true
    {
        if (disableTimer > 0)
            return false;
        return colCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        colCount++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        colCount--;
    }

    void Update()
    {
        disableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        disableTimer = duration;
    }
}
