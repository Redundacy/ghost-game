using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// by @kurtdekker - to make a simple Unity singleton that has no
// predefined data associated with it, eg, a high score manager.
//
// To use: access with SingletonSimple.Instance
//
// DO NOT PUT THIS IN ANY SCENE; this code auto-instantiates itself once.
//
// I do not recommend subclassing unless you really know what you're doing.

public class RestartHandler : MonoBehaviour {
    public RoomData currentRoom;


    // This is really the only blurb of code you need to implement a Unity singleton
    private static RestartHandler _Instance;
    public static RestartHandler Instance {
        get {
            if (!_Instance) {
                _Instance = new GameObject().AddComponent<RestartHandler>();
                // name it for easy recognition
                _Instance.name = _Instance.GetType().ToString();
                // mark root as DontDestroyOnLoad();
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    // implement your Awake, Start, Update, or other methods here...
}