using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/Game Data", order = 1)]
public class GameData : ScriptableObject
{

    [BoxGroup("Total Money")] public int totalMoneyAmount;
    
    [BoxGroup("Ammo Price")] public List<BulletPrice> bulletPrices;

    [BoxGroup("MergeGridData")] public List<GridAmmoInfo> bullets;

    public float highScoreZPos;

    //---------------------------------------------------------------------------------
    [Button]
    public void ResetData()
    {
        totalMoneyAmount = 0;

        PlayerPrefs.DeleteAll();
        foreach (var price in bulletPrices)
        {
            price.isReached = false;
        }
        bullets.Clear();
        SaveManager.SaveGameData(this);
    }

    [Button]
    private void SetData()
    {
        SaveManager.SaveGameData(this);
    }

    
}
