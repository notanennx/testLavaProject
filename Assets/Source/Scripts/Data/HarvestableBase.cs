using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
public class HarvestableBase
{
    // Harvest
    public virtual void OnHarvestComplete(PlayerComponent inputPlayer, PlantComponent inputPlant)
    {

    }
}
