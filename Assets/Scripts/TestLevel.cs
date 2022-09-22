using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevel : MonoBehaviour
{
    [SerializeField] private GameObject enemies;
    private bool state;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F1))
        {
            state = !state;
            enemies.SetActive(state);
        }

        if (Input.GetKeyDown(KeyCode.F2))
            SceneManager.LoadScene("Sebastian");
        
        if (Input.GetKey(KeyCode.F4))
            Application.Quit();
    }
    
    
}
