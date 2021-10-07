using UnityEngine;

public abstract class WeaponManager : MonoBehaviour
{
    public string Name;
    public Rarity Rarity;
    public float Power;
    public GameObject[] Slots;

    protected BaseWeapon Weapon;

    public abstract void Fire(FireContext fireContext);

    public class FireContext
    {
        public readonly Player Player;
        public readonly Entity[] SurroundingEnemies;

        public FireContext(Player player, Entity[] enemies)
        {
            Player = player;
            SurroundingEnemies = enemies;
        }
    }
}
