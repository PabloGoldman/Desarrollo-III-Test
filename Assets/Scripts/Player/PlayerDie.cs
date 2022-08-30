
using UnityEngine;

public class PlayerDie : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
       Debug.Log("recibo daño ");
    }
}
