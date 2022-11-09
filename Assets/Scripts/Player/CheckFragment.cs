using UnityEngine;

public class CheckFragment : MonoBehaviour
{
    [SerializeField] private Transform tanuk;
    [SerializeField] private Transform fox;
    [SerializeField] private Vector3 offSet;
    

    private void Update()
    {
        
        if(tanuk.gameObject.activeInHierarchy)  gameObject.transform.position = tanuk.position+ offSet;
        else if (fox.gameObject.activeInHierarchy)  gameObject.transform.position = fox.position+offSet;
       
    }
    
    
}
