using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevel : MonoBehaviour
{
    [SerializeField] private GameObject enemies;
    private bool state;

    public void Enemies()
    {
        state = !state;
        enemies.SetActive(state);
    }
    
    public void Reload()
    {
         SceneManager.LoadScene("Sebastian");
    }    
    
    public void Exit()
    {
         Application.Quit();
    }
    
    
}
