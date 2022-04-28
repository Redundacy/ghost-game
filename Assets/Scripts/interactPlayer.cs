using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactPlayer : MonoBehaviour
{
    // Bobby Spatz
    // used https://www.youtube.com/watch?v=gGUtoy4Knnw&ab_channel=ScriptingIsFun


    // the object you can interact with
    public GameObject currentInterObj = null;

    public GameObject PossessedObject = null;
    // number in int changes based on what you possess
    public int player;
    public int possessionRadius;
    public LayerMask possessionLayer;
    
    public Transform grabDetect;
    public Transform itemHolder;
    public float rayDist;
    // Start is called before the first frame update
    void Start()
    {
        player = 0;
    }

    // Update is called once per frame
    void Update() {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, possessionRadius, possessionLayer);
        if (collider.Length == 0) {
            if (currentInterObj != null)
            {
                if (player == 0) 
                    currentInterObj.SendMessage("InputPopupOff");
            }
            currentInterObj = null;
        }
        else {
            foreach (Collider2D child in collider) {
                if (child.name == "Wizard" || child.name == "Sheets Ghost") {
                    currentInterObj = child.gameObject;
                    currentInterObj.SendMessage("InputPopupOn");
                }
            }
        }
        interact();
        possess();
        grabItem();
    }


    void interact(){
        if(Input.GetButtonDown ("Interact") && currentInterObj){
            // allows me to talk between scripts DoInteraction is a function in InteractObject
            currentInterObj.SendMessage ("DoInteraction");
        }
    }
    void possess(){
        if (Input.GetButtonDown("Possess") && PossessedObject != null) {
            PossessedObject.SetActive(true);
            PossessedObject.transform.position = transform.position;
            PossessedObject = null;
            player = 0;
            GetComponent<Animator>().SetInteger("PossessionState", player);
        }

        if (Input.GetButtonDown ("Possess") && currentInterObj && PossessedObject == null) {
            PossessedObject = currentInterObj;
            PossessedObject.SendMessage("DoPossession");
            player = PossessedObject.GetComponent<InteractObject>().possessedState;
            GetComponent<Animator>().SetInteger("PossessionState", player);
            // GetComponent<Animator>().Play();
        }
    }
    //Caleb Rosenboom
    //Made using https://www.youtube.com/watch?v=1uq43EIzo-U&t=320s
    void grabItem()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
        // Debug.Log(grabCheck.collider);
        if(grabCheck.collider != null && grabCheck.collider.tag == "Box")
        {
            if (Input.GetButton("Grab"))
            {
                grabCheck.collider.gameObject.transform.parent = itemHolder;
                grabCheck.collider.gameObject.transform.localPosition = Vector3.zero;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else
            {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().velocity.Set(0,0);
            }
        }
    }

    // void OnTriggerEnter2D(Collider2D other){
    //     if(other.CompareTag("InterObject")){
    //         Debug.Log (other.name);
    //         currentInterObj = other.gameObject;
    //     }
    // }
    //
    // void OnTriggerExit2D(Collider2D other){
    //     if(other.CompareTag("InterObject")){
    //         if(other.gameObject == currentInterObj){
    //             currentInterObj = null;
    //         }
    //     }
    // }



}
