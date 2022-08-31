using System.Collections;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    [SerializeField] private float timeToBurst;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform groundChecker;
    private const float groundDistance = 10f;
    [SerializeField] private float blastRadius;
    [SerializeField] private int burstDamage;
    private bool isActive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.simulated = false;
        isActive = false;
    }

    private void Update()
    {
        Fall();
    }

    private void Fall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance ,LayerMask.GetMask("Player") );

        if (!hit) return;
        rb.simulated = true;
    }

    IEnumerator Burst()
    {
        yield return  new WaitForSeconds(timeToBurst);
        rb.simulated = false;
        BurstCollision();
        animator.SetTrigger("Destroy");
        yield return  new WaitForSeconds(0.4f);// tiempo que dura la animacion 
        DestroyImmediate(gameObject);
    }

    

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("ground")&& !isActive)
        {
            isActive = true;
            StartCoroutine(Burst());
        }
        else if (c.gameObject.CompareTag("Player")&& !isActive)
        {
            isActive = true;
            rb.simulated = false;
            BurstCollision();
            animator.SetTrigger("Destroy");
            Destroy(gameObject,0.4f);// tiempo que dura la animacion 
        }
        
        
    }

    private void BurstCollision()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, blastRadius, LayerMask.GetMask("Player"));
        
        if (!hitPlayer) return;
        var obj = hitPlayer.gameObject.GetComponent<IDamageable>();
        obj?.TakeDamage(burstDamage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDistance);
        Gizmos.DrawWireSphere(transform.position,blastRadius);
    }
}
