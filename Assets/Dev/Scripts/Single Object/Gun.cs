using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AmmoInfo bullet;
    public Transform bulletPoint;
    private Stack<BaseAmmo> _bulletPool = new Stack<BaseAmmo>();


    bool _tripleShot;
    public Vector3 bulletScale;
    public float bulletRange;

    private Vector3 _bulletLastPos;

    private void OnEnable()
    {
        EventManager.PlayerHitRangeGate += amount => bulletRange += amount / 10;
        EventManager.PlayerHitBulletSizeUpGate += () => bulletScale *= 2;
        EventManager.PlayerHitTripleShotGate += () => _tripleShot = true;
        EventManager.Fire += Fire;
        EventManager.ShooterPhase += SetPool;
    }

    private void OnDisable()
    {
        EventManager.PlayerHitRangeGate -= amount => bulletRange += amount / 10;
        EventManager.PlayerHitBulletSizeUpGate -= () => bulletScale *= 2;
        EventManager.PlayerHitTripleShotGate -= () => _tripleShot = true;
        EventManager.Fire += Fire;
        EventManager.ShooterPhase -= SetPool;
    }

    private void SetPool()
    {
        FillStack(100);
    }

    public void Fire()
    {
        if (_tripleShot)
        {
            var stackBullet = GetBullet().GetComponent<BaseAmmo>();
            stackBullet.info = bullet;
            stackBullet.AmmoFired(bulletPoint.position, bulletRange, 0, bulletScale);

            var stackBullet1 = GetBullet().GetComponent<BaseAmmo>();
            stackBullet1.info = bullet;
            stackBullet1.AmmoFired(bulletPoint.position, bulletRange, 2, bulletScale);

            var stackBullet2 = GetBullet().GetComponent<BaseAmmo>();
            stackBullet2.info = bullet;
            stackBullet2.AmmoFired(bulletPoint.position, bulletRange, -2, bulletScale);
        }
        else
        {
            var stackBullet = GetBullet().GetComponent<BaseAmmo>();
            stackBullet.info = bullet;

            stackBullet.AmmoFired(bulletPoint.position, bulletRange, 0, bulletScale);
        }
    }

    #region Object Pool

    private void FillStack(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            BaseAmmo obj = Instantiate(bullet.prefab, bulletPoint, true).GetComponent<BaseAmmo>();
            ReleaseBullet(obj);
        }
    }

    private GameObject GetBullet()
    {
        if (_bulletPool.Count > 0)
        {
            GameObject obj = _bulletPool.Pop().gameObject;
            obj.gameObject.SetActive(true);

            return obj;
        }

        var temp = Instantiate(bullet.prefab, bulletPoint);
        return temp;
    }

    public void ReleaseBullet(BaseAmmo ammo)
    {
        _bulletPool.Push(ammo);
        ammo.gameObject.SetActive(false);
    }
    

    #endregion
   
}