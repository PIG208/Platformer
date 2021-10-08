using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform EquipementPosition;
    public WeaponManager CurrentWeapon;
    public BaseWeapon[] Weapons;

    public event Action<BaseWeapon, WeaponManager> WeaponSwitched;

    private void Start()
    {
        Weapons = new BaseWeapon[] { WeaponPrototype.GetWeapon<Gun>(WeaponRegistry.Pistol), WeaponPrototype.GetWeapon<Melee>(WeaponRegistry.Knife) };

        CurrentWeapon = GetGun();
    }

    public WeaponManager GetGun()
    {
        BaseWeapon currentWeapon = Weapons[0];
        WeaponManager weaponObject = Instantiate(currentWeapon.WeaponPrefab, EquipementPosition.position, EquipementPosition.rotation, EquipementPosition).GetComponent<WeaponManager>();
        weaponObject.Weapon = currentWeapon;
        if (WeaponSwitched != null) WeaponSwitched(currentWeapon, weaponObject);
        return weaponObject;
    }
}
