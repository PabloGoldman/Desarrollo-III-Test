using UnityEngine;

public class EnemyDie : MonoBehaviour, IDamageable
{
    private EnemyData enemyData;
    Collider2D col2D;
    private float timeToDestroy = 0.4f;
    [SerializeField]private ParticleSystem hurt;
    

    private void Awake()
    {
        col2D = GetComponent<Collider2D>();
        enemyData = GetComponent<EnemyData>();
    }
    
    public void TakeDamage(float damage)
    {
        enemyData.CurrentHealth -= damage;
        hurt.Play();
        if (enemyData.CurrentHealth <= 0) Die();
    }    
  
    private void Die()
    {
        col2D.enabled = false;
        enemyData.IsDie = true;
        Destroy(gameObject,timeToDestroy);
    }
    
    

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.transform.tag != "Player") return;
            var obj = c.gameObject.GetComponent<IDamageable>();
             obj?.TakeDamage(enemyData.AttackDamage);
    }
}
