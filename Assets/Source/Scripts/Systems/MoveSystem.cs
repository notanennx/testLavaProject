using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Supyrb;

public class MoveSystem : MonoBehaviour
{
    // Hidden
    private InputMoveEvent inputMoveEvent;
    private MoveComponent moveComponent;

    // Awaking
    private void Awake()
    {
        // Get
        moveComponent = GameObject.FindObjectOfType<MoveComponent>();
        inputMoveEvent = Signals.Get<InputMoveEvent>();

        // Subscribe
        inputMoveEvent.AddListener(ProcessInput);
    }

    // Updating
    private void Update()
    {
        // Animation
        moveComponent.GetAnimator().SetBool("IsMoving", moveComponent.GetNavMeshAgent().hasPath);
    }

    // Destroy
    private void OnDestroy()
    {
        inputMoveEvent.RemoveListener(ProcessInput);
    }

    // Processes input
    private void ProcessInput(Vector3 inputPosition)
    {
        moveComponent.GetNavMeshAgent().SetDestination(inputPosition);
    }
}
