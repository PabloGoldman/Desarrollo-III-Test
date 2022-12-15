using UnityEngine;

public class EnemyPlatform : MonoBehaviour
{

    [SerializeField] private float damage;

    private void OnCollisionEnter2D(Collision2D c)
    {
        var obj = c.gameObject.GetComponent<IDamageable>();
        obj?.TakeDamage(damage);
    }
}
