using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBattler : MonoBehaviour
{
    Transform playerSpot;
    [HideInInspector]
    public BattleManager battleManager;
    [HideInInspector]
    public PlayerBattlerAnimator playerAnimator;
    [HideInInspector]
    public PlayerAttacksHandler attackHandler;

    public GameObject damageStar; //the icon that shows damage dealt
    public DefenseMode defenseMode;

    public static float health = 10;
    public static float maxHealth = 10;
    public static float starPoints = 5;
    public static float maxStarPoints = 5;//skill points
    public static float xp; //max is 100
    public static float coins = 3; 
    public float jumpAttackPower = 1;
    public float spinAttackPower = 2;
    public float defense = 0;
    bool leftPadPressed, rightPadPressed, leftPadReleased, rightPadReleased;

    //public float buffLength;
    //public string buffType;

    [HideInInspector]
    public string actionKeyNeeded; //what is needed to achieve the action input
    bool keyCooldown = false; //if the key was pressed early, this is enabled, to prevent registering anymore inputs until cooldown has ended
    [HideInInspector]
    public bool keyCorrect = false; //made true if the correct key was pressed for an attack

    [Header ("Audio")]
    [HideInInspector]
    public AudioSource source;
    public AudioClip blockedSound, damagedSound, jumpStartSound, jumpMissedSound;
    // Start is called before the first frame update
    void Start()
    {
        leftPadPressed = false;
        rightPadPressed = false;
        attackHandler = GetComponent<PlayerAttacksHandler>();
        battleManager = GameObject.FindObjectOfType<BattleManager>();
        playerSpot = GameObject.Find("PlayerSpot").transform;
        playerAnimator = GetComponent<PlayerBattlerAnimator>();
        health = maxHealth;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") < -0.75f)
        {
            Debug.Log("Left Press");
            leftPadPressed = true;
        }
        if (Input.GetAxis("Horizontal") > -0.75f)
        {
            if (leftPadPressed = true)
            {
                Debug.Log("Left Release");
                //leftPadReleased = true;
                leftPadPressed = false;
            }
        }
        if (Input.GetAxis("Horizontal") > 0.75f)
        {
            //Debug.Log("Right Press");
            rightPadPressed = true;
        }



        if(battleManager.gameState == GameState.PlayerAttack)
        {
            if (Input.GetButtonDown("Submit"))
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
            if (Input.GetKeyDown(KeyCode.LeftArrow) || leftPadPressed)
            {
                if (actionKeyNeeded == "left" && !keyCooldown)
                {
                    keyCorrect = true;
                }
                Debug.Log("Left Arrow Pressed");
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || !leftPadPressed)
            {
                if (actionKeyNeeded == "left" && !keyCooldown)
                {
                    keyCorrect = true;
                }
                Debug.Log("Left Arrow Lifted");
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
        source.PlayOneShot(jumpStartSound);
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
            source.PlayOneShot(jumpStartSound);
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
            source.PlayOneShot(jumpMissedSound);
            transform.DOJump(enemy.inFront.position, 0.8f, 3, 1, false);
            yield return new WaitForSeconds(1f);
        }
        transform.DOMove(playerSpot.position, 1f, false);
        yield return new WaitForSeconds(1f);
        keyCorrect = false;
        battleManager.Transition();

    }

    public IEnumerator SpinAttack(EnemyBattler enemy)
    {
        playerAnimator.OnNeutral();


        actionKeyNeeded = "";
        battleManager.gameState = GameState.PlayerAttack;
        transform.DOMove(enemy.inFront.position, 1f, false);
        yield return new WaitForSeconds(1f);
        keyCorrect = false;
        actionKeyNeeded = "left";
        attackHandler.chargeUI.SetActive(true);
        attackHandler.spinPrompts.SetActive(true);
        attackHandler.chargeAmount = 0;
        attackHandler.chargeMeter.fillAmount = attackHandler.chargeAmount/100;
        yield return new WaitUntil (() => keyCorrect);
        source.Play();
        attackHandler.isCharging = true;
        attackHandler.chargeRate = 50;
        keyCorrect = false;
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil (() => keyCorrect || attackHandler.chargeAmount >= 107);
        source.Stop();
        if(attackHandler.chargeAmount >= attackHandler.chargeThreshholdMax || attackHandler.chargeAmount < attackHandler.chargeThreshholdMin)
        {
            //timed incorrectly
            enemy.RecieveDamage(spinAttackPower/2);
        }
        else
        {
            enemy.RecieveDamage(spinAttackPower);
        }
        actionKeyNeeded = "";
        keyCorrect = false;
        attackHandler.chargeUI.SetActive(false);
        attackHandler.spinPrompts.SetActive(false);
        attackHandler.isCharging = false;
        attackHandler.chargeAmount = 0;
        transform.DOMove(playerSpot.position, 1f, false);
        yield return new WaitForSeconds(1f);
        battleManager.Transition();

    }

    public void RecieveDamage(float damage)
    {
        damage -= defense;
        if (damage < 0) damage = 0;
        health -= damage;
        //show the damage star

        if(defense > 0) source.PlayOneShot(blockedSound);
        else source.PlayOneShot(damagedSound);

        RectTransform textTransform = Instantiate(damageStar).GetComponent<RectTransform>();
        textTransform.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Canvas canvas = GameObject.Find("Damage Canvas").GetComponent<Canvas>();
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
