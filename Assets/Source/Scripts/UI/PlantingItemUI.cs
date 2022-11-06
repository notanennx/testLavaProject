using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Supyrb;
using Cinemachine;
using NaughtyAttributes;

public class PlantingItemUI : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private Image iconImage;
    [SerializeField, BoxGroup("Main")] private ScriptablePlant plantToUse;

    // Events
    private PlantEvent plantEvent;

    // Hidden
    private Button mainButton;

    // Getters
    public ScriptablePlant GetPlant() => plantToUse;

    // Awaking
    private void Awake()
    {
        // Get
        mainButton = GetComponent<Button>();
        plantEvent = Signals.Get<PlantEvent>();

        // Subscribe
        mainButton.onClick.AddListener(delegate{OnButtonClicked();});
    }

    // Destroy
    private void OnDestroy()
    {
        // Unsubscribe
        mainButton.onClick.RemoveAllListeners();
    }

    // Sets a plant to item
    public void SetPlant(ScriptablePlant inputPlant)
    {
        // Set
        plantToUse = inputPlant;

        // Icon
        iconImage.sprite = inputPlant.SpriteIcon;
    }

    // Button clicked
    private void OnButtonClicked()
    {
        // Dispatch
        plantEvent.Dispatch(this);
    }
}
