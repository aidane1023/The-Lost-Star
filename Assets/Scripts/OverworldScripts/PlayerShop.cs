using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShop : MonoBehaviour
{

    private List<ItemDisplay> itemDisplays = new List<ItemDisplay>();
    //gets script podium display and creates a list for the objects in it

    private void Start()
    {
      

       itemDisplays.Clear();
        GameObject[] podiumObjects = GameObject.FindGameObjectsWithTag("podium"); //gets all the objects tagged podium and puts it into a list

        foreach (GameObject podiumObject in podiumObjects) //foreach loop that grabs each podiumdisplay script component so it can be use
        {
            ItemDisplay pd = podiumObject.GetComponent<ItemDisplay>();
            if (pd != null)
            {
                itemDisplays.Add(pd); //puts into list
            }
        }



    }

   
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("shop"))
        {
            //activates shop mode for all podiums that are held
            foreach (var display in itemDisplays)
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
            foreach (var display in itemDisplays)
            {
                display.activeShop = false;
            }
        }

    }
}
