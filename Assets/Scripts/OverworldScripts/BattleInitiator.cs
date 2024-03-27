using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleInitiator : MonoBehaviour
{
    public GameObject[] battlers;
    // Start is called before the first frame update
    void Start()
    {
        
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
        SceneManager.LoadScene ("BattleScene");
    }
}
