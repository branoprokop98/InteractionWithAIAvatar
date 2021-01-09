using System;
using System.Collections;
using System.Collections.Generic;
using AIML;
using UnityEngine;
using UnityEngine.UI;
using AIMLbot;
using SpeechLib;
using UnityStandardAssets.Characters.FirstPerson;


public class AI_Bot : MonoBehaviour
{
    [SerializeField] private Text outText;
    [SerializeField] private Canvas canvas;
    [SerializeField] private InputField textField;
    [SerializeField] private GameObject botObject;
    [SerializeField] private GameObject player;
    [SerializeField] private Text errorText;
    private Rigidbody _rigidbody;

    //private SpeechInputForAiml _speechInputForAiml;

    private Hiting _hiting;
    private RigidbodyFirstPersonController _rigidbodyFirstPersonController;
    private Aiml _aiml;
    private Bot AI;
    private User myuser;
    private bool inDialog;
    private string text;
    private float startTime;

    public static AI_Bot aiBot;

    //private SpeechInput _speechInput;

    public string Text
    {
        get => text;
        set => text = value;
    }

    private void Awake()
    {
        //_speechInput = new SpeechInput();
        AI = new Bot();
        myuser = new User("Username Here", AI);
        AI.loadSettings(); //It will Load Settings from its Config Folder with this code
        AI.loadAIMLFromFiles(); //With this Code It Will Load AIML Files from its AIML Folder
    }

    void Start()
    {
        //_speechInputForAiml = new SpeechInputForAiml();
        aiBot = this;
        _aiml = new Aiml();
        outText = outText.GetComponent<Text>();
        _rigidbody = player.GetComponent<Rigidbody>();
        canvas.GetComponent<Canvas>().enabled = false;
        _hiting = new Hiting(2);
        inDialog = false;
        errorText = errorText.GetComponent<Text>();
        errorText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _hiting.getHit() && botObject == _hiting._hit.collider.gameObject &&
            inDialog == false)
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            canvas.GetComponent<Canvas>().enabled = true;
            RigidbodyFirstPersonController.instance.mouseLook.XSensitivity = 0;
            RigidbodyFirstPersonController.instance.mouseLook.YSensitivity = 0;
            inDialog = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && inDialog)
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            canvas.GetComponent<Canvas>().enabled = false;
            RigidbodyFirstPersonController.instance.mouseLook.XSensitivity = 2;
            RigidbodyFirstPersonController.instance.mouseLook.YSensitivity = 2;
            inDialog = false;
        }
    }


    public void botControll(string text)
    {
        _aiml.botInput(text, outText, errorText);
        //AI.isAcceptingUserInput = false; //With this Code it will Disable UserInput For Now
        //User myuser = new User("Username Here", AI); //With This Code We Will Add The User Through AI/Bot
        //AI.isAcceptingUserInput = true; //Now The User Input is Enabled Again with this Code
        // Request r = new Request(text, myuser, AI); //With This Code it will Request The Response From AIML Folders
        // Result res = AI.Chat(r); //With This Code It Will Get Result
        // string output = res.Output; //With this Code It Will Write the Result of Textbox1 Response to Textbox2 text
        // outText.text = output;
        // Debug.LogWarning(output);
        // speechOutput(output);
        //Now Coding Is Finished!
        //Now Add/Copy & Paste AIML Folder & Config Folder to the Project Directory
        //Now Test the Bot
    }

    // public void speechOutput(string output)
    // {
    //     SpVoice voice = new SpVoice();
    //     voice.Speak(output, SpeechVoiceSpeakFlags.SVSFlagsAsync);
    //     voice.SynchronousSpeakTimeout = 100;
    //     voice.Rate = 0;
    // }
}
