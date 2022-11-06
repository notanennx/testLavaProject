using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
public class HarvestableTree : HarvestableBase
{
    // Harvest
    public override void OnHarvestComplete()
    {
        Debug.Log("Can't harvest the tree!");
    }
}
