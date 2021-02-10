using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Text _text;
    private Hiting hitting;
    private LayerMask _layerMask;
    private bool interacting;

    // Start is called before the first frame update
    void Start()
    {
        interacting = false;
        _canvas.enabled = false;
        hitting = new Hiting(2);
    }

    // Update is called once per frame
    void Update()
    {
        //_layerMask = _hiting._hit.transform.gameObject.layer;
        if (Input.GetKeyDown(KeyCode.F) && hitting.getHit() && hitting.hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable") && interacting == false)
        {
            _text.text = "Press F to exit";
            interacting = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && hitting.getHit() && hitting.hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable") && interacting)
        {
            _text.text = "Press F to interact";
            interacting = false;
        }
        else if (hitting.getHit() && hitting.hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable") && interacting == false)
        {
            _text.text = "Press F to interact";
            _canvas.enabled = true;
            interacting = false;
        }
        else if(!hitting.getHit())
        {
            _canvas.enabled = false;
        }
    }
}
