using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Human", menuName = "ScriptableObject/HumanData")]
public class HumanData : ScriptableObject
{
    public float speed = 4.0f;
    public float jumpForce = 7.5f;

    public float attackDamage = 50f;
    public float attackRate = 2f;

    public float attackRange = 0.5f;
}
