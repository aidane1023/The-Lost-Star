using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVictory : MonoBehaviour
{
    public AudioClip win;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source.PlayOneShot(win);
        StartCoroutine(Boom());
    }

    IEnumerator Boom()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
