using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
public class HarvestableTree : HarvestableBase
{
    // Harvest
    public override void OnHarvestComplete(PlayerComponent inputPlayer, PlantComponent inputPlant)
    {
        // Anim
        inputPlayer.GetAnimator().SetTrigger("Shrug");
    }
}
