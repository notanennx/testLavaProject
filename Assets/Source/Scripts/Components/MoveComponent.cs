using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Supyrb;

public class MoveComponent : MonoBehaviour
{
    // Hidden
    private NavMeshAgent navMeshAgent;

    // Getters
    public NavMeshAgent GetNavMeshAgent() => navMeshAgent;

    // Awaking
    private void Awake()
    {
        // Get
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
