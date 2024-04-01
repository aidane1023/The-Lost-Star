using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustProjectile : MonoBehaviour
{
    public float lifeTime = 5;
    public float damage = 1;

    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LifeTime");
    }

    void Update()
    {
        transform.Translate(-Vector3.right * Time.deltaTime * speed);
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        StartCoroutine("Destroyed");
    }
    IEnumerator Destroyed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        PlayerBattler player = other.gameObject.GetComponent<PlayerBattler>();
        if(player != null)
        {
            player.RecieveDamage(damage);
            StartCoroutine("Destroyed");
        }
    }
}
