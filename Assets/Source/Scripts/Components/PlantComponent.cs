using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Supyrb;
using NaughtyAttributes;
using DG.Tweening;

public class PlantComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private Transform timerPoint;
    [SerializeField, BoxGroup("Main")] private Transform sproutTransform;
    [SerializeField, BoxGroup("Main")] private Transform grownupTransform;

    [SerializeReference, SubclassSelector, BoxGroup("Scripts")] private HarvestableBase harvestableBase;

    [SerializeField, BoxGroup("Effects")] private bool doSpineGrowth;

    // Hidden
    private bool isGrown;
    private bool isHarvestable;
    private float time;
    private Vector3 grownupScale;
    private ScriptablePlant scriptablePlant;

    // Events
    private OnPlantGrown onPlantGrown;

    // Getters
    public bool IsGrown() => isGrown;
    public bool IsHarvestable() => isHarvestable;
    public float GetFillAmount() => (time/scriptablePlant.GrowthTime);
    public Transform GetSproutTransform() => sproutTransform;
    public Transform GetGrownupTransform() => grownupTransform;

    public Transform GetTimerPoint() => timerPoint;
    public HarvestableBase GetHarvestScript() => harvestableBase;
    public ScriptablePlant GetScriptablePlant() => scriptablePlant;

    // Setters
    public void SetHarvestable(bool inputBool) => isHarvestable = inputBool;

    // Awaking
    private void Awake()
    {
        // Get
        grownupScale = grownupTransform.localScale;

        // Events
        onPlantGrown = Signals.Get<OnPlantGrown>();
    }

    // Adds growth time
    public void AddGrowthTime()
    {
        // Grow
        if (time >= scriptablePlant.GrowthTime)
            BecomeGrownup();

        // Addup
        time = Mathf.Min(time + 1, scriptablePlant.GrowthTime);
    }

    // Sets a scriptable plant
    public void SetScriptablePlant(ScriptablePlant inputPlant)
    {
        // Set
        time = 0;
        scriptablePlant = inputPlant;
    }

    // Removes itself
    public void Remove()
    {
        transform.DOKill();
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => {
            Destroy(gameObject);
        });
    }

    // Becomes a grown up plant
    public void BecomeGrownup()
    {
        // Exit
        if (isGrown) return;

        // Set
        isGrown = true;

        // Event
        onPlantGrown.Dispatch(this);

        // Effect
        sproutTransform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => {
            // Hide
            sproutTransform.gameObject.SetActive(false);

            // Show
            grownupTransform.localScale = Vector3.zero;
            grownupTransform.gameObject.SetActive(true);

            // Scale-up
            grownupTransform.DOScale(grownupScale, 0.8f).SetEase(Ease.OutBack);

            // Spine-growth
            if (doSpineGrowth)
                grownupTransform.DOLocalRotate(new Vector3(0, 360, 0), 0.8f, RotateMode.LocalAxisAdd);;
        });
    }
}
