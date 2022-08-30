using UnityEngine;

public class PlayerDie : MonoBehaviour, IDamageable
{
    Collider2D col2D;

    private float timeToDestroy = 2.0f;

    private void Start()
    {
        col2D = GetComponent<Collider2D>();
    }

    public void TakeDamage(float damage)
    {
       Debug.Log("recibo daño ");
    }

    private void Die()
    {
        col2D.enabled = false;

        Destroy(gameObject, timeToDestroy);
    }
}
