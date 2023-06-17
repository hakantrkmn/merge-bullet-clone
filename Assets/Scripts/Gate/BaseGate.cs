using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BaseGate : MonoBehaviour
{
    public TextMeshProUGUI gateNameText;

    public virtual void IncreaseAmount() { }
}
