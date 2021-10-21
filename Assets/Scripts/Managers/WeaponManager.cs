using System;
using UnityEngine;

public abstract class WeaponManager : MonoBehaviour
{
    public string Name;
    public Rarity Rarity;
    public float Power;
    public string Description;
    public GameObject[] Slots;
    public Animator WeaponAnimator;
    public float FireInterval = 0.2f;

    private float _timeToFire = 0;

    private void Start()
    {
        Weapon?.RaiseStart(this);
    }

    private void Update()
    {
        Weapon?.RaiseUpdate(this);
        if (_timeToFire > 0) _timeToFire -= Time.deltaTime;
    }

    public BaseWeapon Weapon;

    public virtual void Fire(FireContext fireContext)
    {
        if (!CanFire()) return;

        _timeToFire = FireInterval;
    }

    public bool CanFire()
    {
        return _timeToFire <= 0;
    }

    public T GetWeapon<T>() where T : BaseWeapon
    {
        if (typeof(T).Name != Weapon.Type)
            throw new ArgumentException($"{typeof(T).Name} does not match the type of the weapon");
        return (T)Weapon;
    }
}
