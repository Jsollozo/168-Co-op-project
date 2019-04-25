using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharsSelectNetworkManager : NetworkManager
{
    public int chosenCharacter = 0;
    public GameObject[] characterList;
    public GameObject charSelectPanel;

    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selected = message.chosenClass;

        GameObject player = Instantiate(characterList[selected], Vector3.zero, Quaternion.identity);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.chosenClass = chosenCharacter;

        ClientScene.AddPlayer(conn, 0, test);
    }

    public void ChooseCircle()
    {
        chosenCharacter = 0;
        charSelectPanel.SetActive(false);
    }

    public void ChooseTriangle()
    {
        chosenCharacter = 1;
        charSelectPanel.SetActive(false);
    }

    public void ChooseSquare()
    {
        chosenCharacter = 2;
        charSelectPanel.SetActive(false);
    }
}
