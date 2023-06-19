public enum ButtonTypes
{
    BuyBullet,
    Shot,
    NextLevel,
    TryAgain,
}
public enum MergeAreaStates
{
    ChoosingBullet,
    BulletOnDrag,
    BulletPlaced
}

public enum GameStates
{
    Merge,
    MergeToShooter,
    Shooter,
    LevelEnd,
    Fail,
    Win
}

public enum GateWithAmountType
{
    FireRate,
    Range,

}
public enum GateWithOutAmountType
{
    TripleShot,
    BulletSizeUp
}