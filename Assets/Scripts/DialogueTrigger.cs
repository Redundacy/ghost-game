using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    private static Dictionary<string, DialogueNode> dialogueDictionary = new Dictionary<string, DialogueNode>()
    {
        {"OpeningDialogue1", new DialogueNode("???",new[] {"Hey . . .", "HEY! WAKE UP!"}, 2, new[] {"OpeningDialogue2", null}, new[] {"NEXT", null})},
        {"OpeningDialogue2", new DialogueNode("Jimmy", new[] {"My name's Jimmy","filler 1","filler 2"," filler 3"}, 4, new[] {"OpeningDialogue3", null}, new[] {"NEXT", null})}
    };

    private Dialogue annoyedGoblin = new Dialogue("???", new []{"Can you stop ignoring me?", "I see you've got some <b>POINTS</b>.", "Do you mind giving some to me?",
        "Aww, that's too bad..."}, 3, new[] { "YES", "NO" });

    private Dialogue sDY = new Dialogue("???",
        new[] {"HAHAHAHA! SUCKER! What? did you think I would give you something in return?", "See you NEVER, loser!"},
        0, new string[2]);

    public static void TriggerDialogue(string cutsceneName)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogueDictionary[cutsceneName]);
    }

}
