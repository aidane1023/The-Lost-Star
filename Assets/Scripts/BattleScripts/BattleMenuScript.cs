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
    public GameObject mainButtons, menuAttack, menuSpin, menuSkill, menuRun, menuBag;
    public GameObject[] backButtons, menuBgs, skillButtons, inventoryButtons;

    //TheCanvas
    public GameObject battleButtonCanvas, winScreen, promptImage;

    public GameObject playerHealthTextUI, playerSPTextUI, playerXPTextUI, xpBarUI, playerAttackTitleTextUI, 
    playerAttackDescTextUI, playerSpinTitleTextUI, playerSpinDescTextUI, playerItemTitleTextUI, playerItemDescTextUI,
    playerSkillTitleTextUI, playerSkillDescTextUI;
    public GameObject player, battleManager;
    //private PlayerBattler PlayerBattler;
    private BattleManager battleManagerScript;
    GameObject selectedOption, savedOption;
    public GameObject[] enemyAttackSelectorButtons, enemySpinSelectorButtons, enemySkillSelectorButtons, inventoryDisabledGraphic, inventorySelectedGraphic;
    GameObject[] defaultButtons;

    TextMeshProUGUI healthText, SPText, XPText, attackTitle, attackDesc, spinTitle, spinDesc, itemTitle, itemDesc, skillTitle, skillDesc;
    public TextMeshProUGUI[] inventoryButtonText;
    Image xpBarColorFill;
    UiInventoryScript inventory;
    Button[] inventoryButtonComponent, backButtonComponents;
    //EventTrigger[] buttonHoverTrigger;
    
    private Image attackColor, spinColor, skillColor, runColor, bagColor, inventoryDisabledImage;
    private Image[] backColor, skillButtonColor, inventoryButtonColor;
    int backButtonLength, skillButtonLength, inventoryButtonLength, currentMenu;
    float timer;

    Color disabledColor = new Color(0.4f, 0.4f, 0.4f, 1f);
    Color enabledColor = new Color(0.66f, 0.66f, 0.66f, 1f);
    Color notSelectedColor = new Color(0.66f, 0.66f, 0.66f, 1f);
    Color selectedColor = new Color(1f, 1f, 1f, 1f);
    Color skillSelectedColor = new Color(.6f, 1f, 0.9764706f, 1f);
    Color skillNotSelectedColor = new Color(0.373131f, 0.6132076f, 0.5990854f, 1f);

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //playerBattlerScript = player.GetComponent<PlayerBattler>();
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
        inventory = GetComponent<UiInventoryScript>();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuAttack);

        backButtonLength = backButtons.Length; // ok it's fixed now I think
        backColor = new Image[backButtonLength];
        backButtonComponents = new Button[backButtonLength];
        skillButtonLength = skillButtons.Length; 
        skillButtonColor = new Image[skillButtonLength];
        inventoryButtonLength = inventoryButtons.Length;
        inventoryButtonColor = new Image[inventoryButtonLength];
        inventoryButtonComponent = new Button[inventoryButtonLength];

        for (int i = 0; i < backButtonLength; i++)
        {
            backButtonComponents[i] = backButtons[i].GetComponent<Button>();
        }

        defaultButtons = new GameObject[6]; 
        defaultButtons[0] = menuAttack; //Main Buttons
        defaultButtons[1] = backButtons[0]; // Attack Default
        defaultButtons[2] = backButtons[1]; // Spin Default
        defaultButtons[3] = backButtons[3]; // Skill Default
        defaultButtons[4] = backButtons[4]; // Skill Selector Default
        defaultButtons[5] = backButtons[2]; // Bag Default

        //for (int i = 0; i < defaultButtons.Length; i++)
        //{
        //    Debug.Log("Default: " + defaultButtons[i]);
        //}


        battleButtonCanvas.SetActive(false);
    }

    void Update()
    {
        healthText.text = $"HP: {PlayerBattler.health}/{PlayerBattler.maxHealth}";
        SPText.text = $"SP: {PlayerBattler.starPoints}/{PlayerBattler.maxStarPoints}"; 
        XPText.text = $"XP: {Mathf.Round(PlayerBattler.xp)}/100";  
        xpBarColorFill.fillAmount = (PlayerBattler.xp/100);
        //Debug.Log("Selected game object:" + EventSystem.current.currentSelectedGameObject);
        selectedOption = EventSystem.current.currentSelectedGameObject;
        //Debug.Log("Selected game object: " + selectedOption);

        // This code is a mess but it works 
        if (EventSystem.current.currentSelectedGameObject == null) 
        {
            switch(currentMenu)
            {
                case 0:
                EventSystem.current.SetSelectedGameObject(defaultButtons[0]);
                break;

                case 1:
                EventSystem.current.SetSelectedGameObject(defaultButtons[1]);
                break;

                case 2:
                EventSystem.current.SetSelectedGameObject(defaultButtons[2]);
                break;

                case 3:
                EventSystem.current.SetSelectedGameObject(defaultButtons[5]);
                break;

                case 4:
                EventSystem.current.SetSelectedGameObject(defaultButtons[3]);
                break;

                case 5:
                EventSystem.current.SetSelectedGameObject(defaultButtons[4]);
                break;

                default:
                EventSystem.current.SetSelectedGameObject(defaultButtons[0]);
                break;
            }               
        }

        if (Input.GetButtonDown("Cancel"))
        {
            switch(currentMenu)
            {
                case 1:
                backButtonComponents[0].onClick.Invoke();
                break;

                case 2:
                backButtonComponents[1].onClick.Invoke();
                break;

                case 3:
                backButtonComponents[2].onClick.Invoke();
                break;

                case 4:
                backButtonComponents[3].onClick.Invoke();
                break;

                case 5:
                backButtonComponents[4].onClick.Invoke();
                break;

                default:
                break;
            }               
        }

        if (battleManagerScript.battleWon == true)
        {
            battleButtonCanvas.SetActive(true);
            player.layer = 0;
            mainButtons.SetActive(false);
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
        currentMenu = 3;
        inventory.RefreshInventory();
        savedOption = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(backButtons[2]);

        for (int i = 0; i < inventoryButtonLength; i++)
        {
            inventoryButtonText[i].text = inventory.itemNames[i];
           if (inventoryButtonColor[i] == null)
            {
                inventoryButtonColor[i] = inventoryButtons[i].GetComponent<Image>();
                inventoryButtonComponent[i] = inventoryButtons[i].GetComponent<Button>();
                //buttonHoverTrigger[i] = inventoryButtons[i].GetComponent<EventTrigger>();
                Debug.Log("Inventory " + i + " Button Component Cached");
            }
            if (inventory.isEmpty[i])
            {
                inventoryButtonComponent[i].interactable = false;
                inventoryButtonColor[i].color = disabledColor;
                inventoryDisabledGraphic[i].SetActive(true);
                inventorySelectedGraphic[i].SetActive(false);
                //buttonHoverTrigger[i].isTrigger = false;
            }
            else
            {
                inventoryButtonComponent[i].interactable = true;
                inventoryButtonColor[i].color = enabledColor;
                inventoryDisabledGraphic[i].SetActive(false);
                inventorySelectedGraphic[i].SetActive(true);
            }
        }
    }

    public void OpenSkills()
    {
        currentMenu = 4;
        savedOption = menuSkill;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(backButtons[3]);
    }

    public void OpenSkillSelector(int menuValue)
    {
        currentMenu = menuValue;
    }

    public void EnemyAttackSelector()
    {
        currentMenu = 1;
        savedOption = EventSystem.current.currentSelectedGameObject;
        //Debug.Log("Selected GameObject is: " + EventSystem.current.currentSelectedGameObject);
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
        currentMenu = 2;
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

    public void EnemySkillSelector()
    {
        currentMenu = 5;
        savedOption = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(enemySkillSelectorButtons[0]);
        
        Debug.Log("Enemy Count: " + battleManagerScript.enemyCount);
        for (int i = 0; i < battleManagerScript.enemyCount; i++)
            {
                enemySkillSelectorButtons[i].SetActive(true);
            }
    }

    public void ReturnMenu()
    {
        currentMenu = 0;
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
                backColor[2].color = selectedColor;
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
                    Debug.Log("Back Button Image Component Cached");
                }
                backColor[0].color = notSelectedColor;
                //Debug.Log("Attack Selector Back Button Color Changed");
                break;

            case "Back Spin":
                if (backColor[1] == null){
                    backColor[1] = backButtons[1].GetComponent<Image>();
                    Debug.Log("Back Button Image Component Cached");
                }
                backColor[1].color = notSelectedColor;
                //Debug.Log("Spin Selector Back Button Color Changed");
                break;

            case "Back Inventory":
                if (backColor[2] == null){
                    backColor[2] = backButtons[2].GetComponent<Image>();
                    Debug.Log("Back Button Image Component Cached");
                }
                backColor[2].color = notSelectedColor;
                //Debug.Log("Inventory Back Button Color Changed");
                break;
            
            case "Back Skill":
                if (backColor[3] == null){
                    backColor[3] = backButtons[3].GetComponent<Image>();
                    Debug.Log("Back Button Image Component Cached");
                }
                backColor[3].color = notSelectedColor;
                //Debug.Log("Skill Back Button Color Changed");
                break;

            case "Skill 1":
                if (skillButtonColor[0] == null)
                {
                    skillButtonColor[0] = skillButtons[0].GetComponent<Image>();
                    Debug.Log("Skill Button Image Component Cached");
                }
                skillButtonColor[0].color = skillNotSelectedColor;
                break;

            case "Skill 2":
                if (skillButtonColor[1] == null)
                {
                    skillButtonColor[1] = skillButtons[1].GetComponent<Image>();
                    Debug.Log("Skill Button Image Component Cached");
                }
                skillButtonColor[1].color = skillNotSelectedColor;
                break;

            case "Skill 3":
                if (skillButtonColor[2] == null)
                {
                    skillButtonColor[2] = skillButtons[2].GetComponent<Image>();
                    Debug.Log("Skill Button Image Component Cached");
                }
                skillButtonColor[2].color = skillNotSelectedColor;
                break;

                case "Inventory 1":
                if (inventoryButtonColor[0] == null){
                    inventoryButtonColor[0] = inventoryButtons[0].GetComponent<Image>();
                    Debug.Log("Inventory 1 Button Component Cached");
                }
                if (inventory.isEmpty[0] != true)
                {
                    inventoryButtonColor[0].color = notSelectedColor;
                }
                break;

                case "Inventory 2":
                if (inventoryButtonColor[1] == null){
                    inventoryButtonColor[1] = inventoryButtons[1].GetComponent<Image>();
                    Debug.Log("Inventory 2 Button Component Cached");
                }
                if (inventory.isEmpty[1] != true)
                {
                    inventoryButtonColor[1].color = notSelectedColor;
                }
                break;

                case "Inventory 3":
                if (inventoryButtonColor[2] == null){
                    inventoryButtonColor[2] = inventoryButtons[2].GetComponent<Image>();
                    Debug.Log("Inventory 3 Button Component Cached");
                }
                if (inventory.isEmpty[2] != true)
                {
                    inventoryButtonColor[2].color = notSelectedColor;
                }
                break;

                case "Inventory 4":
                if (inventoryButtonColor[3] == null){
                    inventoryButtonColor[3] = inventoryButtons[3].GetComponent<Image>();
                    Debug.Log("Inventory 4 Button Component Cached");
                }
                if (inventory.isEmpty[3] != true)
                {
                    inventoryButtonColor[3].color = notSelectedColor;
                }
                break;

                case "Inventory 5":
                if (inventoryButtonColor[4] == null){
                    inventoryButtonColor[4] = inventoryButtons[4].GetComponent<Image>();
                    Debug.Log("Inventory 5 Button Component Cached");
                }
                if (inventory.isEmpty[4] != true)
                {
                    inventoryButtonColor[4].color = notSelectedColor;
                }
                break;

                case "Inventory 6":
                if (inventoryButtonColor[5] == null){
                    inventoryButtonColor[5] = inventoryButtons[5].GetComponent<Image>();
                    Debug.Log("Inventory 6 Button Component Cached");
                }
                if (inventory.isEmpty[5] != true)
                {
                    inventoryButtonColor[5].color = notSelectedColor;
                }
                break;

                case "Inventory 7":
                if (inventoryButtonColor[6] == null){
                    inventoryButtonColor[6] = inventoryButtons[6].GetComponent<Image>();
                    Debug.Log("Inventory 7 Button Component Cached");
                }
                if (inventory.isEmpty[6] != true)
                {
                    inventoryButtonColor[6].color = notSelectedColor;
                }
                break;

                case "Inventory 8":
                if (inventoryButtonColor[7] == null){
                    inventoryButtonColor[7] = inventoryButtons[7].GetComponent<Image>();
                    Debug.Log("Inventory 8 Button Component Cached");
                }
                if (inventory.isEmpty[7] != true)
                {
                    inventoryButtonColor[7].color = notSelectedColor;
                }
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
                if (inventoryButtonColor[0] == null)
                {
                    inventoryButtonColor[0] = inventoryButtons[0].GetComponent<Image>();
                    Debug.Log("Inventory 1 Button Component Cached");
                }
                if (inventory.isEmpty[0] != true)
                {
                    inventoryButtonColor[0].color = selectedColor;
                    itemTitle.text = inventory.itemNames[0];
                    itemDesc.text = inventory.itemDescriptions[0];
                }
               

                break;

            case 2:
                if (inventoryButtonColor[1] == null)
                {
                    inventoryButtonColor[1] = inventoryButtons[1].GetComponent<Image>();
                    Debug.Log("Inventory 2 Button Component Cached");
                }
                if (inventory.isEmpty[1] != true)
                {
                    inventoryButtonColor[1].color = selectedColor;
                    itemTitle.text = inventory.itemNames[1];
                    itemDesc.text = inventory.itemDescriptions[1];
                }

                break;

            case 3:
                

                if (inventoryButtonColor[2] == null)
                {
                    inventoryButtonColor[2] = inventoryButtons[2].GetComponent<Image>();
                    Debug.Log("Inventory 3 Button Component Cached");
                }
                if (inventory.isEmpty[2] != true)
                {
                    inventoryButtonColor[2].color = selectedColor;
                    itemTitle.text = inventory.itemNames[2];
                    itemDesc.text = inventory.itemDescriptions[2];
                }

                break;

            case 4:
                if (inventoryButtonColor[3] == null)
                {
                    inventoryButtonColor[3] = inventoryButtons[3].GetComponent<Image>();
                    Debug.Log("Inventory 4 Button Component Cached");
                }
                if (inventory.isEmpty[3] != true)
                {
                    inventoryButtonColor[3].color = selectedColor;
                    itemTitle.text = inventory.itemNames[3];
                    itemDesc.text = inventory.itemDescriptions[3];
                }

                break;

            case 5:
                    if (inventoryButtonColor[4] == null)
                    {
                    inventoryButtonColor[4] = inventoryButtons[4].GetComponent<Image>();
                    Debug.Log("Inventory 5 Button Component Cached");
                }
                if (inventory.isEmpty[4] != true)
                {
                    inventoryButtonColor[4].color = selectedColor;
                    itemTitle.text = inventory.itemNames[4];
                    itemDesc.text = inventory.itemDescriptions[4];
                }

                break;

            case 6:
                if (inventoryButtonColor[5] == null)
                {
                    inventoryButtonColor[5] = inventoryButtons[5].GetComponent<Image>();
                    Debug.Log("Inventory 6 Button Component Cached");
                }
                if (inventory.isEmpty[5] != true)
                {
                    inventoryButtonColor[5].color = selectedColor;
                    itemTitle.text = inventory.itemNames[5];
                    itemDesc.text = inventory.itemDescriptions[5];
                }

                break;

            case 7:
                if (inventoryButtonColor[6] == null)
                {
                    inventoryButtonColor[6] = inventoryButtons[6].GetComponent<Image>();
                    Debug.Log("Inventory 7 Button Component Cached");
                }
                if (inventory.isEmpty[6] != true)
                {
                    inventoryButtonColor[6].color = selectedColor;
                    itemTitle.text = inventory.itemNames[6];
                    itemDesc.text = inventory.itemDescriptions[6];
                }

                break;

            case 8:               
                if (inventoryButtonColor[7] == null)
                {
                    inventoryButtonColor[7] = inventoryButtons[7].GetComponent<Image>();
                    Debug.Log("Inventory 8 Button Component Cached");
                }
                if (inventory.isEmpty[7] != true)
                {
                    inventoryButtonColor[7].color = selectedColor;
                    itemTitle.text = inventory.itemNames[7];
                    itemDesc.text = inventory.itemDescriptions[7];
                }

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
                if (skillButtonColor[0] == null)
                {
                    skillButtonColor[0] = skillButtons[0].GetComponent<Image>();
                    Debug.Log("Skill Button Image Component Cached");
                }
                skillButtonColor[0].color = skillSelectedColor;
                break;

            case 2:
                skillTitle.text = "Skill Title 2";
                skillDesc.text = "Skill Description 2";
                if (skillButtonColor[1] == null)
                {
                    skillButtonColor[1] = skillButtons[1].GetComponent<Image>();
                    Debug.Log("Skill Button Image Component Cached");
                }
                skillButtonColor[1].color = skillSelectedColor;
                break;
            
            case 3:
                skillTitle.text = "Skill Title 3";
                skillDesc.text = "Skill Description 3";
                if (skillButtonColor[2] == null)
                {
                    skillButtonColor[2] = skillButtons[2].GetComponent<Image>();
                    Debug.Log("Skill Button Image Component Cached");
                }
                skillButtonColor[2].color = skillSelectedColor;
                break;

            default:
                skillTitle.text = "";
                skillDesc.text = "Please select a skill.";
                break;
        }
    }
}