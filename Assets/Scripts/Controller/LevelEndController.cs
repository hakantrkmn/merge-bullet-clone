using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndController : MonoBehaviour
{
    public LevelEndCreator levelEndCreator;
    public List<LevelEndBox> destroyedBoxes;


    private void OnEnable()
    {
        EventManager.LevelEndBoxDestroyed+= LevelEndBoxDestroyed;   
    }


    private void OnDisable()
    {
        EventManager.LevelEndBoxDestroyed-= LevelEndBoxDestroyed;   
    }

    private void LevelEndBoxDestroyed(LevelEndBox box)
    {
        destroyedBoxes.Add(box);
    }
}
