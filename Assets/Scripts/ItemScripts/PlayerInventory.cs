using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory")]
[System.Serializable]
public class PlayerInventory : ScriptableObject
{
    public InventoryItemData[] heldItems = new InventoryItemData[8];

    // Method to add an item to the inventory
    public bool AddItem(InventoryItemData item)
    {
        for (int i = 0; i < heldItems.Length; i++)
        {
            if (heldItems[i] == null) 
            {
                heldItems[i] = item; 
                return true; 
            }
        }
        return false; 
    }

     public int CountAvailableSlots()
    {
        int availableSlots = 0;
        for (int i = 0; i < heldItems.Length; i++)
        {
            if (heldItems[i] == null)
                availableSlots++;
        }
        return availableSlots;
    }
}
