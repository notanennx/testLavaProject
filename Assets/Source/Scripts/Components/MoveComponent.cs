using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Supyrb;

public class MoveComponent : MonoBehaviour
{
    // Hidden
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    // Getters
    public Animator GetAnimator() => animator;
    public NavMeshAgent GetNavMeshAgent() => navMeshAgent;

    // Awaking
    private void Awake()
    {
        // Get
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
