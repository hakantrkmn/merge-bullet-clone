using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.ChangeBulletFromData+= ChangeBulletFromData;
        EventManager.RemoveBulletFromData += RemoveBulletFromData;
        EventManager.TryAgainButtonClicked += TryAgainButtonClicked;
        EventManager.NextLevelButtonClicked += NextLevelButtonClicked;
    }

    private void ChangeBulletFromData(Vector2 newIndex,Vector2 oldIndex)
    {
        var gridBulletData = Scriptable.GameData().bullets;

        foreach (var data in gridBulletData)
        {
            if (data.index == oldIndex)
            {
                data.index = newIndex;
                SaveManager.SaveGameData(Scriptable.GameData());
                return;
            }
        }
    }

    private void RemoveBulletFromData(Vector2 index)
    {
        var gridBulletData = Scriptable.GameData().bullets;

        foreach (var data in gridBulletData)
        {
            if (data.index == index)
            {
                gridBulletData.Remove(data);
                SaveManager.SaveGameData(Scriptable.GameData());
                return;
            }
        }
    }

    private void OnDisable()
    {
        EventManager.ChangeBulletFromData-= ChangeBulletFromData;
        EventManager.RemoveBulletFromData -= RemoveBulletFromData;
        EventManager.TryAgainButtonClicked -= TryAgainButtonClicked;
        EventManager.NextLevelButtonClicked -= NextLevelButtonClicked;
    }

    private void NextLevelButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void TryAgainButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}