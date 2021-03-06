﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cinemachine;

public class PlayerUnit : NetworkBehaviour
{
    private Rigidbody2D rb;
    private Health health;
    private GameController gameController;
    
    [SerializeField] CinemachineVirtualCamera vcam = null;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 direction;
    [SerializeField] float angle;

    public GameObject bulletPrefab;
    [SerializeField] protected Transform launchPoint;

    public GameObject healBulletPrefab;

    [SyncVar] public bool marked;
    [SyncVar] public bool dead;

    private SpriteRenderer SpriteR;
    public Sprite DefaultSprite;
    public Sprite MarkedSprite;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        health = this.GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        //Get the sprite renderer
        SpriteR = GetComponentInChildren<SpriteRenderer>();

        if (!hasAuthority)
        {
            return;
        }
    }

    // Update is called once per frame
    virtual public void Update()
    {
        if (marked)
        {
            SpriteR.sprite = MarkedSprite;
        }
        else
        {
            SpriteR.sprite = DefaultSprite;
        }

        // *** only the below should be in update ***
        if(vcam == null && hasAuthority)
        {
            //Debug.Log("vcam is null");
            //Debug.Log("is local player");
            vcam = GameObject.FindGameObjectWithTag("cam").GetComponent<CinemachineVirtualCamera>();
            vcam.m_Follow = this.transform;
            vcam.m_LookAt = this.transform;
        }

        if (!hasAuthority)
        {
            return;
        }

        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        Look();

        if (Input.GetButtonDown("Fire1"))
        {
            if (!marked && !dead)
            {
                Shoot();
            }
            else if (!dead)
            {
                CmdHealShoot();
            }
        }
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
        if (!dead)
        {
            rb.velocity = direction.normalized * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Look()
    { 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!dead)
        {
            angle = Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x) * Mathf.Rad2Deg;
        }

        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            Debug.Log("player collided with bullet");
            TakeDamage(1);
        }
        if (collision.collider.tag == "Enemy")
        {
            Debug.Log("player collided with enemy");
            TakeDamage(1);
        }
        if (collision.collider.tag == "HealBullet")
        {
            Debug.Log("player collided with heal bullet");
            Heal(5f);
        }
    }

    private void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        if (health.GetHealth() <= 0)
        {
            Debug.Log("Frozen/Dead");
            dead = true;
            health.SetBackgroundColor(Color.red);

            if (marked)
                gameController.GameOver();
        }
    }

    private void Heal(float damage)
    {
        if (dead)
        {
            health.Heal(damage);
            if (health.GetHealth() >= health.GetMaxHealth())
            {
                Debug.Log("Revived");
                dead = false;
                health.SetBackgroundColor(Color.gray);
            }
        }
    }

    virtual public void Shoot()
    {
        CmdShoot();
    }

    [Command]
    void CmdShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, launchPoint.position, Quaternion.identity);

        bullet.transform.eulerAngles = this.transform.eulerAngles;

        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());

        //Debug.Log("bullet: " + bullet.transform.rotation);

        NetworkServer.Spawn(bullet);

    }

    [Command]
    void CmdHealShoot()
    {
        GameObject healBullet = Instantiate(healBulletPrefab, launchPoint.position, Quaternion.identity);

        healBullet.transform.eulerAngles = this.transform.eulerAngles;

        Physics2D.IgnoreCollision(healBullet.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());

        //Debug.Log("bullet: " + bullet.transform.rotation);

        NetworkServer.Spawn(healBullet);

    }
}
