using System;
using System.Collections.Generic;
using UnityEngine;


public static class EventManager
{
    
    

    #region InputSystem
    public static Func<Vector2> GetInput;
    public static Func<Vector2> GetInputDelta;
    public static Action InputStarted;
    public static Action InputEnded;
    public static Func<bool> IsTouching;
    public static Func<bool> IsPointerOverUI;
    #endregion


    public static Action<Bullet> BulletMerged;
    public static Action<Bullet> BulletCarried;
    public static Func<List<Transform>> GetGunPoints;
    public static Action<Gun> BulletHitGun;
    public static Action<float,float> ReleaseBullets;
    public static Action ShooterPhase;
    public static Action Fire;
    public static Action<int> PlayerHitRangeGate;
    public static Action<int> PlayerHitFireRateGate;
    public static Action PlayerHitTripleShotGate;
    public static Action PlayerHitBulletSizeUpGate;





}