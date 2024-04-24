using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    public Animator anim;
    private float rand;
    public AudioSource source;
    public AudioClip bunnyHop;

    void Start()
    {
        rand = Random.Range(0f, 2f);
        StartCoroutine(AnimStart(rand));
    }

    IEnumerator AnimStart(float rand)
    {
        yield return new WaitForSeconds(rand);
        anim.SetBool("Play", true);
        source.PlayOneShot(bunnyHop);
    }
}
