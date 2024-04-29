using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInventoryScript : MonoBehaviour
{
    public PlayerInventory playerInventory;

    public GameObject textName, textDescription;
    [HideInInspector]
    public string[] itemNames, itemDescriptions;
    [HideInInspector]
    public int[] itemID;
    [HideInInspector]
    public bool[] isEmpty;
    [HideInInspector]
    public bool[] isConsumable;
    
    int itemLength;
    // Start is called before the first frame update
    void Start()
    {
        itemLength = playerInventory.heldItems.Length;
        itemNames = new string[itemLength];
        itemDescriptions = new string[itemLength];

        isEmpty = new bool[itemLength];
        isConsumable = new bool[itemLength];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshInventory()
    {
        //Debug.Log(playerInventory.heldItems[0].Name);
        for (int i = 0; i < itemLength; i++)
        {
            if(playerInventory.heldItems[i] != null && playerInventory.heldItems[i].ID != -1)
            {
                itemNames[i] = playerInventory.heldItems[i].Name;
                itemDescriptions[i] = playerInventory.heldItems[i].Description;
                isConsumable[i] = playerInventory.heldItems[i].isConsumable;
                //itemID
                isEmpty[i] = false;
            }
            else
            {
                itemNames[i] = "Empty";
                itemDescriptions[i] = "";
                isEmpty[i] = true;
            }
        }
        //itemName[0] = playerInventory.heldItems[0].Name;
        //Debug.Log(itemNames[0] + " " + itemLength);
    }
}
