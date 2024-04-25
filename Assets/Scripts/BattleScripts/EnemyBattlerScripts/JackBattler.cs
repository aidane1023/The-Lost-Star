using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JackBattler : EnemyBattler
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

        var rot = transform.localRotation.eulerAngles; //get the angles
        rot.Set(0, -105, 0); //set the angles
        transform.localRotation = Quaternion.Euler(rot); //update the transform
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack() 
    {
        float r = Random.Range(0f, 1f);
        if(r > 0.5f) StartCoroutine("ClapAttack1");
        else StartCoroutine("ClapAttack2");
    }
    public override void Death() 
    {
        StartCoroutine("DeathAnimation");
    }
    public IEnumerator ClapAttack1()
    {
        Vector3 playerLow = new Vector3(playerFront2.position.x, playerFront.position.y - 0.6f, playerFront.position.z);
        transform.DOJump(playerLow, 1.4f, 3, 1.2f, false);
        yield return new WaitForSeconds(1.4f);
        anim.SetBool("Attack1", true);
        yield return new WaitForSeconds(1.4f);
        player.RecieveDamage(1);
        yield return new WaitForSeconds(0.9f);
        anim.SetBool("Attack1", false);
        transform.DOJump(origin, 1.4f, 3, 1.2f, false);
        yield return new WaitForSeconds(1.5f);
        battleManager.StartCoroutine("EnemyAttacks");
    }

    public IEnumerator ClapAttack2()
    {
        Vector3 playerLow = new Vector3(playerFront2.position.x, playerFront.position.y - 0.6f, playerFront.position.z);
        transform.DOJump(playerLow, 1.4f, 3, 1.2f, false);
        yield return new WaitForSeconds(1.4f);
        anim.SetBool("Attack2", true);
        yield return new WaitForSeconds(1.4f);
        player.RecieveDamage(1);
        yield return new WaitForSeconds(0.9f);
        anim.SetBool("Attack2", false);
        transform.DOJump(origin, 1.4f, 3, 1.2f, false);
        yield return new WaitForSeconds(1.5f);
        battleManager.StartCoroutine("EnemyAttacks");
    }

    public IEnumerator DeathAnimation()
    {
        anim.Play("Dies");
        yield return new WaitForSeconds(2.7f);
        battleManager.waitingForEnemyDeath = false;
    }
}
