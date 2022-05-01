using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
    public Transform FollowTransform;
    public RoomData CurrentRoom;
    public GameObject Player;
    public GameObject Wizard;
    
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
        if (GameObject.Find("RestartHandler")) {
            CurrentRoom = GameObject.Find("RestartHandler").GetComponent<RestartHandler>().currentRoom;
        }
        
        switch (CurrentRoom.name) {
            case "Bobby Starting Room":
                break;
            case "Bobby Bookcase Room":
                Player.transform.SetPositionAndRotation(new Vector3(-55.5f, -13f), Player.transform.rotation);
                break;
            case "Bobby Ladder Room":
                Player.transform.SetPositionAndRotation(new Vector3(-31.5f, -6f), Player.transform.rotation);
                break;
            case "Possession Room":
                Player.transform.SetPositionAndRotation(new Vector3(18.5f, -6f), Player.transform.rotation);
                break;
            case "Caleb Tall Room":
                Player.transform.SetPositionAndRotation(new Vector3(81.5f, -6f), Player.transform.rotation);
                Wizard.transform.SetPositionAndRotation(new Vector3(83.5f, -6f), Wizard.transform.rotation);
                break;
            case "Caleb Book Maze":
                Player.transform.SetPositionAndRotation(new Vector3(143.5f, 62f), Player.transform.rotation);
                Wizard.transform.SetPositionAndRotation(new Vector3(145.5f, 62f), Wizard.transform.rotation);
                break;
            case "Final Room":
                Player.transform.SetPositionAndRotation(new Vector3(223.5f, 28f), Player.transform.rotation);
                Wizard.transform.SetPositionAndRotation(new Vector3(225.5f, 28f), Player.transform.rotation);
                break;
        }
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

    public void OnGUI()
    {

    }
}
