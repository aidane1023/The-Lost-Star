using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemPickup : MonoBehaviour
{
    public PickupType type;
    public bool enemyDrop = true;
    SpriteRenderer r;
    Color clearColor, originalColor;
    bool collectable = false;

    AudioSource source;
    public ParticleSystem particleEffects;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<SpriteRenderer>();
        clearColor = r.color;
        originalColor = clearColor;
        clearColor.a = 0f;
        source = GetComponent<AudioSource>();
        if(enemyDrop)
        {
            StartCoroutine("DespawnTimer"); 
            collectable = false;
        }
        else collectable = true;
    }

    IEnumerator DespawnTimer()
    {
        StartCoroutine("Jump");
        yield return new WaitForSeconds(5);
        r.color = clearColor;
        yield return new WaitForSeconds(0.5f);
        r.color = originalColor;
        yield return new WaitForSeconds(0.5f);
        r.color = clearColor;
        yield return new WaitForSeconds(0.5f);
        r.color = originalColor;
        yield return new WaitForSeconds(0.5f);
        r.color = clearColor;
        yield return new WaitForSeconds(0.5f);
        r.color = originalColor;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("DestroyItem");
    }

    IEnumerator Jump()
    {
        Vector3 jumpPoint;
        float x = Random.Range(-2,2);
        float z = Random.Range(-2,2);
        jumpPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        yield return new WaitForSeconds(0.3f);
        transform.DOJump(jumpPoint, 0.8f, 1, 0.8f, false);
        yield return new WaitForSeconds(0.7f);
        collectable = true;
        transform.DOJump(jumpPoint, 0.4f, 1, 0.4f, false);
        yield return new WaitForSeconds(0.35f);
        transform.DOJump(jumpPoint, 0.2f, 1, 0.2f, false);
    }

    IEnumerator DestroyItem()
    {
        r.color = clearColor;
        //source.Play();
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && collectable)
        {
            if(type == PickupType.Coin)
            {
                PlayerBattler.coins++;
                Debug.Log("coinCollected");
            }
            if(type == PickupType.Heart && PlayerBattler.health < PlayerBattler.maxHealth)
            {
                PlayerBattler.health++;
            }
            if(type == PickupType.Sp && PlayerBattler.starPoints < PlayerBattler.maxStarPoints)
            {
                PlayerBattler.starPoints++;
            }
            collectable = false;
            StartCoroutine("DestroyItem");
        }
    }

}

public enum PickupType
{
    Coin,
    Heart,
    Sp,
    Item
}
