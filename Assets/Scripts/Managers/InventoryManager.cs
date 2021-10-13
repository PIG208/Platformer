using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform EquipementPosition;
    public WeaponManager CurrentWeaponManager;
    public BaseWeapon[] Weapons;
    public float SwitchInterval = Constants.SwitchWeaponInterval;

    private int _currentWeaponIndex = -1;
    private float _lastSwitch;

    public event Action<BaseWeapon, WeaponManager> WeaponSwitched;

    private void Start()
    {
        _lastSwitch = SwitchInterval;
        Weapons = new BaseWeapon[] { WeaponPrototype.GetWeapon<Gun>(WeaponRegistry.Pistol), WeaponPrototype.GetWeapon<Melee>(WeaponRegistry.Knife), WeaponPrototype.GetWeapon<Gun>(WeaponRegistry.Rifle) };

        NextWeapon();
    }

    private void Update()
    {
        if (_lastSwitch > 0) _lastSwitch -= Time.deltaTime;
    }

    public WeaponManager SpawnWeapon(BaseWeapon weapon)
    {
        GameObject weaponObject = Instantiate(weapon.WeaponPrefab, EquipementPosition.position, EquipementPosition.rotation, EquipementPosition);
        WeaponManager weaponManager = weaponObject.GetComponent<WeaponManager>();
        weaponManager.Weapon = weapon;

        if (weaponManager is null)
            throw new ArgumentException($"The weapon {weapon.Name} doesn't have a corresponding WeaponManager");

        return weaponManager;
    }

    public void SwitchWeapon(int index)
    {
        if (_lastSwitch > 0) return;
        _lastSwitch = SwitchInterval;

        if (CurrentWeaponManager != null) Destroy(CurrentWeaponManager.gameObject);

        _currentWeaponIndex = index % Weapons.Length;
        if (_currentWeaponIndex < 0) _currentWeaponIndex = Weapons.Length + _currentWeaponIndex;

        CurrentWeaponManager = SpawnWeapon(Weapons[_currentWeaponIndex]);

        if (WeaponSwitched != null) WeaponSwitched(Weapons[_currentWeaponIndex], CurrentWeaponManager);
    }

    public void NextWeapon() => SwitchWeapon(_currentWeaponIndex + 1);
    public void PrevWeapon() => SwitchWeapon(_currentWeaponIndex - 1);
}
