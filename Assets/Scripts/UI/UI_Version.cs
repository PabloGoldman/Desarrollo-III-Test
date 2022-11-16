using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class UI_Version : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI soulText;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private GameObject Key;

    private int souls;
    private int keys;
    private float currentLife;
    private bool back;
    private const int AmountKeysToWin=7;
    
    private void Awake()
    {
        keys =0;
        souls=0;
        currentLife = 150;
        lifeText.text = "" + currentLife;      
    }

    private void OnEnable()
    {
        PlayerManager.OnHit += Life;
        SoulFragment.OnHit += Souls;
        PlayerManager.OnBuyKey += Keys;
        FinalDoor.OnUseKey += EnableKey;
        CharacterState.OnDeath += Dead;
    }

    private void OnDisable()
    {
        PlayerManager.OnHit -= Life;
        SoulFragment.OnHit -=Souls ;
        PlayerManager.OnBuyKey -= Keys;
        FinalDoor.OnUseKey -= EnableKey;
        CharacterState.OnDeath -= Dead;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            AkSoundEngine.PostEvent("Stop_Musica", gameObject);
            AkSoundEngine.PostEvent("Stop_Ambiente_V2", gameObject);
            SceneManager.LoadScene("Menu");
            
        }
    }
    private void EnableKey()
    {
        Key.SetActive(false);
    }
    private void Souls()
    {
        souls++;
        soulText.text = ""+souls;
    }

    private void Keys()
    {
        souls -=30;
        soulText.text = ""+souls;
        keys++;
        if (keys == AmountKeysToWin)
        {
            Key.SetActive(true);
            keys = 0;
        }
        
        keyText.text = ""+keys;
    }
    
    
    private void Life(float life)
    {
        currentLife = life;
        if (currentLife <= 0) currentLife = 150;
        lifeText.text = "" + currentLife;
    }

    private void Dead()
    {
        currentLife = 150;
        lifeText.text = "" + currentLife;
    }
    

}
