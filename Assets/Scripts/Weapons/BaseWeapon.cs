using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : Modifiable<BaseWeapon>
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
    public GameObject WeaponPrefab { get => WeaponPrototype.GetWeaponPrefab(_weaponRegistry); }
    public List<IModifier<BaseWeapon>> GeneralModifiers = new List<IModifier<BaseWeapon>> { new CommonFireModifer() };

    public event EventHandler<FireEventArgs<WeaponManager>> Fire;
    public void RaiseFire(WeaponManager weaponManager, FireContext context) => Fire?.Invoke(this, new FireEventArgs<WeaponManager>(weaponManager, context));
    public event Action<BaseWeapon, WeaponManager> Start;
    public void RaiseStart(WeaponManager weaponManager) => Start?.Invoke(this, weaponManager);
    public event Action<BaseWeapon, WeaponManager> Update;
    public void RaiseUpdate(WeaponManager weaponManager) => Update?.Invoke(this, weaponManager);

    /// <summary>The type of the weapon</summary>
    public abstract string Type { get; }

    private string _name;
    private Rarity _rarity;
    private float _power;
    private WeaponRegistry _weaponRegistry;

    /// <summary>Initialize a weapon from a weapon maanger</summary>
    public BaseWeapon(WeaponRegistry registry)
    {
        _weaponRegistry = registry;
        WeaponManager weaponManager = WeaponPrefab.GetComponent<WeaponManager>();
        if (weaponManager is null)
        {
            throw new ArgumentException("WeaponPrefab must have a weapon manager");
        }

        this._name = weaponManager.Name;
        this._rarity = weaponManager.Rarity;
        this._power = weaponManager.Power;

        foreach (IModifier<BaseWeapon> modifier in GeneralModifiers)
        {
            modifier.Register(this);
        }
    }

    public void RegisterModifier(IModifier<BaseWeapon> modifier)
    {
        GeneralModifiers.Add(modifier);
        modifier.Register(this);
    }

    public WeaponManager Spawn(Transform transform)
    {
        GameObject weaponObject = GameObject.Instantiate(WeaponPrefab, transform.position, transform.rotation, transform);
        return BindWeapon(weaponObject);
    }

    public WeaponManager Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject weaponObject = GameObject.Instantiate(WeaponPrefab, position, rotation);
        return BindWeapon(weaponObject);
    }

    public WeaponManager BindWeapon(GameObject weaponObject)
    {
        WeaponManager weaponManager = weaponObject.GetComponent<WeaponManager>();

        if (weaponManager is null)
            throw new ArgumentException($"The weapon {this.Name} doesn't have a corresponding WeaponManager");

        weaponManager.Weapon = this;

        return weaponManager;
    }

    public class FireEventArgs<T> : EventArgs where T : WeaponManager
    {
        public T WeaponManager;
        public FireContext FireContext;

        public FireEventArgs(T weaponManager, FireContext fireContext)
        {
            WeaponManager = weaponManager;
            FireContext = fireContext;
        }
    }
}
