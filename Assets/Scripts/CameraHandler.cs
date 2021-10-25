using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
    public Transform FollowTransform;
    public RoomData CurrentRoom;

    private float xMin, xMax, yMin, yMax;
    private float camY, camX;
    private float camOrthsize;
    private float cameraRatio;
    private Camera mainCam;

    private void Start()
    {
        // xMin = CurrentRoom.OffsetX - CurrentRoom.SizeX/2;
        // xMax = CurrentRoom.OffsetX + CurrentRoom.SizeX/2;
        // yMin = CurrentRoom.OffsetY - CurrentRoom.SizeY/2;
        // yMax = CurrentRoom.OffsetX + CurrentRoom.SizeX/2;
        mainCam = GetComponent<Camera>();
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = 16f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // between offset - sizex/2 and offset + sizex/2, but the camera x has to be between those, inward by like 16
        camY = Mathf.Clamp(FollowTransform.position.y, CurrentRoom.OffsetY - CurrentRoom.SizeY / 2 + camOrthsize,
            CurrentRoom.OffsetY + CurrentRoom.SizeY / 2 - camOrthsize);
        camX = Mathf.Clamp(FollowTransform.position.x, CurrentRoom.OffsetX - CurrentRoom.SizeX / 2 + cameraRatio,
            CurrentRoom.OffsetX + CurrentRoom.SizeX / 2 - cameraRatio);
        this.transform.position = new Vector3(camX, camY, this.transform.position.z);
    }
}
