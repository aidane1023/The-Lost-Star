using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory")]
[System.Serializable]
public class PlayerInventory : ScriptableObject
{
    //DO THIS
    public InventoryItemData[] heldItems = new InventoryItemData[8];
}
