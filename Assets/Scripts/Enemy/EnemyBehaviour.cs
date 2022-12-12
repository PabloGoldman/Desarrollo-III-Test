using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private TypeOfMovement typeOfMovement;
    [SerializeField] private TypeOfAttack  typeOfAttack;

    protected bool isFootsteping = false;

    private void Awake()
    {
       SetTypeOfMovement();
        SetTypeOfAttack();
    }

    private void Update()
    {
        if (!isFootsteping)
        {
            StartCoroutine(TriggerFootstepCoroutine());
        }
    }

    protected IEnumerator TriggerFootstepCoroutine()
    {
        isFootsteping = true;
        //AkSoundEngine.PostEvent("Play_FS_TK", gameObject);
        yield return new WaitForSeconds(0.3f);
        isFootsteping = false;
    }

    private void SetTypeOfMovement()
    {
        switch (typeOfMovement)
        {
            case TypeOfMovement.Patrol:
                gameObject.AddComponent<PatrolEnemy>();
                break;
            
            case TypeOfMovement.Flying:
               // gameObject.AddComponent<FlyingEnemy>();
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
                gameObject.AddComponent<DistanceAttack>();
                break;
            
            case TypeOfAttack.Flying:
                gameObject.AddComponent<MeleeFlying>();
                break;

        }
    }
}
