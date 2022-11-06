using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldsSetupSystem : MonoBehaviour
{
    // Hidden
    private FieldComponent[] fieldComponents;

    // Awaking
    private void Awake()
    {
        // Get
        fieldComponents = GameObject.FindObjectsOfType<FieldComponent>();

        // Handle
        foreach (FieldComponent fieldComponent in fieldComponents)
        {
            // Get
            GameObject tilePrefab = fieldComponent.GetTilePrefab();
            List<Vector3> tilePositions = fieldComponent.GetTilePositions();

            // Loop
            foreach (Vector3 tilePosition in tilePositions)
            {
                // Create
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity).transform;
                    newTile.SetParent(fieldComponent.transform, true);
            }
        }
    }
}
