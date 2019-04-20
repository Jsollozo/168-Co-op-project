﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    //private Rigidbody2D rb;

    [SerializeField] float bulletSpeed;

    void Awake()
    {
        //rb = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = transform.forward * bulletSpeed;
    }

    void Update()
    {
        Destroy(this.gameObject, 2);
    }

    private void FixedUpdate()
    {
        this.transform.position += transform.right * bulletSpeed * Time.deltaTime;
    }
}