using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCookie : MonoBehaviour
{
    public static bool FarmerTalk;
    public static bool playOnce = true;

    public PlayerInventory playerInventory;
    private InventoryItemData cookie;
    public Database itemDatabase;

    void Update()
    {
        if (!playOnce)
        {
            playOnce = AddCookie();
        }
        
    }

    bool AddCookie()
    {
        cookie = itemDatabase.GetItem(0);
        if (playerInventory.AddItem(cookie))
        {
            Debug.Log("Cookie Added");
            return true;

        }
        else
        {
            Debug.Log("Inventory is full.");
            return false;
        }
    }
}
