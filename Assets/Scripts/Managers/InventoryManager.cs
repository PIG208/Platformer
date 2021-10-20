using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform EquipementPosition;
    public WeaponManager CurrentWeaponManager;
    public List<BaseWeapon> Weapons;
    public float SwitchInterval = Constants.SwitchWeaponInterval;
    [Tooltip("The weapons this inventory manager will start with")]
    public InventoryPreset Preset = InventoryPreset.Empty;

    private int _currentWeaponIndex = -1;
    private float _lastSwitch;

    public event Action<BaseWeapon, WeaponManager> WeaponSwitched;

    private void Start()
    {
        Weapons = Preset.GetItems();

        NextWeapon();
    }

    private void Update()
    {
        if (_lastSwitch > 0) _lastSwitch -= Time.deltaTime;
    }

    public void SwitchWeapon(int index)
    {
        if (_lastSwitch > 0) return;
        _lastSwitch = SwitchInterval;

        if (CurrentWeaponManager != null) Destroy(CurrentWeaponManager.gameObject);

        _currentWeaponIndex = index % Weapons.Count;
        if (_currentWeaponIndex < 0) _currentWeaponIndex = Weapons.Count + _currentWeaponIndex;

        CurrentWeaponManager = Weapons[_currentWeaponIndex].Spawn(EquipementPosition);

        if (WeaponSwitched != null) WeaponSwitched(Weapons[_currentWeaponIndex], CurrentWeaponManager);
    }

    public void NextWeapon() => SwitchWeapon(_currentWeaponIndex + 1);
    public void PrevWeapon() => SwitchWeapon(_currentWeaponIndex - 1);
}
