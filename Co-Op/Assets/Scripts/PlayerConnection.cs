using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnection : NetworkBehaviour
{

    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //playerPrefab = CharacterSelect.instance.GetCharacter();
        Debug.Log(playerPrefab.name);
        CmdSpawnPlayer();
    }

    GameObject myPlayerUnit;

    [Command]
    void CmdSpawnPlayer()
    {
        Debug.Log(playerPrefab.name);

        GameObject player = Instantiate(playerPrefab);

        //myPlayerUnit = player;

        //Debug.Log(myPlayerUnit.name);

        NetworkServer.SpawnWithClientAuthority(player, connectionToClient);
    }
}
