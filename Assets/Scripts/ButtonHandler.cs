using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {
    public GameObject TutorialPanel;
    public GameObject CreditsPanel;
    public GameObject Camera;

    public GameObject Player;
    public GameObject Wizard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TitleReturn() {
        if (GameObject.Find("RestartHandler")) {
            Destroy(GameObject.Find("RestartHandler"));
        }
        SceneManager.LoadScene("Title Build Scene");
    }

    public void StartGame() {
        SceneManager.LoadScene("Raveyard Build Scene");
    }

    public void ShowTutorial() {
        TutorialPanel.SetActive(!TutorialPanel.activeSelf);
    }

    public void ShowCredits() {
        CreditsPanel.SetActive(!CreditsPanel.activeSelf);
    }

    public void Quit() {
        Application.Quit();
    }

    public void RestartGame(string sceneName) {
        // Collider2D currentRoom = Camera.GetComponent<CameraHandler>().CurrentRoom;
        // RestartHandler restartData = RestartHandler.Instance;
        // restartData.currentRoom = currentRoom;
        SceneManager.LoadScene(sceneName);
    }
}
