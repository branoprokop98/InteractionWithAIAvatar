using Menu.ExitGame;
using Menu.NewGame;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MenuService : MonoBehaviour
    {
        [SerializeField] private Canvas mainMenuCanvas;
        [SerializeField] private Canvas newGameCanvas;
        [SerializeField] private Canvas loadGameCanvas;
        [SerializeField] private Canvas settingsGameCanvas;
        private NewGameController newGame;
        private ExitGameController exitGameController;


        // Start is called before the first frame update
        void Start()
        {
            newGame = new NewGameController(newGameCanvas);
            exitGameController = new ExitGameController();
            mainMenuCanvas.enabled = true;
            newGameCanvas.enabled = false;
            loadGameCanvas.enabled = false;
            settingsGameCanvas.enabled = false;
        }

        public void getNewGameCanvas()
        {
            mainMenuCanvas.enabled = false;
            newGameCanvas.enabled = true;
            loadGameCanvas.enabled = false;
            settingsGameCanvas.enabled = false;
        }

        public void getMainMenuCanvas()
        {
            mainMenuCanvas.enabled = true;
            newGameCanvas.enabled = false;
            loadGameCanvas.enabled = false;
            settingsGameCanvas.enabled = false;
        }
        public void getLoadGameCanvas()
        {
            mainMenuCanvas.enabled = false;
            newGameCanvas.enabled = false;
            loadGameCanvas.enabled = true;
            settingsGameCanvas.enabled = false;
        }

        public void getGameSettingsCanvas()
        {
            mainMenuCanvas.enabled = false;
            newGameCanvas.enabled = false;
            loadGameCanvas.enabled = false;
            settingsGameCanvas.enabled = true;
        }

        public void onStartGame() => newGame.updateXml();
        public void onGameExit() => exitGameController.exitGame();

        public void OnHoverEnter(Button button)
        {
            button.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }

        public void OnHoverExit(Button button)
        {
            button.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
        }
    }
}
