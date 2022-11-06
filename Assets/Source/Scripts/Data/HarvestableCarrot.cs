using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supyrb;
using DG.Tweening;
using Random = UnityEngine.Random;

[Serializable]
public class HarvestableCarrot : HarvestableBase
{
    // Harvest
    public override void OnHarvestComplete(PlayerComponent inputPlayer, PlantComponent inputPlant)
    {
        // Exit
        if (!inputPlant.IsHarvestable()) return;

        // Set
        inputPlant.Remove();
        inputPlant.SetHarvestable(false);

        // Dispatch
        Signals.Get<CreateCarrotsEvent>().Dispatch(Random.Range(6, 9), inputPlant.transform.position);

        // Debugging
        Debug.Log("Harvested carrots!");
    }
}