using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SockPiece : MonoBehaviour
{
    public GameObject RocketPart;

    // Start is called before the first frame update
    void Start()
    {
        RocketPart.SetActive(false);

        if (JackTransition.tPlayed)
        {
            RocketPart.SetActive(true);
        }
    }
}
