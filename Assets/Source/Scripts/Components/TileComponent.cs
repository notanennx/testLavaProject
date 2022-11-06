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
    private PlantMenuEvent plantMenuEvent;

    // Setters
    public void SetOccupied(bool inputBool) => isOccupied = inputBool;

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
