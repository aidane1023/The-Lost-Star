using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBattler : EnemyBattler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Attack() 
    {
        StartCoroutine("DummyAttack");
    }
    public IEnumerator DummyAttack()
    {
        yield return new WaitForSeconds(1);
        if(battleManager == null) battleManager = FindObjectOfType<BattleManager>();
        battleManager.StartCoroutine("EnemyAttacks");

    }
}
