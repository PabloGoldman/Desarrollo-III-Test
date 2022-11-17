using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] FoxState foxState;
    [SerializeField] HumanState humanState;

    [SerializeField] float timePerSwitch;

    public Checkpoint currentCheckPoint;

    [SerializeField] GameObject smokeParticles;

    [Tooltip("Key Fragments needed to unlock a Key")]
    [SerializeField] int KeyFragmentsPerKey; //Cantidad de fragmentos de llave necesarios para conseguir la llave

    [Tooltip("Price of the Key Fragment")]
    [SerializeField] int SoulsPerKeyFragment; //Precio del fragmento de llave

    [SerializeField] int keyFragments = 0;
    [SerializeField] int soulFragments = 0;

    float switchTimer;

    public PlayerData playerData;

    public static event Action<float> OnHit;
    public static event Action OnBuyKey;

    public float currentHealth { get; set; }

    private void Awake()
    {
        Cursor.visible = false;
        currentHealth = playerData.maxHealth;
        switchTimer = 0;

        //foxState.transform.position = transform.position;
        //humanState.transform.position = transform.position;

        SoulFragment.OnHit += AddSoulFragment;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (soulFragments > 10) soulFragments -= 10;
            else soulFragments = 0;
        }
        OnHit?.Invoke(currentHealth);
    }

    public void AddSoulFragment()
    {
        soulFragments++;
    }

    public bool CanUnlockEndDoor()
    {
        return keyFragments >= KeyFragmentsPerKey;
    }

    public bool BuyFragment()
    {
        if (soulFragments >= SoulsPerKeyFragment)
        {
            soulFragments -= SoulsPerKeyFragment;
            keyFragments++;
            OnBuyKey?.Invoke();
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

                //float offset = 2;
                smokeParticles.transform.position = new Vector3(humanState.transform.position.x, humanState.transform.position.y/* + offset*/);
                smokeParticles.GetComponent<ParticleSystem>().Play();
            }
        }

        switchTimer -= Time.deltaTime;

        switch (keyFragments)
        {
            case 0:
                AkSoundEngine.SetState("EstadosMusica", "Musica1");
                break;
            case 1:
                AkSoundEngine.SetState("EstadosMusica", "Musica1");
                break;
            case 2:
                AkSoundEngine.SetState("EstadosMusica", "Musica2");
                break;
            case 3:
                AkSoundEngine.SetState("EstadosMusica", "Musica2");
                break;
            case 4:
                AkSoundEngine.SetState("EstadosMusica", "Musica3");
                break;
            case 5:
                AkSoundEngine.SetState("EstadosMusica", "Musica3");
                break;
            case 6:
                AkSoundEngine.SetState("EstadosMusica", "Musica4");
                break;
            case 7:
                AkSoundEngine.SetState("EstadosMusica", "Musica4");
                break;

        }



        
    }

}
