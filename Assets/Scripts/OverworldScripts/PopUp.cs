using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUp : MonoBehaviour
{
    public Animator anim;
    public Transform player;

    void Update()
    {
        if ((Vector3.Distance(player.position, this.transform.position) <= 1.2f) && Input.GetKeyUp(KeyCode.Z))
        {
            SceneManager.LoadScene ("BattleScene");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("InRange", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("InRange", false);
        }
    }
}
