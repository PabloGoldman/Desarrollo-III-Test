using System;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    [SerializeField] private Transform checkPlayer;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;
    private Rigidbody2D rb;
    private EnemyData enemyData;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    private void Start()
    {
        enemyData = GetComponent<EnemyData>();
    }

    private void Update()
    {
       Follow();
    }

    private void Follow()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPlayer.position, (Vector2.left*enemyData.RayDirection), distance,layer);

        if (hit)
        {
            enemyData.RayDirection *= -1;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
            enemyData.Speed *= -1;
        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.green;
        //Gizmos.DrawLine(checkPlayer.transform.position,checkPlayer.transform.position + (Vector3.left*enemyData.RayDirection)* distance);
    }
}
