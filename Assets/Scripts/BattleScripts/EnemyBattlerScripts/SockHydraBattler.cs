using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SockHydraBattler : EnemyBattler
{
    PlayerBattler player;
    Vector3 origin;
    public Animator anim;

    public AudioClip sockAttack;
    public AudioClip sockDies;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("InBattle", true);
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
    public override void Death() 
    {
        StartCoroutine("DeathAnimation");
    }
    public IEnumerator BiteAttack()
    {
        anim.Play("Slither");
        transform.DOMove(playerFront2.position, 1.4f, false);
        yield return new WaitForSeconds(1.9f);
        anim.Play("Attack");
        yield return new WaitForSeconds(1.7f); //Here
        source.PlayOneShot(sockAttack);
        yield return new WaitForSeconds(0f); //Here
        player.RecieveDamage(1);
        yield return new WaitForSeconds(0.9f);
        anim.Play("Slither");
        transform.DOMove(origin, 1f, false);
        yield return new WaitForSeconds(1f);
        anim.Play("Nod");
        battleManager.StartCoroutine("EnemyAttacks");
    }

    public IEnumerator DeathAnimation()
    {
        anim.Play("Dies");
        yield return new WaitForSeconds(2.3f);
        source.PlayOneShot(sockDies);
        yield return new WaitForSeconds(0.4f);
        battleManager.waitingForEnemyDeath = false;
    }
}
