using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Supyrb;
using NaughtyAttributes;

public class PlayerComponent : MonoBehaviour
{
    // Hidden
    private Animator animator;

    // Getters
    public Animator GetAnimator() => animator;

    // Awaking
    private void Awake()
    {
        // Get
        animator = GetComponentInChildren<Animator>();
    }
}
