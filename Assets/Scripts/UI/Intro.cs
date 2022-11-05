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

    private void InitGame()
    {
        AkSoundEngine.PostEvent("Stop_intro", gameObject);
        SceneManager.LoadScene("Sebastian");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) SceneManager.LoadScene("Sebastian");
    }
}
