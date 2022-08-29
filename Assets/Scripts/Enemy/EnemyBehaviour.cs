using UnityEngine;

enum TypeOfMovement { Patrol, Flying}
enum TypeOfAttack { Melee, Distance}
    
public class EnemyBehaviour : MonoBehaviour
{
   [SerializeField] private TypeOfMovement typeOfMovement;
   [SerializeField] private TypeOfAttack  typeOfAttack;

    private void Awake()
    {
        SetTypeOfMovement();
        SetTypeOfAttack();
    }

    private void SetTypeOfMovement()
    {
        switch (typeOfMovement)
        {
            case TypeOfMovement.Patrol:
                gameObject.AddComponent<PatrolEnemy>();
                break;
            
            case TypeOfMovement.Flying:
                //en desarrollo
                break;
        }
    }
    
    private void SetTypeOfAttack()
    {
        switch (typeOfAttack)
        {
            case TypeOfAttack.Melee:
                gameObject.AddComponent<MeleeAttack>();
                break;
            
            case TypeOfAttack.Distance:
                //en desarrollo
                break;
        }
    }

   

    

    
}
