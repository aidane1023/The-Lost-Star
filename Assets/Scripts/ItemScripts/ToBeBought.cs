using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/ToBeBought")]
[System.Serializable]
public class ToBeBought : ScriptableObject
{
    public InventoryItemData[] heldItems = new InventoryItemData[6];

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

    public int CountItems()
    {
        int availableSlots = 0;
        int totalSlots = 6;
        for (int i = 0; i < heldItems.Length; i++)
        {
            if (heldItems[i] == null)
                availableSlots++;
        }
        return totalSlots - availableSlots;
    }

    public void ClearItems()
    {
        for (int i = 0; i < heldItems.Length; i++)
        {
            heldItems[i] = null;
        }
    }
}
