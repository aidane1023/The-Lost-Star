using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingDummy : MonoBehaviour
{
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
            BattleManager.sceneToLoad = SceneManager.GetActiveScene().buildIndex;
            battleInitiator.InitiateBattle();
        }

    }
}
