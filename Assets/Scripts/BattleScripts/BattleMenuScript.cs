using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BattleMenuScript : MonoBehaviour
{
    public GameObject menuAttack, menuSpin, menuSkill, menuRun, menuBag;
    public GameObject inventoryButton1, enemyAttackButton1, enemySpinButton1;

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

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuAttack);
        //Debug.Log("Selected game object:" + EventSystem.current.currentSelectedGameObject.name);
        //Debug.Log("Health: " + playerBattlerScript.health);
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
    }

    public void EnemySpinSelector()
    {
        savedOption = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(enemySpinButton1);
    }

    public void ReturnMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(savedOption);
    }
}