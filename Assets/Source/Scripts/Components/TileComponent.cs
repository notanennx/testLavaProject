using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Supyrb;
using NaughtyAttributes;

public class TileComponent : MonoBehaviour, IClickable
{
    // Hidden
    private bool isOccupied;
    private PlantComponent plant;
    private PlantMenuEvent plantMenuEvent;

    // Getters
    public bool IsOccupied() => (plant);
    public PlantComponent GetPlant() => plant;

    // Setters
    public void SetPlant(PlantComponent inputPlant) => plant = inputPlant;

    // Awaking
    private void Awake()
    {
        // Get
        plantMenuEvent = Signals.Get<PlantMenuEvent>();
    }

    // Clicked
    public void OnClicked()
    {
        // Exit
        if (isOccupied) return;

        // Dispatch
        plantMenuEvent.Dispatch(this);
    }
}
