using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Vector3 direction;
    [SerializeField] float angle;

    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        Look();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
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


    void CmdShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);

        bullet.transform.eulerAngles = this.transform.eulerAngles;

    }
}
