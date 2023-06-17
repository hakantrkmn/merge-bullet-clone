using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<Transform> gunPoints;

    public List<Gun> guns;


    public float fireRate;
    private float timer;
    private void OnEnable()
    {
        EventManager.PlayerHitFireRateGate += amount => fireRate += amount;
        EventManager.BulletHitGun += gun => guns.Add(gun);
        EventManager.ShooterPhase += ShooterPhase;
    }

    private void ShooterPhase()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].transform.SetParent(gunPoints[i]);
        }
        
        Sequence shooterPhase = DOTween.Sequence();
        foreach (var gun in guns)
        {
            shooterPhase.Join(gun.transform.DOLocalMove(Vector3.zero, .3f));
        }
        
        
    }

    private void OnDisable()
    {
        EventManager.PlayerHitFireRateGate -= amount => fireRate += amount;
        EventManager.ShooterPhase -= ShooterPhase;
        EventManager.BulletHitGun -= gun => guns.Add(gun);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (timer>(1/fireRate))
            {
                timer = 0;
                foreach (var gun in guns)
                {
                    gun.Fire();
                }
            }
        }
    }
}
