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
            optionsMenu = transform.Find("OptionsPanel").gameObject;
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
            Time.timeScale = 0;
            inGamePause.SetActive(true);
            inPause = true;
        }

        public void Resume()
        {
            Time.timeScale = 1;
            inGamePause.SetActive(false);
            optionsMenu.SetActive(false);
            inPause = false;
        }

        public void ReturnToProfile()
        {
            Resume();
            SceneManager.LoadScene(0);
        }
    }
}

