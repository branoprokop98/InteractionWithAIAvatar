using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIML;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechInput : MonoBehaviour
{
    [SerializeField] private GameObject interactObject;
    [SerializeField] private Canvas interactCanvas;
    [SerializeField] private Text speechText;
    [SerializeField] private Text outText;
    [SerializeField] private Text errorText;

    private DictationRecognizer _dictationRecognizer;
    private string m_Recognitions;
    private Hiting _hiting;
    public static bool interacting;
    private Aiml _aiml;
    private string recognizedString;


    private void Start()
    {
        _aiml = new Aiml();
        interacting = false;
        _hiting = new Hiting(2);
        interactCanvas.enabled = false;
        errorText = errorText.GetComponent<Text>();
        errorText.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _hiting.getHit() && _hiting._hit.collider.gameObject == interactObject && interacting == false)
        {
            interacting = true;
            interactCanvas.enabled = true;
            _dictationRecognizer = new DictationRecognizer();
            _dictationRecognizer.Start();
            speechInput();
        }
        else if (Input.GetKeyDown(KeyCode.F) && _hiting.getHit() && _hiting._hit.collider.gameObject == interactObject && interacting)
        {
            interacting = false;
            interactCanvas.enabled = false;
            _dictationRecognizer.Stop();
            _dictationRecognizer.Dispose();
        }
    }

    private void speechInput()
    {
        _dictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogWarningFormat("Dictation result: {0} , {1}", text, confidence);
            m_Recognitions += text + "\n";
            speechText.text = text;
            _aiml.botInput(text, outText, errorText);
        };
    }
}
