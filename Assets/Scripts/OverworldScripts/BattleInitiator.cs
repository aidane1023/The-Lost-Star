using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleInitiator : MonoBehaviour
{
    public GameObject[] battlers;
    public bool isGrunt = false;
    public int enemyID = -1;
    // Start is called before the first frame update
    void Start()
    {
        if(OverworldEnemyManager.enemiesDefeated.Contains(enemyID)) Destroy(this.gameObject);
        if(BattleManager.enemyID == enemyID && enemyID != -1) Defeated();
    }

    // Update is called once per frame
    public void InitiateBattle()
    {
        BattleManager.enemiesToSpawn.Clear();
        BattleManager.overworldSpawn = transform.position;
        foreach(GameObject enemy in battlers)
        {
            BattleManager.enemiesToSpawn.Add(enemy);
        }
        BattleManager.enemyID = enemyID;
        SceneManager.LoadScene ("BattleScene");
    }

    public void Defeated()
    {
        BattleManager.enemyID = -1;
        OverworldEnemyManager.enemiesDefeated.Add(enemyID);
        Destroy(this.gameObject);
    }
}
