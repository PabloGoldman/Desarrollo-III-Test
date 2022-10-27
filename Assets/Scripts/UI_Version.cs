using System;
using UnityEngine;
using TMPro;


public class UI_Version : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentVersion;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI soulText;
    [SerializeField] private TextMeshProUGUI keyText;
    private int souls;
    private int keys;
    private float currentLife;
    
    private void Awake()
    {
        keys = 0;
        souls=0;
        currentLife = 100;
        Show();
    }

    private void OnEnable()
    {
        PlayerManager.OnHit += Life;
        SoulFragment.OnHit += Souls;
        PlayerManager.OnBuyKey += Keys;

    }

    private void OnDisable()
    {
        PlayerManager.OnHit -= Life;
        SoulFragment.OnHit -=Souls ;
        PlayerManager.OnBuyKey -= Keys;
    }

    private void Update()
    {
        ShowLife();
    }

    private void Souls()
    {
        souls++;
        soulText.text = ""+souls;
    }
    
    private void Keys()
    {
        keys++;
        keyText.text = ""+keys;
    }
    
    
    private void Life(float life)
    {
        currentLife = life;
    }
    
    private void ShowLife()
    {
        lifeText.text = "" + currentLife;

        if (currentLife <= 0) currentLife = 100;
    }

    private void Show()
    {
        currentVersion.text = "V" + Application.version;
    }
}
