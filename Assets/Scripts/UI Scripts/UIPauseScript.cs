using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIPauseScript : MonoBehaviour
{
    public GameObject[] inventoryButtons, backButtons, inventorySelectedGraphic, inventoryDisabledGraphic, defaultButtons;
    public TextMeshProUGUI[] inventoryButtonText;
    public GameObject pauseMenu, pausePage, pauseResumeButton, pauseInventoryButton, pauseControlsButton, itemTitleUI, itemDescUI, menuControls, controlsUI, keyboardButton,
    itemDescriptionsUI, itemPanelUI;
    GameObject savedOption;
    UiInventoryScript inventory;

    TextMeshProUGUI itemTitle, itemDesc;

    Scene scene;
    string sceneName;

    int inventoryButtonLength, currentMenu;
    private Image[] inventoryButtonColor;
    Button[] inventoryButtonComponent;
    public Button[] menuButtons;

    Color disabledColor = new Color(0.4f, 0.4f, 0.4f, 1f);
    Color enabledColor = new Color(0.66f, 0.66f, 0.66f, 1f);
    Color notSelectedColor = new Color(0.66f, 0.66f, 0.66f, 1f);
    Color selectedColor = new Color(1f, 1f, 1f, 1f);

    public static bool isPauseActive;
    // Start is called before the first frame update
    void Start()
    {
        inventoryButtonLength = inventoryButtons.Length;
        inventoryButtonColor = new Image[inventoryButtonLength];
        inventoryButtonComponent = new Button[inventoryButtonLength];
        itemTitle = itemTitleUI.GetComponent<TextMeshProUGUI>();
        itemDesc = itemDescUI.GetComponent<TextMeshProUGUI>();
        inventory = GetComponent<UiInventoryScript>();
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        Debug.Log(scene.name);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseResumeButton);
        
    }

    // Update is called once per frame
    void Update()
    {
        isPauseActive = pauseMenu.activeInHierarchy;
        if (Input.GetButtonDown("Cancel"))
        {
            if (currentMenu == 0)
            {
                pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(defaultButtons[0]);
            }
        
            if (currentMenu == 1)
            {
                ReturnMenu();
                itemPanelUI.SetActive(false);
                itemDescriptionsUI.SetActive(false);
            }
            if (currentMenu == 2)
            {
                ReturnMenu();
                controlsUI.SetActive(false);
            }
        }
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(defaultButtons[currentMenu]);
            Debug.Log("Selected");
        }

        if (isPauseActive)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ReturnMenu()
    {
        for (int i = 0; i < 5; i++)
        {
            menuButtons[i].interactable = true;
        }
        EventSystem.current.SetSelectedGameObject(null);
        Debug.Log("Selected GameObject is: Null");
        Debug.Log("Saved Option is: " + savedOption);
        EventSystem.current.SetSelectedGameObject(savedOption);
        Debug.Log("Selected GameObject is: " + EventSystem.current.currentSelectedGameObject);
        if (sceneName == "HUBBuild")
            {
                menuButtons[2].interactable = false;
            }
        currentMenu = 0;
    }

    public void ExitMenu()
    {
        pauseMenu.SetActive(false);          
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenControls()
    {
        currentMenu = 2;
        savedOption = menuControls;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(keyboardButton);
    }

    public void OpenInventory()
    {
        currentMenu = 1;
        inventory.RefreshInventory();
        savedOption = pauseInventoryButton;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(backButtons[0]);

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

    public void DeselectedColor(string buttonName)
    {
        switch(buttonName)
        {
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
}
