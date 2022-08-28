using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float maxHealth = 100;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        //Animacion de que recibe daño

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Animacion de morir 
        gameObject.SetActive(false);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
