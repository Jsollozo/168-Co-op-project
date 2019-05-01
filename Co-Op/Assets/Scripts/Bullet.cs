using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    //private Rigidbody2D rb;

    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float bulletLife;

    bool wantsToDie = false;

    void Awake()
    {
        //rb = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        //rb.velocity = transform.forward * bulletSpeed;
        //Destroy(this.gameObject, bulletLife);
        StartCoroutine(DestroyBulletCoroutine());
    }

    protected void Update()
    {
        if (wantsToDie)
        {
            CmdBulletDestroy();
        }
    }

    protected void FixedUpdate()
    {
        this.transform.position += transform.right * bulletSpeed * Time.deltaTime;
    }

    [Command]
    protected void CmdBulletDestroy()
    {
        NetworkServer.Destroy(this.gameObject);
    }

    protected IEnumerator DestroyBulletCoroutine()
    {
        yield return new WaitForSeconds(bulletLife);
        wantsToDie = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CmdBulletDestroy();
    }
}
