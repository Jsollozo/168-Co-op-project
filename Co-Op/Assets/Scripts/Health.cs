using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    public const float maxHealth = 10f;

    [SyncVar(hook = "OnChangeHealth")] public float currentHealth;

    public Image healthbar;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if(!isServer)
        {
            return;
        }

        currentHealth -= amount;
        OnChangeHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead");
        }
    }

    private void OnChangeHealth(float health)
    {
        currentHealth = health;
        healthbar.fillAmount = currentHealth / maxHealth;
        //Debug.Log(healthbar.fillAmount);
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
