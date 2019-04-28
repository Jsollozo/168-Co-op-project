using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShotgunUnit : PlayerUnit
{
    public override void Shoot()
    {
        CmdShotgunShoot();
    }

    [Command]
    void CmdShotgunShoot()
    {
        var playerCollider = this.GetComponent<Collider2D>();

        GameObject bullet = Instantiate(bulletPrefab, launchPoint.position, Quaternion.identity);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), playerCollider);
        bullet.transform.eulerAngles = this.transform.eulerAngles;

        GameObject bullet2 = Instantiate(bulletPrefab, launchPoint.position, Quaternion.identity);
        Physics2D.IgnoreCollision(bullet2.GetComponent<Collider2D>(), playerCollider);
        bullet2.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0, 0, 25);

        GameObject bullet3 = Instantiate(bulletPrefab, launchPoint.position, Quaternion.identity);
        Physics2D.IgnoreCollision(bullet3.GetComponent<Collider2D>(), playerCollider);
        bullet3.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0,0,-25);

        //Debug.Log("bullet: " + bullet.transform.rotation);

        NetworkServer.Spawn(bullet);
        NetworkServer.Spawn(bullet2);
        NetworkServer.Spawn(bullet3);
    }
}
