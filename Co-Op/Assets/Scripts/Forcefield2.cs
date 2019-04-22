using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Forcefield2 : PlayerUnit
{

    public override void Update()
    {
        base.Update();

        if (Input.GetButtonUp("Fire1") && isLocalPlayer)
        {
            //CmdForcefieldStop();
        }
    }

    public override void Shoot()
    {
        Debug.Log("LeftClick");
        CmdForcefieldDeploy();
    }

    void CmdForcefieldDeploy()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    void CmdForcefieldStop()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
