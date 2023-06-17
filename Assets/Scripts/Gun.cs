using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class Gun : MonoBehaviour
{
    public BulletInfo bullet;
    public Transform bulletPoint;
    private Stack<Bullet> bulletPool = new Stack<Bullet>();


    public bool isTriple;
    public Vector3 bulletScale;
    public float bulletRange;

    private Vector3 bulletLastPos;
    private void OnEnable()
    {
        EventManager.PlayerHitRangeGate += amount => bulletRange += amount;
        EventManager.PlayerHitBulletSizeUpGate += () => bulletScale *= 2;
        EventManager.PlayerHitTripleShotGate += () => isTriple = true;
        EventManager.Fire += Fire;
        EventManager.ShooterPhase += ShooterPhase;
    }

    private void OnDisable()
    {
        EventManager.PlayerHitRangeGate -= amount => bulletRange += amount;
        EventManager.PlayerHitBulletSizeUpGate -= () => bulletScale *= 2;
        EventManager.PlayerHitTripleShotGate -= () => isTriple = true;
        EventManager.Fire += Fire;
        EventManager.ShooterPhase -= ShooterPhase;
    }

    private void ShooterPhase()
    {
        SetPool();
    }
    void SetPool()
    {
        FillStack(100);
    }

    public void Fire()
    {
        if (isTriple)
        {
            var stackBullet = GetBullet().transform;
            
            stackBullet.position = bulletPoint.position;
            bulletLastPos = Quaternion.Euler(0,0,0) *(transform.position + Vector3.forward * bulletRange);
            var tempBullet = stackBullet;
            stackBullet.DOMove(bulletLastPos, 3f).OnComplete(() => { ReleaseBullet(tempBullet.gameObject); });
            
             stackBullet = GetBullet().transform;
            stackBullet.position = bulletPoint.position;
            bulletLastPos = Quaternion.Euler(0,30,0) *(transform.position + Vector3.forward * bulletRange);
             var tempBullet2 = stackBullet;
             stackBullet.transform.DOMove(bulletLastPos, 3f).OnComplete(() => { ReleaseBullet(tempBullet2.gameObject); }); 
             stackBullet = GetBullet().transform;
            stackBullet.position = bulletPoint.position;
            bulletLastPos = Quaternion.Euler(0,-30,0) *(transform.position + Vector3.forward * bulletRange);
            stackBullet.DOMove(bulletLastPos, 3f).OnComplete(() => { ReleaseBullet(stackBullet.gameObject); });
           
        }
        else
        {
            var stackBullet = GetBullet();
            stackBullet.transform.position = bulletPoint.position;
            stackBullet.transform.SetParent(bulletPoint);
            var lastPoint = Quaternion.Euler(0,0,0) *(transform.position + Vector3.forward * bulletRange);
            stackBullet.transform.DOMove(lastPoint, 3f).OnComplete(() => { ReleaseBullet(stackBullet.gameObject); });
        }
        
    }

    public void FillStack(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(bullet.prefab, bulletPoint, true);
            obj.GetComponent<Bullet>().onGun = true;
            ReleaseBullet(obj);
        }
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            GameObject obje = bulletPool.Pop().gameObject;
            obje.gameObject.SetActive(true);

            return obje;
        }

        var temp = Instantiate(bullet.prefab);
        temp.GetComponent<Bullet>().onGun = true;
        return temp;
    }

    public void ReleaseBullet(GameObject obje)
    {
        obje.gameObject.SetActive(false);
        bulletPool.Push(obje.GetComponent<Bullet>());
    }
}