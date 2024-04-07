using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShop : MonoBehaviour
{

    private List<PodiumDisplay> podiumDisplays = new List<PodiumDisplay>();
    //gets script podium display and creates a list for the objects in it

    private void Start()
    {
      

       podiumDisplays.Clear();
        GameObject[] podiumObjects = GameObject.FindGameObjectsWithTag("podium"); //gets all the objects tagged podium and puts it into a list

        foreach (GameObject podiumObject in podiumObjects) //foreach loop that grabs each podiumdisplay script component so it can be use
        {
            PodiumDisplay pd = podiumObject.GetComponent<PodiumDisplay>();
            if (pd != null)
            {
                podiumDisplays.Add(pd); //puts into list
            }
        }



    }

   
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("shop"))
        {
            //activates shop mode for all podiums that are held
            foreach (var display in podiumDisplays)
            {
                display.activeShop = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if (other.CompareTag("shop"))
        {
            // deactivates shop mode for all podiums that are held
            foreach (var display in podiumDisplays)
            {
                display.activeShop = false;
            }
        }

    }
}
