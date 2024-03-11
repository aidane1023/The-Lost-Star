using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattler : MonoBehaviour
{
    public float health = 10;
    public float maxHealth = 10;
    public float starPoints = 5;
    public float maxStarPoints = 5;//skill points
    public float xp; //max is 100
    public float attackPower = 2;
    public float defense = 0;

    public float buffLength;
    public string buffType;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator JumpAttack(EnemyBattler enemy)
    {
        //
        transform.DOMove(enemy.inFront.position, 1f);
        yield return new WaitForSeconds(1f);
        transform.DOJump(enemy.head.position, 1, 1, 1.5f);
    }
}
