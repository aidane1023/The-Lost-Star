using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SockHydraBattler : EnemyBattler
{
    PlayerBattler player;
    Vector3 origin;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBattler>();
        base.Start();
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack() 
    {
        StartCoroutine("BiteAttack");
    }
    public IEnumerator BiteAttack()
    {
        float r = Random.Range(1.3f, 2.1f);
        transform.DOMove(playerFront.position, 1f, false);
        yield return new WaitForSeconds(r);
        yield return new WaitForSeconds(0.4f);
        player.RecieveDamage(2);
        yield return new WaitForSeconds(0.9f);
        transform.DOMove(origin, 1f, false);
        yield return new WaitForSeconds(1f);
        battleManager.EnemyAttacks();
    }
}
