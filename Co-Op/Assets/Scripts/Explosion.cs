using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Explosion : Bullet
{
    private Rigidbody2D rb;
    Vector3 direction;

    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * bulletSpeed;
    }

    public void SetDirection(Vector3 v)
    {
        direction = v;
    }


}
