using UnityEngine;

public class EnemyDie : MonoBehaviour, IDamageable
{
    private EnemyData enemyData;
    private Collider2D col2D;
    private SpriteRenderer mr;
    private float timeToDestroy = 6;
    [SerializeField]private ParticleSystem hurt;
    [SerializeField] private ParticleSystem splashLeft;
    [SerializeField] private ParticleSystem splashRight;
    

    private void Awake()
    {
        col2D = GetComponent<Collider2D>();
        mr = GetComponent<SpriteRenderer>();
        enemyData = GetComponent<EnemyData>();
    }
    
    public void TakeDamage(float damage)
    {
        enemyData.CurrentHealth -= damage;
        SpawnFragment();
        hurt.Play();
        if (enemyData.CurrentHealth <= 0) Die();
    }

    private void SpawnFragment()
    {
        if (enemyData.CurrentHealth < 30)
        {
            splashLeft.subEmitters.SetSubEmitterEmitProbability(0,1);
            splashRight.subEmitters.SetSubEmitterEmitProbability(0,1);
        }
    }
    
    private void Die()
    {
        col2D.enabled = false;
        mr.enabled = false;
        enemyData.IsDie = true;
        Invoke("SetActive",timeToDestroy);
    }
    private void SetActive()
    {    
        gameObject.SetActive(!enemyData.IsDie);
    }
    

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.transform.tag != "Player") return;
            var obj = c.gameObject.GetComponent<IDamageable>();
             obj?.TakeDamage(enemyData.AttackDamage);
    }
}
