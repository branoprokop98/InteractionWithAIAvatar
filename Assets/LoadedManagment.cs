using System.Collections;
using System.Collections.Generic;
using System.IO;
using Menu;
using Menu.NewGame;
using UnityEngine;

public class LoadedManagment : MonoBehaviour
{
    [SerializeField] private GameObject speechInput;
    [SerializeField] private GameObject keyboardInput;
    [SerializeField] private GameObject contextWindowInput;

    private MenuInteraction menuInteraction;
    // Start is called before the first frame update
    void Start()
    {
        ShowCursor.mouseInvisible();
        string path = Path.Combine(Application.streamingAssetsPath, "Menu.xml");
        menuInteraction = XMLWorker.deserialize<MenuInteraction>(path);
        loadPrefab();
    }

    private void loadPrefab()
    {
        switch (menuInteraction.newGame.inputType)
        {
            case 0:
                //speech
                Instantiate(speechInput, speechInput.transform.position, speechInput.transform.rotation);
                break;
            case 1:
                //keyboard
                Instantiate(keyboardInput, keyboardInput.transform.position, keyboardInput.transform.rotation);
                break;
            case 2:
                //context window
                Instantiate(contextWindowInput, contextWindowInput.transform.position, contextWindowInput.transform.rotation);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
