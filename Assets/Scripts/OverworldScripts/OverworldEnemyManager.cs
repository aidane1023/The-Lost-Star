using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemyManager : MonoBehaviour
{
    public static List<int> enemiesDefeated = new List<int>();
    public bool hubReset = false;
    // Start is called before the first frame update
    void Start()
    {
        if(hubReset) enemiesDefeated.Clear();
    }
}
