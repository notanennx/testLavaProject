using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
public class HarvestableGrass : HarvestableBase
{
    // Harvest
    public override void OnHarvestComplete(PlayerComponent inputPlayer, PlantComponent inputPlant)
    {
        Debug.Log("Harvested grass!");
    }
}
