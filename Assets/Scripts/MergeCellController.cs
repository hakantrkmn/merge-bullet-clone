using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MergeCellController : MonoBehaviour
{
    public Collider collider;
    public float Bound => collider.bounds.size.x;

    public BaseAmmo cellBullet;

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
    
    private void BulletMerged(BaseAmmo bullet)
    {
        if (bullet != cellBullet) return;
        
        Destroy(cellBullet.gameObject);
        haveBullet = false;
    }

    private void BulletCarried(BaseAmmo bullet)
    {
        if (bullet == cellBullet)
            haveBullet = false;
    }

    public void SpawnBullet(AmmoInfo bulletInfo,bool load)
    {
        var bullet = Instantiate(bulletInfo.prefab, transform, true);
        bullet.transform.position = bulletPosition.position;
        cellBullet = bullet.GetComponent<BaseAmmo>();
        cellBullet.info = bulletInfo;
        haveBullet = true;
        
        if (load) return;
        Scriptable.GameData().bullets.Add(bulletInfo);
        SaveManager.SaveGameData(Scriptable.GameData());

    }

    public void CarryBackBullet()
    {
        cellBullet.transform.DOMove(bulletPosition.position, .5f);
    }

    

    public bool MergeBullets(BaseAmmo bullet)
    {
        if (haveBullet)
        {
            if (cellBullet.info.level != bullet.info.level) return false;

            Scriptable.GameData().bullets.Remove(cellBullet.info);
            Scriptable.GameData().bullets.Remove(bullet.info);

            var bulletInfo = Scriptable.BulletData().bulletInfos[bullet.info.level + 1];

            EventManager.BulletMerged(bullet);
            Destroy(cellBullet.gameObject);
            SpawnBullet(bulletInfo,false);
            
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
        cellBullet = bullet.GetComponent<BaseAmmo>();
    }
}