using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public GameState gameState = GameState.Transition;

    public PlayerInventory inventory;

    BattleMenuScript menuScript;

    PlayerBattler player;
    public List<EnemyBattler> enemies; //all enemies in a scene
    int enemyTurnsTaken;
    int defeatedEnemies = 0;

    public string playerAttackName; //name of attack player chose, such as "Jump" or "Spin"
    public int moveCost;
    public EnemyBattler target; //current enemy being targeted for an attack

    public static int sceneToLoad = 0;
    public static List<GameObject> enemiesToSpawn = new List<GameObject>(); //make static eventually
    public static Vector3 overworldSpawn;
    public static int enemyID = -1;
    public static int level = 0; //dictates which setpieces spawn
    public static bool canRun = true;
    public List<Transform> enemySpots;

    public List<GameObject> setPieces;
    public GameObject templateGround;

    public int enemyAttacksLeft = -1; //-1 means the enemy turn is done, if 0 set it to -1, and once the enemy turn starts, if -1, set the number to number of enemies

    public int enemyCount; //For UI enemy selectors
    public GameObject backupEnemy; //for testing
    [HideInInspector]
    public bool battleWon;
    [HideInInspector]
    public bool waitingForEnemyDeath;

    [Header ("Audio")]
    AudioSource source;
    public AudioClip runSound, itemUsedSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerBattler>();
        menuScript = GameObject.FindObjectOfType<BattleMenuScript>();
        StartCoroutine("BattleStart");
        if(enemiesToSpawn.Count == 0) enemiesToSpawn.Add(backupEnemy);
        InitialSpawn();

        if(level > 0)
        {
            templateGround.SetActive(false);
            setPieces[(level - 1)].SetActive(true);
        } 
        

        source = GetComponent<AudioSource>();
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
        menuScript.battleButtonCanvas.SetActive(true);
        menuScript.audioSource.SetActive(true);
        menuScript.hoverAudio.Play();
        //player.StartCoroutine("JumpAttack", enemies[0]);
    }

    void InitialSpawn()
    {
        int x = 0;
        foreach (GameObject enemy in enemiesToSpawn)
        {
            GameObject newEnemy = Instantiate(enemy, enemySpots[x].position, Quaternion.identity);
            enemies.Add(newEnemy.GetComponent<EnemyBattler>());
            x++;
            enemyCount++;
        }
    }

    public void Transition()
    {
        Debug.Log("Player Turn");
        if (gameState == GameState.PlayerAttack)
        {
            gameState = GameState.EnemyTurn;
            StartCoroutine(EnemyAttacks());
        }
        else if (gameState == GameState.EnemyTurn)
        {
            
            gameState = GameState.PlayerTurn;
            menuScript.battleButtonCanvas.SetActive(true);
            menuScript.ReturnMenu();
            menuScript.RestartMenu();
            player.playerAnimator.OnThink();
        }
    }

    public void AttackName(string newName) //the player selected the attack they are using
    {
        playerAttackName = newName;
    }
    public void AttackCost(int spCost) 
    {
        moveCost = spCost;
    }

    public void TargetChoosen(int enemyNum) //the player selected their target and will begin attacking
    {
        if(enemies.Count >= (enemyNum + 1) && enemies[enemyNum].gameObject.activeSelf && moveCost <= PlayerBattler.starPoints)
        {
            int i = 0;
            PlayerBattler.starPoints -= moveCost;
            player.playerAnimator.OnNeutral();
            gameState = GameState.PlayerAttack;
            EnemyBattler enemyTarget = enemies[enemyNum];
            if (playerAttackName == "Jump")
            {
                player.StartCoroutine("JumpAttack", enemyTarget);
            }
            if (playerAttackName == "Spin")
            {
                player.StartCoroutine("SpinAttack", enemyTarget);
            }
            if (playerAttackName == "BottleCapAttack")
            {
                player.attackHandler.StartCoroutine("BottleCapAttack", enemyTarget);
                foreach (InventoryItemData item in inventory.heldItems)
                {
                    if(item != null && item.ID == 1)
                    {
                        inventory.heldItems[i] = null;
                        break;
                    }
                    i++;
                }
            }
            if (playerAttackName == "PinAttack")
            {
                player.attackHandler.StartCoroutine("PinAttack", enemyTarget);
                foreach (InventoryItemData item in inventory.heldItems)
                {
                    if(item != null && item.ID == 3)
                    {
                        inventory.heldItems[i] = null;
                        break;
                    }
                    i++;
                }
            }
            if (playerAttackName == "RubberBandAttack")
            {
                player.attackHandler.StartCoroutine("RubberBandAttack", enemyTarget);
                foreach (InventoryItemData item in inventory.heldItems)
                {
                    if(item != null && item.ID == 2)
                    {
                        inventory.heldItems[i] = null;
                        break;
                    } 
                    i++;
                }
            }
            //disableUI
            menuScript.battleButtonCanvas.SetActive(false);
        }
    }

    public void ItemUsed(int buttonNum)
    {
        if(inventory.heldItems[buttonNum] != null && inventory.heldItems[buttonNum].isConsumable)
        {
            source.PlayOneShot(itemUsedSound);
            InventoryItemData item = inventory.heldItems[buttonNum];
            PlayerBattler.health += item.healthRestored;
            if(PlayerBattler.health > PlayerBattler.maxHealth) PlayerBattler.health = PlayerBattler.maxHealth;
            PlayerBattler.starPoints += item.spRestored;
            if(PlayerBattler.starPoints > PlayerBattler.maxStarPoints) PlayerBattler.starPoints = PlayerBattler.maxStarPoints;
            inventory.heldItems[buttonNum] = null;
            gameState = GameState.PlayerAttack;
            Transition();
            menuScript.battleButtonCanvas.SetActive(false);
        } 
    }

    //public void PlayerAttack()

    public IEnumerator EnemyAttacks()
    {
        //check if player died
        if(PlayerBattler.health <= 0) SceneManager.LoadScene("GAMEOVER");
        //check if any enemies died
        yield return new WaitForSeconds(1);
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].gameObject.activeSelf && enemies[i].health <= 0)
            {
                waitingForEnemyDeath = true;
                enemies[i].Death();
                yield return new  WaitUntil(() => !waitingForEnemyDeath);
                enemies[i].gameObject.SetActive(false);
                defeatedEnemies++;
                BridgeBehavior.enemyCount--;
            }
        }
        if(defeatedEnemies >= enemies.Count) BattleEnd(false);
        else
        {
            if (enemyTurnsTaken == enemies.Count)
            {
                enemyTurnsTaken = 0;
                Transition();
            }
            //once all the enemies finish attacking, then it transitions back to the player turn
            else if(enemies[enemyTurnsTaken].gameObject.activeSelf)
            {
                enemies[enemyTurnsTaken].Attack();
                enemyTurnsTaken++;
            }
            else
            {
                enemyTurnsTaken++;
                StartCoroutine(EnemyAttacks());
            }
        }
    }

    public void BattleEnd(bool fled)
    {
        Debug.Log("Battle Ended");
        if(fled && canRun)
        {
            source.PlayOneShot(runSound);
            enemyID = -1;
            SceneManager.LoadScene(sceneToLoad);
        }
        else if(!fled)
        {
            //gain xp and make the enemy die via battle initiator
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

public enum GameState
{
    PlayerTurn, //player choosing which attack to use and what to target
    EnemyTurn, //enemy is attacking and the player can only defend
    PlayerAttack, //game is checking inputs for action commands
    Transition //game is transitioning, aka the player cannot input anything
}
