using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndController : MonoBehaviour
{
    public List<LevelEndBox> destroyedBoxes;
    public Transform highScore;

    private void OnEnable()
    {
        EventManager.ChangeGameState += ChangeGameState;
        EventManager.LevelEndBoxDestroyed += LevelEndBoxDestroyed;
    }

    private void Start()
    {
        highScore.localPosition =
            new Vector3(highScore.localPosition.x, highScore.localPosition.y, Scriptable.GameData().highScoreZPos);
    }

    private void ChangeGameState(GameStates state)
    {
        if (state != GameStates.Win) return;

        if (destroyedBoxes.Count != 0)
        {
            var lastBox = destroyedBoxes[0].transform.localPosition.z;
            foreach (var box in destroyedBoxes)
            {
                if (box.transform.localPosition.z > lastBox)
                {
                    lastBox = box.transform.localPosition.z;
                }
            }

            if (lastBox > highScore.localPosition.z)
            {
                Scriptable.GameData().highScoreZPos = lastBox;
                highScore.localPosition = new Vector3(highScore.localPosition.x, highScore.localPosition.y,
                    Scriptable.GameData().highScoreZPos);
                SaveManager.SaveGameData(Scriptable.GameData());
            }
        }
    }


    private void OnDisable()
    {
        EventManager.ChangeGameState -= ChangeGameState;
        EventManager.LevelEndBoxDestroyed -= LevelEndBoxDestroyed;
    }

    private void LevelEndBoxDestroyed(LevelEndBox box)
    {
        destroyedBoxes.Add(box);
    }
}