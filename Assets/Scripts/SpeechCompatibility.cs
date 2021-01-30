using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechCompatibility : MonoBehaviour
{
    private DictationRecognizer dictationRecognizer;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Canvas crossCanvas;

    // Start is called before the first frame update
    private void Awake()
    {
        crossCanvas.enabled = true;
        canvas.enabled = false;
        try
        {
            dictationRecognizer = new DictationRecognizer();
            dictationRecognizer.Start();
            dictationRecognizer.DictationResult += (text, confidence) =>
            {
                Debug.LogFormat("{0} {1}",text, confidence);
            };
            dictationRecognizer.Stop();
            dictationRecognizer.Dispose();
        }
        catch (Exception e)
        {
            canvas.enabled = true;
            crossCanvas.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hideCanvas()
    {
        canvas.enabled = false;
        crossCanvas.enabled = true;
    }
}
