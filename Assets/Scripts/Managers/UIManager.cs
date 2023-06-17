using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    private int moneyAmount;
    public GameObject mergePanel;

    private void OnEnable()
    {
        EventManager.GoldCollected += GoldCollected;
        EventManager.ShotButtonClicked += () => mergePanel.SetActive(false);
        EventManager.BuyBulletButtonClicked+= BuyBulletButtonClicked;
        EventManager.BulletSold += UpdateMoney;
    }

    private void GoldCollected(int amount)
    {
        moneyAmount += amount;
        Scriptable.GameData().totalMoneyAmount=moneyAmount;
        SaveManager.SaveGameData(Scriptable.GameData());
        moneyText.text = moneyAmount.ToString();
    }

    private void OnDisable()
    {
        EventManager.GoldCollected -= GoldCollected;
        EventManager.ShotButtonClicked -= () => mergePanel.SetActive(false);
        EventManager.BulletSold -= UpdateMoney;
        EventManager.BuyBulletButtonClicked-= BuyBulletButtonClicked;
    }

    private void BuyBulletButtonClicked()
    {
        var priceList = Scriptable.GameData().bulletPrices;
        foreach (var price in priceList)
        {
            if (!price.isReached)
            {
                price.isReached = true;
                Scriptable.GameData().totalMoneyAmount -= price.price;
                SaveManager.SaveGameData(Scriptable.GameData());
                EventManager.BulletSold();
                return;
            }
        }
    }

    private void Start()
    {
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        moneyAmount = Scriptable.GameData().totalMoneyAmount;
        moneyText.text = moneyAmount.ToString();
    }
}
