using UnityEngine;
using UnityEngine.UI;

namespace AIML.ContextWindowInput
{
    public class ContextWindowService : MonoBehaviour
    {
        private ContextWindowTopic contextTopic;
        private ContextWindowSentence contextSentence;
        [SerializeField] private Canvas canvas;
        [SerializeField] private GameObject interactObject;
        [SerializeField] private Canvas textCanvas;
        private Hiting hitting;
        private bool interacting;
        public static int actualLayerOfTopic { get; set; }
        public static int actualLayerOfSentences { get; set; }
        private Aiml aiml;
        private Button btn;

        // Start is called before the first frame update
        void Start()
        {
            contextTopic = new ContextWindowTopic(canvas);
            contextSentence = new ContextWindowSentence(canvas, textCanvas);
            canvas.enabled = false;
            hitting = new Hiting();
            interacting = false;
            actualLayerOfTopic = 0;
            actualLayerOfSentences = 0;
            aiml = new Aiml();
            textCanvas.enabled = false;
            ShowCursor.mouseInvisible();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && hitting.getHit() && hitting.hit.collider.gameObject == interactObject &&
                interacting == false)
            {
                ShowCursor.mouseVisible();
                textCanvas.enabled = true;
                canvas.enabled = true;
                interacting = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && hitting.getHit() &&
                     hitting.hit.collider.gameObject == interactObject &&
                     interacting)
            {
                ShowCursor.mouseInvisible();
                textCanvas.enabled = false;
                canvas.enabled = false;
                interacting = false;
            }
        }

        public void getNextLayerOfTopic() => contextTopic.getNextLayer();


        public void getPrevLayerOfTopic() => contextTopic.getPrevLayer();

        public void getNextLayerOfSentence() => contextSentence.getNextLayer();

        public void getPrevLayerOfSentence() => contextSentence.getPrevLayer();

        public void getSentencesOfTopic(Button button) => contextSentence.getSentencesOfTopic(button);

        public void getTopics() => contextTopic.initTopicsName();

        public void OnHoverEnter(Button button)
        {
            button.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }

        public void OnHoverExit(Button button)
        {
            button.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
        }

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
