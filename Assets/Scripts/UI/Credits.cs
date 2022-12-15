using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private float timeToExit;
    private void Awake()
    {
        Cursor.visible = true;
        Invoke(nameof(Back),timeToExit);
    }
    public void Back()
    {
        AkSoundEngine.PostEvent("Play_UI_ENTER1", gameObject);
        SceneManager.LoadScene("Menu");
    }
    
}
