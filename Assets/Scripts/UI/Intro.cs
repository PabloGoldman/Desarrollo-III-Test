using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private float TimeToChangeScene;
    private void Awake()
    {               
        Invoke("InitGame", TimeToChangeScene);
    }
    private void Start()
    {
        AkSoundEngine.PostEvent("Play_intro", gameObject);
    }

    public void InitGame()
    {
        AkSoundEngine.PostEvent("Stop_intro", gameObject);
        SceneManager.LoadScene("GamePlay");
    }

}
