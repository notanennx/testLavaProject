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

        // Play
        inputPlayer.GetAnimator().SetTrigger("Stomp");

        // Animate
        Signals.Get<OnAnimationComplete>().AddListener(delegate{Harvest(inputPlayer, inputPlant);});
    }

    private void Harvest(PlayerComponent inputPlayer, PlantComponent inputPlant)
    {
        // Set
        inputPlant.Remove();
        inputPlant.SetHarvestable(false);

        // Grab
        PartComponent[] partComponents = inputPlant.GetComponentsInChildren<PartComponent>();
        foreach (PartComponent part in partComponents)
        {
            // Set
            part.transform.parent = null;

            // Get
            Vector3 newAngle = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            Vector3 newPosition = part.transform.localPosition + new Vector3(Random.Range(-1f, 1f), 2f, Random.Range(-1f, 1f));

            // Move
            part.transform.DOLocalRotate(newAngle, 0.6f, RotateMode.LocalAxisAdd);
            part.transform.DOLocalMove(newPosition, Random.Range(0.6f, 0.8f)).OnComplete(() => {
                // Set
                part.transform.parent = inputPlayer.transform;

                // Move
                part.transform.DOLocalRotate(newAngle, 0.3f, RotateMode.LocalAxisAdd);
                part.transform.DOLocalJump(Vector3.zero,  Random.Range(0.4f, 0.8f), 1, 0.3f, false).OnComplete(() => {
                    part.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() => {

                        Signals.Get<CreateCarrotsEvent>().Dispatch(1);
                    });
                });
            });
        }

        // Unsubscribe
        Signals.Get<OnAnimationComplete>().Clear();
    }
}