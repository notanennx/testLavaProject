using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class HarvestableTree : HarvestableBase
{
    // Harvest
    public override void OnHarvestComplete(PlayerComponent inputPlayer, PlantComponent inputPlant)
    {
        // Anim
        inputPlayer.GetAnimator().SetTrigger("Shrug");
        inputPlayer.transform.DOLookAt(inputPlant.transform.position, 0.2f, AxisConstraint.Y);
    }
}
