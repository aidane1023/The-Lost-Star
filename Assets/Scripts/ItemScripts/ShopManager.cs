using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private int totalValue = 0; 

   
    public int TotalValue { get { return totalValue; } }

   
    public void AddItemValue(int value)
    {
        totalValue += value;
    }

   
    public void ResetTotalValue()
    {
        totalValue = 0;
    }
}
