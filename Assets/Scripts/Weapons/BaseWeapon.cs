using UnityEngine;

public abstract class BaseWeapon
{
    public string Name { get => _name; }
    /// <summary>The rarity of the weapon that determines the drop rate</summary>
    public Rarity Rarity { get => _rarity; }
    /// <summary>The power factor of the weapon</summary>
    public float Power { get => _power; }
    /// <summary>The slots for the weapon where the extensions can be plugged to</summary>
    public GameObject[] Slots { get => _slots; }

    /// <summary>The type of the weapon</summary>
    public abstract string Type { get; }

    private string _name;
    private Rarity _rarity;
    private float _power;
    private GameObject[] _slots;

    public BaseWeapon(string name, Rarity rarity, float power, GameObject[] slots)
    {
        this._name = name;
        this._rarity = rarity;
        this._power = power;
        this._slots = slots;
    }

    /// <summary>The defines how the weapon will behave when being fired</summary>
    public abstract void OnFire();
}
