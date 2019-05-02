using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{
    public List<GameObject> enemies;
    public float spawnRate;

    public override void OnStartServer()
    {
        // begin spawning enemies upon server start
        //StartCoroutine(SpawnEnemies());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        // spawn enemies at fixed rate
        for (;;)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        // spawn a random enemy from list
        int rand = Random.Range(0, enemies.Count);

        GameObject spawnedEnemy = Instantiate(enemies[rand], gameObject.transform.position, gameObject.transform.rotation);
        NetworkServer.Spawn(spawnedEnemy);
    }
}
