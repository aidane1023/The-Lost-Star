using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameState gameState = GameState.Transition;

    PlayerBattler player;
    public List<EnemyBattler> enemies; //all enemies in a scene

    public string playerAttackName; //name of attack player chose, such as "Jump" or "Spin"
    public EnemyBattler target; //current enemy being targeted for an attack

    public List<GameObject> enemiesToSpawn; //make static eventually
    public List<Transform> enemySpots;

    public int enemyAttacksLeft = -1; //-1 means the enemy turn is done, if 0 set it to -1, and once the enemy turn starts, if -1, set the number to number of enemies
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerBattler>();
        StartCoroutine("BattleStart");
        InitialSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BattleStart()
    {
        gameState = GameState.Transition;
        yield return new WaitForSeconds(2f);
        gameState = GameState.PlayerTurn;
        player.StartCoroutine("JumpAttack", enemies[0]);
    }

    void InitialSpawn()
    {
        int x = 0;
        foreach (GameObject enemy in enemiesToSpawn)
        {
            GameObject newEnemy = Instantiate(enemy, enemySpots[x].position, Quaternion.identity);
            enemies.Add(newEnemy.GetComponent<EnemyBattler>());
            x++;
        }
    }

    public void Transition()
    {
        //check if any enemies died
        if (gameState == GameState.PlayerAttack)
        {
            gameState = GameState.EnemyTurn;
            EnemyAttacks();
        }
        else if (gameState == GameState.EnemyTurn)
        {
            gameState = GameState.PlayerTurn;
            EnemyAttacks();
        }
    }

    public void AttackChoosen(int enemyNum)
    {
        EnemyBattler enemyTarget = enemies[enemyNum];
        if (playerAttackName == "Jump")
        {
            player.StartCoroutine("JumpAttack", enemyTarget);
        }
    }

    //public void PlayerAttack()

    public void EnemyAttacks()
    {
        //
    }
}

public enum GameState
{
    PlayerTurn, //player choosing which attack to use and what to target
    EnemyTurn, //enemy is attacking and the player can only defend
    PlayerAttack, //game is checking inputs for action commands
    Transition //game is transitioning, aka the playe cannot input anything
}
