using UnityEngine;
using System;


[Serializable]
public struct AmmoInfo
{
    public int level;
    public int damage;
    public GameObject prefab;
    public int health;
    public Gun gun;
}

[Serializable]
public class BulletPrice
{
    public int price;
    public bool isReached;
}

