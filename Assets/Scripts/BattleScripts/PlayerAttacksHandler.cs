using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerAttacksHandler : MonoBehaviour
{
    PlayerBattler player;
    public GameObject chargeUI;
    public GameObject spinPrompts, pinUIPrompts;
    public GameObject zButton;
    public Image chargeMeter;

    public SpriteRenderer heldItem;
    public Sprite pin, bottleCap, rubberBand;
    Vector3 itemOrigin;
    
    public float chargeAmount = 0; //max is 100
    public float chargeThreshholdMin = 90; //lowest amount of chargeAmount for attack to be succesful
    public float chargeThreshholdMax = 110; //highest amount of chargeAmount before attack fails
    public float chargeRate = 0;

    public bool isCharging = false;

    public bool isMashing = false;

    public bool isBalancing = false;
    bool leftDown, rightDown = false;

    public AudioClip bottleCapSpam, bottleCapLaunched, pinLaunched, rubberBandSling, pinBalanceSound;
    float changingPitch;

    //public bool isTiming = false;

    //TODO: ATTACKS(A RYTHYM ONE WHERE A BUTTON FLASHES ON THE SCREEN THAT THE PLAYER PRESSES (EACH PRESS THROWS A BAND), A MASH ONE, AND A BALANCE ONE WHERE THE PLAYER PRESSES LEFT AND RIGHT TO KEEP A BAR IN THE MIDDLE, USE THIS FOR PIN ATTACK)

    // Start is called before the first frame update
    void Start()
    {
        chargeUI.SetActive(false);
        player = FindObjectOfType<PlayerBattler>();
        itemOrigin = heldItem.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCharging == true)
        {
            chargeAmount += chargeRate * Time.deltaTime;
            chargeMeter.fillAmount = chargeAmount/100;
        }
        if(isMashing)
        {
            chargeAmount -= chargeRate * Time.deltaTime;
            chargeMeter.fillAmount = chargeAmount/100;
            if(player.keyCorrect)
            {
                player.keyCorrect = false;
                player.source.PlayOneShot(bottleCapSpam);
                chargeAmount += 5;
            }
            if(chargeAmount > 100) chargeAmount = 100;
        }
        if(isBalancing)
        {
            chargeAmount += chargeRate * Time.deltaTime;
            chargeMeter.fillAmount = chargeAmount/100;

            changingPitch = 1.5f - (1 - chargeAmount/100);
            player.source.pitch = changingPitch;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                leftDown = true;
                rightDown = false;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                leftDown = false;
                rightDown = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)) leftDown = false;
            if (Input.GetKeyUp(KeyCode.RightArrow)) rightDown = false;

            if(leftDown) chargeAmount -= 25 * Time.deltaTime;
            if(rightDown) chargeAmount += 25 * Time.deltaTime;

            if(chargeAmount > 100) chargeAmount = 100;
            if(chargeAmount < 0) chargeAmount = 0;
        }
    }

    public IEnumerator BottleCapAttack(EnemyBattler enemy)
    {
        Debug.Log("bottlecapattack");
        player.playerAnimator.OnNeutral();


        player.actionKeyNeeded = "";
        player.battleManager.gameState = GameState.PlayerAttack;
        yield return new WaitForSeconds(0.5f);
        heldItem.sprite = bottleCap;
        player.keyCorrect = false;
        player.actionKeyNeeded = "z";
        chargeUI.SetActive(true);
        zButton.SetActive(true);
        chargeAmount = 0;
        chargeMeter.fillAmount = chargeAmount/100;
        isMashing = true;
        chargeRate = 10;
        yield return new WaitForSeconds(4);
        heldItem.sprite = bottleCap;
        StartCoroutine("ItemThrown", enemy.transform.position);
        yield return new WaitForSeconds(0.2f);
        if(chargeAmount >= 90)
        {
            //timed correctly
            
            foreach(EnemyBattler otherEnemy in player.battleManager.enemies)
            {
                if(otherEnemy.gameObject.activeSelf) otherEnemy.RecieveDamage(3);
            }
            
        }
        else if(chargeAmount >= 70)
        {
            foreach(EnemyBattler otherEnemy in player.battleManager.enemies)
            {
                if(otherEnemy.gameObject.activeSelf) otherEnemy.RecieveDamage(2);
            }
        }
        else
        {
            foreach(EnemyBattler otherEnemy in player.battleManager.enemies)
            {
                if(otherEnemy.gameObject.activeSelf) otherEnemy.RecieveDamage(1);
            }
        }
        player.source.PlayOneShot(bottleCapLaunched);
        player.actionKeyNeeded = "";
        player.keyCorrect = false;
        chargeUI.SetActive(false);
        zButton.SetActive(false);
        isMashing = false;
        chargeAmount = 0;
        yield return new WaitForSeconds(1f);
        player.battleManager.Transition();

    }

    public IEnumerator PinAttack(EnemyBattler enemy)
    {
        float originalPitch = player.source.pitch;
        player.playerAnimator.OnNeutral();


        player.actionKeyNeeded = "";
        player.battleManager.gameState = GameState.PlayerAttack;
        yield return new WaitForSeconds(0.5f);
        heldItem.sprite = pin;
        chargeUI.SetActive(true);
        pinUIPrompts.SetActive(true);
        chargeAmount = 50;
        chargeMeter.fillAmount = chargeAmount/100;
        isBalancing = true;
        player.source.PlayOneShot(pinBalanceSound);
        chargeRate = Random.Range(-20f, 20f);
        Debug.Log("Changed Rate");
        yield return new WaitForSeconds(0.5f);
        chargeRate = Random.Range(-20f, 20f);
        Debug.Log("Changed Rate");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Changed Rate");
        chargeRate = Random.Range(-20f, 20f);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Changed Rate");
        chargeRate = Random.Range(-20f, 20f);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Changed Rate");
        chargeRate = Random.Range(-20f, 20f);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Changed Rate");
        chargeRate = Random.Range(-20f, 20f);
        yield return new WaitForSeconds(3);

        isBalancing = false;
        player.source.pitch = originalPitch;
        if(chargeAmount >= 45 && chargeAmount < 65)
        {
            //timed correctly
            player.source.PlayOneShot(pinLaunched);
            StartCoroutine("ItemThrown", enemy.transform.position);
            yield return new WaitForSeconds(0.4f);
            enemy.RecieveDamage(4);
        }
        else heldItem.sprite = null;
        player.actionKeyNeeded = "";
        player.keyCorrect = false;
        chargeUI.SetActive(false);
        pinUIPrompts.SetActive(false);
        chargeAmount = 0;
        yield return new WaitForSeconds(1f);
        player.battleManager.Transition();

    }

    public IEnumerator RubberBandAttack(EnemyBattler enemy)
    {
        player.playerAnimator.OnNeutral();
        float t = 0;
        int i = 0;

        player.actionKeyNeeded = "";
        player.battleManager.gameState = GameState.PlayerAttack;
        yield return new WaitForSeconds(0.5f);

        do
        {
            t = Random.Range(0.6f, 1.2f);
            yield return new WaitForSeconds(t - 0.4f);
            player.keyCorrect = false;
            player.actionKeyNeeded = "z";
            zButton.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            if(player.keyCorrect)
            {
                heldItem.sprite = rubberBand;
                player.source.PlayOneShot(rubberBandSling);
                StartCoroutine("ItemThrown", enemy.transform.position);
                yield return new WaitForSeconds(0.4f);
                enemy.RecieveDamage(1);
            }
            else
            {
                i += 4;
                yield return new WaitForSeconds(0.4f);
            }
            zButton.SetActive(false);
            i++;
        }
        while(i <= 3);



        player.actionKeyNeeded = "";
        player.keyCorrect = false;
        zButton.SetActive(false);
        isMashing = false;
        chargeAmount = 0;
        yield return new WaitForSeconds(1f);
        player.battleManager.Transition();

    }

    IEnumerator ItemThrown(Vector3 target)
    {
        heldItem.transform.position = itemOrigin;
        heldItem.transform.DOJump(target, 0.4f, 1, 0.4f, false);
        yield return new WaitForSeconds(0.4f);
        heldItem.sprite = null;
    }
}

public enum AttackName
{
    Null,
    Pin,
    RubberBand,
    BottleCap
}
