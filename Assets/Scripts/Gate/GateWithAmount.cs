using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateWithAmount : BaseGate
{
    public TextMeshProUGUI amountText;
    public int amount;
    public GateWithAmountType type;
    private void OnValidate()
    {
        amountText.text = amount.ToString();

        switch (type)
        {
            case GateWithAmountType.Range:
                gateNameText.text = "Range";
                break;
            case GateWithAmountType.FireRate:
                gateNameText.text = "Fire Rate";
                break;
        }
    }


    public override void IncreaseAmount()
    {
        amount++;
        amountText.text = amount > 0 ? "+" + amount.ToString() : "-" + amount.ToString();
    }
}
