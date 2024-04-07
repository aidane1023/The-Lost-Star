using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BattleMenuScript : MonoBehaviour
{
    public GameObject menuAttack, menuSpin, menuSkill, menuRun, menuBag;
    public GameObject inventoryButton1, enemyAttackButton1, enemySpinButton1;

    //TheCanvas
    public GameObject battleButtonCanvas;

    public GameObject playerHealthTextUI, playerSPTextUI, playerXPTextUI;
    public GameObject player;
    private PlayerBattler playerBattlerScript;
    GameObject selectedOption, savedOption;

    TextMeshProUGUI healthText, SPText, XPText;

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

        //spinTitle.text = "Spin!";
        //spinDesc.text = "Idk what to put here yet tbh...";
    }

    public void ReturnMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(savedOption);
    }
}