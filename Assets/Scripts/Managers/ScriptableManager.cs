using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ScriptableManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] BulletData bulletData;


    //-------------------------------------------------------------------
    void Awake()
    {
        SaveManager.LoadGameData(gameData);

        Scriptable.BulletData = GetBulletData;
        Scriptable.GameData = GetGameData;
    }


    //-------------------------------------------------------------------
    private GameData GetGameData() => gameData;
    private BulletData GetBulletData() => bulletData;


    //-------------------------------------------------------------------

}



public static class Scriptable
{
    public static Func<BulletData> BulletData;
    public static Func<GameData> GameData;
}