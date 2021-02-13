using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

namespace AIML.SpeechInput
{
    public class SpeechInput : MonoBehaviour
    {
        [SerializeField] private GameObject interactObject;
        [SerializeField] private Canvas interactCanvas;
        [SerializeField] private Text speechText;
        [SerializeField] private Text outText;
        [SerializeField] private Text errorText;
        [SerializeField] private Text moodText;

        private DictationRecognizer dictationRecognizer;
        private string m_Recognitions;
        private Hiting hitting;
        public static bool interacting;
        private Aiml aiml;
        private string recognizedString;


        private void Start()
        {
            aiml = new Aiml();
            interacting = false;
            hitting = new Hiting(2);
            interactCanvas.enabled = false;
            errorText = errorText.GetComponent<Text>();
            errorText.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && hitting.getHit() && hitting.hit.collider.gameObject == interactObject &&
                interacting == false)
            {
                interacting = true;
                interactCanvas.enabled = true;
                dictationRecognizer = new DictationRecognizer();
                dictationRecognizer.Start();
                speechInput();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && hitting.getHit() &&
                     hitting.hit.collider.gameObject == interactObject && interacting)
            {
                interacting = false;
                interactCanvas.enabled = false;
                dictationRecognizer.Stop();
                dictationRecognizer.Dispose();
            }
        }

        private void speechInput()
        {
            dictationRecognizer.DictationResult += (text, confidence) =>
            {
                Debug.LogWarningFormat("Dictation result: {0} , {1}", text, confidence);
                m_Recognitions += text + "\n";
                speechText.text = text;
                aiml.botInput(text, outText, errorText, moodText);
            };
        }
    }
}
