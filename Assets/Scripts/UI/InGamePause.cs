using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class InGamePause : MonoBehaviour
    {
        [SerializeField] GameObject inGamePause;
        [SerializeField] GameObject optionsMenu;

        bool inPause = false;

        private void Awake()
        {
            //optionsMenu = transform.Find("OptionsPanel").gameObject;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!inPause)
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
        }

        public void Pause()
        {
            Cursor.visible = true;
            Time.timeScale = 0;

            inGamePause.SetActive(true);
            optionsMenu.SetActive(false);

            inPause = true;
        }

        public void Resume()
        {
            Cursor.visible = false;
            Time.timeScale = 1;
            inGamePause.SetActive(false);
            optionsMenu.SetActive(false);
            inPause = false;
        }

        public void ReturnToMenu()
        {
            AkSoundEngine.PostEvent("Stop_Musica", gameObject);
            AkSoundEngine.PostEvent("Stop_Ambiente_V2", gameObject);
            Resume();
            SceneManager.LoadScene(0);
        }
    }
}

