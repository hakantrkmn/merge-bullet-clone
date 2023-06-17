using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletInfo info;

    public bool onGun;
    private void OnEnable()
    {
        EventManager.ReleaseBullets += ReleaseBullets;
    }

    private void OnDisable()
    {
        EventManager.ReleaseBullets -= ReleaseBullets;
    }

    private void ReleaseBullets(float zPoint, float time)
    {
        transform.DOMoveZ(zPoint, time);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Gun>() && !onGun)
        {
            onGun = true;
            other.GetComponentInParent<Gun>().bullet = info;
            EventManager.BulletHitGun(other.GetComponentInParent<Gun>());
            Destroy(gameObject);
        }
        if (other.GetComponentInParent<GateController>() )
        {
            other.GetComponentInParent<GateController>().IncreaseAmount();
            GetComponentInParent<Gun>().ReleaseBullet(this.gameObject);
        }
    }
}
