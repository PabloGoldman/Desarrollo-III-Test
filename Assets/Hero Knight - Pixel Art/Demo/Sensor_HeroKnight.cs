using UnityEngine;
using System.Collections;

public class Sensor_HeroKnight : MonoBehaviour
{
    public int colCount = 0;

    private float disableTimer;

    [SerializeField] LayerMask groundLayer;

    private void OnEnable()
    {
        colCount = 0;
    }

    public bool IsColliding() //Chequea si hubo alguna colision, si hubo, setea el estado en true
    {
        if (disableTimer > 0)
            return false;
        return colCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            colCount++;
        }

        //if (!other.gameObject.CompareTag("MapCollider"))
        //{
        //}
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            colCount--;
        }

        //if (!other.gameObject.CompareTag("MapCollider"))
        //{
        //    colCount--;
        //}
    }

    void Update()
    {
        disableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        disableTimer = duration;
    }
}
