using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MergeAreaController : MonoBehaviour
{
    public MergeAreaCreator mergeAreaCreator;
    public LayerMask cellLayer;
    public LayerMask groundLayer;
    MergeAreaStates _state;
    public Transform gunsPosition;

    Transform _carryBullet;
    MergeCellController _carryCell;


    private void Start()
    {
        LoadAreaData();
    }

    private void LoadAreaData()
    {
        var loadData = Scriptable.GameData().bullets;
        foreach (var data in loadData)
            CreateBullet(data.level);
    }

    private void OnEnable()
    {
        EventManager.ShotButtonClicked += ReleaseBullets;
        EventManager.BuyBulletButtonClicked += CreateBullet;
    }

    private void OnDisable()
    {
        EventManager.ShotButtonClicked -= ReleaseBullets;
        EventManager.BuyBulletButtonClicked -= CreateBullet;
    }

    private void CreateBullet()
    {
        var bullet = Scriptable.BulletData().bulletInfos[0];
        var emptyCell = mergeAreaCreator.GetEmptyCell();
        emptyCell.SpawnBullet(bullet, false);
    }

    private void CreateBullet(int level)
    {
        var bullet = Scriptable.BulletData().bulletInfos[level];
        var emptyCell = mergeAreaCreator.GetEmptyCell();
        emptyCell.SpawnBullet(bullet, true);
    }

    private void ChooseBulletToDrag()
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
                    _state = MergeAreaStates.BulletOnDrag;
                }
            }
        }
    }

    private void DragBullet()
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
            _state = MergeAreaStates.ChoosingBullet;
        }
    }

    private void CarryBackBullet()
    {
        _carryCell.CarryBackBullet();
        _carryCell = null;
        _carryBullet = null;
    }

    private void PlaceBullet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, cellLayer))
        {
            if (hit.transform.GetComponent<MergeCellController>() != _carryCell)
            {
                if (hit.transform.GetComponent<MergeCellController>()
                    .MergeBullets(_carryBullet.GetComponent<BaseAmmo>())) return;

                CarryBackBullet();
            }
            else
            {
                CarryBackBullet();
            }
        }
        else
        {
            CarryBackBullet();
        }
    }

    void ReleaseBullets()
    {
        Sequence release = DOTween.Sequence();
        release.AppendCallback(() => EventManager.ReleaseBullets(gunsPosition.position.z, 2));
        release.AppendInterval(2.1f);
        release.AppendCallback(() => EventManager.ShooterPhase());
    }

    private void Update()
    {
        switch (_state)
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