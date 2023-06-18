using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public GameStates state;
    public List<Transform> gunPoints;
    List<Gun> _guns;


    public float fireRate;
    private float timer;


    private void Start()
    {
        _guns = new List<Gun>();
    }

    private void OnEnable()
    {
        EventManager.ChangeGameState += ChangeGameState;
        EventManager.PlayerHitFireRateGate += amount => fireRate += amount / 10;
        EventManager.BulletHitGun += BulletHitGun;
        EventManager.ShooterPhase += ShooterPhase;
    }

    private void BulletHitGun(Gun gun)
    {
        if (!_guns.Contains(gun))
        {
            _guns.Add(gun);
        }
        else
        {
            var tempGun = _guns.Find((x) => x.bullet.prefab == gun.bullet.prefab);
            if (tempGun.bullet.level < gun.bullet.level)
                tempGun.bullet = gun.bullet;
        }
    }

    private void ChangeGameState(GameStates obj)
    {
        state = obj;

        if (state == GameStates.Fail)
            EventManager.CanPlayerMove(false);
    }

    private void ShooterPhase()
    {
        state = GameStates.MergeToShooter;

        for (int i = 0; i < _guns.Count; i++)
        {
            _guns[i].transform.SetParent(gunPoints[i]);
        }

        Sequence shooterPhase = DOTween.Sequence();
        foreach (var gun in _guns)
            shooterPhase.Join(gun.transform.DOLocalMove(Vector3.zero, .3f));

        shooterPhase.AppendCallback((() => EventManager.ChangeGameState(GameStates.Shooter)));
    }

    private void OnDisable()
    {
        EventManager.ChangeGameState -= ChangeGameState;
        EventManager.PlayerHitFireRateGate -= amount => fireRate += amount / 10;
        EventManager.ShooterPhase -= ShooterPhase;
        EventManager.BulletHitGun -= BulletHitGun;
    }


    void Update()
    {
        if (state != GameStates.Shooter) return;

        if (Input.GetMouseButtonDown(0))
        {
            EventManager.CanPlayerMove(true);
        }

        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (timer > (1 / fireRate))
            {
                timer = 0;
                foreach (var gun in _guns)
                {
                    gun.Fire();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            EventManager.CanPlayerMove(false);
        }
    }
}