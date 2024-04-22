using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetector : MonoBehaviour
{
    private WallPuzzle puzzle;
    public GameObject wallPuzzle;

    private void Start()
    {
        puzzle = wallPuzzle.GetComponent<WallPuzzle>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("ball"))
        {
            if (gameObject.CompareTag("blueRed"))
            {
                puzzle.BlueMove();
                puzzle.RedMove();
                // wall move
            }

            if (gameObject.CompareTag("greenBlue"))
            {
                puzzle.GreenMove();
                puzzle.BlueMove();
                //wall move
            }

        }
        
    }
}
