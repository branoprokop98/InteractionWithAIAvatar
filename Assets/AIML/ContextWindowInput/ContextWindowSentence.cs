﻿using System;
using System.Collections.Generic;
using AIML.ContextWindowInput;
using UnityEngine;
using UnityEngine.UI;

namespace AIML.ContextWindowInput
{
    public class ContextWindowSentence : ContextLayer
    {
        private readonly Canvas canvas;
        private readonly Canvas textCanvas;
        private readonly LoadSentences sentences;
        private readonly Aiml aiml;

        public ContextWindowSentence(Canvas buttonCanvas, Canvas textCanvas)
        {
            this.textCanvas = textCanvas;
            this.canvas = buttonCanvas;
            sentences = new LoadSentences();
            aiml = new Aiml();
        }

        public void getSentencesOfTopic(Button button, Animator animator)
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
            Text moodText = textCanvas.transform.GetChild(3).gameObject.GetComponent<Text>();
            aiml.botInput(nameOfTopic, outText, errorText, moodText, animator);
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
                    buttonText.text = sentences.ListOfAimlSentences[ContextWindowService.actualLayerOfSentences][i]
                        .Pattern;
                    button.gameObject.SetActive(true);
                }
                catch (Exception e)
                {
                    button.gameObject.SetActive(false);
                }
            }
        }

        public void getNextLayer()
        {
            ContextWindowService.actualLayerOfSentences++;
            if (sentences.tryLayerOfSentencesBounce(1, sentences.ListOfAimlSentences) == -1)
            {
                return;
            }

            for (int i = 0; i < canvas.transform.childCount - 3; i++)
            {
                Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
                Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
                try
                {
                    buttonText.text = sentences.ListOfAimlSentences[ContextWindowService.actualLayerOfSentences][i]
                        .Pattern;
                    button.gameObject.SetActive(true);
                }
                catch (Exception e)
                {
                    button.gameObject.SetActive(false);
                }
            }
        }

        public void getPrevLayer()
        {
            ContextWindowService.actualLayerOfSentences--;
            if (sentences.tryLayerOfSentencesBounce(0, sentences.ListOfAimlSentences) == -1)
            {
                return;
            }

            for (int i = 0; i < canvas.transform.childCount - 3; i++)
            {
                Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
                Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
                try
                {
                    buttonText.text = sentences.ListOfAimlSentences[ContextWindowService.actualLayerOfSentences][i]
                        .Pattern;
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
