using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class BaseAmmo : MonoBehaviour
{
    public AmmoInfo info;
    private Tween _movement;

    public virtual void OnEnable()
    {
        EventManager.ReleaseBullets += ReleaseBullets;
    }

    public virtual void OnDisable()
    {
        EventManager.ReleaseBullets -= ReleaseBullets;
    }


    protected virtual void ReleaseBullets(float zPoint, float time)
    {
        transform.DOMoveZ(zPoint, time);
    }

    protected virtual void AmmoHitGun(Gun gun)
    {
        info.gun = gun;
        gun.bullet = info;
        EventManager.BulletHitGun(gun);
        
        Destroy(gameObject);
    }

    protected virtual void AmmoHitGate(BaseGate gate)
    {
        _movement.Kill();
        gate.IncreaseAmount();
        info.gun.ReleaseBullet(this);

    }
    
    protected virtual void AmmoHitLevelEndBox(LevelEndBox box)
    {
        box.TakeDamage(info.damage);
        _movement.Kill();
        info.gun.ReleaseBullet(this);

    }
    
    public virtual void AmmoFired(Vector3 startPos,float range,float angle,Vector3 scale)
    {
        transform.position = startPos;
        var destination = Quaternion.Euler(0,angle,0) *(transform.position + (Vector3.forward * range));
        transform.localScale = scale;
        _movement = transform.DOMove(destination, 10).SetSpeedBased().OnComplete(() =>
        {
            info.gun.ReleaseBullet(this);
        });
    }
    
    protected virtual void AmmoHitObstacle(Obstacle obstacle)
    {
        _movement.Kill();
        info.gun.ReleaseBullet(this);

    }
    protected virtual void AmmoHitMissile()
    {
        _movement.Kill();
        info.gun.ReleaseBullet(this);

    }
    
    protected virtual void AmmoHitWall(Wall wall)
    {
        info.health -= wall.damage;
        Destroy(wall.gameObject);

        if (info.health > 0) return;
        _movement.Kill();
        Destroy(gameObject);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Gun>() && info.gun==null)
        {
            AmmoHitGun(other.GetComponentInParent<Gun>());
            return;
        }

        if (other.GetComponentInParent<BaseGate>())
        {
            AmmoHitGate(other.GetComponentInParent<BaseGate>());
            return;
        }
        if (other.GetComponentInParent<Wall>())
        {
            AmmoHitWall(other.GetComponentInParent<Wall>());
            return;
        }
        if (other.GetComponentInParent<LevelEndBox>())
        {
            AmmoHitLevelEndBox(other.GetComponentInParent<LevelEndBox>());
            return;
        }
        if (other.GetComponentInParent<Obstacle>())
        {
            AmmoHitObstacle(other.GetComponentInParent<Obstacle>());
            return;
        }
        if (other.GetComponentInParent<Missile>())
        {
            AmmoHitMissile();
            return;
        }
    }
}