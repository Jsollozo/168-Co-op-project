using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldCollision : MonoBehaviour
{
    public float damageOverTime = 3f;

    [SerializeField] float maxCapacity = 80f;

    private float currentCap = 0;

    void OnParticleCollision(GameObject other)
    {

        if(!other.CompareTag("Player"))
        {
            //Debug.Log(other.name);
            if(currentCap <= maxCapacity)
            {
                other.GetComponent<Health>().TakeDamage(damageOverTime * Time.deltaTime);
                currentCap += 1;
            }
        }
    }
}
