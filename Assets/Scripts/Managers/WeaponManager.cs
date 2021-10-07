using UnityEngine;

public abstract class WeaponManager : MonoBehaviour
{
    public string Name { get; protected set; }
    public Rarity Rarity { get; protected set; }
    public float Power { get; protected set; }
    public GameObject[] Slots { get; protected set; }

    protected BaseWeapon Weapon;
}
