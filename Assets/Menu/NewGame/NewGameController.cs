using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.NewGame
{
    public class NewGameController : MonoBehaviour
    {
        private MenuInteraction menuInteraction;

        private void Start()
        {
            menuInteraction = new MenuInteraction();
        }

        public void setGender()
        {
            menuInteraction.newGame.gender = this.transform.GetChild(2).gameObject.GetComponent<Dropdown>().value;
            updateXml();
        }

        public void setInput()
        {
            menuInteraction.newGame.inputType = this.transform.GetChild(1).gameObject.GetComponent<Dropdown>().value;
            updateXml();
        }

        public void setName()
        {
            menuInteraction.newGame.name = this.transform.GetChild(3).gameObject.GetComponent<InputField>().text;
            updateXml();
        }

        public void updateXml()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "Menu.xml");
            XMLWorker.serialize( menuInteraction, path);
        }
    }
}
