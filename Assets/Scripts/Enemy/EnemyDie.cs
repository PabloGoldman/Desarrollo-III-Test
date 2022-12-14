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
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        mr = GetComponent<SpriteRenderer>();
        enemyData = GetComponent<EnemyData>();
    }
    
    public void TakeDamage(float damage)
    {
        enemyData.CurrentHealth -= damage;
        SpawnFragment();
        animator.SetTrigger("Hurt");
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
        rb.velocity = new Vector2( 0 , 0);
        col2D.enabled = false;
        mr.enabled = false;
        enemyData.IsDie = true;
        Invoke("SetActive",timeToDestroy);
    }
    private void SetActive()
    {    
        splashLeft.subEmitters.SetSubEmitterEmitProbability(0,0);
        splashRight.subEmitters.SetSubEmitterEmitProbability(0,0);
        enemyData.ReSpawn = true;
        gameObject.SetActive(false);
    }
    
    

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.transform.tag != "Player") return;
            var obj = c.gameObject.GetComponent<IDamageable>();
             obj?.TakeDamage(enemyData.AttackDamage);
    }
}
