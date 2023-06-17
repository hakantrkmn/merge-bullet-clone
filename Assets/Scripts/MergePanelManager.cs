using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergePanelManager : MonoBehaviour
{
    private int _moneyAmount;
    public Button buyBulletButton;
    public TextMeshProUGUI priceText;

    private void Start()
    {
        CheckIfCanBuyBullet();
    }

    private void OnEnable()
    {
        EventManager.BulletSold += CheckIfCanBuyBullet;
    }

    private void OnDisable()
    {
        EventManager.BulletSold -= CheckIfCanBuyBullet;
    }

    private void CheckIfCanBuyBullet()
    {
        _moneyAmount = Scriptable.GameData().totalMoneyAmount;
        var priceList = Scriptable.GameData().bulletPrices;
        foreach (var price in priceList)
        {
            if (!price.isReached)
            {
                priceText.text = price.price.ToString();
                if (_moneyAmount>=price.price)
                {
                    buyBulletButton.interactable = true;
                }
                else
                {
                    buyBulletButton.interactable = false;

                }
                return;
            }
        }
        
    }
}
