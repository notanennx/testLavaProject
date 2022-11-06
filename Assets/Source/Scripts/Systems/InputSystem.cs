using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supyrb;

public class InputSystem : MonoBehaviour
{
    // Hidden
    private Camera mainCamera;
    private InputEvent inputEventSignal;

    // Awaking
    private void Awake()
    {
        // Get
        mainCamera = Camera.main;
        inputEventSignal = Signals.Get<InputEvent>();
    }

    // Update
    private void Update()
    {
        ProcessInput();
    }

    // Handles input
    private void ProcessInput()
    {
        // Clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast
            RaycastHit rayHit;
            Ray rayCast = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayCast, out rayHit))
            {
                // Get
                Transform objectHit = rayHit.transform;

                // Dispatch
                inputEventSignal.Dispatch(rayHit.point);
            }
        }
    }
}
