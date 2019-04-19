using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerConnection : NetworkBehaviour
{

    public GameObject playerPrefab;

    private GameObject myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        CmdSpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command]
    void CmdSpawnPlayer()
    {
        GameObject go = Instantiate(playerPrefab);

        myPlayer = go;

        go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);

        NetworkServer.Spawn(go);
    }
}
