using System;
using System.Collections;
using System.Collections.Generic;
using AIML;
using UnityEngine;
using UnityEngine.UI;

public class ContextWindow : MonoBehaviour
{
    private LoadTopics topics;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject interactObject;
    private Hiting _hiting;
    private bool interacting;
    private int actualLayer;

    // Start is called before the first frame update
    void Start()
    {
        topics = new LoadTopics();
        canvas.enabled = false;
        _hiting = new Hiting();
        interacting = false;
        actualLayer = 0;
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
        List<List<Topics>> topics = this.topics.ListOfTopics;
        for (int i = 0; i < canvas.transform.childCount - 2; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            buttonText.text = topics[actualLayer][i].TopicName;
        }
    }

    public void getNextLayer()
    {
        actualLayer++;
        List<List<Topics>> topics = this.topics.ListOfTopics;
        try
        {
            if (topics[actualLayer][0] == null){}
        }
        catch (Exception e)
        {
            actualLayer--;
            return;
        }
        for (int i = 0; i < canvas.transform.childCount - 2; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            try
            {
                buttonText.text = topics[actualLayer][i].TopicName;
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
        List<List<Topics>> topics = this.topics.ListOfTopics;
        try
        {
            if (topics[actualLayer][0] == null){}
        }
        catch (Exception e)
        {
            actualLayer++;
            return;
        }
        for (int i = 0; i < canvas.transform.childCount - 2; i++)
        {
            Button button = canvas.transform.GetChild(i).gameObject.GetComponent<Button>();
            Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            try
            {
                buttonText.text = topics[actualLayer][i].TopicName;
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
    }
}
