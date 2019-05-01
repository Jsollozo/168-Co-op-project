using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class EnemyUnit : NetworkBehaviour
{
    public float moveSpeed;

    private GameObject target;
    private Rigidbody2D rb;
    protected Health health;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    virtual public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

        // prevent agent from making unwanted rotations
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    virtual public void Update()
    {
        // update target
        CmdFindTarget();
    }

    private void FixedUpdate()
    {
        CmdMoveTowardTarget();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            TakeDamage(1);
        }
        if (collision.collider.tag == "Player")
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        if (health.GetHealth() <= 0)
        {
            Die();
        }
    }

    virtual protected void Die()
    {
        Destroy(gameObject);
    }

    [Command]
    void CmdFindTarget()
    {
        // check if a target has already been set
        if (target != null)
        {
            // if target is marked, there is no need to do anything
            if (target.GetComponent<PlayerUnit>().marked == true)
            {
                return;
            }
        }
        // if target is not a valid marked player, find the marked player and set as target
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if (player.GetComponent<PlayerUnit>().marked == true)
            {
                target = player;
                return;
            }
        }
        // if no marked player is found, set target to null and stop movement
        target = null;
        agent.isStopped = true;
    }

    [Command]
    void CmdMoveTowardTarget()
    {
        // only run on server, when enemy has a valid target
        if (target != null && isServer)
        {
            // set rotation
            Vector3 direction = target.transform.position - transform.position;
            transform.right = direction;

            //// set velocity
            //rb.velocity = direction.normalized * moveSpeed;

            // use agent to control movement
            agent.destination = target.transform.position;
            agent.isStopped = false;
        }
    }
}
