using System.Collections;
using System.Collections.Generic;
using AIML;
using UnityEngine;

public class ContextWindow : MonoBehaviour
{
    private LoadTopics topics;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject interactObject;
    private Hiting _hiting;
    private bool interacting;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        topics = new LoadTopics();
        canvas.enabled = false;
        _hiting = new Hiting();
        interacting = false;
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
        else if (Input.GetKey(KeyCode.F) && _hiting.getHit() &&
                 _hiting._hit.collider.gameObject == interactObject &&
                 interacting)
        {
            canvas.enabled = false;
            interacting = false;
        }
    }
}
