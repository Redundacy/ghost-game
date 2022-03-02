using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour {
    public GameObject tutorialPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Toggle Tutorial Panel")) {
            tutorialPanel.SetActive(!tutorialPanel.activeSelf);
        }
    }
}
