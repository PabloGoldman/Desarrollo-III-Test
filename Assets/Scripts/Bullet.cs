
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnCollisionEnter2D(Collision2D c)
    {
        switch (c.gameObject.tag)
        {
            case"ground":
                Destroy(gameObject);
                break;
            
            case"Player":
                var obj = c.gameObject.GetComponent<IDamageable>();
                obj?.TakeDamage(damage);
                Destroy(gameObject);
                break;
        }
       
    }
}
