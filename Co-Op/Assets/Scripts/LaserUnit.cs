using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaserUnit : PlayerUnit
{
    public override void Shoot()
    {
        CmdLaserShoot();
    }

    [Command]
    void CmdLaserShoot()
    {
        var playerCollider = this.GetComponent<Collider2D>();

        GameObject laser = Instantiate(bulletPrefab, launchPoint.position, Quaternion.identity);
        Physics2D.IgnoreCollision(laser.GetComponent<Collider2D>(), playerCollider);
        laser.transform.eulerAngles = this.transform.eulerAngles;

        NetworkServer.Spawn(laser);
    }
}
