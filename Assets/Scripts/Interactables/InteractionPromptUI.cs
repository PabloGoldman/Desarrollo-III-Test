using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI prompText;

    //Cambiar el promp text por un "Already purchased" cuando compras la llave

    [SerializeField] GameObject player;

    public bool isDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        uiPanel.SetActive(false);
    }

    private void LateUpdate()
    {
        transform.localScale = player.transform.localScale;
    }

    public void SetUp(string text)
    {
        prompText.text = text;
        uiPanel.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        isDisplayed = false;
        uiPanel.SetActive(false);
    }
}
