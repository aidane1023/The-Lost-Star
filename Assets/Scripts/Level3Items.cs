using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Items : MonoBehaviour
{
    public bool mozzieTalked;
    public bool watchFound, watchGiven;
    public bool lighterFound, lighterGiven;
    public bool ticketFound, ticketGiven;

    public NPCBehavior behaviorScript;

    private void Start()
    {
        mozzieTalked = false;
        watchFound = false;
        lighterFound = false;
        ticketFound = false;
    }

    public void Update()
    {
        if (ticketGiven == true)
        {
            behaviorScript.enabled = true;
            behaviorScript.interactObjects[0].SetActive(true);
        }
    }

    public void mozzie()
    {
        mozzieTalked = true;
    }

    public void watch()
    {
        watchFound = true;
    }

    public void lighter()
    {
        lighterFound = true;
    }

    public void ticket()
    {
        ticketFound = true;
    }


}
