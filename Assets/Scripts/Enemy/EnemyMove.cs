using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform checkGround;
    [SerializeField] private float distance;
    [SerializeField] private bool flip;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkGround.position, Vector2.down, distance);

        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (hit) return;
        flip = !flip;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        speed *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawLine(checkGround.transform.position,checkGround.transform.position + Vector3.down* distance );
        
    }
}
