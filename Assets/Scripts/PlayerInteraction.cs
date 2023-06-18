using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool haveShield;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<BaseGate>() as GateWithAmount)
        {
            var temp = other.GetComponentInParent<BaseGate>() as GateWithAmount;
            switch (temp.type)
            {
                case GateWithAmountType.Range:
                    EventManager.PlayerHitRangeGate(temp.amount);
                    Destroy(other.GetComponentInParent<GateWithAmount>().gameObject);
                    break;
                case GateWithAmountType.FireRate:
                    EventManager.PlayerHitFireRateGate(temp.amount);
                    Destroy(other.GetComponentInParent<GateWithAmount>().gameObject);
                    break;
            }
        }

        if (other.GetComponentInParent<BaseGate>() as GateWithOutAmount)
        {
            var temp = other.GetComponentInParent<BaseGate>() as GateWithOutAmount;
            switch (temp.type)
            {
                case GateWithOutAmountType.TripleShot:
                    EventManager.PlayerHitTripleShotGate();
                    Destroy(temp.gameObject);
                    break;
                case GateWithOutAmountType.BulletSizeUp:
                    EventManager.PlayerHitBulletSizeUpGate();
                    Destroy(temp.gameObject);
                    break;
            }
        }

        if (!haveShield)
        {
            if (other.GetComponentInParent<Obstacle>())
            {
                EventManager.ChangeGameState(GameStates.Fail);
            }

            if (other.GetComponentInParent<Missile>())
            {
                EventManager.ChangeGameState(GameStates.Fail);
            }
        }

        if (other.GetComponent<Gold>())
        {
            EventManager.GoldCollected(other.GetComponent<Gold>().goldAmount);
            Destroy(other.gameObject);
        }

        if (other.GetComponentInParent<LevelEndBox>())
        {
            EventManager.ChangeGameState(GameStates.Win);
        }

        if (other.GetComponent<Shield>())
        {
            Destroy(other.gameObject);
            Sequence shield = DOTween.Sequence();
            shield.AppendCallback((() => haveShield = true));
            shield.AppendInterval(3);
            shield.AppendCallback((() => haveShield = false));
        }
    }
}