using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Supyrb;

public class MoveSystem : MonoBehaviour
{
    // Events
    private MoveEvent moveEvent;
    private MoveEventBusy moveEventBusy;
    private DestinationReachedEvent destinationReachedEvent;

    // Hidden
    private bool isBusy;
    private bool isMoving;
    private NavMeshAgent navMeshAgent;
    private MoveComponent moveComponent;

    // Awaking
    private void Awake()
    {
        // Get
        moveComponent = GameObject.FindObjectOfType<MoveComponent>();
        navMeshAgent = moveComponent.GetNavMeshAgent();;

        // Events
        moveEvent = Signals.Get<MoveEvent>();
        moveEventBusy = Signals.Get<MoveEventBusy>();
        destinationReachedEvent = Signals.Get<DestinationReachedEvent>();

        // Subscribe
        moveEvent.AddListener(ProcessInput);
        moveEventBusy.AddListener(ProcessInputBusy);
    }

    // Destroy
    private void OnDestroy()
    {
        // Unsubscribe
        moveEvent.RemoveListener(ProcessInput);
        moveEventBusy.RemoveListener(ProcessInputBusy);
    }

    // Updating
    private void Update()
    {
        // Animation
        moveComponent.GetAnimator().SetBool("IsMoving", moveComponent.GetNavMeshAgent().hasPath);

        // Destination
        if ((IsCompletedPath()) && (isMoving))
        {
            isBusy = false;
            isMoving = false;
            Debug.Log("Completed");
        }
    }

    // Processes input
    private void ProcessInput(Vector3 inputPosition)
    {
        // Exit
        if (isBusy) return;

        // Move
        isMoving = true;
        navMeshAgent.SetDestination(inputPosition);
    }

    // Processes input
    private void ProcessInputBusy(Vector3 inputPosition)
    {
        // Move
        isBusy = true;
        isMoving = true;
        navMeshAgent.SetDestination(inputPosition);

        // Debugging
        Debug.Log("Busy input!");
    }

    // Returns if path completed or not
    private bool IsCompletedPath() => ((navMeshAgent.remainingDistance != Mathf.Infinity) && (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete) && (navMeshAgent.remainingDistance == 0));
}
