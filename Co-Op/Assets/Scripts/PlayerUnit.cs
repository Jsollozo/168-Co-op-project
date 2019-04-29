using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cinemachine;

public class PlayerUnit : NetworkBehaviour
{
    private Rigidbody2D rb;
    private Health health;
    
    [SerializeField] CinemachineVirtualCamera vcam = null;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 direction;
    [SerializeField] float angle;

    public GameObject bulletPrefab;
    [SerializeField] protected Transform launchPoint;

    [SyncVar] public bool marked;

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

        if(vcam == null)
        {
            Debug.Log("vcam is null");
            if(hasAuthority)
            {
                Debug.Log("is local player");
                vcam = GameObject.FindGameObjectWithTag("cam").GetComponent<CinemachineVirtualCamera>();
                vcam.m_Follow = this.transform;
                vcam.m_LookAt = this.transform;
            }
        }

        if (!hasAuthority)
        {
            return;
        }

        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        Look();

        if (Input.GetButtonDown("Fire1"))
        {
            if (!marked)
            {
                Shoot();
            }
            else
            {
                //Marked player shooting goes here
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
        rb.velocity = direction.normalized * moveSpeed;
    }

    void Look()
    { 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        angle = Mathf.Atan2(mousePos.y - this.transform.position.y, mousePos.x - this.transform.position.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            Debug.Log("player collided with bullet");
            Destroy(collision.collider.gameObject);
            health.TakeDamage(1);
            if (health.GetHealth() <= 0)
            {
                Debug.Log("Frozen/Dead");
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

        Debug.Log("bullet: " + bullet.transform.rotation);

        NetworkServer.Spawn(bullet);

    }
}
