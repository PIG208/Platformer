using System;
using UnityEngine;

public abstract class WeaponManager : MonoBehaviour
{
    public string Name;
    public Rarity Rarity;
    public float Power;
    public GameObject[] Slots;
    public Animator WeaponAnimator;

    public BaseWeapon Weapon;

    public abstract void Fire(FireContext fireContext);
    public T GetWeapon<T>() where T : BaseWeapon
    {
        if (typeof(T).Name != Weapon.Type)
            throw new ArgumentException($"{typeof(T).Name} does not match the type of the weapon");
        return (T)Weapon;
    }
}
