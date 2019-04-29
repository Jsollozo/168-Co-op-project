using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{
    public List<GameObject> enemies;
    public GameObject markItem;
    public float spawnRate;

    public override void OnStartServer()
    {
        // begin spawning enemies upon server start
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnMarkItems());
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

    void SpawnEnemy()
    {
        // to start with: spawn an enemy
        GameObject spawnedEnemy = Instantiate(enemies[0], gameObject.transform.position, gameObject.transform.rotation);
        NetworkServer.Spawn(spawnedEnemy);
    }

    IEnumerator SpawnMarkItems()
    {
        for (;;)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnMarkItem();
        }
    }

    void SpawnMarkItem()
    {
        GameObject spawnedItem = Instantiate(markItem, gameObject.transform.position, gameObject.transform.rotation);
        NetworkServer.Spawn(spawnedItem);
    }
}
