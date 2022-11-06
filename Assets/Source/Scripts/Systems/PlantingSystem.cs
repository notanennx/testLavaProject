using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Supyrb;
using Cinemachine;
using NaughtyAttributes;

public class PlantingSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private ScriptablePlant[] allowedPlants;
    [SerializeField, BoxGroup("Main")] private CinemachineVirtualCamera virtualCamera;

    [SerializeField, BoxGroup("User Interface")] private RectTransform plantingMenuTransform;

    // Hidden
    private PlantMenuEvent plantMenuEvent;

    // Awaking
    private void Awake()
    {
        // Get
        plantMenuEvent = Signals.Get<PlantMenuEvent>();

        // Subscribe
        plantMenuEvent.AddListener(OnPlantClicked);
    }

    // Destroy
    private void OnDestroy()
    {
        plantMenuEvent.RemoveListener(OnPlantClicked);
    }

    // Handles clicks on plant
    private void OnPlantClicked(PlanterComponent inputPlanter)
    {
        // Set
        virtualCamera.m_Follow = inputPlanter.transform;
        virtualCamera.m_Priority = 100;
        Debug.Log("Focusing camera!");

        // Open
        ShowPlantingMenu();
    }

    // Opens a planting menu
    private void ShowPlantingMenu()
    {
        // Set
        GameSystem.IsMenuOpen = true;
    }
}
