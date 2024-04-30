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

    void Awake()
    {
        battleInitiator = GetComponent<BattleInitiator>();
        StartCoroutine("SpawnCooldown");
    }

    void Update()
    {
        if ((Vector3.Distance(player.position, this.transform.position) <= range))
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        if ((Vector3.Distance(player.position, this.transform.position) <= 0.8f) && canStartBattle)
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
}
