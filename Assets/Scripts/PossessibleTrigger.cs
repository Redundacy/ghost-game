using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessibleTrigger : MonoBehaviour {
    public interactPlayer InteractPlayer;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InterObject")) {
            Debug.Log(other.name);
            InteractPlayer.currentInterObj = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InterObject")) {
            if (other.gameObject == InteractPlayer.currentInterObj) {
                InteractPlayer.currentInterObj = null;
            }
        }
    }
}
