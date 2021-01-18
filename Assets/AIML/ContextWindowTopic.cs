using System;
using System.Collections;
using System.Collections.Generic;
using AIML;
using UnityEngine;
using UnityEngine.UI;

public class ContextWindowTopic : MonoBehaviour, ContextLayer
{
    private LoadTopics topics;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject interactObject;
    private Hiting _hiting;
    private bool interacting;
    private int actualLayer;
    private int actualLayerOfSentences;
    private ContextWindowSentences contextWindowSentences;

    // Start is called before the first frame update
    void Start()
    {
        contextWindowSentences = new ContextWindowSentences();
        topics = new LoadTopics();
        canvas.enabled = false;
        _hiting = new Hiting();
        interacting = false;
        actualLayer = 0;
        actualLayerOfSentences = 0;
        initTopicsName();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _hiting.getHit() && _hiting._hit.collider.gameObject == interactObject &&
            interacting == false)
        {
            canvas.enabled = true;
            interacting = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && _hiting.getHit() &&
                 _hiting._hit.collider.gameObject == interactObject &&
                 interacting)
        {
            canvas.enabled = false;
            interacting = false;
        }
    }

    private Button btn;

    public void initTopicsName()
    {
        actualLayer = 0;
        actualLayerOfSentences = -2;
        for (int i = 0; i < canvas.transform.childCount - 3; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            try
            {
                buttonText.text = topics.ListOfTopics[actualLayer][i].TopicName;
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
        actualLayer++;
        try
        {
            if (topics.ListOfTopics[actualLayer][0] == null)
            {
            }
        }
        catch (Exception e)
        {
            actualLayer--;
            return;
        }

        for (int i = 0; i < canvas.transform.childCount - 3; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            try
            {
                buttonText.text = topics.ListOfTopics[actualLayer][i].TopicName;
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
        actualLayer--;
        try
        {
            if (this.topics.ListOfTopics[actualLayer][0] == null)
            {
            }
        }
        catch (Exception e)
        {
            actualLayer++;
            return;
        }

        for (int i = 0; i < canvas.transform.childCount - 3; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            try
            {
                buttonText.text = this.topics.ListOfTopics[actualLayer][i].TopicName;
                button.gameObject.SetActive(true);
            }
            catch (Exception e)
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void getSentencesOfTopic(Button button)
    {
        Debug.Log(button.transform.GetChild(0).gameObject.GetComponent<Text>().text);
        string nameOfTopic = button.transform.GetChild(0).gameObject.GetComponent<Text>().text;
        foreach (List<Topics> listsOfTopics in topics.ListOfTopics)
        {
            foreach (Topics topic in listsOfTopics)
            {
                if (topic.TopicName.Equals(nameOfTopic))
                {
                    contextWindowSentences.listSentences(topic.PathToTopic);
                    initSentences();
                    return;
                }
            }
        }
    }

    public void initSentences()
    {
        actualLayerOfSentences = 0;
        actualLayer = -2;
        for (int i = 0; i < canvas.transform.childCount - 3; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            try
            {
                buttonText.text = contextWindowSentences.ListOfAimlSentences[actualLayerOfSentences][i].Pattern;
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
        actualLayerOfSentences++;
        try
        {
            if (contextWindowSentences.ListOfAimlSentences[actualLayerOfSentences][0] == null)
            {
            }
        }
        catch (Exception e)
        {
            actualLayerOfSentences--;
            return;
        }

        for (int i = 0; i < canvas.transform.childCount - 3; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            try
            {
                buttonText.text = contextWindowSentences.ListOfAimlSentences[actualLayerOfSentences][i].Pattern;
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
        actualLayerOfSentences--;
        try
        {
            if (contextWindowSentences.ListOfAimlSentences[actualLayerOfSentences][0] == null)
            {
            }
        }
        catch (Exception e)
        {
            actualLayerOfSentences++;
            return;
        }

        for (int i = 0; i < canvas.transform.childCount - 3; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            try
            {
                buttonText.text = contextWindowSentences.ListOfAimlSentences[actualLayerOfSentences][i].Pattern;
                button.gameObject.SetActive(true);
            }
            catch (Exception e)
            {
                button.gameObject.SetActive(false);
            }
        }
    }
}
