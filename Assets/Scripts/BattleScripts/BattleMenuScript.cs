using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class BattleMenuScript : MonoBehaviour
{
    public GameObject menuAttack, menuSpin, menuSkill, menuRun, menuBag;
    public GameObject inventoryButton1, enemyAttackButton1, enemySpinButton1;

    //TheCanvas
    public GameObject battleButtonCanvas;

    public GameObject playerHealthTextUI, playerSPTextUI, playerXPTextUI, playerAttackTitleTextUI, playerAttackDescTextUI, playerSpinTitleTextUI, playerSpinDescTextUI, playerItemTitleTextUI, playerItemDescTextUI;
    public GameObject player;
    private PlayerBattler playerBattlerScript;
    GameObject selectedOption, savedOption;

    TextMeshProUGUI healthText, SPText, XPText, attackTitle, attackDesc, spinTitle, spinDesc, itemTitle, itemDesc;

    void Start()
    {
        playerBattlerScript = player.GetComponent<PlayerBattler>();
        healthText = playerHealthTextUI.GetComponent<TextMeshProUGUI>();
        SPText = playerSPTextUI.GetComponent<TextMeshProUGUI>();
        XPText = playerXPTextUI.GetComponent<TextMeshProUGUI>();

        //attackTitle = playerAttackTitleTextUI.GetComponent<TextMeshProUGUI>();
        //attackDesc = playerAttackDescTextUI.GetComponent<TextMeshProUGUI>();
        //spinTitle = playerSpinTitleTextUI.GetComponent<TextMeshProUGUI>();
        //spinDesc = playerSpinDescTextUI.GetComponent<TextMeshProUGUI>();
        //itemTitle = playerItemTitleTextUI.GetComponent<TextMeshProUGUI>();
        //itemDesc = playerItemDescTextUI.GetComponent<TextMeshProUGUI>();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuAttack);
        //Debug.Log("Selected game object:" + EventSystem.current.currentSelectedGameObject.name);
        //Debug.Log("Health: " + playerBattlerScript.health);

        battleButtonCanvas.SetActive(false);
    }

    void Update()
    {
        healthText.text = $"Health: {playerBattlerScript.health}/{playerBattlerScript.maxHealth}";
        SPText.text = $"SP: {playerBattlerScript.starPoints}/{playerBattlerScript.maxStarPoints}"; 
        XPText.text = $"XP: {playerBattlerScript.xp}/100";  
        //Debug.Log("Selected game object:" + EventSystem.current.currentSelectedGameObject);
        selectedOption = EventSystem.current.currentSelectedGameObject;
        //Debug.Log("Selected game object: " + selectedOption);       
    }

    void menuAnimator()
    {
        //EventSystem.current.currentSelectedGameObject;
    }

    public void OpenInventory()
    {
        savedOption = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(inventoryButton1);
    }

    public void EnemyAttackSelector()
    {
        savedOption = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(enemyAttackButton1);
        
        //attackTitle.text = "Jump!";
        //attackDesc.text = "Jump on the enemy and press Z at the right time to jump again and deal even more damage!";
    }

    public void EnemySpinSelector()
    {
        savedOption = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(enemySpinButton1);

        spinTitle.text = "Spin!";
        spinDesc.text = "Idk what to put here yet tbh...";
    }

    public void ReturnMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(savedOption);
    }

    public void ItemDescriptions(int value)
    {
        switch (value)
        {
            case 1:
                itemTitle.text = "Item Title 1";
                itemDesc.text = "Item Description 1";
                break;

            case 2:
                itemTitle.text = "Item Title 2";
                itemDesc.text = "Item Description 2";
                break;
            
            case 3:
                itemTitle.text = "Item Title 3";
                itemDesc.text = "Item Description 3";
                break;
            
            case 4:
                itemTitle.text = "Item Title 4";
                itemDesc.text = "Item Description 4";
                break;
            
            case 5:
                itemTitle.text = "Item Title 5";
                itemDesc.text = "Item Description 5";
                break;
            
            case 6:
                itemTitle.text = "Item Title 6";
                itemDesc.text = "Item Description 6";
                break;
            
            case 7:
                itemTitle.text = "Item Title 7";
                itemDesc.text = "Item Description 7";
                break;
            
            case 8:
                itemTitle.text = "Item Title 8";
                itemDesc.text = "Item Description 8";
                break;
            
            default:
                itemTitle.text = "";
                itemDesc.text = "Select an item.";
                break;      
        }
    }
}