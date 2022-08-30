using UnityEngine;

public class EnemyDie : MonoBehaviour, IDamageable
{
    private EnemyData enemyData;
    private Animator animator;
    Collider2D col2D;
    private float timeToDestroy = 2.0f;

    private void Awake()
    {
        col2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        enemyData= GetComponent<EnemyBehaviour>()._enemyData;
    }

    public void TakeDamage(float damage)
    {
        enemyData.CurrentHealth -= damage;
        animator.SetTrigger("take damage");
        if (enemyData.CurrentHealth <= 0) Die();
    }

    private void Die()
    {
        animator.SetTrigger("die");
        col2D.enabled = false;
        enemyData.IsDie = true;
        
        Destroy(gameObject,timeToDestroy);
    }


}
