using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UIHUDScript : MonoBehaviour
{
    //public GameObject playerHealthTextUI, playerSPTextUI, playerXPTextUI, playerCoinTextUI;
    //public GameObject player;

    //TextMeshProUGUI healthText, SPText, XPText, coinText;

    //private PlayerStats playerStatsScript;

    // Start is called before the first frame update
    void Start()
    {
        //playerStatsScript = player.GetComponent<>();
        healthText = playerHealthTextUI.GetComponent<TextMeshProUGUI>();
        SPText = playerSPTextUI.GetComponent<TextMeshProUGUI>();
        XPText = playerXPTextUI.GetComponent<TextMeshProUGUI>();
        xpBarColorFill = xpBarUI.GetComponent<Image>();
        coinText = playerCoinTextUI.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"HP: {PlayerBattler.health}/{PlayerBattler.maxHealth}";
        SPText.text = $"SP: {PlayerBattler.starPoints}/{PlayerBattler.maxStarPoints}";
        XPText.text = $"XP: {PlayerBattler.xp}/100";
        xpBarColorFill.fillAmount = (PlayerBattler.xp/100);
        coinText.text = $"Coins: {PlayerBattler.coins}";
    }
}
