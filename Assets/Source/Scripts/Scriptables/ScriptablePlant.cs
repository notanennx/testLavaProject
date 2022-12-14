using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "ScriptablePlant", menuName = "testLavaProject/ScriptablePlant", order = 0)]
public class ScriptablePlant : ScriptableObject
{
    // Vars
    [BoxGroup("Main")] public int ExpReward;
    [BoxGroup("Main")] public float GrowthTime;
    [BoxGroup("Main")] public Sprite SpriteIcon;
    [BoxGroup("Main")] public GameObject PlantPrefab;

    [BoxGroup("Details")] public float HarvestDistance;
}
