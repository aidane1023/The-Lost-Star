using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemyManager : MonoBehaviour
{
    public static List<int> enemiesDefeated = new List<int>();
    public bool hubReset = false;
    public bool bossDefeated = false;
    bool spawnedPiece = false;

    public GameObject rocketPiece;
    public int rocketPieceNum = 0;
    public Transform rocketSpawn;
    // Start is called before the first frame update
    void Start()
    {
        if (hubReset) enemiesDefeated.Clear();

    }

    //void Update()
    //{
       // if (bossDefeated && !spawnedPiece)
        //{
            //spawnedPiece = true;
            //GameObject newRocket = Instantiate(rocketPiece, rocketSpawn.position, Quaternion.identity);
            //newRocket.GetComponent<Rocket>().pickUpType = rocketPieceNum;
            //newRocket.GetComponent<Rocket>().UpdateSprite();
        //}
    //}
}
