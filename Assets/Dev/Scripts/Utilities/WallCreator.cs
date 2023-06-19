using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    public List<Wall> walls;
    public Vector2 gridSize;
    public GameObject wallPrefab;
    public float padding;
    public Transform wallArea;
    [Button]
    public void CreateMergeArea()
    {
        ClearCreatedWall();
        
        for (int i = 0; i < gridSize.y; i++)
        {
            for (int j = 0; j < gridSize.x; j++)
            {
                var wall = Instantiate(wallPrefab.gameObject,wallArea,true).GetComponent<Wall>();
                var gridBound = wall.Bound+padding;
                var xStart = ((gridSize.x * gridBound) - (gridBound)) / 2;
                var yStart = ((gridSize.y * gridBound) - (gridBound)) / 2;
                wall.transform.position = new Vector3((-xStart+(gridBound*j)), 0, -yStart+(gridBound*i)) + wallArea.position;
                walls.Add(wall);

                
            }
        }
    }
    
    void ClearCreatedWall()
    {
        foreach (var wall in walls)
        {
            if (Application.isEditor)
                DestroyImmediate(wall.gameObject);
            else
                Destroy(wall.gameObject);
        }
        walls.Clear();
    }
}
