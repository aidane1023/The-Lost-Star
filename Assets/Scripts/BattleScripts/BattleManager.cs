using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameState gameState = GameState.Transition;

    PlayerBattler player;
    public List<EnemyBattler> enemies; //all enemies in a scene

    public EnemyBattler target; //current enemy being targeted for an attack

    public List<GameObject> enemiesToSpawn; //make static eventually
    public List<Transform> enemySpots;
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
}

public enum GameState
{
    PlayerTurn, //player choosing which attack to use and what to target
    EnemyTurn, //enemy is attacking and the player can only defend
    PlayerAttack, //game is checking inputs for action commands
    Transition //game is transitioning, aka the playe cannot input anything
}
