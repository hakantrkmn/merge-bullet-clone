using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MergeCellController : MonoBehaviour
{
    public Collider collider;
    public float Bound => collider.bounds.size.x;

    public Bullet cellBullet;

    public Transform bulletPosition;

    public bool haveBullet;

    private void OnEnable()
    {
        EventManager.BulletCarried += BulletCarried;
        EventManager.BulletMerged += BulletMerged;
    }
    
    private void OnDisable()
    {
        EventManager.BulletMerged -= BulletMerged;
        EventManager.BulletCarried -= BulletCarried;
    }
    
    private void BulletMerged(Bullet bullet)
    {
        if (bullet != cellBullet) return;
        
        Destroy(cellBullet.gameObject);
        haveBullet = false;
    }

    private void BulletCarried(Bullet bullet)
    {
        if (bullet == cellBullet)
            haveBullet = false;
    }

    public void SpawnBullet(BulletInfo bulletInfo)
    {
        var bullet = Instantiate(bulletInfo.prefab, transform, true);
        bullet.transform.position = bulletPosition.position;
        cellBullet = bullet.GetComponent<Bullet>();
        cellBullet.info = bulletInfo;
        haveBullet = true;
    }

    public void CarryBackBullet()
    {
        cellBullet.transform.DOMove(bulletPosition.position, .5f);
    }


    public bool MergeBullets(Bullet bullet)
    {
        if (haveBullet)
        {
            if (cellBullet.info.level != bullet.info.level) return false;
            
            var bulletInfo = Scriptable.BulletData().bulletInfos[bullet.info.level + 1];
            EventManager.BulletMerged(bullet);
            Destroy(cellBullet.gameObject);
            SpawnBullet(bulletInfo);
            
            return true;

        }

        EventManager.BulletCarried(bullet);
        CarryBulletTheCell(bullet.transform);
        return true;
    }

    void CarryBulletTheCell(Transform bullet)
    {
        bullet.SetParent(transform);
        haveBullet = true;
        bullet.position = bulletPosition.position;
        cellBullet = bullet.GetComponent<Bullet>();
    }
}