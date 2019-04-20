using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    public List<GameObject> characterList = new List<GameObject>();

    private GameObject chosenCharacter;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        int n = Random.Range(0, characterList.Count);

        chosenCharacter = characterList[n];

        characterList.RemoveAt(n);

        GameObject player = Instantiate(chosenCharacter, Vector3.zero, Quaternion.identity);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
