using UnityEngine;

public class Bullet : MonoBehaviour,IDamageable
{
    [SerializeField] private int damage;
    [SerializeField] private ParticleSystem destroy;

    public void TakeDamage(float damage)
    {
       destroy.Play();
    }
    
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
