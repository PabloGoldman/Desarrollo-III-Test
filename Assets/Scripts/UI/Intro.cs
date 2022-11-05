using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private float TimeToChangeScene;
    private void Awake()
    {
  
        Invoke("InitGame", TimeToChangeScene);
    }

    private void InitGame()
    {
        SceneManager.LoadScene("Sebastian");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) SceneManager.LoadScene("Sebastian");
    }
}
