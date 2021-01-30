using Menu.NewGame;
using UnityEngine;

namespace Menu
{
    public class MenuService : MonoBehaviour
    {
        [SerializeField] private Canvas mainMenuCanvas;
        [SerializeField] private Canvas newGameCanvas;
        private NewGameController newGame;


        // Start is called before the first frame update
        void Start()
        {
            newGame = new NewGame.NewGameController();
            mainMenuCanvas.enabled = true;
            newGameCanvas.enabled = false;
        }

        public void getNewGameCanvas()
        {
            mainMenuCanvas.enabled = false;
            newGameCanvas.enabled = true;
        }

        public void getMainMenuCanvas()
        {
            mainMenuCanvas.enabled = true;
            newGameCanvas.enabled = false;
        }
    }
}
