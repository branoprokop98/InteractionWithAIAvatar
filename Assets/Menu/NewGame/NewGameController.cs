using System;
using System.IO;
using System.Xml.Serialization;
using AIML;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.NewGame
{
    public class NewGameController
    {
        private readonly MenuInteraction menuInteraction;
        private readonly Canvas canvas;
        private string pathToConfig;

        public NewGameController(Canvas canvas)
        {
            menuInteraction = new MenuInteraction();
            pathToConfig = Path.Combine(Environment.CurrentDirectory, Path.Combine("config", "Settings.xml"));
            this.canvas = canvas;
            menuInteraction.saveInfo.mood = -1;
        }

        public void setGender()
        {
            menuInteraction.newGame.gender = canvas.transform.GetChild(2).gameObject.GetComponent<Dropdown>().value;
            //updateXml();
        }

        private void setInput()
        {
            menuInteraction.newGame.inputType = canvas.transform.GetChild(1).gameObject.GetComponent<Dropdown>().value;
            //updateXml();
        }

        public void setName()
        {
            menuInteraction.newGame.name = canvas.transform.GetChild(3).gameObject.GetComponent<InputField>().text;
            //updateXml();
        }

        public void updateXml()
        {
            //setGender();
            setInput();
            //setName();
            string path = Path.Combine(Application.streamingAssetsPath, "Menu.xml");
            XMLWorker.serialize( menuInteraction, path);
            updateConfig();
        }

        private void updateConfig()
        {
            AimlSettings aimlSettings = XMLWorker.deserialize<AimlSettings>(pathToConfig);
            aimlSettings.settings.Find(x => x.nameOfAttribute == "name").valueOfAttribute =
                menuInteraction.newGame.name;
            aimlSettings.settings.Find(x => x.nameOfAttribute == "gender").valueOfAttribute =
                menuInteraction.newGame.gender == 0 ? "Male" : "Female";
            XMLWorker.serialize(aimlSettings, pathToConfig);
        }
    }
}
