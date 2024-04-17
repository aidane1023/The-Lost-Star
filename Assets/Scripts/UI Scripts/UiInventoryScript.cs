using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInventoryScript : MonoBehaviour
{
    public PlayerInventory inventory;

    public string[] itemName;
    //private string[] itemNameDisplay;
    int itemNameLength;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(inventory.heldItems[0].name);
        itemNameLength = inventory.heldItems.Length;
        //itemName.Length = new string[itemNameLength];
        //itemName[0] = inventory[0].Name;
        Debug.Log(itemNameLength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
