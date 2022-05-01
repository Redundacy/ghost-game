using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPromptHandler : MonoBehaviour
{
    public GameObject PromptGameObject;

    void OnTriggerEnter2D(Collider2D collider)
    {
        PromptGameObject.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        PromptGameObject.SetActive(false);
    }
}
