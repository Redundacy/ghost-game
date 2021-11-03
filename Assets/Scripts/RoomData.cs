using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Rooms/RoomData",order = 1)]
public class RoomData : ScriptableObject {

    public float SizeX;
    public float SizeY;

    public float OffsetX;
    public float OffsetY;

}
