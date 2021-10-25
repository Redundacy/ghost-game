using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransition : MonoBehaviour {
    public GameObject MainCamera;
    public RoomData From;
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

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (UpDown) {
                MainCamera.GetComponent<CameraHandler>().CurrentRoom =
                    collision.attachedRigidbody.velocity.y > 0 ? To : From;
                collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, collision.attachedRigidbody.velocity.y + 2);
            }
            else {
                MainCamera.GetComponent<CameraHandler>().CurrentRoom = Input.GetAxisRaw("Horizontal") > 0 ? To : From;
            }
        }
    }
}
