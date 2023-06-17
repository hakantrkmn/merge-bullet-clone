using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MergeAreaCreator : MonoBehaviour
{
    public Vector2 gridSize;
    public GameObject cellPrefab;
    public float padding;

    public List<MergeCellController> cells;
    public Transform cellParent;

    private void OnEnable()
    {
        EventManager.GetGunPoints += GetGunPoints;
    }

    private void OnDisable()
    {
        EventManager.GetGunPoints -= GetGunPoints;
    }

    private List<Transform> GetGunPoints()
    {
        var tempList = new List<Transform>();

        for (int i = 0; i < gridSize.x; i++)
            tempList.Add(cells[i].transform);

        return tempList;
    }

    [Button]
    public void CreateMergeArea()
    {
        ClearCreatedCells();

        for (int i = 0; i < gridSize.y; i++)
        {
            for (int j = 0; j < gridSize.x; j++)
            {
                var cell = Instantiate(cellPrefab.gameObject, cellParent, true).GetComponent<MergeCellController>();
                var gridBound = cell.Bound + padding;
                var xStart = ((gridSize.x * gridBound) - (gridBound)) / 2;
                var yStart = ((gridSize.y * gridBound) - (gridBound)) / 2;
                cell.transform.localPosition = new Vector3((-xStart + (gridBound * j)), 0, -yStart + (gridBound * i));
                cells.Add(cell);
            }
        }
    }

    public MergeCellController GetEmptyCell()
    {
        foreach (var cell in cells)
            if (cell.cellBullet == null)
                return cell;

        return null;
    }

    private void ClearCreatedCells()
    {
        foreach (var cell in cells)
        {
            if (Application.isEditor)
                DestroyImmediate(cell.gameObject);
            else
                Destroy(cell.gameObject);
        }

        cells.Clear();
    }
}