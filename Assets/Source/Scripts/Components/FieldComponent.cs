using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;

public class FieldComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private int width = 4;
    [SerializeField, BoxGroup("Main")] private int height = 4;
    [SerializeField, BoxGroup("Main")] private float tileSize = 1f;
    [SerializeField, BoxGroup("Main")] private GameObject tilePrefab;

    [SerializeField, BoxGroup("Debug")] private bool isDebugging = true;

    // Getters
    public GameObject GetTilePrefab() => tilePrefab;

    // Returns all tile positions
    public List<Vector3> GetTilePositions()
    {
        // Get
        List<Vector3> tileList = new List<Vector3>();

        // Loop
        for (int iw = 0; iw < width; iw++)
        {
            for (int ih = 0; ih < height; ih++)
            {
                float widthAdd = (iw * tileSize);// + (0.5f * (width * tileSize));
                float heightAdd = (ih * tileSize);// + (0.5f * (height * tileSize));
                Vector3 newPos = transform.position + new Vector3(widthAdd, 0, heightAdd);

                tileList.Add(newPos);
            }
        }

        // Return
        return tileList;
    }

    // Debugging
    private void OnDrawGizmos()
    {
        // Exit
        if (!isDebugging) return;

        // Loop
        foreach (Vector3 tilePosition in GetTilePositions())
            Handles.Label(tilePosition, "Tile");
    }
}
