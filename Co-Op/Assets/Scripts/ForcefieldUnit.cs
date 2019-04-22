using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ForcefieldUnit : PlayerUnit
{

    public GameObject forcefieldPrefab;

    private GameObject myForcefield = null;

    public override void Update()
    {
        base.Update();

        if (Input.GetButtonUp("Fire1"))
        {
            CmdForcefieldStop();
        }
    }

    public override void Shoot()
    {
        Debug.Log("LeftClick");
        CmdForcefieldDeploy();   
    }

    [Command]
    void CmdForcefieldDeploy()
    {

        Debug.Log("Forcefield");
        if (myForcefield == null)
        {
            GameObject forcefield = Instantiate(forcefieldPrefab, this.transform.position, this.transform.rotation, this.transform);

            forcefield.transform.SetParent(this.transform);

            myForcefield = forcefield;

            NetworkServer.Spawn(forcefield);
        }
    }

    [Command]
    void CmdForcefieldStop()
    {
        if(myForcefield != null)
        {
            NetworkServer.Destroy(myForcefield);
        }
    }
}
