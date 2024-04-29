using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBattler : EnemyBattler
{
    public override void Attack() 
    {
        StartCoroutine("DummyAttack");
    }
    public override void Death() 
    {
        StartCoroutine("DeathAnimation");
    }
    public IEnumerator DummyAttack()
    {
        yield return new WaitForSeconds(0.1f);
        if(battleManager == null) battleManager = FindObjectOfType<BattleManager>();
        battleManager.StartCoroutine("EnemyAttacks");

    }

    public IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        if(battleManager == null) battleManager = FindObjectOfType<BattleManager>();
        battleManager.waitingForEnemyDeath = false;
    }
}
