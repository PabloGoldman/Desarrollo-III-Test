using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "ScriptableObject/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float speed = 4.0f;
    public float jumpForce = 7.5f;
    public float rollForce = 6.0f;
    public float attackDamage = 50f;
    public float attackRate = 2f;
    public float maxLifePoints = 100f;
    public float attackRange = 0.5f;

    public bool noBlood = false;

    public LayerMask enemyLayers;

    [HideInInspector] public float lifePoints;

    public void Start()
    {
        lifePoints = maxLifePoints;
    }
}
