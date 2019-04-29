using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExplodingEnemy : EnemyUnit
{
    public GameObject bulletPrefab;

    private List<Vector3> directions = new List<Vector3>();

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        directions.Add(new Vector3(0f, 1f, 0f)); // Up
        directions.Add(new Vector3(1f, 1f, 0f)); // Up Right
        directions.Add(new Vector3(1f, 0f, 0f)); // Right
        directions.Add(new Vector3(1f, -1f, 0f)); // Down Right

        directions.Add(new Vector3(0f, -1f, 0f)); // Down
        directions.Add(new Vector3(-1f, -1f, 0f)); // Down Left
        directions.Add(new Vector3(-1f, 0f, 0f)); // Left
        directions.Add(new Vector3(-1f, 1f, 0f)); // Up Left
    }

    // Update is called once per frame
    public override void Update()
    {
        if(health.GetHealth() <= 0)
        {
            Explode();
            Destroy(this.gameObject);
        }
    }

    private void Explode()
    {
        CmdExplode();
    }

    [Command]
    void CmdExplode()
    {
        for (int i = 0; i < directions.Count; i++)
        {
            //Debug.Log(directions[i]);
            Vector3 newDirection = this.transform.position + directions[i];

            float angle = Mathf.Atan2(newDirection.y - this.transform.position.y, newDirection.x - this.transform.position.x) * Mathf.Rad2Deg;

            GameObject explode = Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
            explode.GetComponent<Explosion>().SetDirection(directions[i]);

            NetworkServer.Spawn(explode);
        }
    }
}
