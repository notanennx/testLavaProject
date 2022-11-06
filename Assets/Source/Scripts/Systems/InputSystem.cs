using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supyrb;

public class InputSystem : MonoBehaviour
{
    // Hidden
    private Camera mainCamera;
    private InputMoveEvent inputMoveEvent;

    // Awaking
    private void Awake()
    {
        // Get
        mainCamera = Camera.main;
        inputMoveEvent = Signals.Get<InputMoveEvent>();
    }

    // Update
    private void Update()
    {
        ProcessInput();
    }

    // Handles input
    private void ProcessInput()
    {
        // Exit
        if (GameSystem.IsMenuOpen) return;

        // Clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast
            RaycastHit rayHit;
            Ray rayCast = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayCast, out rayHit))
            {
                // Planting
                Transform objectHit = rayHit.transform;
                if (objectHit.TryGetComponent(out IClickable clickableComponent))
                {
                    clickableComponent.OnClicked();
                }
                // Moving it
                else
                {
                    // Dispatch
                    inputMoveEvent.Dispatch(rayHit.point);
                }
            }
        }
    }
}
