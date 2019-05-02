using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cinemachine;

public class MarkItem : NetworkBehaviour
{
    [SerializeField] bool wantsToDie = false;
    [SerializeField] float lifetime = 3f;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        //StartCoroutine(DestroyItemCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        //if(wantsToDie)
        //{
        //    CmdDestroyItem();
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerUnit player = collision.collider.GetComponent<PlayerUnit>();
            if (!player.marked)
            {
                CmdUnmarkPlayer();

                //Mark the player that hit the item
                player.marked = true;

                // Start game
                gameController.GameStart();
            }
            CmdDestroyItem();
        }
    }

    //private IEnumerator DestroyItemCoroutine()
    //{
    //    yield return new WaitForSeconds(lifetime);
    //    wantsToDie = true;

    //}

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

    [Command]
    void CmdDestroyItem()
    {
        NetworkServer.Destroy(this.gameObject);
    }
}
