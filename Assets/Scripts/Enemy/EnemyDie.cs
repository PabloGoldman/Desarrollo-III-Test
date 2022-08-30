using System;
using System.Collections;
using UnityEngine;

public class EnemyDie : MonoBehaviour, IDamageable
{
    private EnemyData enemyData;
    private Animator animator;
    Collider2D col2D;
    private float timeToDestroy = 2.0f;

    private void Start()
    {
        col2D = GetComponent<Collider2D>();
        enemyData = GetComponent<EnemyData>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        enemyData.CurrentHealth -= damage;
        animator.SetTrigger("take damage");
        if (enemyData.CurrentHealth <= 0) Die();
    }

    void Die()
    {
        animator.SetTrigger("die");
        col2D.enabled = false;
        enemyData.IsDie = true;
        
        Destroy(gameObject,timeToDestroy);
    }

   
}
