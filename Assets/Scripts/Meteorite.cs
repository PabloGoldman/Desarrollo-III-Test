using System;
using UnityEngine;

public class Meteorite : MonoBehaviour, IDamageable
{
    public static event Action OnEndGame;
    public void TakeDamage(float damage)
    {
      OnEndGame?.Invoke();
    }
}
