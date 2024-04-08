using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public GameObject player;
    bool textPlaying = false;
    

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(player.transform.position, this.transform.position) <= 3f) && Input.GetKeyUp(KeyCode.Z) && !textPlaying)
        {
            player.GetComponent<PlayerController>().enabled = false;
            textPlaying = true;

            StartCoroutine("Reset");
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(10f);
        player.GetComponent<PlayerController>().enabled = true;
        textPlaying = true;
    }
}
