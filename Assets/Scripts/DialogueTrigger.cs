using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Text pointsText;
    public int points = 0;
    public bool gobAccepted = false;
    public int gobRejectedOrIgnored = 0;

    public Slider slider;
    private float thenTime = 0;

    private Dialogue startingDialogue = new Dialogue("???",
        new[]
        {
            "Heeeey there!", "I see you've got some <b>POINTS</b>.", "Do you mind giving some to me?",
            "Aww, that's too bad..."
        }, 3, new[] {"YES", "NO"});
    private Dialogue annoyedGoblin = new Dialogue("???", new []{"Can you stop ignoring me?", "I see you've got some <b>POINTS</b>.", "Do you mind giving some to me?",
        "Aww, that's too bad..."}, 3, new[] { "YES", "NO" });

    private Dialogue sDY = new Dialogue("???",
        new[] {"HAHAHAHA! SUCKER! What? did you think I would give you something in return?", "See you NEVER, loser!"},
        0, new string[2]);

    public void clickPointsButton()
    {
        if (Time.time - thenTime > 1)
        {
            thenTime = Time.time;
            pointsChange(1);
            if (points >= 10 && !gobAccepted)
            {
                gobAccepted = true;
                TriggerDialogue(startingDialogue);
            } else if (gobAccepted)
            {
                if (gobRejectedOrIgnored >= 3)
                {
                    TriggerDialogue(annoyedGoblin);
                }
                gobRejectedOrIgnored++;
            }
        }
    }

    public void pointsChange(int pC)
    {
        points += pC;
        pointsText.text = "Points: " + points;

    }

    public void TriggerDialogue(Dialogue dialogue)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void Update()
    {
        slider.value = math.clamp(1-(Time.time - thenTime), 0, 1);
        slider.gameObject.GetComponentInChildren<Text>().text = "" + Math.Round(slider.value, 2);
    }

}
