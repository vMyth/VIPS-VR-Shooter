using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;

    public bool isDead;

    public static PlayerHealth singleton;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DamagePlayer(float damage)
    {
        if (currentHealth >= 0)
        {
            currentHealth -= damage;
        }
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        Debug.Log("PLAYER DEAD");
    }
}
