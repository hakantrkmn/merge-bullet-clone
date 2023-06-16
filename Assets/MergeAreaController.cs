using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeAreaController : MonoBehaviour
{
    public MergeAreaCreator mergeAreaCreator;
    public LayerMask cellLayer;
    public LayerMask groundLayer;
    public MergeAreaStates state;

    Transform _carryBullet;
    MergeCellController _carryCell;

    void CreateBullet()
    {
        var bullet = Scriptable.BulletData().bulletInfos[0];
        var emptyCell = mergeAreaCreator.GetEmptyCell();
        emptyCell.SpawnBullet(bullet);
    }

    void ChooseBulletToDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, cellLayer))
            {
                if (hit.transform.GetComponent<MergeCellController>().haveBullet)
                {
                    _carryBullet = hit.transform.GetComponent<MergeCellController>().cellBullet.transform;
                    _carryCell = hit.transform.GetComponent<MergeCellController>();
                    state = MergeAreaStates.BulletOnDrag;
                }
            }
        }
    }

    void DragBullet()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayer))
            {
                _carryBullet.transform.position = hit.point + Vector3.up;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            PlaceBullet();
            state = MergeAreaStates.ChoosingBullet;

        }
        
    }

    void PlaceBullet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, cellLayer))
            {
                if (hit.transform.GetComponent<MergeCellController>() != _carryCell)
                {
                    if (hit.transform.GetComponent<MergeCellController>()
                        .MergeBullets(_carryBullet.GetComponent<Bullet>())) return;

                    _carryCell.CarryBackBullet();
                    _carryCell = null;
                    _carryBullet = null;
                }
                else
                {
                    _carryCell.CarryBackBullet();
                    _carryCell = null;
                    _carryBullet = null;
                }
            }
            else
            {
                _carryCell.CarryBackBullet();
                _carryCell = null;
                _carryBullet = null;
            }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateBullet();
        }

        switch (state)
        {
           case MergeAreaStates.ChoosingBullet:
               ChooseBulletToDrag();
               break;
           case MergeAreaStates.BulletOnDrag:
               DragBullet();
               break;
           
        }

        

        
        
        
        
    }
}