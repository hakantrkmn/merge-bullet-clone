using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergePanelManager : MonoBehaviour
{
    private int _moneyAmount;
    public Button buyBulletButton;
    public TextMeshProUGUI priceText;
    public Button shotButton;
    private void Start()
    {
        CheckIfCanBuyBullet();
        CheckIfCanShot();
    }

    private void OnEnable()
    {
        EventManager.BulletSold += CheckIfCanBuyBullet;
    }

    private void OnDisable()
    {
        EventManager.BulletSold -= CheckIfCanBuyBullet;
    }

    void CheckIfCanShot()
    {
        var bullets = Scriptable.GameData().bullets;
        shotButton.interactable = bullets.Count>0;
    }

    private void CheckIfCanBuyBullet()
    {
        shotButton.interactable = true;
        _moneyAmount = Scriptable.GameData().totalMoneyAmount;
        var priceList = Scriptable.GameData().bulletPrices;
        foreach (var price in priceList.Where(price => !price.isReached))
        {
            priceText.text = price.price.ToString();
            buyBulletButton.interactable = _moneyAmount>=price.price;
            return;
        }
        
    }
}
