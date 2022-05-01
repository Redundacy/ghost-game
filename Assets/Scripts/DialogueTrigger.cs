using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public string triggeredCutscene;
    private static Dictionary<string, DialogueNode> dialogueDictionary = new Dictionary<string, DialogueNode>()
    {
        {"OpeningDialogue1", new DialogueNode("???",new[] {"Hey . . .", "HEY! WAKE UP!"}, 2, new[] {"OpeningDialogue2", null}, new[] {"NEXT", null})},
        {"OpeningDialogue2", new DialogueNode("You", new []{"What?", "Who are you?", "Where am I?"}, 3, new []{"OpeningDialogue3", null}, new []{"NEXT", null})},
        {"OpeningDialogue3", new DialogueNode("Jimmy?", new[] {"My name's Jimmy.","You were stuck in that hole down there.","Did you fall asleep during the party?", "Nice costume, by the way."}, 4, new[] {"OpeningDialogue4", ""}, new[] {"NEXT", null})},
        {"OpeningDialogue4", new DialogueNode("You", new[] {"What party? What costume?", "I don't know what I'm doing here."}, 2, new[] {"OpeningDialogue5", ""}, new[] {"NEXT", null})},
        {"OpeningDialogue5", new DialogueNode("Jimmy", new[] {"Let me show you!","First we gotta get out of this yard.","I was looking for a nice place to enjoy the music, and got locked in here.", "If we work together, we can get back to the party."}, 4, new[] {null, ""}, new[] {"NEXT", null})},
        {"LeverCutscene1", new DialogueNode("Jimmy", new []{"Alright!","Now, we just need to get past the rest of these gates.","You may have to move some rave equipment around to open them.",}, 3, new []{"LeverCutscene2", ""}, new []{"NEXT", null})},
        {"LeverCutscene2", new DialogueNode("You", new []{"Okay.","(I wonder how he gets around.)","(Maybe I can speed him up somehow.)",}, 3, new []{null, ""}, new []{"NEXT", null})},
        {"FinalRoomDialogue1", new DialogueNode("Jimmy", new []{"And the rave is right over there."}, 1, new []{"FinalRoomDialogue2", ""}, new []{"NEXT", null})},
        {"FinalRoomDialogue2", new DialogueNode("You", new []{"But I don't want to go to a rave.","I want to figure out why I'm here."}, 2, new []{"FinalRoomDialogue3", ""}, new []{"NEXT", null})},
        {"FinalRoomDialogue3", new DialogueNode("Jimmy", new []{"Oh, if that's the case, then I can help you get to the entrance of the graveyard.", "Since the rave is crowding the normal path, we should be able to go through this crypt."}, 2, new []{null, ""}, new []{"NEXT", null})}
    };

    public void TriggerDialogue(string cutsceneName)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogueDictionary[cutsceneName]);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        TriggerDialogue(triggeredCutscene);
    }

}
