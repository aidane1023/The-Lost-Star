using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DustBunnyBattler : EnemyBattler
{
    public GameObject projectile;

    public AudioSource source;
    public AudioClip bunnyShot;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack() 
    {
        StartCoroutine("DustAttack");
    }
    public IEnumerator DustAttack()
    {
        yield return new WaitForSeconds(1);
        GameObject newProjectile = Instantiate(projectile, projectileStart.position, Quaternion.identity);
        source.PlayOneShot(bunnyShot);
        yield return new WaitForSeconds(3);
        battleManager.EnemyAttacks();
    }
}
