using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    //public static CustomNetworkManager instance;

    public List<GameObject> playerPrefabs = new List<GameObject>();

    [SerializeField] GameObject playerConnection;

    GameObject chosenCharacter;

    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage)
    {
        int i = Random.Range(0, playerPrefabs.Count);

        chosenCharacter = playerPrefabs[i];

        playerPrefabs.RemoveAt(i);

        GameObject go = Instantiate(chosenCharacter, Vector3.zero, Quaternion.identity);

        NetworkServer.AddPlayerForConnection(conn, go);
    }

    public void GetCharacterName()
    {
        Debug.Log(chosenCharacter.name);
    }
}
