using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Items : MonoBehaviour
{
    public bool mozzieTalked;
    public bool watchFound;
    public bool lighterFound;
    public bool ticketFound;

    private void Start()
    {
        mozzieTalked = false;
        watchFound = false;
        lighterFound = false;
        ticketFound = false;
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
