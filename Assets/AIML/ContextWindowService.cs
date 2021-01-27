﻿using System;
using System.Collections;
using System.Collections.Generic;
using AIML;
using UnityEngine;
using UnityEngine.UI;

public class ContextWindowService : MonoBehaviour
{
    private LoadTopics topics;
    private LoadSentences loadSentences;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject interactObject;
    [SerializeField] private Canvas textCanvas;
    private Hiting _hiting;
    private bool interacting;
    public static int actualLayerOfTopic { get; set; }
    public static int actualLayerOfSentences { get; set; }
    private Aiml aiml;
    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        loadSentences = new LoadSentences();
        topics = new LoadTopics();
        canvas.enabled = false;
        _hiting = new Hiting();
        interacting = false;
        actualLayerOfTopic = 0;
        actualLayerOfSentences = 0;
        aiml = new Aiml();
        //initTopicsName();
        textCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _hiting.getHit() && _hiting._hit.collider.gameObject == interactObject &&
            interacting == false)
        {
            textCanvas.enabled = true;
            canvas.enabled = true;
            interacting = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && _hiting.getHit() &&
                 _hiting._hit.collider.gameObject == interactObject &&
                 interacting)
        {
            textCanvas.enabled = false;
            canvas.enabled = false;
            interacting = false;
        }
    }

    // public void initTopicsName()
    // {
    //     actualLayerOfTopic = 0;
    //     actualLayerOfSentences = -2;
    //     for (int i = 0; i < canvas.transform.childCount - 3; i++)
    //     {
    //         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
    //         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
    //         try
    //         {
    //             buttonText.text = topics.ListOfTopics[actualLayerOfTopic][i].TopicName;
    //             button.gameObject.SetActive(true);
    //         }
    //         catch (Exception e)
    //         {
    //             button.gameObject.SetActive(false);
    //         }
    //     }
    // }
    //
    // public void getNextLayer()
    // {
    //     actualLayerOfTopic++;
    //     if (tryLayerOfTopicBounce(1) == -1)
    //     {
    //         return;
    //     };
    //
    //     for (int i = 0; i < canvas.transform.childCount - 3; i++)
    //     {
    //         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
    //         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
    //         try
    //         {
    //             buttonText.text = topics.ListOfTopics[actualLayerOfTopic][i].TopicName;
    //             button.gameObject.SetActive(true);
    //         }
    //         catch (Exception e)
    //         {
    //             button.gameObject.SetActive(false);
    //         }
    //     }
    // }
    //
    // public void getPrevLayer()
    // {
    //     actualLayerOfTopic--;
    //     if (tryLayerOfTopicBounce(0) == -1)
    //     {
    //         return;
    //     };
    //
    //     for (int i = 0; i < canvas.transform.childCount - 3; i++)
    //     {
    //         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
    //         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
    //         try
    //         {
    //             buttonText.text = this.topics.ListOfTopics[actualLayerOfTopic][i].TopicName;
    //             button.gameObject.SetActive(true);
    //         }
    //         catch (Exception e)
    //         {
    //             button.gameObject.SetActive(false);
    //         }
    //     }
    // }

    // public void getSentencesOfTopic(Button button)
    // {
    //     Debug.Log(button.transform.GetChild(0).gameObject.GetComponent<Text>().text);
    //     string nameOfTopic = button.transform.GetChild(0).gameObject.GetComponent<Text>().text;
    //     foreach (List<Topics> listsOfTopics in topics.ListOfTopics)
    //     {
    //         foreach (Topics topic in listsOfTopics)
    //         {
    //             if (topic.TopicName.Equals(nameOfTopic))
    //             {
    //                 loadSentences.listSentences(topic.PathToTopic);
    //                 initSentences();
    //                 return;
    //             }
    //         }
    //     }
    //
    //     Text outText = textCanvas.transform.GetChild(0).gameObject.GetComponent<Text>();
    //     Text errorText = textCanvas.transform.GetChild(1).gameObject.GetComponent<Text>();
    //     aiml.botInput(nameOfTopic, outText, errorText);
    // }
    //
    // public void initSentences()
    // {
    //     actualLayerOfSentences = 0;
    //     actualLayerOfTopic = -2;
    //     for (int i = 0; i < canvas.transform.childCount - 3; i++)
    //     {
    //         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
    //         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
    //         try
    //         {
    //             buttonText.text = loadSentences.ListOfAimlSentences[actualLayerOfSentences][i].Pattern;
    //             button.gameObject.SetActive(true);
    //         }
    //         catch (Exception e)
    //         {
    //             button.gameObject.SetActive(false);
    //         }
    //     }
    // }
    //
    // public void getNextLayerSentence()
    // {
    //     actualLayerOfSentences++;
    //     if (tryLayerOfSentencesBounce(1) == -1)
    //     {
    //         return;
    //     }
    //
    //     for (int i = 0; i < canvas.transform.childCount - 3; i++)
    //     {
    //         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
    //         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
    //         try
    //         {
    //             buttonText.text = loadSentences.ListOfAimlSentences[actualLayerOfSentences][i].Pattern;
    //             button.gameObject.SetActive(true);
    //         }
    //         catch (Exception e)
    //         {
    //             button.gameObject.SetActive(false);
    //         }
    //     }
    // }
    //
    // public void getPrevLayerSentence()
    // {
    //     actualLayerOfSentences--;
    //     if (tryLayerOfSentencesBounce(0) == -1)
    //     {
    //         return;
    //     }
    //
    //     for (int i = 0; i < canvas.transform.childCount - 3; i++)
    //     {
    //         Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
    //         Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
    //         try
    //         {
    //             buttonText.text = loadSentences.ListOfAimlSentences[actualLayerOfSentences][i].Pattern;
    //             button.gameObject.SetActive(true);
    //         }
    //         catch (Exception e)
    //         {
    //             button.gameObject.SetActive(false);
    //         }
    //     }
    // }

    private void testLayerBounceForSentences(List<List<AIMLStructure>> listOfSentences)
    {
        if (listOfSentences[actualLayerOfSentences][0] == null)
        {
            throw new Exception("Out of range");
        }
    }

    public int tryLayerOfSentencesBounce(int layerDirection, List<List<AIMLStructure>> listOfSentences)
    {
        try
        {
            testLayerBounceForSentences(listOfSentences);
        }
        catch (Exception e)
        {
            if (layerDirection == 0)
            {
                actualLayerOfSentences++;
            }
            else
            {
                actualLayerOfSentences--;
            }

            Debug.LogWarning(e.Message);
            return -1;
        }

        return 0;
    }

    private void testLayerBounceForTopic()
    {
        if (topics.ListOfTopics[actualLayerOfTopic][0] == null)
        {
            throw new Exception("Topic layer is out of range");
        }
    }

    public int tryLayerOfTopicBounce(int layerDirection)
    {
        try
        {
            testLayerBounceForTopic();
        }
        catch (Exception e)
        {
            if (layerDirection == 1)
            {
                actualLayerOfTopic--;
            }
            else
            {
                actualLayerOfTopic++;
            }
            Debug.Log(e.Message);
            return -1;
        }
        return 0;
    }
}
