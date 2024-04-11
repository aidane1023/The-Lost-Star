using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    BattleInitiator battleInitiator;

    public float speed;

    void Awake()
    {
        battleInitiator = GetComponent<BattleInitiator>();
    }

    void Update()
    {
        if ((Vector3.Distance(player.position, this.transform.position) <= 3f))
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        if ((Vector3.Distance(player.position, this.transform.position) <= 0.4f))
        {
            BattleManager.sceneToLoad = SceneManager.GetActiveScene().buildIndex;
            battleInitiator.InitiateBattle();
        }

    }
}
