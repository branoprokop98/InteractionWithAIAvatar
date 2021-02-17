using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameFromSave : MonoBehaviour
{
    private string saveGameLocation;
    [SerializeField] private Image image;


    private List<string> saves;
    // Start is called before the first frame update
    void Start()
    {
        saves = new List<string>();
        saveGameLocation = Path.Combine(Application.streamingAssetsPath, "Save\\");
        findSaves();
    }

    private void findSaves()
    {
        string [] fileEntries = Directory.GetFiles(saveGameLocation, "*.xml");
        foreach (string fileName in fileEntries)
        {
            saves.Add(fileName);
        }
    }

    private void instantiateButton()
    {

    }

}
