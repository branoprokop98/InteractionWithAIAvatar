using System.IO;
using System.Xml.Serialization;
using Menu.NewGame;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneLoader : MonoBehaviour
    {
        private MenuInteraction menuInteraction;
        public void loadGame()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "Menu.xml");
            menuInteraction = XMLWorker.deserialize<MenuInteraction>(path);
            SceneManager.LoadScene((int) 1);
        }
    }
}
