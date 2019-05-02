using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnection : NetworkBehaviour
{

    public GameObject playerPrefab;
    public int positionIndex;
    private Vector3[] positions;

    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[] {
            new Vector3(3,2,0),
            new Vector3(-3,2,0),
            new Vector3(-3,-2,0),
            new Vector3(3,-2,0)
        };

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

        GameObject player = Instantiate(playerPrefab, positions[positionIndex], Quaternion.identity);

        //myPlayerUnit = player;

        //Debug.Log(myPlayerUnit.name);

        NetworkServer.SpawnWithClientAuthority(player, connectionToClient);
    }
}
