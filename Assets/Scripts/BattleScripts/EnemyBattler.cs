using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattler : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float xpWorth; //how much xp it is worth
    public float coinDropRate; //how many coins it drops
    public string[] attacks; //thelist of attacks this enemy can use
    [Header("Transforms")]
    public Transform inFront;
    public Transform head;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack1()
    {
            //
    }


}
