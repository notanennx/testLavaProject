using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supyrb;
using DG.Tweening;
using NaughtyAttributes;

public class HarvestingSystem : MonoBehaviour
{
    // Hidden
    private PlayerComponent playerComponent;

    // Events
    private OnPlantHarvest onPlantHarvest;

    // Awaking
    private void Awake()
    {
        // Get
        playerComponent = GameObject.FindObjectOfType<PlayerComponent>();

        // Events
        onPlantHarvest = Signals.Get<OnPlantHarvest>();

        // Subscribe
        onPlantHarvest.AddListener(OnPlantHarvest);
    }

    // Destroy
    private void OnDestroy()
    {
        // Unsubscribe
        onPlantHarvest.RemoveListener(OnPlantHarvest);
    }

    // Plant harvesting is done here
    private void OnPlantHarvest(PlantComponent inputPlant)
    {
        // Do
        inputPlant.GetHarvestScript().OnHarvestComplete(playerComponent, inputPlant);
    }
}
