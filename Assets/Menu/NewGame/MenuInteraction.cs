using System;
using System.Xml.Serialization;
using AIMLbot.AIMLTagHandlers;
using UnityEngine.Rendering;

namespace Menu.NewGame
{
    [XmlRoot("mainMenu")]
    public class MenuInteraction
    {
        [XmlElement("newGame")]
        public NewGame newGame { get; set; }

        [XmlElement("save-info")]
        public SaveInfo saveInfo { get; set; }

        public MenuInteraction()
        {
            newGame = new NewGame();
            saveInfo = new SaveInfo();
        }

    }

    public class NewGame
    {
        [XmlElement("gender")] public int gender{ get; set; }
        [XmlElement("inputType")] public int inputType{ get; set; }
        [XmlElement("name")] public string name{ get; set; }
    }

    public class SaveInfo
    {
        [XmlElement("date-time")] public string DateTime{ get; set; }
    }
}
