using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cinemachine;

public class MarkItem : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(gameObject);
            PlayerUnit player = collision.collider.GetComponent<PlayerUnit>();
            if (!player.marked)
            {
                CmdUnmarkPlayer();

                //Mark the player that hit the item
                player.marked = true;
            }
        }
    }

    [Command]
    void CmdUnmarkPlayer()
    {
        //Unmark all players
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; ++i)
        {
            players[i].GetComponent<PlayerUnit>().marked = false;
        }
    }
}
