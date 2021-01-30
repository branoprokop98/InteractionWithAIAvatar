using AIMLbot;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

namespace AIML.KeyboardInput
{
    public class AI_Bot : MonoBehaviour
    {
        [SerializeField] private Text outText;
        [SerializeField] private Canvas canvas;
        [SerializeField] private InputField textField;
        [SerializeField] private GameObject botObject;
        [SerializeField] private GameObject player;
        [SerializeField] private Text errorText;
        private Rigidbody _rigidbody;

        //private SpeechInputForAiml _speechInputForAiml;

        private Hiting _hiting;
        private RigidbodyFirstPersonController _rigidbodyFirstPersonController;
        private Aiml _aiml;
        private Bot AI;
        private User myuser;
        private bool inDialog;
        private string text;
        private float startTime;

        public static AI_Bot aiBot;

        //private SpeechInput _speechInput;

        public string Text
        {
            get => text;
            set => text = value;
        }

        private void Awake()
        {
            //_speechInput = new SpeechInput();
            AI = new Bot();
            myuser = new User("Username Here", AI);
            AI.loadSettings(); //It will Load Settings from its Config Folder with this code
            AI.loadAIMLFromFiles(); //With this Code It Will Load AIML Files from its AIML Folder
        }

        void Start()
        {
            //_speechInputForAiml = new SpeechInputForAiml();
            aiBot = this;
            _aiml = new Aiml();
            outText = outText.GetComponent<Text>();
            _rigidbody = player.GetComponent<Rigidbody>();
            canvas.enabled = false;
            _hiting = new Hiting(2);
            inDialog = false;
            errorText = errorText.GetComponent<Text>();
            errorText.enabled = false;
            ShowCursor.mouseInvisible();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && _hiting.getHit() && botObject == _hiting._hit.collider.gameObject &&
                inDialog == false)
            {
                ShowCursor.mouseVisible();
                _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                canvas.enabled = true;
                RigidbodyFirstPersonController.instance.mouseLook.XSensitivity = 0;
                RigidbodyFirstPersonController.instance.mouseLook.YSensitivity = 0;
                inDialog = true;
            }
            else if (Input.GetKeyDown(KeyCode.F) && inDialog)
            {
                ShowCursor.mouseInvisible();
                _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                canvas.enabled = false;
                RigidbodyFirstPersonController.instance.mouseLook.XSensitivity = 2;
                RigidbodyFirstPersonController.instance.mouseLook.YSensitivity = 2;
                inDialog = false;
            }
        }


        public void botControll(string text)
        {
            _aiml.botInput(text, outText, errorText);

        }

    }
}
