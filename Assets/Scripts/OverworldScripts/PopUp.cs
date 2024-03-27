using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class PopUp : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    BattleInitiator battleInitiator;

    void Awake()
    {
        battleInitiator = GetComponent<BattleInitiator>();
    }

    void Update()
    {
        if ((Vector3.Distance(player.position, this.transform.position) <= 1.2f) && Input.GetKeyUp(KeyCode.Z))
        {
            battleInitiator.InitiateBattle();
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
