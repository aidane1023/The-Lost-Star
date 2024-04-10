using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class BattleMenuScript : MonoBehaviour
{
    public GameObject menuAttack, menuSpin, menuSkill, menuRun, menuBag;
    public GameObject[] backButtons;
    //public GameObject inventoryButton1;

    //TheCanvas
    public GameObject battleButtonCanvas;

    public GameObject playerHealthTextUI, playerSPTextUI, playerXPTextUI, xpBarUI, playerAttackTitleTextUI, playerAttackDescTextUI, playerSpinTitleTextUI, playerSpinDescTextUI, playerItemTitleTextUI, playerItemDescTextUI;
    public GameObject player, battleManager;
    private PlayerBattler playerBattlerScript;
    private BattleManager battleManagerScript;
    GameObject selectedOption, savedOption;
    public GameObject[] enemyAttackSelectorButtons, enemySpinSelectorButtons, inventoryButtons;

    TextMeshProUGUI healthText, SPText, XPText, attackTitle, attackDesc, spinTitle, spinDesc, itemTitle, itemDesc;
    Image xpBarColorFill;
    
    private Image attackColor, spinColor, skillColor, runColor, bagColor;
    private Image[] backColor;
    int buttonLength;

    Color notSelectedColor = new Color(0.3f, 0.3f, 0.3f, 1f);
    Color selectedColor = new Color(1f, 1f, 1f, 1f);

    void Start()
    {
        playerBattlerScript = player.GetComponent<PlayerBattler>();
        battleManagerScript = battleManager.GetComponent<BattleManager>();
        healthText = playerHealthTextUI.GetComponent<TextMeshProUGUI>();
        SPText = playerSPTextUI.GetComponent<TextMeshProUGUI>();
        XPText = playerXPTextUI.GetComponent<TextMeshProUGUI>();
        xpBarColorFill = xpBarUI.GetComponent<Image>();

        attackColor = menuAttack.GetComponent<Image>();
        spinColor = menuSpin.GetComponent<Image>();
        skillColor = menuSkill.GetComponent<Image>();
        runColor = menuRun.GetComponent<Image>();
        bagColor = menuBag.GetComponent<Image>();

        attackTitle = playerAttackTitleTextUI.GetComponent<TextMeshProUGUI>();
        attackDesc = playerAttackDescTextUI.GetComponent<TextMeshProUGUI>();
        itemTitle = playerItemTitleTextUI.GetComponent<TextMeshProUGUI>();
        itemDesc = playerItemDescTextUI.GetComponent<TextMeshProUGUI>();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuAttack);

        battleButtonCanvas.SetActive(false);
    }

    void Update()
    {
        healthText.text = $"HP: {playerBattlerScript.health}/{playerBattlerScript.maxHealth}";
        SPText.text = $"SP: {playerBattlerScript.starPoints}/{playerBattlerScript.maxStarPoints}"; 
        XPText.text = $"XP: {Mathf.Round(playerBattlerScript.xp)}/100";  
        xpBarColorFill.fillAmount = (playerBattlerScript.xp/100);
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
        EventSystem.current.SetSelectedGameObject(inventoryButtons[0]);
    }

    public void EnemyAttackSelector()
    {
        savedOption = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(enemyAttackSelectorButtons[0]);

        Debug.Log("Enemy Count: " + battleManagerScript.enemyCount);
        for (int i = 0; i < battleManagerScript.enemyCount; i++)
            {
                enemyAttackSelectorButtons[i].SetActive(true);
            }

        buttonLength = backButtons.Length; // FIX THIS SHIT!!!!! NOW!!!!
        for (int i = 0; i < buttonLength; i++)
        {
            if (backButtons[i].activeSelf == true && backColor[i] != null)
            {
                backColor[i] = backButtons[i].GetComponent<Image>();
            }
        }

        attackTitle.text = "Jump!";
        attackDesc.text = "Jump on the enemy and press Z at the right time to jump again and deal even more damage!";
    }

    public void EnemySpinSelector()
    {
        savedOption = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(enemySpinSelectorButtons[0]);

        Debug.Log("Enemy Count: " + battleManagerScript.enemyCount);
        for (int i = 0; i < battleManagerScript.enemyCount; i++)
            {
                enemySpinSelectorButtons[i].SetActive(true);
            }

        attackTitle.text = "Spin!";
        attackDesc.text = "Hold    until the right time to deal extra damage!";
    }

    public void ReturnMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(savedOption);
    }

    public void SelectedColor(string buttonName)
    {
        switch (buttonName)
        {
            case "Attack":
                attackColor.color = selectedColor;
                Debug.Log("Attack Sprite Color Changed");
                break;

            case "Spin":
                spinColor.color = selectedColor;
                Debug.Log("Spin Sprite Color Changed");
                break;

            case "Skill":
                skillColor.color = selectedColor;
                Debug.Log("Skill Sprite Color Changed");
                break;
            
            case "Run":
                runColor.color = selectedColor;
                Debug.Log("Run Sprite Color Changed");
                break;
            
            case "Bag":
                bagColor.color = selectedColor;
                Debug.Log("Bag Sprite Color Changed");
                break;

            //case "Back 1":
            //    backColor[0].color = selectedColor;
            //    Debug.Log("Attack Selector Back Button Color Changed");
            //    break;

            case "default":
                break;
        }
    }

    public void DeselectedColor(string buttonName)
    {
        switch(buttonName)
        {
            case "Attack":
                attackColor.color = notSelectedColor;
                Debug.Log("Attack Sprite Color Changed");
                break;

            case "Spin":
                spinColor.color = notSelectedColor;
                Debug.Log("Spin Sprite Color Changed");
                break;

            case "Skill":
                skillColor.color = notSelectedColor;
                Debug.Log("Skill Sprite Color Changed");
                break;
            
            case "Run":
                runColor.color = notSelectedColor;
                Debug.Log("Run Sprite Color Changed");
                break;
            
            case "Bag":
                bagColor.color = notSelectedColor;
                Debug.Log("Bag Sprite Color Changed");
                break;

            case "default":
                break;  
        }
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