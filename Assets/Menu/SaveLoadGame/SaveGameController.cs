using System;
using System.IO;
using Menu.NewGame;
using UnityEngine;

namespace Menu.SaveLoadGame
{
    public class SaveGameController
    {
        private string fileName;
        private string fileToSave;
        private string savePath;
        private int counter;
        private MenuInteraction menuInteraction;

        public SaveGameController()
        {
            counter = 0;
            fileName = "Save" + counter;
            this.fileToSave = Path.Combine(Application.streamingAssetsPath, "Menu.xml");
            this.savePath = Path.Combine(Application.streamingAssetsPath, "Save\\" + fileName + ".xml");
            menuInteraction = XMLWorker.deserialize<MenuInteraction>(fileToSave);
        }


        public void saveGame()
        {
            menuInteraction.saveInfo.DateTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            XMLWorker.serialize(menuInteraction, fileToSave);
            if (File.Exists(savePath))
            {
                countFileName();
            }
            File.Copy(fileToSave, savePath);
        }

        private void countFileName()
        {
            counter++;
            fileName = "Save" + counter;
            this.savePath = Path.Combine(Application.streamingAssetsPath, "Save\\" + fileName + ".xml");
        }

    }
}
