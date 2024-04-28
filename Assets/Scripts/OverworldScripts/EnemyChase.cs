using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    BattleInitiator battleInitiator;
    bool canStartBattle = false;

    public float speed;
    public float range;
    public bool soldier;
    public Animator anim;

    void Awake()
    {
        battleInitiator = GetComponent<BattleInitiator>();
        StartCoroutine("SpawnCooldown");
    }

    void Update()
    {
            if ((Vector3.Distance(player.position, this.transform.position) <= range))
            {
                if (soldier) StartCoroutine(DelayJump());
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

        if ((Vector3.Distance(player.position, this.transform.position) <= 0.4f) && canStartBattle)
        {
            BattleManager.sceneToLoad = SceneManager.GetActiveScene().buildIndex;
            battleInitiator.InitiateBattle();
        }
    }

    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(3);
        canStartBattle = true;
    }

    void OnTriggerEnter()
    {
        anim.SetBool("InRange", true);
    }

    IEnumerator DelayJump()
    {
        speed = 0;
        yield return new WaitForSeconds(0.25f);
        speed = 4;
        yield return new WaitForSeconds(0.5f);
        speed = 0;
        yield return new WaitForSeconds(0.25f);
    }
}
