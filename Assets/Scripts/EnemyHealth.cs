using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;

    EnemyAI enemyAI;

    public bool isEnemyDead;

    public Collider[] enemyColliders;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    public void DeductHealth(float deductHealth)
    {
        if (!isEnemyDead)
        {
            enemyHealth -= deductHealth;

            if (enemyHealth <= 0)
            {
                EnemyDead();
            }
        }
    }

    void EnemyDead()
    {
        enemyHealth = 0f;
        isEnemyDead = true;
        foreach (var col in enemyColliders)
        {
            col.enabled = false;
        }
        enemyAI.EnemyDeathAnimation();
        Destroy(gameObject, 10f);
    }
}
