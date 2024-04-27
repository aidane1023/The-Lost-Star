using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID = -1;
    public string Name;
    public bool isConsumable = true;
    public AttackName attack;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public int maxStackSize;
    public int Durability;
    public int maxDurability;
    public int Value;
    public int healthRestored, spRestored;
    public int spCost;

    //NOTE THIS GOES UNUSED, IGNORE THIS FUNCTION
    public void ConsumableUsed(bool inBattle)
    {
        if(inBattle)
        {
            PlayerBattler.health += healthRestored;
            if(PlayerBattler.health > PlayerBattler.maxHealth) PlayerBattler.health = PlayerBattler.maxHealth;
            PlayerBattler.starPoints += spRestored;
            if(PlayerBattler.starPoints > PlayerBattler.maxStarPoints) PlayerBattler.starPoints = PlayerBattler.maxStarPoints;
        }
        //remove the item
    }

    public bool IsAttack()
    {
        if(attack == AttackName.Null) return false;
        else return true;
    }
}
