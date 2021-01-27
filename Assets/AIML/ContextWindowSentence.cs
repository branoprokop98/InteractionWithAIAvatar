using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AIML
{
    public class ContextWindowSentence :MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Canvas textCanvas;
        private LoadSentences sentences;
        private ContextWindowService contextService;
        private Aiml aiml;
        private void Start()
        {
            sentences = new LoadSentences();
            contextService = this.gameObject.GetComponent<ContextWindowService>();
            aiml = new Aiml();
        }

        public void getSentencesOfTopic(Button button)
    {
        Debug.Log(button.transform.GetChild(0).gameObject.GetComponent<Text>().text);
        string nameOfTopic = button.transform.GetChild(0).gameObject.GetComponent<Text>().text;
        foreach (List<Topics> listsOfTopics in LoadTopics.listOfTopics)
        {
            foreach (Topics topic in listsOfTopics)
            {
                if (topic.TopicName.Equals(nameOfTopic))
                {
                    sentences.listSentences(topic.PathToTopic);
                    initSentences();
                    return;
                }
            }
        }

        Text outText = textCanvas.transform.GetChild(0).gameObject.GetComponent<Text>();
        Text errorText = textCanvas.transform.GetChild(1).gameObject.GetComponent<Text>();
        aiml.botInput(nameOfTopic, outText, errorText);
    }

    public void initSentences()
    {
        ContextWindowService.actualLayerOfSentences = 0;
        ContextWindowService.actualLayerOfTopic = -2;
        for (int i = 0; i < canvas.transform.childCount - 3; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            try
            {
                buttonText.text = sentences.ListOfAimlSentences[ContextWindowService.actualLayerOfSentences][i].Pattern;
                button.gameObject.SetActive(true);
            }
            catch (Exception e)
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void getNextLayerSentence()
    {
        ContextWindowService.actualLayerOfSentences++;
        if (contextService.tryLayerOfSentencesBounce(1, sentences.ListOfAimlSentences) == -1)
        {
            return;
        }

        for (int i = 0; i < canvas.transform.childCount - 3; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            try
            {
                buttonText.text = sentences.ListOfAimlSentences[ContextWindowService.actualLayerOfSentences][i].Pattern;
                button.gameObject.SetActive(true);
            }
            catch (Exception e)
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void getPrevLayerSentence()
    {
        ContextWindowService.actualLayerOfSentences--;
        if (contextService.tryLayerOfSentencesBounce(0, sentences.ListOfAimlSentences) == -1)
        {
            return;
        }

        for (int i = 0; i < canvas.transform.childCount - 3; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            try
            {
                buttonText.text = sentences.ListOfAimlSentences[ContextWindowService.actualLayerOfSentences][i].Pattern;
                button.gameObject.SetActive(true);
            }
            catch (Exception e)
            {
                button.gameObject.SetActive(false);
            }
        }
    }
    }
}
