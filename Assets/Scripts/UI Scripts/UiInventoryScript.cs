using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInventoryScript : MonoBehaviour
{
    public PlayerInventory inventory;

    public GameObject textName, textDescription;
    [HideInInspector]
    public string[] itemNames, itemDescriptions;
    [HideInInspector]
    public bool[] isEmpty;
    
    int itemLength;
    // Start is called before the first frame update
    void Start()
    {
        itemLength = inventory.heldItems.Length;
        itemNames = new string[itemLength];
        itemDescriptions = new string[itemLength];

        isEmpty = new bool[itemLength];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshInventory()
    {
        //Debug.Log(inventory.heldItems[0].Name);
        for (int i = 0; i < itemLength; i++)
        {
            if(inventory.heldItems[i] != null)
            {
                itemNames[i] = inventory.heldItems[i].Name;
                itemDescriptions[i] = inventory.heldItems[i].Description;
                isEmpty[i] = false;
            }
            else
            {
                itemNames[i] = "Empty";
                itemDescriptions[i] = "";
                isEmpty[i] = true;
            }
        }
        //itemName[0] = inventory.heldItems[0].Name;
        //Debug.Log(itemNames[0] + " " + itemLength);
    }
}
