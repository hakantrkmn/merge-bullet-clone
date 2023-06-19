using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelEndCreator : MonoBehaviour
{
    public Vector2 gridSize;
    public GameObject levelEndPrefab;
    public float padding;
    public List<LevelEndBox> boxes;
    [Button]
    public void CreateLevelEnd()
    {
        ClearCreatedBoxes();
        
        for (int i = 0; i < gridSize.y; i++)
        {
            for (int j = 0; j < gridSize.x; j++)
            {
                var box = Instantiate(levelEndPrefab.gameObject,transform,true).GetComponent<LevelEndBox>();
                var gridBound = box.Bound+padding;
                var xStart = ((gridSize.x * gridBound) - (gridBound)) / 2;
                box.transform.localPosition = new Vector3((-xStart+(gridBound*j)), 0, (gridBound*i));
                box.health = (i + 1) * 100;
                boxes.Add(box);

                
            }
        }
    }

    private void ClearCreatedBoxes()
    {
        foreach (var box in boxes)
        {
            if (Application.isEditor)
                DestroyImmediate(box.gameObject);
            else
                Destroy(box.gameObject);
        }
        boxes.Clear();
    }

}
