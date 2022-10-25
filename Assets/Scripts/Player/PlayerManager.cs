using UnityEngine;
using System;
public class PlayerManager : MonoBehaviour
{
    [SerializeField] FoxState foxState;
    [SerializeField] HumanState humanState;

    [SerializeField] float timePerSwitch;

    public Checkpoint currentCheckPoint;

    [SerializeField] GameObject smokeParticles;

    [SerializeField] int FragmentsPerKey; //Cantidad de fragmentos de llave necesarios para conseguir la llave
    [SerializeField] int KeyFragmentPrice; //Precio del fragmento de llave

    [SerializeField] int keyFragments = 0;
    [SerializeField] int soulFragments = 0;

    float switchTimer;

    public PlayerData playerData;
    public static event Action<float> OnHit;
    public float currentHealth { get; set; }

    private void Awake()
    {
        currentHealth = playerData.maxHealth;
        switchTimer = 0;

        foxState.transform.position = transform.position;
        humanState.transform.position = transform.position;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnHit?.Invoke(currentHealth);
    }

    public bool CanUnlockEndDoor()
    {
        if (keyFragments >= FragmentsPerKey)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool BuyFragment()
    {
        if (soulFragments >= KeyFragmentPrice)
        {
            soulFragments -= KeyFragmentPrice;
            keyFragments++;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if (switchTimer < 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (humanState.gameObject.activeInHierarchy && !humanState.isDead)
                {

                    humanState.gameObject.SetActive(false);

                    foxState.gameObject.SetActive(true);

                    foxState.SwitchState();

                    foxState.gameObject.transform.position = humanState.gameObject.transform.position;

                    foxState.gameObject.transform.localScale = humanState.gameObject.transform.localScale;
                }
                else if (!foxState.isDead)
                {

                    foxState.gameObject.SetActive(false);

                    humanState.gameObject.SetActive(true);

                    humanState.SwitchState();

                    humanState.gameObject.transform.position = foxState.gameObject.transform.position;

                    humanState.gameObject.transform.localScale = foxState.gameObject.transform.localScale;
                }

                switchTimer = timePerSwitch;

                float offset = 1;
                smokeParticles.transform.position = new Vector3(humanState.transform.position.x, humanState.transform.position.y + offset);
                smokeParticles.GetComponent<ParticleSystem>().Play();
            }
        }

        switchTimer -= Time.deltaTime;
    }

}
