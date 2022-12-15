using UnityEngine;

public class EnemyStalactite : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Transform groundChecker;
    [SerializeField]private ParticleSystem hurt;
    private const float groundDistance = 15f;
    [SerializeField] private int damage;
    private bool isActive;
    private Collider2D col2D;
    private SpriteRenderer mr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col2D = GetComponent<BoxCollider2D>();
        mr = GetComponent<SpriteRenderer>();
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
        animator.SetTrigger("Fall");
        rb.simulated = true;
    }
    private void DestroyStalactite()
    {
        isActive = true;
        rb.simulated = false;
        animator.SetTrigger("Hurt");
        hurt.Play();
        mr.enabled = false;
        col2D.enabled = false;
        Destroy(gameObject,5);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("ground") && !isActive)
        {
            AkSoundEngine.PostEvent("Play_HURT_TK", gameObject);
            DestroyStalactite();
        }
        if (!c.gameObject.CompareTag("Player") || isActive) return;
        var obj = c.gameObject.GetComponent<IDamageable>();
        obj?.TakeDamage(damage);
        DestroyStalactite();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDistance);
    }
}
