using UnityEngine;

public class EnemyDie : MonoBehaviour, IDamageable
{
    private EnemyData enemyData;
    private SpriteRenderer sr;
    Collider2D col2D;
    private float timeToDestroy = 1.0f;
    [SerializeField] private Color32 hurtColor;

    private void Awake()
    {
        col2D = GetComponent<Collider2D>();
        enemyData = GetComponent<EnemyData>();
        sr = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float damage)
    {
        enemyData.CurrentHealth -= damage;
        ChangeColor();
        if (enemyData.CurrentHealth <= 0) Die();
    }
    
    private void ChangeColor()
    {
        sr.color = hurtColor;
        Invoke("OriginalColor",0.3f);
    }

    private void OriginalColor()
    {
        sr.color= Color.white;
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
