using System.Xml.Serialization;

namespace Menu.NewGame
{
    [XmlRoot("mainMenu")]
    public class MenuInteraction
    {
        [XmlElement("newGame")]
        public NewGame newGame { get; set; }

        [XmlElement("systemInfo")]
        public SystemInfo sysInfo { get; set; }

        public MenuInteraction()
        {
            newGame = new NewGame();
        }
    }

    public class NewGame
    {
        [XmlElement("gender")] public int gender{ get; set; }
        [XmlElement("inputType")] public int inputType{ get; set; }
        [XmlElement("name")] public string name{ get; set; }
    }

    public class SystemInfo
    {
        [XmlElement("operatingSystem")] public string operatingSystem{ get; set; }
    }
}
