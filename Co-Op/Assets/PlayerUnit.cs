using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 direction;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!hasAuthority)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
    }

    private void FixedUpdate()
    {
        if (!hasAuthority)
        {
            return;
        }

        MovePlayer();
    }

    void MovePlayer()
    {
        rb.velocity = direction.normalized * moveSpeed;
    }
}
