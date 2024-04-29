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
    public Vector3 rocketPlacement;
    public int rocketPieceNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (JackTransition.tPlayed)
        {
            spawnedPiece = true;
            Instantiate(RocketPart, rocketPlacement, Quaternion.identity);
            rocketPieceNum = RocketPart.GetComponent<Rocket>().pickUpType;
            GameManager.Instance.SetPickupStatus(3);
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
