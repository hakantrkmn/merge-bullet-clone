using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    private int _moneyAmount;
    public CanvasGroup mergePanel;
    public CanvasGroup winPanel;
    public CanvasGroup failPanel;

    private void OnEnable()
    {
        EventManager.ChangeGameState += ChangeGameState;
        EventManager.GoldCollected += GoldCollected;
        EventManager.ShotButtonClicked += ShotButtonClicked;
        EventManager.BuyBulletButtonClicked+= BuyBulletButtonClicked;
        EventManager.BulletSold += UpdateMoney;
    }

    private void ShotButtonClicked()
    {
        ChangePanelVisibility(false, mergePanel);
    }

    private void ChangeGameState(GameStates state)
    {
        switch (state)
        {
            case GameStates.Fail:
                ChangePanelVisibility(true, failPanel);
                break;
            case GameStates.Win:
                ChangePanelVisibility(true, winPanel);
                break;
        }
    }

    private void ChangePanelVisibility(bool show,CanvasGroup panel)
    {
        panel.interactable = show;
        panel.alpha = show ? 1 : 0;
        panel.blocksRaycasts = show;
    }

    private void GoldCollected(int amount)
    {
        Scriptable.GameData().totalMoneyAmount+=amount;
        SaveManager.SaveGameData(Scriptable.GameData());
        UpdateMoney();
    }

    private void OnDisable()
    {
        EventManager.ChangeGameState -= ChangeGameState;
        EventManager.GoldCollected -= GoldCollected;
        EventManager.ShotButtonClicked -= ShotButtonClicked;
        EventManager.BulletSold -= UpdateMoney;
        EventManager.BuyBulletButtonClicked-= BuyBulletButtonClicked;
    }

    private void BuyBulletButtonClicked()
    {
        var priceList = Scriptable.GameData().bulletPrices;
        foreach (var price in priceList.Where(price => !price.isReached))
        {
            price.isReached = true;
            Scriptable.GameData().totalMoneyAmount -= price.price;
            SaveManager.SaveGameData(Scriptable.GameData());
            EventManager.BulletSold();
            return;
        }
    }

    private void Start()
    {
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        DOVirtual.Int((int)_moneyAmount, (int)Scriptable.GameData().totalMoneyAmount,.2f, (x) =>
        {
            moneyText.text = AbbrevationUtility.AbbreviateNumber(x);

        });

        _moneyAmount = Scriptable.GameData().totalMoneyAmount;
    }
}
