using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
    public Sprite Depressed;

    public Sprite Pressed;

    public GameObject ConnectedGameObject;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Depressed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
            GetComponent<SpriteRenderer>().sprite = Pressed;
            // Debug.Log("the collider is: " + collision.name);
            ConnectedGameObject.SendMessage ("Open",SendMessageOptions.RequireReceiver);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        ConnectedGameObject.SendMessage ("Close",SendMessageOptions.RequireReceiver);
        GetComponent<SpriteRenderer>().sprite = Depressed;

    }


}
