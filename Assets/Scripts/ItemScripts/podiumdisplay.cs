using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class podiumdisplay : MonoBehaviour
{

    public Database itemDatabase;
    public 

    public void Update()
    {
        randomDisplay();
    }

    void randomDisplay()
    {
        if (itemDatabase == null || itemDatabase.Items.Count == 0) return;

        int randomID = Random.Range(0, itemDatabase.Items.Count);
        InventoryItemData randomItem = itemDatabase.GetItem(randomID);
        Debug.Log(randomItem);
    }
}
