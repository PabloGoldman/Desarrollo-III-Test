using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] FoxState foxState;
    [SerializeField] HumanState humanState;

    public PlayerData playerData;

    public float currentHealth { get; set; }

    private void Awake()
    {
        currentHealth = playerData.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
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

                humanState.gameObject.transform.position = foxState.gameObject.transform.position;
            }
        }
    }
}
