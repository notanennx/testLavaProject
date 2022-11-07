using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supyrb;
using DG.Tweening;

[Serializable]
public class HarvestableGrass : HarvestableBase
{
    // Harvest
    public override void OnHarvestComplete(PlayerComponent inputPlayer, PlantComponent inputPlant)
    {
        // Exit
        if (!inputPlant.IsHarvestable()) return;

        // Play
        inputPlayer.GetAnimator().SetTrigger("Stomp");

        // Animate
        Signals.Get<OnAnimationComplete>().AddListener(delegate{Harvest(inputPlant);});
    }

    private void Harvest(PlantComponent inputPlant)
    {
        // Set
        inputPlant.Remove();
        inputPlant.SetHarvestable(false);

        // Unsubscribe
        Signals.Get<OnAnimationComplete>().Clear();
    }
}
