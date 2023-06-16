using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ScriptableManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] PlayerMovementSettings PlayerMovementSettings;
    [SerializeField] BulletData bulletData;


    //-------------------------------------------------------------------
    void Awake()
    {
        SaveManager.LoadGameData(gameData);

        Scriptable.BulletData = GetBulletData;
        Scriptable.GameData = GetGameData;
        Scriptable.PlayerSettings = GetPlayerMovementSettings;
    }


    //-------------------------------------------------------------------
    GameData GetGameData() => gameData;
    BulletData GetBulletData() => bulletData;


    //-------------------------------------------------------------------
    PlayerMovementSettings GetPlayerMovementSettings() => PlayerMovementSettings;

}



public static class Scriptable
{
    public static Func<BulletData> BulletData;
    public static Func<GameData> GameData;
    public static Func<PlayerMovementSettings> PlayerSettings;
}