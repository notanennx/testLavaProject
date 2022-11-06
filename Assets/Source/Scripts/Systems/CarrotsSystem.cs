using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Supyrb;
using TMPro;
using DG.Tweening;
using Random = UnityEngine.Random;
using NaughtyAttributes;

public class CarrotsSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Label")] private string labelFormat;
    [SerializeField, BoxGroup("Label")] private TMP_Text labelText;
    [SerializeField, BoxGroup("Label")] private RectTransform labelTransform;

    [SerializeField, BoxGroup("Carrots")] private GameObject carrotPrefab;
    [SerializeField, BoxGroup("Carrots")] private RectTransform carrotHolderTransform;

    // Events
    private CreateCarrotsEvent createCarrotsEvent;

    // Hidden
    private int amount;
    private int tweenAmount;
    private Camera mainCamera;

    // Awaking
    private void Awake()
    {
        // Get
        mainCamera = Camera.main;

        // Events
        createCarrotsEvent = Signals.Get<CreateCarrotsEvent>();

        // Subscribe
        createCarrotsEvent.AddListener(CreateCarrots);
    }

    // Destroy
    private void OnDestroy()
    {
        // Unsubscribe
        createCarrotsEvent.RemoveListener(CreateCarrots);
    }

    // Creates carrots
    private void CreateCarrots(int inputAmount, Vector3 inputPosition)
    {
        // Spawn
        AddCarrots(Random.Range(6, 9), inputPosition);
    }

    // Adds carrots!
    private void AddCarrots(int inputAmount, Vector3 inputPosition)
    {
        // Get
        Vector3 carrotPosition = mainCamera.WorldToScreenPoint(inputPosition);

        // Set
        amount += inputAmount;

        // Create multiple 2D carrots
        int carrotsLeft = Mathf.Min(inputAmount, 16);
        int carrotsRequired = carrotsLeft;
        for (int i = 0; i < carrotsRequired; i++)
        {
            // Create
            Transform newCarrotTransform = Instantiate(carrotPrefab, carrotHolderTransform).transform;
                newCarrotTransform.position = carrotPosition + new Vector3(Random.Range(-64, 64), Random.Range(-64, 64), 0);
                newCarrotTransform.localScale = Vector3.zero;
                newCarrotTransform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));

            // Tweening
            newCarrotTransform.SetParent(labelTransform, true);
            newCarrotTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).SetDelay(0.05f * i).OnComplete(() => {
                //newCarrotTransform.DOLocalRotate(new Vector3(0, 0, Random.Range(0, 360)), 0.6f, RotateMode.LocalAxisAdd);
                newCarrotTransform.DOLocalMove(Vector3.zero, 0.4f).SetDelay(Random.Range(0.1f, 0.15f)).OnComplete(() => {
                    newCarrotTransform.DOScale(Vector3.zero, 0.2f).OnComplete(() => {

                        Destroy(newCarrotTransform.gameObject);

                        carrotsLeft -= 1;
                        if (carrotsLeft == 0)
                            UpdateLabel();
                    });
                });
            });
        }
    }

    // Updates carrots label
    private void UpdateLabel()
    {
        // Tween
        DOTween.To(() => tweenAmount, x => tweenAmount = x, amount, 0.4f).OnUpdate(() => {
            labelText.text = string.Format(labelFormat, tweenAmount);
        });

        // Effects
        labelTransform.DORewind();
        labelTransform.DOKill();
        labelTransform.DOPunchScale((0.1f * Vector3.one), 0.3f, 6, 1);
        labelTransform.DOShakePosition(0.6f, 16f, 10, 40, false, true);
    }

    /*

    // Rewards exp when plant is grown
    private void OnPlantGrown(PlantComponent inputPlant)
    {
        // Get
        int expRewardAmount = inputPlant.GetScriptablePlant().ExpReward;;
        Vector3 plantPosition = mainCamera.WorldToScreenPoint(inputPlant.GetTimerPoint().position);

        // Set
        expAmount += expRewardAmount;

        // Popup
        AddExperiencePopup(expRewardAmount, mainCamera.WorldToScreenPoint(inputPlant.GetTimerPoint().position));

        // Label
        AddExperienceToLabel();
    }
    */
}
