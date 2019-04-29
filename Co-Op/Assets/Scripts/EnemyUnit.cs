using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyUnit : NetworkBehaviour
{
    public float moveSpeed;

    [SerializeField] protected GameObject target;
    protected Rigidbody2D rb;

    protected Health health;

    // Start is called before the first frame update
    virtual public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        // TEMP: find a player upon spawning to track
        CmdFindTarget();
    }

    // Update is called once per frame
    virtual public void Update()
    {
        if (health.GetHealth() <= 0)
        {
            Destroy(gameObject);
        }
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
