using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private SpriteRenderer sr;
    [SerializeField] private Transform tanuk;
    [SerializeField] private Transform fox;
    [SerializeField] private float TimeToUnfollow;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = true;
    }

    private void Update()
    {
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
