using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
public class HarvestableCarrot : HarvestableBase
{
    // Harvest
    public override void OnHarvestComplete()
    {
        Debug.Log("Harvested carrots!");
    }
}