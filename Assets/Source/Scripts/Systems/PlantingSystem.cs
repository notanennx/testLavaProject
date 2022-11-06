using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Supyrb;
using Cinemachine;
using NaughtyAttributes;

public class PlantingSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private ScriptablePlant[] allowedPlants;
    [SerializeField, BoxGroup("Main")] private CinemachineVirtualCamera virtualCamera;

    [SerializeField, BoxGroup("User Interface")] private GameObject plantingItemPrefab;
    [SerializeField, BoxGroup("User Interface")] private RectTransform plantingMenuContent;
    [SerializeField, BoxGroup("User Interface")] private RectTransform plantingMenuTransform;

    // Events
    private PlantEvent plantEvent;
    private PlantMenuEvent plantMenuEvent;

    // Awaking
    private void Awake()
    {
        // Get
        plantEvent = Signals.Get<PlantEvent>();
        plantMenuEvent = Signals.Get<PlantMenuEvent>();

        // Subscribe
        plantEvent.AddListener(OnItemClicked);
        plantMenuEvent.AddListener(OnTileClicked);
    }

    // Destroy
    private void OnDestroy()
    {
        plantEvent.RemoveListener(OnItemClicked);
        plantMenuEvent.RemoveListener(OnTileClicked);
    }

    // Handles clicks on tile
    private void OnTileClicked(PlanterComponent inputPlanter)
    {
        // Set
        virtualCamera.m_Follow = inputPlanter.transform;
        virtualCamera.m_Priority = 100;

        // Menu
        ShowPlantingMenu();
    }

    // Handles clicks on plant
    private void OnItemClicked(PlantingItemUI inputPlantItem)
    {
        // Set
        virtualCamera.m_Priority = 0;

        // Menu
        HidePlantingMenu();
    }

    // Opens a planting menu
    private void ShowPlantingMenu()
    {
        // Set
        GameSystem.IsMenuOpen = true;
        plantingMenuTransform.gameObject.SetActive(true);

        // Fillup
        FillupContents();
    }

    // Hides a planting menu
    private void HidePlantingMenu()
    {
        // Set
        GameSystem.IsMenuOpen = false;
        plantingMenuTransform.gameObject.SetActive(false);
    }

    // Cleans planting items contents
    private void CleanupContents()
    {
        PlantingItemUI[] plantingItems = plantingMenuContent.GetComponentsInChildren<PlantingItemUI>();
        foreach (PlantingItemUI plantingItemUI in plantingItems)
            Destroy(plantingItemUI.gameObject);
    }

    // Fills up contents
    private void FillupContents()
    {
        // Clean
        CleanupContents();

        // Loop and create
        foreach (ScriptablePlant itemPlant in allowedPlants)
        {
            // Create
            PlantingItemUI newItem = Instantiate(plantingItemPrefab, plantingMenuContent).GetComponent<PlantingItemUI>();
                newItem.SetPlant(itemPlant);
        }
    }
}
