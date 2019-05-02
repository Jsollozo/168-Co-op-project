using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PowerupSpawner : NetworkBehaviour
{
    public List<GameObject> enemies;
    public GameObject markItem;
    public float spawnRate;

    public override void OnStartServer()
    {
        // begin spawning powerups upon server start
        //StartCoroutine(SpawnMarkItems());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnMarkItems()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnMarkItem();
        }
    }

    public void SpawnMarkItem()
    {
        GameObject spawnedItem = Instantiate(markItem, gameObject.transform.position, gameObject.transform.rotation);
        NetworkServer.Spawn(spawnedItem);
    }
}
