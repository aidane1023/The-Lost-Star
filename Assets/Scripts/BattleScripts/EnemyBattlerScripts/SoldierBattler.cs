using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoldierBattler : EnemyBattler
{
    PlayerBattler player;
    SpriteRenderer renderer;
    public Sprite[] sprites;
    Vector3 origin;

    public AudioClip attackSFX;
    public AudioClip dieSFX;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBattler>();
        renderer = GetComponent<SpriteRenderer>();
        base.Start();
        origin = transform.position;
    }

    public override void Attack() 
    {
        StartCoroutine("SmackAttack");
    }
    public override void Death() 
    {
        StartCoroutine("DeathAnimation");
    }
    public IEnumerator SmackAttack()
    {
        float r = Random.Range(1.3f, 2.1f);
        transform.DOMove(playerFront.position, 1f, false);
        yield return new WaitForSeconds(r);
        renderer.sprite = sprites[1];
        yield return new WaitForSeconds(0.4f);
        source.PlayOneShot(attackSFX);
        player.RecieveDamage(2);
        renderer.sprite = sprites[2];
        yield return new WaitForSeconds(0.9f);
        renderer.sprite = sprites[0];
        transform.DOMove(origin, 1f, false);
        yield return new WaitForSeconds(1f);
        battleManager.StartCoroutine("EnemyAttacks");
    }

    public IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        source.PlayOneShot(dieSFX);
        battleManager.waitingForEnemyDeath = false;
    }
}
