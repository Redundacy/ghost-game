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
    private int player;

    // Start is called before the first frame update
    void Start()
    {
        player = 0;
    }

    // Update is called once per frame
    void Update()
    {
        interact();
        possess();
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
        }

        if (Input.GetButtonDown ("Possess") && currentInterObj && PossessedObject == null) {
            PossessedObject = currentInterObj;
            PossessedObject.SendMessage("DoPossession");
            // GetComponent<Animator>().Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == ("InterObject")) {
            Debug.Log(collision.collider.name);
            currentInterObj = collision.collider.gameObject;
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("InterObject")) {
            Debug.Log(collision.collider.name);
            currentInterObj = collision.collider.gameObject;
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("InterObject")){
            Debug.Log (other.name);
            currentInterObj = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("InterObject")){
            if(other.gameObject == currentInterObj){
                currentInterObj = null;
            }
        }
    }



}
