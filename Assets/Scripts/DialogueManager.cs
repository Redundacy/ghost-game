using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject leftButton;
    public GameObject rightButton;

    public Animator animator;
    public GameObject[] MovingGameObjects;
    public GameObject PlayerGameObject;

    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    private DialogueNode thisDialogue;
    private int lineNumber;

    public void StartDialogue(DialogueNode dialogue)
    {
        thisDialogue = dialogue;
        animator.SetBool("IsOpen", true);
        PlayerGameObject.GetComponent<PlayerController>().inCutscene = true;
        nameText.text = dialogue.name;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        sentences.Clear();
        lineNumber = 1;
        
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(true);
    }

    public void DisplayNextSentence(bool isRightButton)
    {
        if (sentences.Count == 0)
        {
            if (isRightButton && thisDialogue.NextDialogueNodes[0] != null)
            {
                IEnumerator additionalActions = CutsceneActions(thisDialogue.NextDialogueNodes[0]);
                StartCoroutine(additionalActions);
                GetComponent<DialogueTrigger>().TriggerDialogue(thisDialogue.NextDialogueNodes[0]);
                return;
            }
            else
            {
                EndDialogue();
                return;
            }
        }
        Debug.Log(lineNumber);
        if (lineNumber == thisDialogue.choiceLineNumber)
        {
            if (thisDialogue.choices[1] != null)
            {
                leftButton.SetActive(true);
                leftButton.GetComponentInChildren<Text>().text = thisDialogue.choices[1];
            }
            rightButton.GetComponentInChildren<Text>().text = thisDialogue.choices[0];
        } else
        {
            leftButton.SetActive(false);
            rightButton.GetComponentInChildren<Text>().text = "Continue";
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        lineNumber++;
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        PlayerGameObject.GetComponent<PlayerController>().inCutscene = false;
    }

    public IEnumerator CutsceneActions(string cutsceneName)
    {
        switch (cutsceneName)
        {
            case "OpeningDialogue2":
                PlayerGameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 12, ForceMode2D.Impulse);
                break;
            case "OpeningDialogue5":
                PlayerGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                yield return new WaitForSeconds(4f);
                PlayerGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            case "LeverCutscene2":
                MovingGameObjects[0].GetComponent<Collider2D>().enabled = true;
                MovingGameObjects[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                break;
        }

        yield return new WaitForSeconds(1);
    }
}
