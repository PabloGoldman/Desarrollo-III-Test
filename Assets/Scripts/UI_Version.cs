using System;
using UnityEngine;
using TMPro;


public class UI_Version : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentVersion;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI soulText;
    private int souls;
    private float currentLife;
    
    private void Awake()
    {
        souls=0;
        currentLife = 100;
        Show();
    }

    private void OnEnable()
    {
        PlayerManager.OnHit += Life;
        SoulFragment.OnHit += Souls;
    }

    private void OnDisable()
    {
        PlayerManager.OnHit -= Life;
        SoulFragment.OnHit -=Souls ;
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
