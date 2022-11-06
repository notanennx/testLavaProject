using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Supyrb;

public class MoveSystem : MonoBehaviour
{
    // Hidden
    private InputEvent inputEventSignal;
    private MoveComponent moveComponent;

    // Awaking
    private void Awake()
    {
        // Get
        moveComponent = GameObject.FindObjectOfType<MoveComponent>();
        inputEventSignal = Signals.Get<InputEvent>();

        // Subscribe
        inputEventSignal.AddListener(ProcessInput);
    }

    // Destroy
    private void OnDestroy()
    {
        inputEventSignal.RemoveListener(ProcessInput);
    }

    // Processes input
    private void ProcessInput(Vector3 inputPosition)
    {
        moveComponent.GetNavMeshAgent().SetDestination(inputPosition);
    }
}
