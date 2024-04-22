using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private int totalValue = 0; //private total value
    public int TotalValue { get { return totalValue; } } //public totalvalue


    public void AddItemValue(int value)
    {
        totalValue += value;
    }

}
