using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
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
            switch (other.GetComponentInParent<GateWithOutAmount>().type)
            {
                case GateWithOutAmountType.TripleShot:
                    EventManager.PlayerHitTripleShotGate();
                    Destroy(other.GetComponentInParent<GateWithOutAmount>().gameObject);
                    break;
                case GateWithOutAmountType.BulletSizeUp:
                    EventManager.PlayerHitBulletSizeUpGate();
                    Destroy(other.GetComponentInParent<GateWithOutAmount>().gameObject);
                    break;
            }
        }

        if (other.GetComponentInParent<Obstacle>())
        {
            EventManager.ChangeGameState(GameStates.Fail);
        }
        if (other.GetComponentInParent<Missile>())
        {
            EventManager.ChangeGameState(GameStates.Fail);
        }
        if (other.GetComponent<Gold>())
        {
            EventManager.GoldCollected(other.GetComponent<Gold>().goldAmount);
            Destroy(other.gameObject);
        }
    }
}
