using System;
using UnityEngine;
using TMPro;


public class UI_Version : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentVersion;
    [SerializeField] private TextMeshProUGUI lifeText;
    private float currentLife;
    
    private void Awake()
    {
        currentLife = 100;
        Show();
    }

    private void OnEnable()
    {
        PlayerManager.OnHit += Life;
    }

    private void OnDisable()
    {
        PlayerManager.OnHit -= Life;
    }

    private void Update()
    {
        ShowLife();
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
