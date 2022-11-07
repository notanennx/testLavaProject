using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Supyrb;
using TMPro;
using DG.Tweening;
using NaughtyAttributes;

public class ExperienceSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Label")] private string expLabelFormat;
    [SerializeField, BoxGroup("Label")] private TMP_Text expLabelText;
    [SerializeField, BoxGroup("Label")] private RectTransform expLabel;

    [SerializeField, BoxGroup("Popups")] private GameObject expPopupPrefab;
    [SerializeField, BoxGroup("Popups")] private RectTransform popupsHolderTransform;

    // Hidden
    private int expAmount;
    private int tweenExpAmount;
    private Camera mainCamera;

    // Events
    private OnPlantGrown onPlantGrown;

    // Awaking
    private void Awake()
    {
        // Get
        mainCamera = Camera.main;

        // Events
        onPlantGrown = Signals.Get<OnPlantGrown>();

        // Subscribe
        onPlantGrown.AddListener(OnPlantGrown);
    }

    // Destroy
    private void OnDestroy()
    {
        // Unsubscribe
        onPlantGrown.RemoveListener(OnPlantGrown);
    }

    // Adds experience to label
    private void AddExperienceToLabel()
    {
        // Tween
        DOTween.To(() => tweenExpAmount, x => tweenExpAmount = x, expAmount, 0.4f).OnUpdate(() => {
            expLabelText.text = string.Format(expLabelFormat, tweenExpAmount);
        });

        // Punching
        expLabel.DORewind();
        expLabel.DOKill();
        expLabel.DOPunchScale((0.1f * Vector3.one), 0.3f, 6, 1);
    }

    // Adds an experience popup
    private void AddExperiencePopup(int inputAmount, Vector3 inputPosition)
    {
        // Create
        TMP_Text newExpPopup = Instantiate(expPopupPrefab, popupsHolderTransform).GetComponent<TMP_Text>();
            newExpPopup.text = "+"+inputAmount.ToString()+"EXP";
            newExpPopup.transform.position = inputPosition;

        // Tweening
        newExpPopup.transform.DOLocalMove(newExpPopup.transform.localPosition + new Vector3(0, 128, 0), 0.4f).OnComplete(() => {
            newExpPopup.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() => {
                Destroy(newExpPopup.gameObject);
            });
        });
    }

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
}
