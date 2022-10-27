using System;
using UnityEngine;
using TMPro;


public class UI_Version : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentVersion;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI soulText;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private GameObject background;
    
    private int souls;
    private int keys;
    private float currentLife;
    private bool back;
    
    private void Awake()
    {
        keys = 0;
        souls=0;
        currentLife = 100;
        lifeText.text = "" + currentLife;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            back = !back;
            background.SetActive(back);
        }
    }

    private void Souls()
    {
        souls++;
        soulText.text = ""+souls;
    }

    private void Keys()
    {
        souls -=30 ;
        soulText.text = ""+souls;
        keys++;
        keyText.text = ""+keys;
    }
    
    
    private void Life(float life)
    {
        currentLife = life;
        if (currentLife <= 0) currentLife = 100;
        lifeText.text = "" + currentLife;
    }

    private void Show()
    {
        currentVersion.text = "V" + Application.version;
    }
}
