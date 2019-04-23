using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    //private Rigidbody2D rb;

    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLife;

    bool wantsToDie = false;

    void Awake()
    {
        //rb = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = transform.forward * bulletSpeed;
        //Destroy(this.gameObject, bulletLife);
        StartCoroutine(DestroyBulletCoroutine());
    }

    void Update()
    {
        if (wantsToDie)
        {
            CmdBulletDestroy();
        }
    }

    private void FixedUpdate()
    {
        this.transform.position += transform.right * bulletSpeed * Time.deltaTime;
    }

    [Command]
    void CmdBulletDestroy()
    {
        NetworkServer.Destroy(this.gameObject);
    }

    private IEnumerator DestroyBulletCoroutine()
    {
        yield return new WaitForSeconds(bulletLife);
        wantsToDie = true;

    }
}
