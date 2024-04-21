using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : MonoBehaviour
{
    public PlayerInventory inventory;

    public GameObject textName, textDescription, price;
    private string[] itemName;
    int itemNameLength;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(inventory.heldItems[0].Name);
        itemNameLength = inventory.heldItems.Length;
        itemName = new string[inventory.heldItems.Length];
        itemName[0] = inventory.heldItems[0].Name;
        Debug.Log(itemName[0] + " " + itemNameLength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
