using System.IO;
using System.Xml.Serialization;
using Menu.NewGame;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneLoader : MonoBehaviour
    {
        public void loadGame()
        {
            SceneManager.LoadScene((int) 1);
        }
    }
}
