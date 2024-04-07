using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingDummy : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    BattleInitiator battleInitiator;

    public static bool cleared = true;

    void Awake()
    {
        battleInitiator = GetComponent<BattleInitiator>();
    }

    void Update()
    {
        if ((Vector3.Distance(player.position, this.transform.position) <= 3.3f))
        {
            anim.SetBool("InRange", true);
        }
        else
        {
            anim.SetBool("InRange", false);
        }

        if ((Vector3.Distance(player.position, this.transform.position) <= 2f) && Input.GetKeyUp(KeyCode.Z))
        {
            BattleManager.sceneToLoad = SceneManager.GetActiveScene().buildIndex;
            battleInitiator.InitiateBattle();
        }

    }
}
