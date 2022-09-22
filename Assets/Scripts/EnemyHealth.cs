using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;

    EnemyAI enemyAI;

    public bool isEnemyDead;

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
        isEnemyDead = true;
        enemyAI.EnemyDeathAnimation();
        Destroy(gameObject, 10f);
    }
}
