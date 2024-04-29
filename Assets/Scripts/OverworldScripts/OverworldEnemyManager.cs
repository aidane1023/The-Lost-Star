using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemyManager : MonoBehaviour
{
    public static List<int> enemiesDefeated = new List<int>();
    public bool hubReset = false;
    public bool bossDefeated = false;
    bool spawnedPiece = false;

    public GameObject RocketPart;
    
    // Start is called before the first frame update
    void Start()
    {
        RocketPart.SetActive(false);

        if (JackTransition.tPlayed)
        {
            RocketPart.SetActive(true);
        }

        if(hubReset) enemiesDefeated.Clear();
    }

    void Update()
    {
        if(bossDefeated && !spawnedPiece)
        {   
            
        }
    }
}
