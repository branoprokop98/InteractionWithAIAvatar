using System;
using UnityEngine;
using UnityEngine.UI;

namespace AIML
{
    public class ContextWindowTopic : MonoBehaviour, ContextLayer
    {
        [SerializeField] private Canvas canvas;
        private LoadTopics topics;
        private ContextWindowService contextService;

        private void Start()
        {
            topics = new LoadTopics();
            contextService = this.gameObject.GetComponent<ContextWindowService>();
            initTopicsName();
        }

        public void initTopicsName()
        {
            ContextWindowService.actualLayerOfTopic = 0;
            ContextWindowService.actualLayerOfSentences = -2;
            for (int i = 0; i < canvas.transform.childCount - 3; i++)
            {
                Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
                Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
                try
                {
                    buttonText.text = topics.ListOfTopics[ContextWindowService.actualLayerOfTopic][i].TopicName;
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
            ContextWindowService.actualLayerOfTopic++;
            if (contextService.tryLayerOfTopicBounce(1) == -1)
            {
                return;
            };

            for (int i = 0; i < canvas.transform.childCount - 3; i++)
            {
                Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
                Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
                try
                {
                    buttonText.text = topics.ListOfTopics[ContextWindowService.actualLayerOfTopic][i].TopicName;
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
            ContextWindowService.actualLayerOfTopic--;
            if (contextService.tryLayerOfTopicBounce(0) == -1)
            {
                return;
            };

            for (int i = 0; i < canvas.transform.childCount - 3; i++)
            {
                Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
                Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
                try
                {
                    buttonText.text = this.topics.ListOfTopics[ContextWindowService.actualLayerOfTopic][i].TopicName;
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
