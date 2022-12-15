using System;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private SpriteRenderer sr;
    private Animator animator;
    private EnemyData enemyData;
    [SerializeField] private Transform tanuk;
    [SerializeField] private Transform fox;
    [SerializeField] private float TimeToUnfollow;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyData = GetComponent<EnemyData>();
        rb = GetComponent<Rigidbody2D>();
        AkSoundEngine.PostEvent("Play_Abejas", gameObject);
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = true;
    }

    private void OnEnable()
    {
        agent.isStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }


    private void Update()
    {
        if (enemyData.IsDie)
        {
            CancelInvoke();
            agent.isStopped = true;
            
        }
        
        Flip();
        if(tanuk.gameObject.activeInHierarchy) agent.SetDestination( tanuk.position);
        else if (fox.gameObject.activeInHierarchy) agent.SetDestination( fox.position);
    }

    private void Flip()
    {
        if (tanuk.gameObject.activeInHierarchy)
        {
            if (tanuk.position.x > transform.position.x) sr.flipX = true;
            else sr.flipX = false;
        }
        else if (fox.gameObject.activeInHierarchy)
        {
            if (fox.position.x > transform.position.x) sr.flipX = true;
            else sr.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.CompareTag("Player")) return;
        CancelInvoke();
        agent.isStopped = false;
       
    }
    
    private void OnCollisionEnter2D(Collision2D c)
    {
        if (!c.transform.CompareTag("Player")) return;
        animator.SetTrigger("Attack");
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (!c.CompareTag("Player")) return;
        Invoke( nameof(Stop),TimeToUnfollow);
    }

    private void Stop()
    {
        agent.isStopped = true;
    }
}
