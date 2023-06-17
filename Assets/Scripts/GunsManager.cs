using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GunsManager : MonoBehaviour
{
    public GameObject gunPrefab;

    private void Start()
    {
        SetGunPoints();
    }

    [Button]
    public void SetGunPoints()
    {
        var points = EventManager.GetGunPoints();
        for (int i = 0; i < points.Count; i++)
        {
            var gun = Instantiate(gunPrefab, transform);
            gun.transform.position = new Vector3(points[i].position.x, transform.position.y, transform.position.z);
        }
    }
}
