using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Supyrb;
using NaughtyAttributes;

public class PlanterComponent : MonoBehaviour, IClickable
{
    // Vars
    [SerializeField, BoxGroup("Main")] private GameObject plantToSpawn;

    // Hidden
    private PlantMenuEvent plantMenuEvent;

    // Awaking
    private void Awake()
    {
        // Get
        plantMenuEvent = Signals.Get<PlantMenuEvent>();
    }

    [Button]
    private void SpawnPlant()
    {
        // Exit
        if (!plantToSpawn) return;

        // Create
        Transform newPlant = Instantiate(plantToSpawn, transform).transform;
    }

    // Clicked
    public void OnClicked()
    {
        // Dispatch
        plantMenuEvent.Dispatch(this);
    }
}
