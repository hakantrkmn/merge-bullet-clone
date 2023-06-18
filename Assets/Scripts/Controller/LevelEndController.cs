using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndController : MonoBehaviour
{
    public LevelEndCreator levelEndCreator;
    public List<LevelEndBox> destroyedBoxes;
    public Transform highScore;

    private void OnEnable()
    {
        EventManager.ChangeGameState += ChangeGameState;
        EventManager.LevelEndBoxDestroyed+= LevelEndBoxDestroyed;   
    }

    private void Start()
    {
        highScore.position = new Vector3(highScore.position.x, highScore.position.y, Scriptable.GameData().highScoreZPos);
    }

    private void ChangeGameState(GameStates state)
    {
        if (state==GameStates.Win)
        {
            var lastBox = destroyedBoxes[0].transform.position.z;
            foreach (var box in destroyedBoxes)
            {
                if (box.transform.position.z > lastBox)
                {
                    lastBox = box.transform.position.z;
                }
            }

            if (lastBox>highScore.position.z)
            {
                Scriptable.GameData().highScoreZPos = lastBox;
                highScore.position = new Vector3(highScore.position.x, highScore.position.y, Scriptable.GameData().highScoreZPos);
                SaveManager.SaveGameData(Scriptable.GameData());
            }
        }
    }


    private void OnDisable()
    {
        EventManager.ChangeGameState -= ChangeGameState;
        EventManager.LevelEndBoxDestroyed-= LevelEndBoxDestroyed;   
    }

    private void LevelEndBoxDestroyed(LevelEndBox box)
    {
        destroyedBoxes.Add(box);
    }
}
