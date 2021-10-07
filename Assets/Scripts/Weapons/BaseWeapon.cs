using UnityEngine;

public abstract class BaseWeapon
{
    /* Name, Rarity, Power, and Slots are predefined information
    provided in the weapon prefab. These attributes should not be
    modified during runtime.
    */
    public string Name { get => _name; }
    /// <summary>The rarity of the weapon that determines the drop rate</summary>
    public Rarity Rarity { get => _rarity; }
    /// <summary>The power factor of the weapon</summary>
    public float Power { get => _power; }
    /// <summary>The prefab object that contains a WeaponManager</summary>
    public GameObject WeaponPrefab { get => _weaponPrefab; }

    /// <summary>The type of the weapon</summary>
    public abstract string Type { get; }

    private string _name;
    private Rarity _rarity;
    private float _power;
    private GameObject _weaponPrefab;

    /// <summary>Initialize a weapon from a weapon maanger</summary>
    public BaseWeapon(GameObject weaponPrefab)
    {
        WeaponManager weaponManager = weaponPrefab.GetComponent<WeaponManager>();
        if (weaponManager is null)
        {
            throw new System.ArgumentException("WeaponPrefab must have a weapon manager");
        }

        this._name = weaponManager.Name;
        this._rarity = weaponManager.Rarity;
        this._power = weaponManager.Power;
        this._weaponPrefab = weaponPrefab;
    }
}
