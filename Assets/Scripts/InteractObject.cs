using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{

    private BoxCollider2D ColliderDoor;
    private SpriteRenderer Renderer;

    public int possessedState = 0;
    public Material OutlineMaterial;
    public Material DefaultMaterial;

    public Sprite OpenState;
    public Sprite ClosedState;
    public GameObject ConnectedGameObject;
    public string CutsceneTrigger;
    public DialogueTrigger dialogueTrigger;

    private bool cutsceneActivated = false;

    void Start ()
    {
        ColliderDoor = GetComponent<BoxCollider2D>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void DoInteraction(){
        if (ConnectedGameObject)
        {
            if (Renderer.sprite == ClosedState)
            {
                ConnectedGameObject.SendMessage("Open");
                Renderer.sprite = OpenState;
                if (!cutsceneActivated)
                {
                    dialogueTrigger.TriggerDialogue(CutsceneTrigger);
                    cutsceneActivated = true;
                }
            }
            else
            {
                ConnectedGameObject.SendMessage("Close");
                Renderer.sprite = ClosedState;
            }
        }
    }

    public void DoPossession() {
        gameObject.SetActive (false);
    }

    public void InputPopupOn()
    {
        transform.Find("Interact Icon").gameObject.SetActive(true);
        Renderer.material = OutlineMaterial;
    }

    public void InputPopupOff()
    {
        transform.Find("Interact Icon").gameObject.SetActive(false);
        Renderer.material = DefaultMaterial;
    }

    public void Open(){
        //Opens door
        Renderer.sprite = OpenState;
        ColliderDoor.enabled = false;
    }

    public void Close(){
        //Closes door
        gameObject.SetActive (true);
        ColliderDoor.enabled = true;
        Renderer.sprite = ClosedState;
    }
}
