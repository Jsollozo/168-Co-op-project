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

        CmdSpawnPlayer();
    }

    GameObject myPlayerUnit;

    [Command]
    void CmdSpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab);

        myPlayerUnit = player;

        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
    }
}
