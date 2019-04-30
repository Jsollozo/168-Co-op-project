using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LaserUnit : PlayerUnit
{
    public Material laserMat;

    private Vector3 target;

    public override void Shoot()
    {
        {
            CmdLaserShoot();
        }
    }

    [Command]
    void CmdLaserShoot()
    {
        RpcLaserShoot();

        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //var playerCollider = this.GetComponent<Collider2D>();

        GameObject laser = Instantiate(bulletPrefab, launchPoint.position, Quaternion.identity);
        //Physics2D.IgnoreCollision(laser.GetComponent<Collider2D>(), playerCollider);
        //laser.transform.eulerAngles = this.transform.eulerAngles;
        LineRenderer Beam = laser.GetComponent<LineRenderer>();


        Beam.positionCount = 2;
        Beam.material = laserMat;
        Beam.startWidth = Beam.endWidth = 0.15f;
        Beam.enabled = true;
        Beam.SetPosition(0, launchPoint.position);
        Beam.SetPosition(1, target);
        Destroy(laser, 0.1f);

        //Show line on client side

        //Calculate hit
        /*var diff = target - launchPoint.position;
        var dist = Vector2.Distance(launchPoint.position, target);
        RaycastHit2D[] hits = Physics2D.RaycastAll(launchPoint.position, diff, dist);

        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].collider.name);
            Health targetHealth = hits[i].transform.GetComponent<Health>();



            if (targetHealth != null && hits[i].collider.CompareTag("Enemy"))
            {
                targetHealth.TakeDamage(1);
            }
        }*/

        NetworkServer.Spawn(laser);
    }

    [ClientRpc]
    void RpcLaserShoot()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var playerCollider = this.GetComponent<Collider2D>();

        GameObject laser = Instantiate(bulletPrefab, launchPoint.position, Quaternion.identity);
        LineRenderer Beam = laser.GetComponent<LineRenderer>();

        Beam.positionCount = 2;
        Beam.material = laserMat;
        Beam.startWidth = Beam.endWidth = 0.15f;
        Beam.enabled = true;
        Beam.SetPosition(0, launchPoint.position);
        Beam.SetPosition(1, target);

        Destroy(laser, 0.1f);

        //Calculate hit
        var diff = target - launchPoint.position;
        var dist = Vector2.Distance(launchPoint.position, target);
        RaycastHit2D[] hits = Physics2D.RaycastAll(launchPoint.position, diff, dist);

        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].collider.name);
            Health targetHealth = hits[i].transform.GetComponent<Health>();



            if (targetHealth != null && hits[i].collider.CompareTag("Enemy"))
            {
                targetHealth.TakeDamage(1);
            }
        }
        //NetworkServer.Spawn(laser);
    }
}

