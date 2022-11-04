using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentVersion;
    [SerializeField] private GameObject startGame;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject credits;

    private bool activeOptions;
    private bool activeCredits;
    private bool activeStartGame;

    private void Awake()
    {
        activeCredits = false;
        activeOptions = false;
        activeStartGame = false;
         Show();
    }

    public void StarGame()
    {
        activeStartGame = !activeStartGame;
        startGame.SetActive(activeStartGame);
    }
    
    public void Options()
    {
        activeOptions = !activeOptions;
        options.SetActive(activeOptions);
    }
    
    public void Credits()
    {
        activeCredits = !activeCredits;
        credits.SetActive(activeCredits);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Intro");
    }
    
    private void Show()
    {
            currentVersion.text = "V" + Application.version;
    }
}
