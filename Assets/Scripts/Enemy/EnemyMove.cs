using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform checkGround;
    [SerializeField] private float distance;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkGround.position, Vector2.down, distance);
        
        rb.MovePosition(new Vector2(rb.position.x +speed*Time.deltaTime,rb.position.y));

        if (hit) return;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        speed *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawLine(checkGround.transform.position,checkGround.transform.position + Vector3.down* distance );
        
    }
}
