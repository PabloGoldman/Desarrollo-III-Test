using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] FoxState foxState;
    [SerializeField] HumanState humanState;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (humanState.gameObject.activeInHierarchy)
            {
                humanState.SwitchState();

                humanState.gameObject.SetActive(false);

                foxState.gameObject.SetActive(true);

                foxState.gameObject.transform.position = humanState.gameObject.transform.position;
            }
            else
            {
                foxState.SwitchState();

                foxState.gameObject.SetActive(false);

                humanState.gameObject.SetActive(true);

                humanState.gameObject.transform.position = humanState.gameObject.transform.position;
            }
        }
    }
}
