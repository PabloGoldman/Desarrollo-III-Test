using System.Collections;
using UnityEngine;

public class EnemyStalactite : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Transform groundChecker;
    private const float groundDistance = 15f;
    [SerializeField] private int damage;
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
    private void DestroyStalactite()
    {
        isActive = true;
        rb.simulated = false;
        animator.SetTrigger("Destroy");
        Destroy(gameObject,0.4f);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("ground")&& !isActive)
            DestroyStalactite();

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
