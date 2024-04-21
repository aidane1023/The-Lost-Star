using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleInitiator : MonoBehaviour
{
    public GameObject[] battlers;
    public GameObject[] pickups;
    public bool isGrunt = false;
    public int enemyID = -1;
    public int level = 0;
    public bool canRunFromFight = true;

    public bool isTutorial = false; //variable to check if fighting the dummy
    // Start is called before the first frame update
    void Start()
    {
        if(OverworldEnemyManager.enemiesDefeated.Contains(enemyID)) Destroy(this.gameObject);
        if(BattleManager.enemyID == enemyID && enemyID != -1) Defeated();
    }

    // Update is called once per frame
    public void InitiateBattle()
    {
        if(isTutorial) TrainingDummy.cleared = true;

        BattleManager.enemiesToSpawn.Clear();
        BattleManager.overworldSpawn = transform.position;
        foreach(GameObject enemy in battlers)
        {
            BattleManager.enemiesToSpawn.Add(enemy);
        }
        BattleManager.enemyID = enemyID;
        BattleManager.level = level;
        if(canRunFromFight) BattleManager.canRun = true;
        else BattleManager.canRun = false;
        SceneManager.LoadScene ("BattleScene");
    }

    public void Defeated()
    {
        BattleManager.enemyID = -1;
        OverworldEnemyManager.enemiesDefeated.Add(enemyID);
        foreach(GameObject pickup in pickups)
        {
            Instantiate(pickup, transform.position, Quaternion.identity);
            Debug.Log("Made A Coin");
        }
        Destroy(this.gameObject);
    }
}
