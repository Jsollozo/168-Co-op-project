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
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        GameObject bullet2 = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        GameObject bullet3 = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);

        bullet.transform.eulerAngles = this.transform.eulerAngles;
        bullet2.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0,0,25);
        bullet3.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0,0,-25);

        //Debug.Log("bullet: " + bullet.transform.rotation);

        NetworkServer.Spawn(bullet);
        NetworkServer.Spawn(bullet2);
        NetworkServer.Spawn(bullet3);
    }
}
