using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Collider swordCollider;
    [SerializeField] private float attackInterval = 0.5f;

    //private float lastHitTime = 0;
    public Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isDead = false;

    void Awake()
    {
        swordCollider.enabled = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    public void OnDeath()
    {
        Debug.Log("orc dead");
        isDead = true;
        navMeshAgent.isStopped = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(isDead) return;
        if(Vector3.Distance(transform.position, player.position)> 1f)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(player.position);
            animator.SetBool("running", true);
        }
        else
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("running", false);
            //if(Time.time - lastAttackTime >)
        }
    }

    public void StartAttack()
    {
        swordCollider.enabled = true;
    }
    public void EndAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        //Destroy(gameObject);
    }
}
