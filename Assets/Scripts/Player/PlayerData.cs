using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "ScriptableObject/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float maxHealth = 100f;

    public LayerMask enemyLayers;
}
