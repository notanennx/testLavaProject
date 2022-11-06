using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Supyrb;
using NaughtyAttributes;

public class TimerLabelUI : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private Image fillerImage;

    // Getters
    public Image GetFiller() => fillerImage;
}
