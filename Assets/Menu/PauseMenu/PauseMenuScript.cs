using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu.PauseMenu
{
    public class PauseMenuScript : MonoBehaviour
    {
        [SerializeField] private Canvas pauseCanvas;
        [SerializeField] private Canvas hitCanvas;


        private bool isPaused;
        // Start is called before the first frame update
        void Start()
        {
            isPaused = false;
            //pauseCanvas.SetActive(false);
            pauseCanvas.enabled = false;
            hitCanvas.enabled = true;
            ShowCursor.mouseInvisible();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    resume();
                }
                else
                {
                    pause();
                }
            }
        }

        public void resume()
        {
            //pauseCanvas.SetActive(false);
            pauseCanvas.enabled = false;
            hitCanvas.enabled = true;
            ShowCursor.mouseInvisible();
            Time.timeScale = 1f;
            isPaused = false;
            Debug.Log("Resume game ...");
        }

        private void pause()
        {
            //pauseCanvas.SetActive(true);
            if (!InteractCanvas.interacting && InteractCanvas.showPauseMenu)
            {
                pauseCanvas.enabled = true;
                hitCanvas.enabled = false;
                ShowCursor.mouseVisible();
                Time.timeScale = 0.1f;
                isPaused = true;
            }
        }

        public void loadMenu()
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1f;
            Debug.Log("Load Menu ...");
        }
    }
}
