using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

    public int choiceLineNumber; // the number of lines before a choice
    public string[] NextDialogueNodes = new string[2];
    public string[] choices = new string[2];

    public DialogueNode(string Name, string[] Sentences, int CLN, string[] nextDialogueNodes, string[] Choices)
    {
        name = Name;
        sentences = Sentences;
        choiceLineNumber = CLN;
        NextDialogueNodes = nextDialogueNodes;
        choices = Choices;
    }

}
