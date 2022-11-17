using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentVersion;    
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject howToPlay;

    private bool activeOptions;
    private bool activeHowToPlay;
    
    private void Awake()
    {
        Cursor.visible = true;
        AkSoundEngine.PostEvent("Play_Musica_Menu", gameObject);
        activeOptions = false;        
        activeHowToPlay=false;
        Show();
    }

    public void HowToPlay()
    {
       AkSoundEngine.PostEvent("Play_UI_ENTER1", gameObject);
        activeHowToPlay = !activeHowToPlay;
        howToPlay.SetActive(activeHowToPlay);
    }

    public void StarGame()
    {
        AkSoundEngine.PostEvent("Play_UI_ENTER1", gameObject);
        AkSoundEngine.PostEvent("Stop_Musica_Menu", gameObject);
        SceneManager.LoadScene("Intro");

    }
    
    public void Options()
    {
        AkSoundEngine.PostEvent("Play_UI_ENTER1", gameObject);
        activeOptions = !activeOptions;
        options.SetActive(activeOptions);
    }
    
    public void Credits()
    {
        AkSoundEngine.PostEvent("Play_UI_ENTER1", gameObject);
        AkSoundEngine.PostEvent("Stop_Musica_Menu", gameObject);
        SceneManager.LoadScene("Credits");
    }

    public void Exit()
    {
        AkSoundEngine.PostEvent("Play_UI_ENTER1", gameObject);
        Application.Quit();
    }

   
    
    private void Show()
    {
        currentVersion.text = "V" + Application.version;
    }
}
