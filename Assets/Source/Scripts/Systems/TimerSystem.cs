using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Supyrb;
using DG.Tweening;
using NaughtyAttributes;

public class TimerSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private GameObject timerLabelPrefab;
    [SerializeField, BoxGroup("Main")] private RectTransform timersHolderTransform;

    // Events
    private OnPlantGrown onPlantGrown;
    private OnPlantCreated onPlantCreated;

    // Hidden
    private float elapsedTime;
    private Camera mainCamera;
    private Dictionary<PlantComponent, TimerLabelUI> plantTimerDictionary = new Dictionary<PlantComponent, TimerLabelUI>();

    // Awaking
    private void Awake()
    {
        // Get
        mainCamera = Camera.main;

        // Events
        onPlantGrown = Signals.Get<OnPlantGrown>();
        onPlantCreated = Signals.Get<OnPlantCreated>();

        // Subscribe
        onPlantGrown.AddListener(OnPlantGrown);
        onPlantCreated.AddListener(OnPlantCreated);
    }

    // Destroy
    private void OnDestroy()
    {
        // Unsubscribe
        onPlantGrown.RemoveListener(OnPlantGrown);
        onPlantCreated.RemoveListener(OnPlantCreated);
    }

    // Updating
    private void Update()
    {
        // Count
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 1f)
        {
            // Null
            elapsedTime = 0f;

            // Add growth
            UpdateTimers();
        }

        // Updates timers
        UpdateTimerPositions();
    }

    // On plant grown
    private void OnPlantGrown(PlantComponent inputPlant)
    {
        // Get
        TimerLabelUI plantTimer = plantTimerDictionary[inputPlant];

        // Remove
        plantTimerDictionary.Remove(inputPlant);

        // Destroy
        plantTimer.transform.DOKill();
        plantTimer.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => {
            Destroy(plantTimer.gameObject);
        });
    }

    // On plant created
    private void OnPlantCreated(PlantComponent inputPlant)
    {
        // Create
        TimerLabelUI newTimer = Instantiate(timerLabelPrefab, timersHolderTransform).GetComponent<TimerLabelUI>();
            newTimer.GetFiller().fillAmount = 0;

        // Assign
        plantTimerDictionary.Add(inputPlant, newTimer);
    }

    // Updates our timers
    private void UpdateTimers()
    {
        // Loop and add growth
        foreach (KeyValuePair<PlantComponent, TimerLabelUI> plantTimer in plantTimerDictionary.ToList())
            plantTimer.Key.AddGrowthTime();
    }

    // Updates timer positions
    private void UpdateTimerPositions()
    {
        // Loop
        foreach (KeyValuePair<PlantComponent, TimerLabelUI> plantTimer in plantTimerDictionary)
        {
            // Get
            Vector2 targetPosition = mainCamera.WorldToScreenPoint(plantTimer.Key.GetTimerPoint().position);

            // Move
            plantTimer.Value.transform.position = Vector2.Lerp(plantTimer.Value.transform.position, targetPosition, (64f * Time.deltaTime));
            plantTimer.Value.GetFiller().fillAmount = Mathf.Lerp(plantTimer.Value.GetFiller().fillAmount, plantTimer.Key.GetFillAmount(), (4f * Time.deltaTime));
        }
    }
}
