using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyUnit : NetworkBehaviour
{
    public float moveSpeed;

    [SerializeField] GameObject target;
    private Rigidbody2D rb;

    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        // TEMP: find a player upon spawning to track
        CmdFindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // just move toward target for now
        CmdMoveTowardTarget();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            Destroy(collision.collider.gameObject);
            health.TakeDamage(1);
            if (health.GetHealth() <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    [Command]
    void CmdFindTarget()
    {
        // TODO: this code will be replaced when the marked mechanic is implemented
        target = GameObject.FindGameObjectWithTag("Player");
    }

    [Command]
    void CmdMoveTowardTarget()
    {
        if (target != null && isServer)
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.right = direction;
            rb.velocity = direction.normalized * moveSpeed;
        }
    }
}
