using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{

    private BoxCollider2D ColliderDoor;
    private SpriteRenderer RendererDoor;

    public int possessedState = 0;

    void Start ()
    {
        ColliderDoor = GetComponent<BoxCollider2D>();
        RendererDoor = GetComponent<SpriteRenderer>();
    }

    public void DoInteraction(){
        //open text box or flip lever
        // gameObject.SetActive (false);
    }

    public void DoPossession() {
        gameObject.SetActive (false);
    }

    public void InputPopupOn()
    {
        transform.Find("Interact Icon").gameObject.SetActive(true);
    }

    public void InputPopupOff()
    {
        transform.Find("Interact Icon").gameObject.SetActive(false);
    }

    public void Open(){
        //Opens door
        ColliderDoor.enabled = false;
        RendererDoor.enabled = false;
    }

    public void Close(){
        //Closes door
        gameObject.SetActive (true);
        ColliderDoor.enabled = true;
        RendererDoor.enabled = true;
    }
}
