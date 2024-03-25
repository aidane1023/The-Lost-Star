using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattler : MonoBehaviour
{
    Transform playerSpot;
    BattleManager battleManager;

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
        
    }

    public IEnumerator JumpAttack(EnemyBattler enemy)
    {
        battleManager.gameState = GameState.PlayerAttack;
        transform.DOMove(enemy.inFront.position, 1f, false);
        yield return new WaitForSeconds(1f);
        transform.DOJump(enemy.head.position, 0.8f, 1, 1, false);
        yield return new WaitForSeconds(0.6f);
        actionKeyNeeded = "z";
        yield return new WaitForSeconds(0.4f);
        enemy.RecieveDamage(jumpAttackPower);
        if(keyCorrect)
        {
            //timed correctly!
            transform.DOJump(enemy.head.position, 0.8f, 1, 1, false);
            yield return new WaitForSeconds(0.6f);
            actionKeyNeeded = "z";
            yield return new WaitForSeconds(0.4f);
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
        battleManager.Transition();

    }

    public IEnumerator KeyCooldownTimer()
    {
        yield return new WaitForSeconds(0.5f);
        keyCooldown = false;
    }
}
