using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ScreenTransition : MonoBehaviour {
    public GameObject MainCamera;
    public Collider2D To;
    public bool UpDown;
    public CinemachineConfiner Confiner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Confiner.m_BoundingShape2D = To;
        }
    }
}
