using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public ButtonTypes type;

    public void ButtonClicked()
    {
        switch (type)
        {
            case ButtonTypes.BuyBullet:
                EventManager.BuyBulletButtonClicked();
                break;
            case ButtonTypes.Shot:
                EventManager.ShotButtonClicked();
                break;
        }
    }
}
