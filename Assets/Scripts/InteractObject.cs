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

    void Start ()
    {
        ColliderDoor = GetComponent<BoxCollider2D>();
        Renderer = GetComponent<SpriteRenderer>();
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
        Renderer.material = OutlineMaterial;
    }

    public void InputPopupOff()
    {
        transform.Find("Interact Icon").gameObject.SetActive(false);
        Renderer.material = DefaultMaterial;
    }

    public void Open(){
        //Opens door
        ColliderDoor.enabled = false;
        Renderer.enabled = false;
    }

    public void Close(){
        //Closes door
        gameObject.SetActive (true);
        ColliderDoor.enabled = true;
        Renderer.enabled = true;
    }
}
