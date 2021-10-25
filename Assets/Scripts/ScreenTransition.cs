using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransition : MonoBehaviour {
    public GameObject MainCamera;
    public RoomData To;
    public bool UpDown;

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
            if (UpDown) {
                MainCamera.GetComponent<CameraHandler>().CurrentRoom = To;
            }
            else {
                MainCamera.GetComponent<CameraHandler>().CurrentRoom = To;
            }
        }
    }
}
