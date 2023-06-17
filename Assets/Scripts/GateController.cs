using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public GateType type;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI gateNameText;

    public int amount;

    private void OnValidate()
    {
        amountText.text = amount > 0 ? "+" + amount.ToString() : amount.ToString();

        switch (type)
        {
            case GateType.Range:
                gateNameText.text = "Range";
                break;
            case GateType.FireRate:
                gateNameText.text = "Fire Rate";
                break;
            case GateType.TripleShot:
                gateNameText.text = "Triple Shot";
                break;
            case GateType.BulletSizeUp:
                gateNameText.text = "Bullet Size Up";
                break;
        }
    }


    public void IncreaseAmount()
    {
        amount++;
        amountText.text = amount > 0 ? "+" + amount.ToString() : "-" + amount.ToString();
    }
}
