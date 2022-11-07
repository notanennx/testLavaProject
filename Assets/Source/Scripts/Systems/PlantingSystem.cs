using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Supyrb;
using Cinemachine;
using NaughtyAttributes;
using DG.Tweening;

public class PlantingSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private ScriptablePlant[] allowedPlants;
    [SerializeField, BoxGroup("Main")] private CinemachineVirtualCamera virtualCamera;

    [SerializeField, BoxGroup("User Interface")] private GameObject plantingItemPrefab;
    [SerializeField, BoxGroup("User Interface")] private RectTransform plantingMenuContent;
    [SerializeField, BoxGroup("User Interface")] private RectTransform plantingMenuTransform;

    // Hidden
    private EventSystem eventSystem;
    private TileComponent tileComponent;
    private MoveComponent moveComponent;
    private PlantComponent plantComponent;
    private ScriptablePlant plantToPlace;

    // Events
    private PlantEvent plantEvent;
    private MoveEventBusy moveEventBusy;
    private PlantMenuEvent plantMenuEvent;
    private OnPlantCreated onPlantCreated;
    private OnPlantHarvest onPlantHarvest;
    private DestinationReachedEvent destinationReachedEvent;

    // Awaking
    private void Awake()
    {
        // Get
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
        moveComponent = GameObject.FindObjectOfType<MoveComponent>();

        // Events
        plantEvent = Signals.Get<PlantEvent>();
        moveEventBusy = Signals.Get<MoveEventBusy>();
        plantMenuEvent = Signals.Get<PlantMenuEvent>();
        onPlantCreated = Signals.Get<OnPlantCreated>();
        onPlantHarvest = Signals.Get<OnPlantHarvest>();
        destinationReachedEvent = Signals.Get<DestinationReachedEvent>();

        // Subscribe
        plantEvent.AddListener(OnItemClicked);
        plantMenuEvent.AddListener(OnTileClicked);
    }

    // Destroy
    private void OnDestroy()
    {
        // Unsubscribe
        plantEvent.RemoveListener(OnItemClicked);
        plantMenuEvent.RemoveListener(OnTileClicked);
        destinationReachedEvent.RemoveListener(PerformPlanting);
    }

    // Updating
    private void Update()
    {
        // Exit
        //if ((eventSystem.IsPointerOverGameObject(0)) || (eventSystem.IsPointerOverGameObject(-1))) return;

        // Exit
        //if ((Input.GetMouseButtonDown(0)) && (GameSystem.IsMenuOpen))
        //{
        //    GameSystem.IsMenuOpen = false;
        //    HidePlantingMenu();
        //}
    }

    // Handles clicks on tile
    private void OnTileClicked(TileComponent inputTile)
    {
        // Set
        tileComponent = inputTile;

        // Empty
        if (!tileComponent.IsOccupied())
        {
            // Menu
            ShowPlantingMenu();

            // Camera
            virtualCamera.m_Follow = tileComponent.transform;
            virtualCamera.m_Priority = 100;
        }
        // Non empty
        else
        {
            // Get
            PlantComponent tilePlant = tileComponent.GetPlant();

            // It's grown
            if (tilePlant.IsGrown())
            {
                // Set
                plantComponent = tilePlant;
                moveComponent.GetNavMeshAgent().stoppingDistance = plantComponent.GetScriptablePlant().HarvestDistance;

                // Move
                moveEventBusy.Dispatch(tileComponent.transform.position);
                destinationReachedEvent.AddListener(PerformHarvesting);
            }
        }
    }

    // Performs harvesting but not here
    private void PerformHarvesting()
    {
        onPlantHarvest.Dispatch(plantComponent);

        // Unsubscribe
        destinationReachedEvent.RemoveListener(PerformHarvesting);
    }

    // Handles clicks on plant
    private void OnItemClicked(PlantingItemUI inputPlantItem)
    {
        // Set
        plantToPlace = inputPlantItem.GetPlant();
        moveComponent.GetNavMeshAgent().stoppingDistance = 0;

        // Makes char go planting
        moveEventBusy.Dispatch(tileComponent.transform.position);
        destinationReachedEvent.AddListener(PerformPlanting);

        // Menu
        HidePlantingMenu();
    }

    // Performs planting
    private void PerformPlanting()
    {
        // Animate char, plant something


        // Create
        PlantComponent newPlant = Instantiate(plantToPlace.PlantPrefab, tileComponent.transform).GetComponent<PlantComponent>();
            newPlant.SetScriptablePlant(plantToPlace);

            // Cache
            Vector3 sproutScale = newPlant.GetSproutTransform().localScale;

            // Effect
            newPlant.GetSproutTransform().localScale = Vector3.zero;
            newPlant.GetSproutTransform().DOScale(sproutScale, 0.2f).SetEase(Ease.InBack).OnComplete(() => {
                newPlant.GetSproutTransform().DOPunchScale(0.1f * Vector3.one, 0.6f, 6, 1);
            });

            // Assign
            tileComponent.SetPlant(newPlant);

            // Dispatch
            onPlantCreated.Dispatch(newPlant);

        
        // Resetting
        plantToPlace = null;
        tileComponent = null;

        // Unsubscribe
        destinationReachedEvent.RemoveListener(PerformPlanting);
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

        // Camera
        virtualCamera.m_Priority = 0;
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
