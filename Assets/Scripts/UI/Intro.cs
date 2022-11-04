using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private void Awake()
    {
        Invoke("InitGame", 40);
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
