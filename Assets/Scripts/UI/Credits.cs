using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
    }

    public void Back()
    {
        AkSoundEngine.PostEvent("Play_UI_ENTER1", gameObject);
        SceneManager.LoadScene("Menu");
    }
    
}
