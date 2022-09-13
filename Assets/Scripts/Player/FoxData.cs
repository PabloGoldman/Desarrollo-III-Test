using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fox", menuName = "ScriptableObject/FoxData")]
public class FoxData : ScriptableObject
{
    public float speed = 4.0f;
    public float jumpForce = 7.5f;
    public float rollForce = 6.0f;
}
