using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    Animator anim;

    bool isDead = false;

    public float damageAmount = 30f;
    float attackTime = 2f;
    public bool canAttack = true;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDead && !PlayerHealth.singleton.isDead)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > 2)
            {
                Chase();
            }
            else if (canAttack)
            {
                Attack();
            }
        }
        else
        {
            Disable();
        }
    }

    private void Chase()
    {
        agent.updatePosition = true;
        agent.SetDestination(target.position);
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);
    }
    private void Attack()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 5f * Time.deltaTime);
        agent.updatePosition = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", true);
        StartCoroutine(AttackTime());
    }
    private void Disable()
    {
        agent.updatePosition = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
    }
    IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        PlayerHealth.singleton.DamagePlayer(damageAmount);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }

    public void EnemyDeathAnimation()
    {
        //if (!isDead)
        {
            isDead = true;
            anim.SetTrigger("isDead");
        }
    }
}
