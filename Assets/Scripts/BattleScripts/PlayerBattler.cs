using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBattler : MonoBehaviour
{
    Transform playerSpot;
    BattleManager battleManager;
    [HideInInspector]
    public PlayerBattlerAnimator playerAnimator;

    public GameObject damageStar; //the icon that shows damage dealt
    public DefenseMode defenseMode;

    public float health = 10;
    public float maxHealth = 10;
    public float starPoints = 5;
    public float maxStarPoints = 5;//skill points
    public float xp; //max is 100
    public float jumpAttackPower = 1;
    public float spinAttackPower = 2;
    public float defense = 0;

    public float buffLength;
    public string buffType;

    string actionKeyNeeded; //what is needed to achieve the action input
    bool keyCooldown = false; //if the key was pressed early, this is enabled, to prevent registering anymore inputs until cooldown has ended
    bool keyCorrect = false; //made true if the correct key was pressed for an attack
    // Start is called before the first frame update
    void Start()
    {
        battleManager = GameObject.FindObjectOfType<BattleManager>();
        playerSpot = GameObject.Find("PlayerSpot").transform;
        playerAnimator = GetComponent<PlayerBattlerAnimator>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(battleManager.gameState == GameState.PlayerAttack)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (actionKeyNeeded == "z" && !keyCooldown)
                {
                    keyCorrect = true;
                }
                else if (actionKeyNeeded != null && !keyCooldown)
                {
                    keyCooldown = true;
                    StartCoroutine("KeyCooldownTimer");
                }
            }
        }

        if(battleManager.gameState == GameState.EnemyTurn)
        {
            if (Input.GetKeyDown(KeyCode.Z) && !keyCooldown)
            {
                if(defenseMode == DefenseMode.Guard)
                {
                    StartCoroutine("Guard");
                }
            }
        }
        
    }

    public IEnumerator JumpAttack(EnemyBattler enemy)
    {
        playerAnimator.OnNeutral();


        actionKeyNeeded = "";
        battleManager.gameState = GameState.PlayerAttack;
        transform.DOMove(enemy.inFront.position, 1f, false);
        yield return new WaitForSeconds(1f);
        transform.DOJump(enemy.head.position, 0.8f, 1, 0.8f, false);
        yield return new WaitForSeconds(0.6f);
        actionKeyNeeded = "z";
        yield return new WaitForSeconds(0.2f);
        actionKeyNeeded = "";
        enemy.RecieveDamage(jumpAttackPower);
        if(keyCorrect)
        {
            //timed correctly!
            keyCorrect = false;
            transform.DOJump(enemy.head.position, 0.8f, 1, 0.8f, false);
            yield return new WaitForSeconds(0.6f);
            actionKeyNeeded = "z";
            yield return new WaitForSeconds(0.2f);
            actionKeyNeeded = "";
            enemy.RecieveDamage(jumpAttackPower);
            transform.DOJump(enemy.inFront.position, 0.8f, 1, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            //timed incorrectly
            transform.DOJump(enemy.inFront.position, 0.8f, 3, 1, false);
            yield return new WaitForSeconds(1f);
        }
        transform.DOMove(playerSpot.position, 1f, false);
        yield return new WaitForSeconds(1f);
        keyCorrect = false;
        battleManager.Transition();

    }

    public void RecieveDamage(float damage)
    {
        damage -= defense;
        if (damage < 0) damage = 0;
        health -= damage;
        //show the damage star

        RectTransform textTransform = Instantiate(damageStar).GetComponent<RectTransform>();
        textTransform.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Canvas canvas = GameObject.Find("2D UI Canvas").GetComponent<Canvas>();
        textTransform.SetParent(canvas.transform);

        playerAnimator.StartCoroutine("OnHurt");
    }

    public IEnumerator KeyCooldownTimer()
    {
        yield return new WaitForSeconds(0.7f);
        keyCooldown = false;
    }

    ///////////////////////////////
    ///DEFENSE FUNCTIONS///////////
    ///////////////////////////////
    IEnumerator Guard()
    {
        playerAnimator.OnGuard();
        keyCooldown = true;
        defense += 1;
        //guard active
        yield return new WaitForSeconds(0.3f);
        playerAnimator.OnCooldown();
        defense -= 1;
        //guard off
        yield return new WaitForSeconds(1.1f);
        playerAnimator.OnNeutral();
        if (keyCooldown) keyCooldown = false;
    }
}

public enum DefenseMode
{
    Guard,
    Jump,
    Counter, //superguard nearby enemies when they get close
    Reflect //swing, against projectiles

}
