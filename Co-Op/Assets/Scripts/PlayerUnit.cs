using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 direction;
    [SerializeField] float angle;

    public GameObject bulletPrefab;

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
    virtual public void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        Look();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
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

    virtual public void Shoot()
    {
        CmdShoot();
    }

    [Command]
    void CmdShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);

        bullet.transform.eulerAngles = this.transform.eulerAngles;

        Debug.Log("bullet: " + bullet.transform.rotation);

        NetworkServer.Spawn(bullet);
    }
}
