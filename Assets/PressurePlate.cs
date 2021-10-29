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

    void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().sprite = Pressed;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().sprite = Depressed;
    }
}
