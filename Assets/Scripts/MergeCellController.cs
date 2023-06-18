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

    public Vector2 _cellIndex;
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

    public void SpawnBullet(AmmoInfo bulletInfo, bool load)
    {
        var bullet = Instantiate(bulletInfo.prefab, transform, true);
        bullet.transform.position = bulletPosition.position;
        cellBullet = bullet.GetComponent<BaseAmmo>();
        bullet.GetComponent<BaseAmmo>().info = bulletInfo;
        haveBullet = true;
        var gridAmmoInfo = new GridAmmoInfo
        {
            index = _cellIndex,
            level = bulletInfo.level
        };

        if (load) return;
        Scriptable.GameData().bullets.Add(gridAmmoInfo);
        SaveManager.SaveGameData(Scriptable.GameData());
    }

    public void CarryBackBullet()
    {
        cellBullet.transform.DOMove(bulletPosition.position, .5f);
    }


    public bool MergeBullets(BaseAmmo bullet,Vector2 cellIndex)
    {
        if (haveBullet)
        {
            if (cellBullet.info.level != bullet.info.level) return false;

            if (Scriptable.BulletData().bulletInfos.Count <= cellBullet.info.level +1 ) return false;
            
            
            EventManager.RemoveBulletFromData(cellIndex);
            EventManager.RemoveBulletFromData(_cellIndex);

            var bulletInfo = Scriptable.BulletData().bulletInfos[bullet.info.level + 1];

            EventManager.BulletMerged(bullet);
            Destroy(cellBullet.gameObject);
            SpawnBullet(bulletInfo, false);

            return true;
        }

        EventManager.BulletCarried(bullet);
        CarryBulletTheCell(bullet.transform,cellIndex);
        return true;
    }

    private void CarryBulletTheCell(Transform bullet,Vector2 cellIndex)
    {
        EventManager.ChangeBulletFromData(_cellIndex,cellIndex);
        bullet.SetParent(transform);
        haveBullet = true;
        bullet.position = bulletPosition.position;
        cellBullet = bullet.GetComponent<BaseAmmo>();
    }
}