using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleMenuScript : MonoBehaviour
{
    public GameObject menuAttack, menuSpin, menuSkill, menuRun, menuBag;
    public GameObject[] backButtons;

    //TheCanvas
    public GameObject battleButtonCanvas, winScreen, promptImage;

    public GameObject playerHealthTextUI, playerSPTextUI, playerXPTextUI, xpBarUI, playerAttackTitleTextUI, 
    playerAttackDescTextUI, playerSpinTitleTextUI, playerSpinDescTextUI, playerItemTitleTextUI, playerItemDescTextUI,
    playerSkillTitleTextUI, playerSkillDescTextUI;
    public GameObject player, battleManager;
    private PlayerBattler playerBattlerScript;
    private BattleManager battleManagerScript;
    GameObject selectedOption, savedOption;
    public GameObject[] enemyAttackSelectorButtons, enemySpinSelectorButtons, inventoryButtons, skillButtons;

    TextMeshProUGUI healthText, SPText, XPText, attackTitle, attackDesc, spinTitle, spinDesc, itemTitle, itemDesc, skillTitle, skillDesc;
    Image xpBarColorFill;
    
    private Image attackColor, spinColor, skillColor, runColor, bagColor;
    private Image[] backColor;
    int buttonLength;
    float timer;

    Color notSelectedColor = new Color(0.66f, 0.66f, 0.66f, 1f);
    Color selectedColor = new Color(1f, 1f, 1f, 1f);
    Color skillSelectedColor = new Color(.6f, 1f, 0.9764706f, 1f);
    Color skillNotSelectedColor = new Color(0.373131f, 0.6132076f, 0.5990854f, 1f);

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
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
        skillTitle = playerSkillTitleTextUI.GetComponent<TextMeshProUGUI>();
        skillDesc = playerSkillDescTextUI.GetComponent<TextMeshProUGUI>();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuAttack);

        battleButtonCanvas.SetActive(false);

        buttonLength = backButtons.Length; // ok it's fixed now I think
        backColor = new Image[buttonLength];
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
        
        if (battleManagerScript.battleWon == true)
        {
            battleButtonCanvas.SetActive(true);
            menuAttack.SetActive(false);
            menuSpin.SetActive(false);
            menuSkill.SetActive(false);
            menuRun.SetActive(false);
            menuBag.SetActive(false);
            winScreen.SetActive(true);

            if (timer < 2.0f)
            {
                timer += Time.deltaTime;
            }
            else 
            {
                promptImage.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    SceneManager.LoadScene(BattleManager.sceneToLoad);
                }
            }
        }
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

    public void OpenSkills()
    {
        savedOption = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(skillButtons[0]);
    }

    public void EnemyAttackSelector()
    {
        savedOption = EventSystem.current.currentSelectedGameObject;
        Debug.Log("Selected GameObject is: " + EventSystem.current.currentSelectedGameObject);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(enemyAttackSelectorButtons[0]);

        Debug.Log("Enemy Count: " + battleManagerScript.enemyCount);
        for (int i = 0; i < battleManagerScript.enemyCount; i++)
            {
                enemyAttackSelectorButtons[i].SetActive(true);
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

    public void ReturnMenu(GameObject returnButton)
    {
        EventSystem.current.SetSelectedGameObject(null);
        //Debug.Log("Selected GameObject is: Null");
        //Debug.Log("Saved Option is: " + savedOption);
        EventSystem.current.SetSelectedGameObject(savedOption);
        //Debug.Log("Selected GameObject is: " + EventSystem.current.currentSelectedGameObject);
    }

    public void SelectedColor(string buttonName)
    {
        switch (buttonName)
        {
            case "Attack":
                attackColor.color = selectedColor;
                //Debug.Log("Attack Sprite Color Changed");
                break;

            case "Spin":
                spinColor.color = selectedColor;
                //Debug.Log("Spin Sprite Color Changed");
                break;

            case "Skill":
                skillColor.color = selectedColor;
                //Debug.Log("Skill Sprite Color Changed");
                break;
            
            case "Run":
                runColor.color = selectedColor;
                //Debug.Log("Run Sprite Color Changed");
                break;
            
            case "Bag":
                bagColor.color = selectedColor;
                //Debug.Log("Bag Sprite Color Changed");
                break;

            case "Back Attack":
                if (backColor[0] == null){
                    backColor[0] = backButtons[0].GetComponent<Image>();
                    Debug.Log("Back Button Component");
                }
                backColor[0].color = selectedColor;
                //Debug.Log("Attack Selector Back Button Color Changed");
                break;

            case "Back Spin":
                if (backColor[1] == null){
                    backColor[1] = backButtons[1].GetComponent<Image>();
                    Debug.Log("Back Button Component");
                }
                backColor[1].color = selectedColor;
                //Debug.Log("Spin Selector Back Button Color Changed");
                break;

            case "Back Inventory":
                if (backColor[2] == null){
                    backColor[2] = backButtons[2].GetComponent<Image>();
                    Debug.Log("Back Button Component");
                }
                backColor[0].color = selectedColor;
                //Debug.Log("Inventory Back Button Color Changed");
                break;
            
            case "Back Skill":
                if (backColor[3] == null){
                    backColor[3] = backButtons[3].GetComponent<Image>();
                    Debug.Log("Back Button Component");
                }
                backColor[3].color = selectedColor;
                //Debug.Log("Skill Back Button Color Changed");
                break;

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
                //Debug.Log("Attack Sprite Color Changed");
                break;

            case "Spin":
                spinColor.color = notSelectedColor;
                //Debug.Log("Spin Sprite Color Changed");
                break;

            case "Skill":
                skillColor.color = notSelectedColor;
                //Debug.Log("Skill Sprite Color Changed");
                break;
            
            case "Run":
                runColor.color = notSelectedColor;
                //Debug.Log("Run Sprite Color Changed");
                break;
            
            case "Bag":
                bagColor.color = notSelectedColor;
                //Debug.Log("Bag Sprite Color Changed");
                break;

            case "Back Attack":
                if (backColor[0] == null){
                    backColor[0] = backButtons[0].GetComponent<Image>();
                    Debug.Log("Back Button Component");
                }
                backColor[0].color = notSelectedColor;
                //Debug.Log("Attack Selector Back Button Color Changed");
                break;

            case "Back Spin":
                if (backColor[1] == null){
                    backColor[1] = backButtons[1].GetComponent<Image>();
                    Debug.Log("Back Button Component");
                }
                backColor[1].color = notSelectedColor;
                //Debug.Log("Spin Selector Back Button Color Changed");
                break;

            case "Back Inventory":
                if (backColor[2] == null){
                    backColor[2] = backButtons[2].GetComponent<Image>();
                    Debug.Log("Back Button Component");
                }
                backColor[0].color = notSelectedColor;
                //Debug.Log("Inventory Back Button Color Changed");
                break;
            
            case "Back Skill":
                if (backColor[3] == null){
                    backColor[3] = backButtons[3].GetComponent<Image>();
                    Debug.Log("Back Button Component");
                }
                backColor[3].color = notSelectedColor;
                //Debug.Log("Skill Back Button Color Changed");
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

    public void SkillDescriptions(int value)
    {
        switch(value)
        {
            case 1:
                skillTitle.text = "Skill Title 1";
                skillDesc.text = "Skill Description 1";
                break;

            case 2:
                skillTitle.text = "Skill Title 2";
                skillDesc.text = "Skill Description 2";
                break;
            
            case 3:
                skillTitle.text = "Skill Title 3";
                skillDesc.text = "Skill Description 3";
                break;

            case 4:
                skillTitle.text = "Skill Title 4";
                skillDesc.text = "Skill Description 4";
                break;

            case 5:
                skillTitle.text = "Skill Title 5";
                skillDesc.text = "Skill Description 5";
                break;

            case 6:
                skillTitle.text = "Skill Title 6";
                skillDesc.text = "Skill Description 6";
                break;

            case 7:
                skillTitle.text = "Skill Title 7";
                skillDesc.text = "Skill Description 7";
                break;

            case 8:
                skillTitle.text = "Skill Title 8";
                skillDesc.text = "Skill Description 8";
                break;

            case 9:
                skillTitle.text = "Skill Title 9";
                skillDesc.text = "Skill Description 9";
                break;

            case 10:
                skillTitle.text = "Skill Title 10";
                skillDesc.text = "Skill Description 10";
                break;

            default:
                skillTitle.text = "";
                skillDesc.text = "Please select an item.";
                break;
        }
    }
}