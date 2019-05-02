using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour
{
    public float initialWaveTime;
    public int initialWaveSpawns;
    public int scoreThreshold;
    public float waveTimeMult;
    public int additionalWaveSpawns;
    public int maxEnemies;
    public float powerupDelay;

    private float currentWaveTime;
    private int currentWaveSpawns;
    private int currentEnemies;

    private GameObject[] enemySpawners;
    private GameObject[] powerupSpawners;
    [SerializeField] GameObject markItem;

    private bool gameStarted;
    private bool gameOver;

    private int currentScore;

    private Coroutine spawnEnemies;
    private Coroutine spawnPowerups;

    public override void OnStartServer()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            // initialize (this only needs to be done on the server)
            enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
            powerupSpawners = GameObject.FindGameObjectsWithTag("PowerupSpawner");
            gameStarted = false;
            gameOver = false;
            currentScore = 0;
            currentEnemies = 0;
            currentWaveTime = initialWaveTime;
            currentWaveSpawns = initialWaveSpawns;

            StartCoroutine(UpdateWaves());

            // spawn mark item
            CmdSpawnFirstMarkItem();

            // display start message

        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GameStart()
    {
        CmdGameStart();
    }

    public void GameOver()
    {
       
        CmdGameOver();
            
    }

    public void AddScore(int score)
    {
        CmdAddScore(score);
    }

    IEnumerator SpawnEnemies()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(currentWaveTime);
            for (int i = 0; i < currentWaveSpawns; ++i)
            {
                CmdSpawnEnemy();
            }
        }
    }

    IEnumerator SpawnPowerups()
    {
        while (!gameOver)
        {
            // if there is no mark item on the field, spawn mark item after a delay
            if (GameObject.FindGameObjectWithTag("MarkItem") == null)
            {
                yield return new WaitForSeconds(powerupDelay);
                CmdSpawnMarkItem();
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator UpdateWaves()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(0.5f);
            int currentWave = currentScore / scoreThreshold;
            if (currentWave > 0)
                currentWaveTime = currentWave * waveTimeMult * initialWaveTime;
            currentWaveSpawns = currentWave * additionalWaveSpawns + initialWaveSpawns;
        }
    }

    [Command]
    void CmdGameStart()
    {
        if (!gameStarted)
        {
            // remove start message

            // spawn first wave immediately
            for (int i = 0; i < currentWaveSpawns; ++i)
                CmdSpawnEnemy();
            // start spawning
            spawnEnemies = StartCoroutine(SpawnEnemies());
            spawnPowerups = StartCoroutine(SpawnPowerups());

            gameStarted = true;
        }
    }

    [Command]
    void CmdGameOver()
    {
        gameOver = true;
        // stop spawning
        StopCoroutine(spawnEnemies);
        StopCoroutine(spawnPowerups);
        
        // delete game objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            NetworkServer.Destroy(enemy);

        GameObject[] markitems = GameObject.FindGameObjectsWithTag("MarkItem");
        foreach (GameObject item in markitems)
            NetworkServer.Destroy(item);

        // prompt user to restart

    }

    [Command]
    void CmdGameRestart()
    {
        // reset player positions
        // reset health and marked status
        // reset score/spawn rates
        // reset game status
        // respawn mark item
    }

    [Command]
    void CmdAddScore(int score)
    {
        currentScore += score;
        --currentEnemies;
    }

    [Command]
    void CmdSpawnFirstMarkItem()
    {
        GameObject item = Instantiate(markItem, Vector3.zero, Quaternion.identity);
        NetworkServer.Spawn(item);
    }

    [Command]
    void CmdSpawnMarkItem()
    {
        // spawn a mark item from a random spawner
        int i = Random.Range(0, powerupSpawners.Length);
        powerupSpawners[i].GetComponent<PowerupSpawner>().SpawnMarkItem();
    }

    [Command]
    void CmdSpawnEnemy()
    {
        if (currentEnemies <= maxEnemies)
        {
            // spawn an enemy from a random spawner
            int i = Random.Range(0, enemySpawners.Length);
            enemySpawners[i].GetComponent<EnemySpawner>().SpawnEnemy();
            ++currentEnemies;
        }
    }
}
