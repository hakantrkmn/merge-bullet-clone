using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateWithOutAmount : BaseGate
{
    public  GateWithOutAmountType type;
    private void OnValidate()
    {

        switch (type)
        {
            case GateWithOutAmountType.TripleShot:
                gateNameText.text = "Triple Shot";
                break;
            case GateWithOutAmountType.BulletSizeUp:
                gateNameText.text = "Bullet Size Up";
                break;
        }
    }
}
