using UnityEngine;
using System;


[Serializable]
public struct AmmoInfo
{
    public int level;
    public int damage;
    public GameObject prefab;
    public int health;
    [HideInInspector]public Gun gun;
}

[Serializable]
public class GridAmmoInfo
{
    public int level;
    public Vector2 index;
}

[Serializable]
public class BulletPrice
{
    public int price;
    public bool isReached;
}

