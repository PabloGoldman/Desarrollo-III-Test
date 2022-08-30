using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyData _enemyData; 

    private void Awake()
    {
        _enemyData.Init();
        _enemyData.GroundChecker = transform.Find("CheckGround");
        _enemyData.PointOfView= transform.Find("FieldOfView");
        
        SetTypeOfMovement();
        SetTypeOfAttack();
    }

    private void SetTypeOfMovement()
    {
        switch (_enemyData.TypeOfMovement)
        {
            case TypeOfMovement.Patrol:
                gameObject.AddComponent<PatrolEnemy>().Init(_enemyData);;
                break;
            
            case TypeOfMovement.Flying:
                //en desarrollo
                break;
        }
    }
    
    private void SetTypeOfAttack()
    {
        switch (_enemyData.TypeOfAttack)
        {
            case TypeOfAttack.Melee:
                gameObject.AddComponent<MeleeAttack>().Init(_enemyData);
                break;
            
            case TypeOfAttack.Distance:
                //en desarrollo
                break;
            
            case TypeOfAttack.Burst:
                gameObject.AddComponent<BurstAttack>().Init(_enemyData);;
                break;
        }
    }

   

    

    
}
