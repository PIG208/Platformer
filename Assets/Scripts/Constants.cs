using System.Collections.Generic;

public static class Constants
{
    /// <summary>The damage is given by BaseWeapon.Power * Constants.DamageFact</summary>
    public const float DamageFactor = 4f;

    public const float BaseAttackInterval = 0.22f;
    public const float PickupInterval = 0.22f;
    public const float SwitchWeaponInterval = 0.02f;

    public const string MisslePrefab = "Weapons/Bullets/Missile";
    public const string BulletPrefab = "Weapons/Bullets/Regular";
    public const string BouncyPrefab = "Weapons/Bullets/BouncyBullet";
    public const string CollectablePrefab = "Collectable";
    public const string PlayerPrefab = "Entities/Player";

    public static float DropRate(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return 0.7f;
            case Rarity.Rare:
                return 0.2f;
            case Rarity.Epic:
                return 0.09f;
            case Rarity.Legendary:
                return 0.01f;
        }
        return 0f;
    }
}