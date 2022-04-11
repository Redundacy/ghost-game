using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

    public int choiceLineNumber;
    public string[] choices = new string[2];

    public Dialogue(string Name, string[] Sentences, int CLN, string[] Choices)
    {
        name = Name;
        sentences = Sentences;
        choiceLineNumber = CLN;
        choices = Choices;
    }

}
