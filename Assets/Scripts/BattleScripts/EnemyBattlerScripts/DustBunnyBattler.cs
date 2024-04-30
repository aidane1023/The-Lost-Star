using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DustBunnyBattler : EnemyBattler
{
    public GameObject projectile;

    public AudioClip bunnyShot;
    public AudioClip bunnyDies;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public override void Attack() 
    {
        StartCoroutine("DustAttack");
    }
    public override void Death() 
    {
        StartCoroutine("DeathAnimation");
    }
    public IEnumerator DustAttack()
    {
        yield return new WaitForSeconds(1);
        GameObject newProjectile = Instantiate(projectile, projectileStart.position, Quaternion.identity);
        source.PlayOneShot(bunnyShot);
        yield return new WaitForSeconds(3);
        battleManager.StartCoroutine("EnemyAttacks");
    }
    public IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        source.PlayOneShot(bunnyDies);
        battleManager.waitingForEnemyDeath = false;
    }
}
