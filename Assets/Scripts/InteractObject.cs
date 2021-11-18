using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{

    public void DoInteraction(){
        //open text box or flip lever
        gameObject.SetActive (false);
    }

    public void DoPossession() {
        gameObject.SetActive (false);
    }

    public void Open(){
        //Opens door
        gameObject.SetActive (false);
    }

    public void Close(){
        //Closes door
        gameObject.SetActive (true);
    }
}
