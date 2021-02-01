using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.NewGame
{
    public class NewGameController
    {
        private readonly MenuInteraction menuInteraction;
        private readonly Canvas canvas;

        public NewGameController(Canvas canvas)
        {
            menuInteraction = new MenuInteraction();
            this.canvas = canvas;
        }

        private void setGender()
        {
            menuInteraction.newGame.gender = canvas.transform.GetChild(2).gameObject.GetComponent<Dropdown>().value;
            //updateXml();
        }

        private void setInput()
        {
            menuInteraction.newGame.inputType = canvas.transform.GetChild(1).gameObject.GetComponent<Dropdown>().value;
            //updateXml();
        }

        private void setName()
        {
            menuInteraction.newGame.name = canvas.transform.GetChild(3).gameObject.GetComponent<InputField>().text;
            //updateXml();
        }

        public void updateXml()
        {
            setGender();
            setInput();
            setName();
            string path = Path.Combine(Application.streamingAssetsPath, "Menu.xml");
            XMLWorker.serialize( menuInteraction, path);
        }
    }
}
