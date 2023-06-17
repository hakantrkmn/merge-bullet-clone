using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GateController>())
        {
            switch (other.GetComponent<GateController>().type)
            {
                case GateType.Range:
                    Debug.Log("klsndfşnkdfg");
                    EventManager.PlayerHitRangeGate(other.GetComponent<GateController>().amount);
                    break;
                case GateType.FireRate:
                    EventManager.PlayerHitFireRateGate(other.GetComponent<GateController>().amount);
                    break;
                case GateType.TripleShot:
                    EventManager.PlayerHitTripleShotGate();
                    break;
                case GateType.BulletSizeUp:
                    EventManager.PlayerHitBulletSizeUpGate();
                    break;
                    
            }
        }
    }
}
