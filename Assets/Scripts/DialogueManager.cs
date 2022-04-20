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
        nameText.text = dialogue.name;
        
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
                DialogueTrigger.TriggerDialogue(thisDialogue.NextDialogueNodes[0]);
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
        // FindObjectOfType<DialogueTrigger>().gobAccepted = false;
        // FindObjectOfType<DialogueTrigger>().gobRejectedOrIgnored ++;
        animator.SetBool("IsOpen", false);
    }
}
