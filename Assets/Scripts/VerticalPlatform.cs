using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour {
    public float WaitTime;
    private PlatformEffector2D effector;
    // Start is called before the first frame update
    void Start() {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("GoDown")) {
            WaitTime = 0.5f;
        }
        if (Input.GetButton("GoDown")) { 
            if (WaitTime <= 0) {
                effector.rotationalOffset = 180f;
                WaitTime = 0.5f;
            }
            else {
                WaitTime -= Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Jump")) { 
            effector.rotationalOffset = 0f;
        }
    } 
}
