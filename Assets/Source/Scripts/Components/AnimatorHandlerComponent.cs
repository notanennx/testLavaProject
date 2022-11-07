using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Supyrb;
using NaughtyAttributes;

public class AnimatorHandlerComponent : MonoBehaviour
{
    // Events
    private OnAnimationComplete onAnimationComplete;

    // Awaking
    private void Awake()
    {
        // Get
        onAnimationComplete = Signals.Get<OnAnimationComplete>();
    }

    // Stomp animation completed
    public void OnStompComplete()
    {
        // Dispatch
        onAnimationComplete.Dispatch();
    }

    // Swipe animation completed
    public void OnSwipeComplete()
    {
        // Dispatch
        onAnimationComplete.Dispatch();
    }
}
